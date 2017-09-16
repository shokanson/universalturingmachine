using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace Hokanson.UniversalTuringMachine
{
	public class TuringMachine
	{
		#region Implementation

		private readonly Hashtable _specification = new Hashtable();
        private bool _specLoaded;
        private readonly Regex _stopRegex = new Regex("11110");
        private readonly Regex _leftRegex = new Regex("1110");
        private readonly Regex _rightRegex = new Regex("110");
        private readonly Regex _oneRegex = new Regex("10");
        private readonly Regex _zeroRegex = new Regex("0");

		#endregion

		#region Public Methods

		public Action GetAction(State s)
		{
			if (!_specLoaded) throw new SpecNotLoadedException();
            if (!_specification.ContainsKey(s)) throw new UndefinedStateException(s);
            
            return (Action)_specification[s];
		}

		public void LoadSpec(string specStr)
		{
			AddAction(new State(0, '0'), new Action(0, '0', Direction.R));

			int pos = 0, oldPos = 0;
			uint currState = 0;
			char currInput = '1';
			string spec = specStr + "110";	// "110" removed for 'optimization', so
														// add it back
			while (pos < spec.Length)
			{
				var dir = Direction.Undef;
				int dirPos = 0x7FFFFFFF, matchLen = 0;

				// find first direction
				Match match = _stopRegex.Match(spec, pos);
				if (match.Success)
				{
					dir = Direction.Stop;
					dirPos = match.Index;
					matchLen = match.Length;
				}
				match = _leftRegex.Match(spec, pos);
				if (match.Success && match.Index < dirPos)
				{
					dir = Direction.L;
					dirPos = match.Index;
					matchLen = match.Length;
				}
				match = _rightRegex.Match(spec, pos);
				if (match.Success && match.Index < dirPos)
				{
					dir = Direction.R;
					dirPos = match.Index;
					matchLen = match.Length;
				}

				if (dir == Direction.Undef)
				{
					throw new SpecLoadException(spec.Substring(pos, 20));
				}

				pos = dirPos;
				StateChar sc = GetStateAndChar(spec.Substring(oldPos, pos-oldPos));

				var s = new State(currState, currInput);
				var a = new Action(sc.State, sc.C, dir);

				AddAction(s, a);

				pos += matchLen;
				oldPos = pos;
				currInput = ( currInput == '0' ? '1' : '0' );
				if (currInput == '0')
				{
					currState++;
				}
			}


			_specLoaded = true;
		}

		#endregion

		#region Object Overrides

		public override string ToString()
		{
			var sb = new StringBuilder();

			foreach (State s in _specification.Keys)
			{
				var a = (Action)_specification[s];
				sb.Append(string.Format("{0}:{1}--->{2}:{3}:{4}", s.Num, s.Input, a.ChangeStateTo, a.ChangeTapeTo, a.Dir));
				sb.Append("\r\n");
			}

			return sb.ToString();
		}

		#endregion

		#region Private Methods

		private struct StateChar
		{
			public uint State;
			public char C;
		}

		private StateChar GetStateAndChar(string str)
		{
			StateChar sc;

			int pos = 0;
			var bin = new StringBuilder();
			while (pos < str.Length)
			{
				var match = _oneRegex.Match(str, pos);
				if (match.Success && match.Index == pos)
				{
					bin.Append('1');
					pos += match.Length;
					continue;
				}
				
				match = _zeroRegex.Match(str, pos);
				if (match.Success)
				{
					bin.Append('0');
					pos += match.Length;
					continue;
				}

				throw new SpecLoadException(str.Substring(pos));
			}

			string binStr = bin.ToString();
			if (binStr.Length == 0)
			{
				sc.State = 0;
				sc.C = '0';
			}
			else if (binStr.Length == 1)
			{
				sc.State = 0;
				sc.C = binStr[0];
			}
			else
			{
				sc.C = binStr[binStr.Length-1];
				sc.State = Convert.ToUInt32(binStr.Substring(0, binStr.Length-1), 2);
			}

			return sc;
		}

		private void AddAction(State s, Action a)
		{
			if (_specification.ContainsKey(s)) throw new StateAlreadyDefinedException(s, a);
			
            _specification.Add(s, a);
		}

		#endregion
	}
}

/*
$Log: /Hokanson.UniversalTuringMachine/TuringMachine.cs $ $NoKeyWords:$
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 3     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

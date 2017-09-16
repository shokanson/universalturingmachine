using System;
using System.Text;
using System.Threading;

namespace Hokanson.UniversalTuringMachine
{
	public class TuringRunner
	{
		#region Construction

		public TuringRunner(TuringMachine machine, string input, ManualResetEvent manualEvent)
		{
			_machine = machine;
			_input = new StringBuilder(input);
			_manualEvent = manualEvent;
		}

		#endregion

		#region Implementation

		private readonly TuringMachine _machine;
		private readonly StringBuilder _input;
		private int _inputPos;
		private uint _internalState;
		private readonly ManualResetEvent _manualEvent;

		#endregion

		#region Events

		public event TuringRunEvent RunEvent;

		#endregion

		#region Public Methods

		public void Run()
		{
			try
			{
				_manualEvent.WaitOne();

				ProcessEvent(TuringEvent.Run, null, null, ToString(), string.Empty);

				bool keepGoing = true;
				while (keepGoing)
				{
					keepGoing = Step();
				}

				ProcessEvent(TuringEvent.Done, null, null, ToString(), string.Empty);
			}
			catch (Exception e)
			{
				ProcessEvent(TuringEvent.Error, null, null, string.Empty, e.Message);
			}
		}

		public bool Step()
		{
			_manualEvent.WaitOne();

			var s = new State(_internalState, _input[_inputPos]);
			var a = _machine.GetAction(s);

			// do action
			_internalState = a.ChangeStateTo;
			_input[_inputPos] = a.ChangeTapeTo;
			switch (a.Dir)
			{
				case Direction.R:
					if (++_inputPos == _input.Length)
					{
						_input.Append('0');
					}
					break;
				case Direction.L:
					if (--_inputPos == -1)
					{
						_input.Insert(0, '0');
						_inputPos = 0;
					}
					break;
				case Direction.Stop:
					break;
			}

			ProcessEvent(TuringEvent.Step, s, a, ToString(), string.Empty);

			return ( a.Dir != Direction.Stop );
		}

		#endregion

		#region Object Overrides

		public override string ToString()
		{
			var s = new StringBuilder(_input.ToString());
			s.Append("\r\n");
			for (int i = 0; i < _inputPos; ++i)
				s.Append(' ');
			s.Append('^');
			return s.ToString();
		}

		#endregion

		#region Private Methods

		private void ProcessEvent(TuringEvent te, State s, Action a, string input, string error)
		{
			TuringRunEvent tre = RunEvent;
			if (tre != null)
			{
				tre(this, new TuringRunEventArgs(te, s, a, input, error));
			}
		}

		#endregion
	}
}

/*
$Log: /Hokanson.UniversalTuringMachine/TuringRunner.cs $ $NoKeyWords:$
 * 
 * 2     7/04/07 2:27a Sean
 * refactoring to remove potential race condition; making members readonly
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 3     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

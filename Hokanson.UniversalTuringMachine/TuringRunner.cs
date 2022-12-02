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

		public event EventHandler<TuringRunEventArgs> RunEvent;

		#endregion

		#region Public Methods

		public void Run()
		{
			try
			{
				_manualEvent.WaitOne();

				ProcessEvent(TuringEvent.Run, null, null, ToString());

				while (Step()) {}

				ProcessEvent(TuringEvent.Done, null, null, ToString());
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

			ProcessEvent(TuringEvent.Step, s, a, ToString());

			return ( a.Dir != Direction.Stop );
		}

		#endregion

		#region Object Overrides

		public override string ToString() => $"{_input}\r\n{new String(' ', _inputPos)}^";

		#endregion

		#region Private Methods

		private void ProcessEvent(TuringEvent te, State s, Action a, string input) => ProcessEvent(te, s, a, input, null);

		private void ProcessEvent(TuringEvent te, State s, Action a, string input, string error) => RunEvent?.Invoke(this, new TuringRunEventArgs(te, s, a, input, error));

		#endregion
	}
}

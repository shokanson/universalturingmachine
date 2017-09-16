namespace Hokanson.UniversalTuringMachine
{
	public enum TuringEvent { Run, Step, Error, Done };

	public class TuringRunEventArgs
	{
		#region Construction

		public TuringRunEventArgs(TuringEvent e, State s, Action a, string input, string error)
		{
			TuringEvt = e;
			State = s;
			Action = a;
			Input = input;
			Error = error;
		}

		#endregion

		#region Public Properties

		public State State { get; private set; }
		public TuringEvent TuringEvt { get; private set; }
		public Action Action { get; private set; }
		public string Input { get; private set; }
		public string Error { get; private set; }

		#endregion
	}
}

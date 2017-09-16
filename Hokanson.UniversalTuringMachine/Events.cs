namespace Hokanson.UniversalTuringMachine
{
	public enum TuringEvent { Run, Step, Error, Done };

	public class TuringRunEventArgs
	{
		#region Construction

		public TuringRunEventArgs(TuringEvent e, State s, Action a, string input, string error)
		{
			_e = e;
			_s = s;
			_a = a;
			_input = input;
			_error = error;
		}

		#endregion

		#region Implementation Data

		private readonly TuringEvent _e;
        private readonly State _s;
        private readonly Action _a;
        private readonly string _input;
        private readonly string _error;

		#endregion

		#region Public Properties

		public State State
		{
			get { return _s; }
		}

		public TuringEvent TuringEvt
		{
			get { return _e; }
		}

		public Action action
		{
			get { return _a; }
		}

		public string Input
		{
			get { return _input; }
		}

		public string Error
		{
			get { return _error; }
		}

		#endregion
	}

	public delegate void TuringRunEvent(object sender, TuringRunEventArgs e);
}

/*
$Log: /Hokanson.UniversalTuringMachine/Events.cs $ $NoKeyWords:$
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 3     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

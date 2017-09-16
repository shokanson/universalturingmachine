namespace Hokanson.UniversalTuringMachine
{
	public class State
	{
		#region Construction

		public State(uint num, char input)
		{
			_num = num;
			_input = input;			
		}
		
		#endregion

		#region Implementation

		private readonly uint _num;
        private readonly char _input;
		
		#endregion

		#region Public Properties

		public uint Num
		{
			get { return _num; }
		}

		public char Input
		{
			get { return _input; }
		}
		
		#endregion

		#region Object Overrides

		public override bool Equals(object obj)
		{
			var state = obj as State;

			return ( state != null && _num == state._num && _input == state._input );
		}

		public override int GetHashCode()
		{
			return (int)_num + 41*_input.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("state: {0}; input: {1}", _num, _input);
		}
		
		#endregion
	}
}

/*
$Log: /Hokanson.UniversalTuringMachine/State.cs $ $NoKeyWords:$
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 2     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

namespace Hokanson.UniversalTuringMachine
{
	public enum Direction { Undef, R, L, Stop }

	public class Action
	{
		#region Construction

		public Action(uint changeStateTo, char changeTapeTo, Direction dir)
		{
			_changeStateTo = changeStateTo;
			_changeTapeTo = changeTapeTo;
			_dir = dir;
		}

		#endregion

		#region Implementation

        private readonly char _changeTapeTo;
        private readonly uint _changeStateTo;
        private readonly Direction _dir;
		
		#endregion

		#region Public Properties

		public char ChangeTapeTo
		{
			get { return _changeTapeTo; }
		}
		public uint ChangeStateTo
		{
			get { return _changeStateTo; }
		}

		public Direction Dir
		{
			get { return _dir; }
		}

		#endregion

		#region Object Overrides

		public override string ToString()
		{
			return string.Format("changeTapeTo: {0}; changeStateTo: {1}; direction: {2}", _changeTapeTo, _changeStateTo, _dir);
		}

		#endregion
	}
}

/*
$Log: /Hokanson.UniversalTuringMachine/Action.cs $ $NoKeyWords:$
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 2     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

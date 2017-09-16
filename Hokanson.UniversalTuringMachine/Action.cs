namespace Hokanson.UniversalTuringMachine
{
	public enum Direction { Undef, R, L, Stop }

	public class Action
	{
		#region Construction

		public Action(uint changeStateTo, char changeTapeTo, Direction dir)
		{
			ChangeStateTo = changeStateTo;
			ChangeTapeTo = changeTapeTo;
			Dir = dir;
		}

		#endregion

		#region Public Properties

		public char ChangeTapeTo { get; private set; }
		public uint ChangeStateTo { get; private set; }
		public Direction Dir { get; private set; }

		#endregion

		#region Object Overrides

		public override string ToString() => $"changeTapeTo: {ChangeTapeTo}; changeStateTo: {ChangeStateTo}; direction: {Dir}";

		#endregion
	}
}

namespace Hokanson.UniversalTuringMachine
{
	public class State
	{
		#region Construction

		public State(uint num, char input)
		{
			Num = num;
			Input = input;
		}

		#endregion

		#region Public Properties

		public uint Num { get; private set; }
		public char Input { get; private set; }

		#endregion

		#region Object Overrides

		public override bool Equals(object obj)
		{
            return (obj is State state && Num == state.Num && Input == state.Input);
        }

		public override int GetHashCode() => (int)Num + 41 * Input.GetHashCode();

		public override string ToString() => $"state: {Num}; input: {Input}";

		#endregion
	}
}

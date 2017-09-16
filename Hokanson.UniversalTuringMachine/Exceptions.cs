using System;

namespace Hokanson.UniversalTuringMachine
{
	public class StateAlreadyDefinedException : Exception
	{
		#region Construction

		public StateAlreadyDefinedException(State s, Action a)
			: base($"state already defined: {s} --> {a}")
		{}

		#endregion
	}

	public class SpecNotLoadedException : Exception
	{
		#region Construction

		public SpecNotLoadedException()
			: base("Turing Machine specification not loaded")
		{}

		#endregion
	}

	public class SpecLoadException : Exception
	{
		#region Construction

		public SpecLoadException(string err)
			: base(err)
		{}

		#endregion
	}

	public class UndefinedStateException : Exception
	{
		#region Construction

		public UndefinedStateException(State s)
			: base($"undefined state: {s}")
		{}

		#endregion
	}
}

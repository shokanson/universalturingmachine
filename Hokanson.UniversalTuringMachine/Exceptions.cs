using System;

namespace Hokanson.UniversalTuringMachine
{
	public class StateAlreadyDefinedException : Exception
	{
		#region Construction

		public StateAlreadyDefinedException(State s, Action a)
			: base(string.Format("state already defined: {0} --> {1}", s, a))
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
			: base(string.Format("undefined state: {0}", s))
		{}

		#endregion
	}
}

/*
$Log: /Hokanson.UniversalTuringMachine/Exceptions.cs $ $NoKeyWords:$
 * 
 * 1     2/17/07 1:17a Sean
 * moving to own assembly
 * 
 * 3     1/23/07 11:28p Sean
 * results of ReSharper analysis
*/

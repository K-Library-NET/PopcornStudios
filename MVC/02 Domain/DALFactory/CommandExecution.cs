using System;

namespace BLS_AspNet.DALFactory
{
	/// <summary>
	/// CommandExecution specifies when the commands should be ***really*** executed.
	/// </summary>
	public enum CommandExecution
	{
		Immediate,
		NoOutputQueued
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamSharp.Core.Sh4.Instructions.Logic
{
	public class NotInstruction : Instruction
	{
		/// <summary>
		/// 
		/// </summary>
		private Register sourceRegister = null;
		/// <summary>
		/// 
		/// </summary>
		private Register destinationRegister = null;

		/// <summary>
		///		Initializes a new instance of the <see cref="NotInstruction"/> class.
		/// </summary>
		/// <param name="code">The instruction code.</param>
		public NotInstruction(uint code)
			: base(code)
		{
		}

		protected override void CheckForCorrectness()
		{
		}

		protected override void DecomposeParameters()
		{
			var sourceRegisterID = (this.Code & 0x00F0u) >> 4;
			var destinationRegisterID = (this.Code & 0x0F00u) >> 8;

			this.sourceRegister = Register.GeneralPurposeRegisters[sourceRegisterID];
			this.destinationRegister = Register.GeneralPurposeRegisters[destinationRegisterID];
		}

		public override void Execute()
		{
			this.destinationRegister.Value = ~this.sourceRegister.Value;
		}

		public override string ToString()
		{
			return "not " + this.sourceRegister.ToString() + ", " + this.destinationRegister.ToString();
		}
	}
}

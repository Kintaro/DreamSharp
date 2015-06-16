using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DreamSharp.Core.Sh4.Instructions.Logic
{
	/// <summary>
	/// 
	/// </summary>
	public class XorInstruction : Instruction
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
		/// 
		/// </summary>
		private uint immediateValue = 0;

		/// <summary>
		///		Initializes a new instance of the <see cref="XorInstruction"/> class.
		/// </summary>
		/// <param name="code">The instruction code.</param>
		public XorInstruction(uint code)
			: base(code)
		{
		}

		protected override void CheckForCorrectness()
		{
		}

		protected override void DecomposeParameters()
		{
			var firstByte = (this.Code & 0xF000u) >> 12;

			var sourceRegisterID = (this.Code & 0x00F0u) >> 4;
			var destinationRegisterID = (this.Code & 0x0F00u) >> 8;
			var immediateValue = (this.Code & 0x00FFu);

			if (firstByte == 0x02u)
			{
				this.sourceRegister = Register.GeneralPurposeRegisters[sourceRegisterID];
				this.destinationRegister = Register.GeneralPurposeRegisters[destinationRegisterID];
			}
			else
				this.immediateValue = immediateValue;
		}

		public override void Execute()
		{
			if (this.sourceRegister == null)
			{
				var sndByte = (this.Code & 0x0F00u) >> 8;
				if (sndByte == 0x0Au)
					Register.GeneralPurposeRegisters[0].Value ^= this.immediateValue;
			}
			else
			{
				this.destinationRegister.Value ^= this.sourceRegister.Value;
			}
		}

		public override string ToString()
		{
			if (this.sourceRegister == null)
			{
				var sndByte = (this.Code & 0x0F00u) >> 8;
				if (sndByte == 0x0Au)
					return "xor #" + this.immediateValue.ToString() + ", " + Register.GeneralPurposeRegisters[0].ToString();
				else if (sndByte == 0x0Eu)
					return "xor #" + this.immediateValue.ToString() + ", @(" + Register.GeneralPurposeRegisters[0].ToString() + ", GBR)";
			}
			else
				return "xor " + this.sourceRegister.ToString() + ", " + this.destinationRegister.ToString();
			throw new NotSupportedException();
		}
	}
}

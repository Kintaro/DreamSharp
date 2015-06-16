// -------------------------------------------------------------------------------
// DreamWave - A free .NET driven DreamCast emulator
// -------------------------------------------------------------------------------
// Project: DreamSharp.Core.Sh4
// Description:
//     This library is the core part of the SH-4 CPU emulation.
//
// -------------------------------------------------------------------------------
// Copyright (c) 2011, Simon Wollwage
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS

using System.Collections.Generic;

namespace DreamSharp.Core.Sh4
{
	/// <summary>
	/// 
	/// </summary>
	public class Register
	{
		/// <summary>
		/// 
		/// </summary>
		protected uint bank0Value = 0;
		/// <summary>
		/// 
		/// </summary>
		protected uint bank1Value = 0;
		/// <summary>
		/// 
		/// </summary>
		protected readonly uint registerID = 0;

		/// <summary>
		/// 
		/// </summary>
		public static readonly Register[] GeneralPurposeRegisters = new Register[]
		{
			new Register(0x00u),
			new Register(0x01u),
			new Register(0x02u),
			new Register(0x03u),
			new Register(0x04u),
			new Register(0x05u),
			new Register(0x06u),
			new Register(0x07u),
			new Register(0x08u),
			new Register(0x09u),
			new Register(0x0Au),
			new Register(0x0Bu),
			new Register(0x0Cu),
			new Register(0x0Du),
			new Register(0x0Eu),
			new Register(0x0Fu),
		};

		/// <summary>
		/// 
		/// </summary>
		public static readonly FloatingPointRegister[] FloatingPointRegisters = new FloatingPointRegister[]
		{
			new FloatingPointRegister(0x00u),
			new FloatingPointRegister(0x01u),
			new FloatingPointRegister(0x02u),
			new FloatingPointRegister(0x03u),
			new FloatingPointRegister(0x04u),
			new FloatingPointRegister(0x05u),
			new FloatingPointRegister(0x06u),
			new FloatingPointRegister(0x07u),
			new FloatingPointRegister(0x08u),
			new FloatingPointRegister(0x09u),
			new FloatingPointRegister(0x0Au),
			new FloatingPointRegister(0x0Bu),
			new FloatingPointRegister(0x0Cu),
			new FloatingPointRegister(0x0Du),
			new FloatingPointRegister(0x0Eu),
			new FloatingPointRegister(0x0Fu),
		};

		/// <summary>
		/// 
		/// </summary>
		public static readonly SinglePrecisionRegister[] SinglePrecisionRegisters = new SinglePrecisionRegister[]
		{
			new SinglePrecisionRegister(0x00u),
			new SinglePrecisionRegister(0x01u),
			new SinglePrecisionRegister(0x02u),
			new SinglePrecisionRegister(0x03u),
			new SinglePrecisionRegister(0x04u),
			new SinglePrecisionRegister(0x05u),
			new SinglePrecisionRegister(0x06u),
			new SinglePrecisionRegister(0x07u),
			new SinglePrecisionRegister(0x08u),
			new SinglePrecisionRegister(0x09u),
			new SinglePrecisionRegister(0x0Au),
			new SinglePrecisionRegister(0x0Bu),
			new SinglePrecisionRegister(0x0Cu),
			new SinglePrecisionRegister(0x0Du),
			new SinglePrecisionRegister(0x0Eu),
			new SinglePrecisionRegister(0x0Fu),
			new SinglePrecisionRegister(0x10u),
			new SinglePrecisionRegister(0x11u),
			new SinglePrecisionRegister(0x12u),
			new SinglePrecisionRegister(0x13u),
			new SinglePrecisionRegister(0x14u),
			new SinglePrecisionRegister(0x15u),
			new SinglePrecisionRegister(0x16u),
			new SinglePrecisionRegister(0x17u),
			new SinglePrecisionRegister(0x18u),
			new SinglePrecisionRegister(0x19u),
			new SinglePrecisionRegister(0x1Au),
			new SinglePrecisionRegister(0x1Bu),
			new SinglePrecisionRegister(0x1Cu),
			new SinglePrecisionRegister(0x1Du),
			new SinglePrecisionRegister(0x1Eu),
			new SinglePrecisionRegister(0x1Fu),
		};

		/// <summary>
		/// 
		/// </summary>
		public static readonly StatusRegister StatusRegister = new StatusRegister();
		/// <summary>
		/// 
		/// </summary>
		public static readonly ProgramCounter ProgramCounter = new ProgramCounter();
		/// <summary>
		/// 
		/// </summary>
		public static readonly FloatingPointStatusControlRegister FloatingPointStatusControlRegister = new FloatingPointStatusControlRegister();

		/// <summary>
		///     Initializes a new instance of the <see cref="Register"/> class.
		/// </summary>
		/// <param name="registerID">The register ID.</param>
		public Register(uint registerID)
		{
			this.registerID = registerID;
		}

		/// <summary>
		///     Gets or sets the register's value.
		/// </summary>
		/// <value>
		///     The value inside the register.
		/// </value>
		public uint Value 
		{
			get
			{
				if (this.registerID < 0x08u)
					return StatusRegister.RegisterBank ? this.bank1Value : this.bank0Value;
				else
					return this.bank0Value;
			}
			set
			{
				if (StatusRegister.RegisterBank && this.registerID < 0x08u)
					this.bank1Value = value;
				else
					this.bank0Value = value;
			}
		}

		/// <summary>
		/// Returns a <see cref="System.String"/> that represents this instance.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String"/> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return "R" + this.registerID.ToString() + "<" + (StatusRegister.RegisterBank ? "1" : "0") + ">";
		}
	}
}

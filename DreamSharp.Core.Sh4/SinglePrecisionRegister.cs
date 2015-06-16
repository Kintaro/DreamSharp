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

namespace DreamSharp.Core.Sh4
{
	/// <summary>
	/// 
	/// </summary>
	public class SinglePrecisionRegister : Register
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SinglePrecisionRegister"/> class.
		/// </summary>
		/// <param name="registerID">The register ID.</param>
		public SinglePrecisionRegister(uint registerID)
			: base(registerID)
		{
		}

		/// <summary>
		/// Gets or sets the single precision value.
		/// </summary>
		/// <value>
		/// The single precision value.
		/// </value>
		public float SinglePrecisionValue
		{
			get
			{
				var isHigherValue = this.registerID % 2 == 0;
				if (isHigherValue)
					return Register.FloatingPointRegisters[this.registerID / 2].HighSinglePrecisionValue;
				else
					return Register.FloatingPointRegisters[(this.registerID - 1) / 2].LowSinglePrecisionValue;
			}
			set
			{
				var isHigherValue = this.registerID % 2 == 0;
				if (isHigherValue)
					Register.FloatingPointRegisters[this.registerID / 2].HighSinglePrecisionValue = value;
				else
					Register.FloatingPointRegisters[(this.registerID - 1) / 2].LowSinglePrecisionValue = value;
			}
		}

		public override string ToString()
		{
			return "FR" + this.registerID.ToString() + "<" + (FloatingPointStatusControlRegister.RegisterBank ? "1" : "0") + ">";
		}
	}
}

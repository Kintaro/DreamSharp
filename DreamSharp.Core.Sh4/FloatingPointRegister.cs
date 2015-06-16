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

using System;

namespace DreamSharp.Core.Sh4
{
	/// <summary>
	/// 
	/// </summary>
	public class FloatingPointRegister : Register
	{
		/// <summary>
		/// 
		/// </summary>
		private long bank0FloatingPointValue = 0;
		/// <summary>
		/// 
		/// </summary>
		private long bank1FloatingPointValue = 0;

		/// <summary>
		/// Initializes a new instance of the <see cref="FloatingPointRegister"/> class.
		/// </summary>
		/// <param name="registerID">The register ID.</param>
		public FloatingPointRegister(uint registerID)
			: base(registerID)
		{
		}

		private long internalBankValue
		{
			get
			{
				if (FloatingPointStatusControlRegister.RegisterBank)
					return this.bank1FloatingPointValue;
				else
					return this.bank0FloatingPointValue;
			}
			set
			{
				if (FloatingPointStatusControlRegister.RegisterBank)
					this.bank1FloatingPointValue = value;
				else
					this.bank0FloatingPointValue = value;
			}	
		}

		/// <summary>
		/// 
		/// </summary>
		public double FloatingPointValue
		{
			get
			{
				return BitConverter.Int64BitsToDouble(this.internalBankValue);
			}
			set
			{
				this.internalBankValue = BitConverter.DoubleToInt64Bits(value);
			}
		}

		/// <summary>
		///     Gets or sets the high single precision value.
		/// </summary>
		/// <value>
		///     The high single precision value.
		/// </value>
		public float HighSinglePrecisionValue
		{
			get
			{
				return BitConverter.ToSingle(BitConverter.GetBytes(this.internalBankValue >> 32), 0);
			}
			set
			{
				unchecked { this.internalBankValue = (this.internalBankValue & (long)0x00000000FFFFFFFFL) | (long)((long)BitConverter.ToUInt32(BitConverter.GetBytes(value), 0) << 32); }
			}
		}

		/// <summary>
		///     Gets or sets the low single precision value.
		/// </summary>
		/// <value>
		///     The low single precision value.
		/// </value>
		public float LowSinglePrecisionValue
		{
			get
			{
				return BitConverter.ToSingle(BitConverter.GetBytes(this.internalBankValue & 0x00000000FFFFFFFFL), 0);
			}
			set
			{
				unchecked { this.internalBankValue = (this.internalBankValue & (long)0xFFFFFFFF00000000L) | (long)((long)BitConverter.ToUInt32(BitConverter.GetBytes(value), 0)); }
			}
		}

		public override string ToString()
		{
			return "DR" + this.registerID.ToString() + "<" + (FloatingPointStatusControlRegister.RegisterBank ? "1" : "0") + ">";
		}
	}
}

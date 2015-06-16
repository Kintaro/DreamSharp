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
	public class FloatingPointStatusControlRegister : Register
	{
		/// <summary>
		/// 
		/// </summary>
		public enum PrecisionModeEnum
		{
			/// <summary>
			/// 
			/// </summary>
			SinglePrecision,
			/// <summary>
			/// 
			/// </summary>
			DoublePrecision
		}

		/// <summary>
		///     Initializes a new instance of the <see cref="FloatingPointStatusControlRegister"/> class.
		/// </summary>
		public FloatingPointStatusControlRegister()
			: base(uint.MaxValue)
		{
		}

		/// <summary>
		///     Gets or sets the precision mode.
		/// </summary>
		/// <value>
		///     The precision mode.
		/// </value>
		public PrecisionModeEnum PrecisionMode
		{
			get { return ((this.Value & 0x80000u) == 0x80000u) ? PrecisionModeEnum.DoublePrecision : PrecisionModeEnum.SinglePrecision; }
			set { this.Value = (this.Value & 0x7FFFFu) | (value == PrecisionModeEnum.DoublePrecision ? 0x80000u : 0x00u); }
		}

		/// <summary>
		///		Gets or sets a value indicating whether [register bank].
		/// </summary>
		/// <value>
		///   <c>true</c> if [register bank]; otherwise, <c>false</c>.
		/// </value>
		public bool RegisterBank
		{
			get { return (this.Value & 0x200000u) == 0x200000u; }
			set { this.Value = (this.Value & 0xFFDFFFFFu) | (value ? 0x200000u : 0x00u); }
		}
	}
}

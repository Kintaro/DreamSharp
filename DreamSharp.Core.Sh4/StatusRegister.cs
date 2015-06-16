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
	public class StatusRegister : Register
	{
		/// <summary>
		///		Initializes a new instance of the <see cref="StatusRegister"/> class.
		/// </summary>
		public StatusRegister()
			: base(uint.MaxValue)
		{
			this.bank0Value = 0x20000000u;
		}

		/// <summary>
		///		Gets or sets a value indicating whether this <see cref="StatusRegister"/> is T.
		/// </summary>
		/// <value>
		///   <c>true</c> if T; otherwise, <c>false</c>.
		/// </value>
		public bool T
		{
			get { return (this.Value & 0x01u) == 0x01u; }
			set { this.Value = (this.Value & 0xFFFFFFFEu) | (value ? 0x01u : 0x00u); }
		}

		/// <summary>
		///		Gets or sets a value indicating whether this <see cref="StatusRegister"/> is S.
		/// </summary>
		/// <value>
		///   <c>true</c> if S; otherwise, <c>false</c>.
		/// </value>
		public bool S
		{
			get { return (this.Value & 0x02u) == 0x02u; }
			set { this.Value = (this.Value & 0xFFFFFFFDu) | (value ? 0x01u : 0x00u); }
		}

		/// <summary>
		///		Gets or sets a value indicating whether [register bank].
		/// </summary>
		/// <value>
		///   <c>true</c> if [register bank]; otherwise, <c>false</c>.
		/// </value>
		public bool RegisterBank
		{
			get { return (this.Value & 0x20000000u) == 0x20000000u; }
			set { this.Value = (this.Value & 0xDFFFFFFFu) | (value ? 0x20000000u : 0x00u); }
		}
	}
}

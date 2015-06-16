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

namespace DreamSharp.Core.Sh4.Instructions.SystemControl
{
    /// <summary>
    /// 
    /// </summary>
    public class LdsInstruction : Instruction
    {
        /// <summary>
        /// 
        /// </summary>
        private Register register = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="LdsInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public LdsInstruction(uint code)
            : base(code)
        {
        }

        protected override void CheckForCorrectness()
        {
        }

        protected override void DecomposeParameters()
        {
            var registerID = (this.Code & 0x0F00u) >> 8;
            this.register = Register.GeneralPurposeRegisters[registerID];
        }

        public override void Execute()
        {
            if ((this.Code & 0x00FFu) == 0x6A)
                Register.FloatingPointStatusControlRegister.Value = this.register.Value;
        }

        public override string ToString()
        {
            return "lds " + this.register.ToString() + ", FPSCR";
        }
    }
}

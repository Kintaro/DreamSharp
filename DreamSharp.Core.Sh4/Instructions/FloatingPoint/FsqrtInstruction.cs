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

namespace DreamSharp.Core.Sh4.Instructions.FloatingPoint
{
    /// <summary>
    /// 
    /// </summary>
    public class FsqrtInstruction : Instruction
    {
        /// <summary>
        ///     The destination register (Rn) and second operand
        /// </summary>
        private FloatingPointRegister doubleDestinationRegister = null;
        /// <summary>
        /// 
        /// </summary>
        private SinglePrecisionRegister singleDestinationRegister = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="FsqrtInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public FsqrtInstruction(uint code)
            : base(code)
        {
        }

        protected override void CheckForCorrectness()
        {
        }

        protected override void DecomposeParameters()
        {
            this.doubleDestinationRegister = Register.FloatingPointRegisters[(this.Code & 0x0E00u) >> 9];
            this.singleDestinationRegister = Register.SinglePrecisionRegisters[(this.Code & 0x0F00u) >> 8];
        }

        public override void Execute()
        {
            if (Register.FloatingPointStatusControlRegister.PrecisionMode == FloatingPointStatusControlRegister.PrecisionModeEnum.SinglePrecision)
                this.singleDestinationRegister.SinglePrecisionValue = (float)Math.Sqrt(this.singleDestinationRegister.SinglePrecisionValue);
            else
                this.doubleDestinationRegister.FloatingPointValue = Math.Sqrt(this.doubleDestinationRegister.FloatingPointValue);
        }

        public override string ToString()
        {
            if (Register.FloatingPointStatusControlRegister.PrecisionMode == FloatingPointStatusControlRegister.PrecisionModeEnum.SinglePrecision)
                return "fsqrt " + this.singleDestinationRegister.ToString();
            else
                return "fsqrt " + this.doubleDestinationRegister.ToString();
        }
    }
}

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

namespace DreamSharp.Core.Sh4.Instructions.Transfer
{
    /// <summary>
    /// 
    /// </summary>
    public class SwapInstruction : Instruction
    {
        /// <summary>
        ///     The source register (Rm) and first operand
        /// </summary>
        private Register sourceRegister = null;
        /// <summary>
        ///     The destination register (Rn) and second operand
        /// </summary>
        private Register destinationRegister = null;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SwapInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public SwapInstruction(uint code)
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
            var high = 0u;
            var low = 0u;

            if ((this.Code & 0x000Fu) == 0x08u)
            {
                high = (this.sourceRegister.Value & 0x00F0u) >> 4;
                low = this.sourceRegister.Value & 0x000Fu;

                this.destinationRegister.Value = (this.destinationRegister.Value & 0xFFFFFF00u) | ((low << 4) | high);
            }
            else if ((this.Code & 0x000Fu) == 0x09u)
            {
                high = (this.sourceRegister.Value & 0xFF00u) >> 8;
                low = this.sourceRegister.Value & 0x00FFu;

                this.destinationRegister.Value = (low << 8) | high;
            }
        }

        public override string ToString()
        {
            if ((this.Code & 0x000Fu) == 0x08u)
                return "swap.b " + this.sourceRegister.ToString() + ", " + this.destinationRegister.ToString();
            else if ((this.Code & 0x000Fu) == 0x09u)
                return "swap.w " + this.sourceRegister.ToString() + ", " + this.destinationRegister.ToString();
            throw new NotSupportedException();
        }
    }
}

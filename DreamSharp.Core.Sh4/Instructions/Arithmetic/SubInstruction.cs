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

namespace DreamSharp.Core.Sh4.Instructions.Arithmetic
{
    /// <summary>
    /// 
    /// </summary>
    public class SubInstruction : Instruction
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
        ///     Initializes a new instance of the <see cref="SubInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public SubInstruction(uint code)
            : base(code)
        {

        }

        protected override void CheckForCorrectness()
        {
            var opCode = (this.Code & 0xF000u) >> 12;
            if (opCode == 0x03u && (this.Code & 0x000Fu) != 0x08u)
                throw new NotSupportedException("This opcode is not supported by SubInstruction");
        }

        protected override void DecomposeParameters()
        {
            var opCode = (this.Code & 0xF000u) >> 12;

            var destinationRegisterID = (this.Code & 0x0F00u) >> 8;
            var sourceRegisterID = (this.Code & 0x00F0u) >> 4;

            this.sourceRegister = Register.GeneralPurposeRegisters[sourceRegisterID];
            this.destinationRegister = Register.GeneralPurposeRegisters[destinationRegisterID];
        }

        public override void Execute()
        {
            this.destinationRegister.Value -= this.sourceRegister.Value;
        }

        public override string ToString()
        {
            string source = this.sourceRegister.ToString();
            string destination = this.destinationRegister.ToString();
            return "sub " + source + ", " + destination;
        }
    }
}

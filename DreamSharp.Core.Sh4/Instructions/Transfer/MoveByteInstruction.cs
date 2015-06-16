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

namespace DreamSharp.Core.Sh4.Instructions.Transfer
{
    /// <summary>
    /// 
    /// </summary>
    public class MoveByteInstruction : Instruction
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
        /// 
        /// </summary>
        private uint immediateValue = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        public MoveByteInstruction(uint code)
            : base(code)
        {
        }

        protected override void CheckForCorrectness()
        {
        }

        protected override void DecomposeParameters()
        {
            var firstByte = (this.Code & 0xF000u) >> 12;
            var destinationRegisterID = (this.Code & 0x0F00u) >> 8;
            var sourceRegisterID = (this.Code & 0x00F0u) >> 4;
            var immediateValue = (this.Code & 0x00FFu);

            if (firstByte == 0x02u || firstByte == 0x06u)
                this.sourceRegister = Register.GeneralPurposeRegisters[sourceRegisterID];
            else
                this.immediateValue = immediateValue;
            this.destinationRegister = Register.GeneralPurposeRegisters[destinationRegisterID];
        }

        public override void Execute()
        {
            var firstByte = (this.Code & 0xF000u) >> 12;
            var lastByte = this.Code & 0x000Fu;

            this.destinationRegister.Value = this.sourceRegister.Value;
        }

        public override string ToString()
        {
            string source = this.sourceRegister == null ? this.immediateValue.ToString() : this.sourceRegister.ToString();
            string destination = this.destinationRegister.ToString();
            return "mov.b " + source + ", " + destination;
        }
    }
}

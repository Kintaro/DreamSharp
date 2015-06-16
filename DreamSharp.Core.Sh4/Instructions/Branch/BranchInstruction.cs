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

namespace DreamSharp.Core.Sh4.Instructions.Branch
{
    /// <summary>
    /// 
    /// </summary>
    public class BranchInstruction : Instruction
    {
        /// <summary>
        /// 
        /// </summary>
        private enum BranchType
        {
            /// <summary>
            ///     Branch if false
            /// </summary>
            BF,
            /// <summary>
            ///     Delayed branch if false
            /// </summary>
            BFS,
            /// <summary>
            ///     Branch if true
            /// </summary>
            BT,
            /// <summary>
            ///     Delayed branch if true
            /// </summary>
            BTS,
            /// <summary>
            ///     Delayed branch
            /// </summary>
            BRA,
            /// <summary>
            ///     Branch register if false
            /// </summary>
            BRAF,
            /// <summary>
            ///     Delayed branch
            /// </summary>
            BSR,
            /// <summary>
            ///     
            /// </summary>
            BSRF,
            /// <summary>
            /// 
            /// </summary>
            JMP,
            /// <summary>
            /// 
            /// </summary>
            JSR,
            /// <summary>
            /// 
            /// </summary>
            RTS
        }

        /// <summary>
        /// 
        /// </summary>
        private Register register = null;
        /// <summary>
        /// 
        /// </summary>
        private uint label = 0;
        /// <summary>
        /// 
        /// </summary>
        private BranchType branchType = BranchType.BF;

        /// <summary>
        /// Initializes a new instance of the <see cref="BranchInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public BranchInstruction(uint code)
            : base(code)
        {
        }

        protected override void CheckForCorrectness()
        {
        }

        protected override void DecomposeParameters()
        {
            var firstByte = (this.Code & 0xF000u) >> 12;
            var secondByte = (this.Code & 0xF000u) >> 8;
            var label = (this.Code & 0x00FFu);

            if (firstByte == 0x08u && secondByte == 0x0Bu) this.branchType = BranchType.BF;
            if (firstByte == 0x08u && secondByte == 0x0Fu) this.branchType = BranchType.BFS;

            switch (this.branchType)
            {
                case BranchType.BF: this.label = label; break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override void Execute()
        {
            switch (this.branchType)
            {
                case BranchType.BF:
                    {
                        if (!Register.StatusRegister.T) Register.ProgramCounter.JumpToAddress(this.label * 2 + Register.ProgramCounter.Value + 4);
                    } break;
            }
        }

        public override string ToString()
        {
            string type = this.branchType.ToString().ToLower();
            string operand = this.register == null ? "0x" + this.label.ToString("X") : this.register.ToString();

            if (this.branchType == BranchType.JMP || this.branchType == BranchType.JSR) operand = "@" + operand;

            return type + " " + operand;
        }
    }
}

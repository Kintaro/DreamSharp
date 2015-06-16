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
    public class CompareInstruction : Instruction
    {
        /// <summary>
        /// 
        /// </summary>
        private enum CompareType
        {
            /// <summary>
            /// 
            /// </summary>
            EqImm,
            /// <summary>
            /// 
            /// </summary>
            Eq,
            /// <summary>
            /// 
            /// </summary>
            Hs,
            /// <summary>
            /// 
            /// </summary>
            Ge,
            /// <summary>
            /// 
            /// </summary>
            Hi,
            /// <summary>
            /// 
            /// </summary>
            Gt,
            /// <summary>
            /// 
            /// </summary>
            Pz,
            /// <summary>
            /// 
            /// </summary>
            Pl,
            /// <summary>
            /// 
            /// </summary>
            Str
        }

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
        private CompareType compareType = CompareType.EqImm;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CompareInstruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public CompareInstruction(uint code)
            : base(code)
        {
        }

        protected override void CheckForCorrectness()
        {
        }

        protected override void DecomposeParameters()
        {
            var opCode = (this.Code & 0xF000u) >> 12;
            var lastByte = (this.Code & 0x000Fu);
            var intermediateByte = (this.Code & 0x00F0u) >> 4;

            var destinationRegisterID = (this.Code & 0x0F00u) >> 8;
            var sourceRegisterID = (this.Code & 0x00F0u) >> 4;
            var immValue = (this.Code & 0x00FFu);

            if (opCode == 0x08u)
            {
                this.immediateValue = immValue;
                this.compareType = CompareType.EqImm;
            }
            else
            {
                this.sourceRegister = Register.GeneralPurposeRegisters[sourceRegisterID];

                if (opCode == 0x03u && lastByte == 0x00u) this.compareType = CompareType.Eq;
                else if (opCode == 0x03u && lastByte == 0x02u) this.compareType = CompareType.Hs;
                else if (opCode == 0x03u && lastByte == 0x03u) this.compareType = CompareType.Ge;
                else if (opCode == 0x03u && lastByte == 0x06u) this.compareType = CompareType.Hi;
                else if (opCode == 0x03u && lastByte == 0x07u) this.compareType = CompareType.Gt;
                else if (opCode == 0x04u && intermediateByte == 0x01u && lastByte == 0x01u) this.compareType = CompareType.Pz;
                else if (opCode == 0x04u && intermediateByte == 0x01u && lastByte == 0x05u) this.compareType = CompareType.Pl;
                else if (opCode == 0x02u && lastByte == 0x0Cu) this.compareType = CompareType.Str;
            }
            this.destinationRegister = Register.GeneralPurposeRegisters[destinationRegisterID];
        }

        public override void Execute()
        {
            var value = this.sourceRegister == null ? this.immediateValue : this.sourceRegister.Value;
            var result = 0u;
            switch (this.compareType)
            {
                case CompareType.EqImm: result = (value == this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Eq: result = (value == this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Hs: result = (value <= this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Ge: result = (value <= this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Hi: result = (value < this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Gt: result = (value < this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Pz: result = (0 <= this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Pl: result = (0 < this.destinationRegister.Value) ? 0x01u : 0x00u; break;
                case CompareType.Str:
                    {
                        var source = this.sourceRegister.Value;
                        var dest = this.destinationRegister.Value;
                        result = (GetByte(source, 0) == GetByte(dest, 0))
                            || (GetByte(source, 1) == GetByte(dest, 1))
                            || (GetByte(source, 2) == GetByte(dest, 2))
                            || (GetByte(source, 3) == GetByte(dest, 3)) ? 0x01u : 0x00u;
                    } break;
                default:
                    throw new NotSupportedException();
            }
        }

        public override string ToString()
        {
            string source = this.sourceRegister == null ? "#" + this.immediateValue.ToString() : this.sourceRegister.ToString();
            string destination = this.destinationRegister.ToString();
            string type = "cmp." + this.compareType.ToString().ToLower();

            if (this.compareType == CompareType.Pz || this.compareType == CompareType.Pl)
                return type + " " + destination;
            else
                return type + " " + source + ", " + destination;
        }

        /// <summary>
        ///     Gets the byte.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        private byte GetByte(uint value, int index)
        {
            return BitConverter.GetBytes(value)[index];
        }
    }
}

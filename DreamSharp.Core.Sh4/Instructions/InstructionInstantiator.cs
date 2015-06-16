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
using System.Linq;

namespace DreamSharp.Core.Sh4.Instructions
{
    public class InstructionInstantiator
    {
        private Type[][] instructionTable = new Type[][]
        {
            // 0000
            new Type[]
            {
                null,
                null,
                null,
                typeof(Branch.BranchInstruction),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                typeof(Branch.BranchInstruction),
                null,
                null,
                null,
                null,
            },
            // 0001
            null,
            // 0010
            new Type[]
            {
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveWordInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Logic.XorInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Arithmetic.CompareInstruction),
                typeof(Transfer.XtrctInstruction),
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveByteInstruction),
            },
            // 0011
            new Type[]
            {
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.CompareInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.SubInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddInstruction),
                typeof(Arithmetic.AddWithCarryInstruction),
                typeof(Arithmetic.AddWithOverflowInstruction),
            },
            // 0100
            new Type[]
            {
                null,
                typeof(Arithmetic.CompareInstruction),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                typeof(SystemControl.LdsInstruction),
                typeof(Branch.BranchInstruction),
                typeof(Shift.ShadInstruction),
                null,
                null,
                null,
            },
            // 0101
            null,
            // 0110
            new Type[]
            {
                typeof(Transfer.MoveByteInstruction),
                typeof(Transfer.MoveWordInstruction),
                null,
                typeof(Transfer.MoveInstruction),
                null,
                null,
                null,
                typeof(Logic.NotInstruction),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
            },
            // 0111
            Enumerable.Repeat<Type> (typeof(Arithmetic.AddInstruction), 16).ToArray<Type>(),
            // 1000
            Enumerable.Repeat<Type> (typeof(Arithmetic.CompareInstruction), 16).ToArray<Type>(),
            // 1001
            null,
            // 1010
            null,
            // 1011
            null,
            // 1100
            Enumerable.Repeat<Type> (typeof(Logic.XorInstruction), 16).ToArray<Type>(),
            // 1101
            null,
            // 1110
            Enumerable.Repeat<Type> (typeof(Transfer.MoveInstruction), 16).ToArray<Type>(),
            // 1111
            new Type[]
            {
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                typeof(FloatingPoint.FsqrtInstruction),
                null,
                null,
            },
        };

        /// <summary>
        ///     Instantiates from instruction code.
        /// </summary>
        /// <param name="instructionCode">The instruction code.</param>
        /// <returns></returns>
        public Instruction InstantiateFromInstructionCode(uint instructionCode)
        {
            var firstByte = (instructionCode & 0xF000u) >> 12;
            var secondByte = (instructionCode & 0x0F00u) >> 8;
            var thirdByte = (instructionCode & 0x00F0u) >> 4;
            var lastByte = instructionCode & 0x000Fu;

            Type type = null;
            if (firstByte == 0x08u && secondByte > 0x08u)
                type = typeof(Branch.BranchInstruction);
            else if (firstByte == 0x00u && secondByte == 0x00u && lastByte == 0x08u && (thirdByte == 0x05u || thirdByte == 0x01u))
                type = typeof(SystemControl.SetInstruction);
            else if (firstByte == 0x00u && secondByte == 0x00u && lastByte == 0x08u && (thirdByte == 0x00u || thirdByte == 0x02u || thirdByte == 0x04u))
                type = typeof(SystemControl.ClearInstruction);
            else
                type = this.instructionTable[firstByte][lastByte];
            return System.Activator.CreateInstance(type, new object[] { instructionCode }, null) as Instruction;
        }
    }
}

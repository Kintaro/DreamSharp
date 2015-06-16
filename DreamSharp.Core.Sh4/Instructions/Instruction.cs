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

namespace DreamSharp.Core.Sh4.Instructions
{
    /// <summary>
    ///     A base class for all instructions, holding the instruction code
    ///     ued to create the instruction and providing an interface
    ///     for some shared methods.
    /// </summary>
    public abstract class Instruction
    {
        /// <summary>
        ///     The instruction code
        /// </summary>
        private readonly uint code = 0;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Instruction"/> class.
        /// </summary>
        /// <param name="code">The instruction code.</param>
        public Instruction(uint code)
        {
            this.code = code;
            this.CheckForCorrectness();
            this.DecomposeParameters();
        }

        /// <summary>
        ///     Gets the instruction code.
        /// </summary>
        public uint Code { get { return this.code; } }

        /// <summary>
        ///     Checks for correctness.
        /// </summary>
        protected abstract void CheckForCorrectness();
        /// <summary>
        ///     Decomposes the parameters.
        /// </summary>
        protected abstract void DecomposeParameters();
        /// <summary>
        ///     Executes the instruction.
        /// </summary>
        public abstract void Execute();
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public abstract override string ToString();
    }
}

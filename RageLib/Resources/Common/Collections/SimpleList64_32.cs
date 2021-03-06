﻿/*
    Copyright(c) 2016 Neodymium

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in
    all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
    THE SOFTWARE.
*/

using System;
using System.Collections.Generic;

namespace RageLib.Resources.Common
{
    // TODO: Find a better name to these classes
    public class SimpleList64_32<T> : ResourceSystemBlock where T : unmanaged
    {
        public override long BlockLength
        {
            get { return 16; }
        }

        // structure data
        public ulong EntriesPointer;
        public uint EntriesCount;
        public uint EntriesCapacity;

        // reference data
        public SimpleArray<T> Entries;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.EntriesPointer = reader.ReadUInt64();
            this.EntriesCount = reader.ReadUInt32();
            this.EntriesCapacity = reader.ReadUInt32();

            // read reference data
            this.Entries = reader.ReadBlockAt<SimpleArray<T>>(
                this.EntriesPointer, // offset
                this.EntriesCapacity
            );

            // TODO: see https://github.com/carmineos/gta-toolkit/issues/13
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.EntriesPointer = (ulong)(this.Entries != null ? this.Entries.BlockPosition : 0);
            this.EntriesCount = (uint)(this.Entries != null ? this.Entries.Count : 0);
            this.EntriesCapacity = (uint)(this.Entries != null ? this.Entries.Count : 0);

            // write structure data
            writer.Write(this.EntriesPointer);
            writer.Write(this.EntriesCount);
            writer.Write(this.EntriesCapacity);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            return Entries == null ? Array.Empty<IResourceBlock>() : new IResourceBlock[] { Entries };
        }
    }
}

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

using System.Collections.Generic;

namespace RageLib.Resources.Common
{
    public class ResourceSimpleList64<T> : ResourceSystemBlock where T : IResourceSystemBlock, new()
    {
        public override long Length
        {
            get { return 16; }
        }

        // structure data
        public ulong ValuesPointer;
        public ushort ValuesCount1;
        public ushort ValuesCount2;
        public uint Unknown_Ch;

        // reference data
        public ResourceSimpleArray<T> Values;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.ValuesPointer = reader.ReadUInt64();
            this.ValuesCount1 = reader.ReadUInt16();
            this.ValuesCount2 = reader.ReadUInt16();
            this.Unknown_Ch = reader.ReadUInt32();

            // read reference data
            this.Values = reader.ReadBlockAt<ResourceSimpleArray<T>>(
                this.ValuesPointer, // offset
                this.ValuesCount2
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.ValuesPointer = (ulong)(this.Values != null ? this.Values.Position : 0);
            this.ValuesCount1 = (ushort)(this.Values != null ? this.Values.Count : 0);
            this.ValuesCount2 = (ushort)(this.Values != null ? this.Values.Count : 0);

            // write structure data
            writer.Write(this.ValuesPointer);
            writer.Write(this.ValuesCount1);
            writer.Write(this.ValuesCount2);
            writer.Write(this.Unknown_Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>();
            if (Values != null) list.Add(Values);
            return list.ToArray();
        }
    }
}

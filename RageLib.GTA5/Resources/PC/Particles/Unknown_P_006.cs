/*
    Copyright(c) 2017 Neodymium

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

using RageLib.Resources.Common;
using System;
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Particles
{
    public class Unknown_P_006 : ResourceSystemBlock
    {
        public override long BlockLength => 0x30;

        // structure data
        public ArrayHeader64<Unknown_P_009> Unknown_0h_Header;
        public ulong Unknown_10h; // 0x0000000000000000
        public ulong Unknown_18h; // 0x0000000000000000
        public uint Unknown_20h;
        public uint Unknown_24h;
        public ulong Unknown_28h; // 0x0000000000000000

        // reference data
        public SimpleArray<Unknown_P_009> Unknown_0h_Data;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            // read structure data
            this.Unknown_0h_Header = reader.ReadStruct<ArrayHeader64<Unknown_P_009>>();
            this.Unknown_10h = reader.ReadUInt64();
            this.Unknown_18h = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt64();

            // read reference data
            this.Unknown_0h_Data = reader.ReadBlockAt<SimpleArray<Unknown_P_009>>(Unknown_0h_Header.EntriesPointer, Unknown_0h_Header.EntriesCount);
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            // update structure data
            this.Unknown_0h_Header.EntriesPointer = (ulong)(this.Unknown_0h_Data != null ? this.Unknown_0h_Data.BlockPosition : 0);
            this.Unknown_0h_Header.EntriesCount = (ushort)(this.Unknown_0h_Data != null ? this.Unknown_0h_Data.Count : 0);
            this.Unknown_0h_Header.EntriesCapacity = (ushort)(this.Unknown_0h_Data != null ? this.Unknown_0h_Data.Count : 0);

            // write structure data
            writer.WriteStruct(this.Unknown_0h_Header);
            writer.Write(this.Unknown_10h);
            writer.Write(this.Unknown_18h);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (Unknown_0h_Data != null) list.Add(Unknown_0h_Data);
            return list.ToArray();
        }
    }
}

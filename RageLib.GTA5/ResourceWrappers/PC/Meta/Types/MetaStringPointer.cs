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

using RageLib.Data;
using System;
using System.Diagnostics;

namespace RageLib.GTA5.ResourceWrappers.PC.Meta.Types
{
    public class MetaStringPointer : IMetaValue
    {
        public int DataBlockIndex { get; set; }
        public int DataOffset { get; set; }
        public int StringLength { get; set; }
        public int StringCapacity { get; set; }

        // Reference values
        public string Value { get; set; }

        public MetaStringPointer()
        { }

        public MetaStringPointer(string value)
        {
            this.Value = value;
        }

        public void Read(DataReader reader)
        {
            var blockIndexAndOffset = reader.ReadUInt32();
            this.DataBlockIndex = (int)(blockIndexAndOffset & 0x00000FFF);
            this.DataOffset = (int)((blockIndexAndOffset & 0xFFFFF000) >> 12);
            
            var unknown_4h = reader.ReadUInt32();
            Debug.Assert(unknown_4h == 0);

            var count1 = reader.ReadUInt16();
            var count2 = reader.ReadUInt16();
            
            // one is the length with null terminator, but they are often inverted
            var length = Math.Min(count1, count2);
            var length_null = Math.Max(count1, count2);

            // check they are either equal or differ of 1
            Debug.Assert(length_null - length <= 1);

            var unknown_Ch = reader.ReadUInt32();
            Debug.Assert(unknown_Ch == 0);

            this.StringLength = count1;
            this.StringCapacity = count2;
        }

        public void Write(DataWriter writer)
        {
            uint blockIndexAndOffset = (uint)DataBlockIndex | ((uint)DataOffset << 12);
            writer.Write(blockIndexAndOffset);
            writer.Write((uint)0);
            writer.Write((ushort)StringLength);
            writer.Write((ushort)(StringCapacity));
            writer.Write((uint)0);
        }
    }
}
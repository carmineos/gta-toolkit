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
using System.Collections.Generic;

namespace RageLib.Resources.GTA5.PC.Drawables
{
    // pgBase
    // crJointData
    public class Joints : PgBase64
    {
        public override long BlockLength => 0x40;

        // structure data
        public ulong RotationLimitsPointer;
        public ulong TranslationLimitsPointer;
        public uint Unknown_20h; // 0x00000000
        public uint Unknown_24h; // 0x00000000
        public uint Unknown_28h; // 0x00000000
        public uint Unknown_2Ch; // 0x00000000
        public ushort RotationLimitsCount;
        public ushort TranslationLimitsCount;
        public ushort Unknown_34h; // 0x0000
        public ushort Unknown_36h; // 0x0001
        public uint Unknown_38h; // 0x00000000
        public uint Unknown_3Ch; // 0x00000000

        // reference data
        public ResourceSimpleArray<JointRotationLimit> RotationLimits;
        public ResourceSimpleArray<JointTranslationLimit> TranslationLimits;

        /// <summary>
        /// Reads the data-block from a stream.
        /// </summary>
        public override void Read(ResourceDataReader reader, params object[] parameters)
        {
            base.Read(reader, parameters);

            // read structure data
            this.RotationLimitsPointer = reader.ReadUInt64();
            this.TranslationLimitsPointer = reader.ReadUInt64();
            this.Unknown_20h = reader.ReadUInt32();
            this.Unknown_24h = reader.ReadUInt32();
            this.Unknown_28h = reader.ReadUInt32();
            this.Unknown_2Ch = reader.ReadUInt32();
            this.RotationLimitsCount = reader.ReadUInt16();
            this.TranslationLimitsCount = reader.ReadUInt16();
            this.Unknown_34h = reader.ReadUInt16();
            this.Unknown_36h = reader.ReadUInt16();
            this.Unknown_38h = reader.ReadUInt32();
            this.Unknown_3Ch = reader.ReadUInt32();

            // read reference data
            this.RotationLimits = reader.ReadBlockAt<ResourceSimpleArray<JointRotationLimit>>(
                this.RotationLimitsPointer, // offset
                this.RotationLimitsCount
            );
            this.TranslationLimits = reader.ReadBlockAt<ResourceSimpleArray<JointTranslationLimit>>(
                this.TranslationLimitsPointer, // offset
                this.TranslationLimitsCount
            );
        }

        /// <summary>
        /// Writes the data-block to a stream.
        /// </summary>
        public override void Write(ResourceDataWriter writer, params object[] parameters)
        {
            base.Write(writer, parameters);

            // update structure data
            this.RotationLimitsPointer = (ulong)(this.RotationLimits != null ? this.RotationLimits.BlockPosition : 0);
            this.TranslationLimitsPointer = (ulong)(this.TranslationLimits != null ? this.TranslationLimits.BlockPosition : 0);
            this.RotationLimitsCount = (ushort)(this.RotationLimits != null ? this.RotationLimits.Count : 0);
            this.TranslationLimitsCount = (ushort)(this.TranslationLimits != null ? this.TranslationLimits.Count : 0);

            // write structure data
            writer.Write(this.RotationLimitsPointer);
            writer.Write(this.TranslationLimitsPointer);
            writer.Write(this.Unknown_20h);
            writer.Write(this.Unknown_24h);
            writer.Write(this.Unknown_28h);
            writer.Write(this.Unknown_2Ch);
            writer.Write(this.RotationLimitsCount);
            writer.Write(this.TranslationLimitsCount);
            writer.Write(this.Unknown_34h);
            writer.Write(this.Unknown_36h);
            writer.Write(this.Unknown_38h);
            writer.Write(this.Unknown_3Ch);
        }

        /// <summary>
        /// Returns a list of data blocks which are referenced by this block.
        /// </summary>
        public override IResourceBlock[] GetReferences()
        {
            var list = new List<IResourceBlock>(base.GetReferences());
            if (RotationLimits != null) list.Add(RotationLimits);
            if (TranslationLimits != null) list.Add(TranslationLimits);
            return list.ToArray();
        }
    }
}

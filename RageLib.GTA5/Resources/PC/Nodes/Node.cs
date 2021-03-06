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

using RageLib.Data;

namespace RageLib.Resources.GTA5.PC.Nodes
{
    public struct Node : IResourceStruct<Node>
    {
        // structure data
        public uint Unknown_0h; // 0x00000000
        public uint Unknown_4h; // 0x00000000
        public uint Unknown_8h; // 0x00000000
        public uint Unknown_Ch; // 0x00000000
        public ushort Unknown_10h;
        public ushort Unknown_12h;
        public uint Unknown_14h;
        public uint Unknown_18h;
        public uint Unknown_1Ch;
        public uint Unknown_20h;
        public uint Unknown_24h;

        public Node ReverseEndianness()
        {
            return new Node()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_8h = EndiannessExtensions.ReverseEndianness(Unknown_8h),
                Unknown_Ch = EndiannessExtensions.ReverseEndianness(Unknown_Ch),
                Unknown_10h = EndiannessExtensions.ReverseEndianness(Unknown_10h),
                Unknown_12h = EndiannessExtensions.ReverseEndianness(Unknown_12h),
                Unknown_14h = EndiannessExtensions.ReverseEndianness(Unknown_14h),
                Unknown_18h = EndiannessExtensions.ReverseEndianness(Unknown_18h),
                Unknown_1Ch = EndiannessExtensions.ReverseEndianness(Unknown_1Ch),
                Unknown_20h = EndiannessExtensions.ReverseEndianness(Unknown_20h),
                Unknown_24h = EndiannessExtensions.ReverseEndianness(Unknown_24h),
            };
        }
    }
}

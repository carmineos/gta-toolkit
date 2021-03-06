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

namespace RageLib.Resources.GTA5.PC.Navigations
{
    public struct SectorDataUnk : IResourceStruct<SectorDataUnk>
    {
        // structure data
        public ushort Unknown_0h;
        public ushort Unknown_2h;
        public ushort Unknown_4h;
        public ushort Unknown_6h;

        public SectorDataUnk ReverseEndianness()
        {
            return new SectorDataUnk()
            {
                Unknown_0h = EndiannessExtensions.ReverseEndianness(Unknown_0h),
                Unknown_2h = EndiannessExtensions.ReverseEndianness(Unknown_2h),
                Unknown_4h = EndiannessExtensions.ReverseEndianness(Unknown_4h),
                Unknown_6h = EndiannessExtensions.ReverseEndianness(Unknown_6h),
            };
        }
    }
}

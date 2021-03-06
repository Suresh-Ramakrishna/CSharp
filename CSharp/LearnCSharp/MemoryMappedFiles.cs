﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryMappedFiles
{
    class Program
    {
        static void Main(string[] args)
        {
            long offset = 0x10000000; // 256 megabytes
            long length = 0x20000000; // 512 megabytes

            // Create the memory-mapped file.
            using (var mmf = MemoryMappedFile.CreateFromFile(@"C:\ExtremelyLargeImage.data", FileMode.Open, "ImgA"))
            {
                // Create a random access view, from the 256th megabyte (the offset)
                // to the 768th megabyte (the offset plus length).
                using (var accessor = mmf.CreateViewAccessor(offset, length))
                {
                    int colorSize = Marshal.SizeOf(typeof(MyColor));
                    MyColor color;

                    // Make changes to the view.
                    for (long i = 0; i < length; i += colorSize)
                    {
                        accessor.Read(i, out color); //Read is generic
                        color.Brighten(10);
                        accessor.Write(i, ref color);
                    }
                }
            }
        }
    }
    public struct MyColor
    {
        public short Red;
        public short Green;
        public short Blue;
        public short Alpha;

        // Make the view brighter.
        public void Brighten(short value)
        {
            Red = (short)Math.Min(short.MaxValue, (int)Red + value);
            Green = (short)Math.Min(short.MaxValue, (int)Green + value);
            Blue = (short)Math.Min(short.MaxValue, (int)Blue + value);
            Alpha = (short)Math.Min(short.MaxValue, (int)Alpha + value);
        }
    }
}

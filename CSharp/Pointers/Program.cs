﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pointers
{
    class Program
    {
        static void Main(string[] args)
        {
            unsafe
            {
                int test = 0;
                int* ptr = &test;
                Console.WriteLine(*ptr);
            }
        }
    }
}

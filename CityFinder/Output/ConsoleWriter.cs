﻿using System;

namespace CityFinder.Output
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string text)
        {
            Console.WriteLine(text);
        }
    }
}

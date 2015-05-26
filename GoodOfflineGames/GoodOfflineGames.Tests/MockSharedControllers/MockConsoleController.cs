﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using GOG.Interfaces;

namespace GoodOfflineGames.Tests
{
    public class MockConsoleController : IConsoleController
    {
        public string Read()
        {
            return string.Empty;
        }

        public string ReadLine()
        {
            return string.Empty;
        }

        public string ReadPrivateLine()
        {
            return string.Empty;
        }

        public void Write(string message, params object[] data)
        {
        }

        public void WriteLine(string message, params object[] data)
        {
        }
    }
}
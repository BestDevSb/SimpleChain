using System;
using Apache.Ignite.Core;

namespace SimpleBlockChain.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var ignite = Ignition.Start();
        }        
    }
}

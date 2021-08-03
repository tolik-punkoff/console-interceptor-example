using System;
using System.Collections.Generic;
using System.Text;

namespace testapp
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();

            while (true)
            {
                int n = rnd.Next(0, int.MaxValue);
                string t = Convert.ToString(n, 16);
                Console.WriteLine(t.ToUpperInvariant());
                System.Threading.Thread.Sleep(250);
            }
        }
    }
}

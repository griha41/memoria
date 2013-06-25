/* ........................................................................ *
 * (c) 2010 Laurent Dupuis (www.dupuis.me)                                  *
 * ........................................................................ *
 * < This program is free software: you can redistribute it and/or modify
 * < it under the terms of the GNU General Public License as published by
 * < the Free Software Foundation, either version 3 of the License, or
 * < (at your option) any later version.
 * < 
 * < This program is distributed in the hope that it will be useful,
 * < but WITHOUT ANY WARRANTY; without even the implied warranty of
 * < MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * < GNU General Public License for more details.
 * < 
 * < You should have received a copy of the GNU General Public License
 * < along with this program.  If not, see <http://www.gnu.org/licenses/>.
 * ........................................................................ */
using System;
using System.Text;

namespace MPHTest
{
    class Program
    {
        class KeyGenerator : MPH.IKeySource
        {
            readonly uint _nbKeys;
            uint _currentKey;

            public KeyGenerator(uint nbKeys)
            {
                _nbKeys = nbKeys;
            }

            public uint NbKeys { get { return _nbKeys; } }

            public byte[] Read()
            {
                return Encoding.UTF8.GetBytes( string.Format("KEY-{0}",_currentKey++) );
            }

            public void Rewind()
            {
                _currentKey=0;
            }
        }


        static void Main(string[] args)
        {
            // Create a unique string generator
            var keyGenerator = new KeyGenerator(20000000);

            // Derivate a minimum perfect hash function
            Console.WriteLine("Generating minimum perfect hash function for {0} keys", keyGenerator.NbKeys);
            var start = DateTime.Now;
            var hashFunction =  MPH.MinPerfectHash.Create(keyGenerator, 1);

            Console.WriteLine("Completed in {0:0.000000} s", DateTime.Now.Subtract(start).TotalMilliseconds / 1000.0);
            
            // Show the extra hash space necessary
            Console.WriteLine("Hash function map {0} keys to {1} hashes (load factor: {2:0.000000}%)",
                keyGenerator.NbKeys,hashFunction.N,
                ((keyGenerator.NbKeys * 100) / (double)hashFunction.N));

            // Check for any collision
            var used = new System.Collections.BitArray((int)hashFunction.N);
            keyGenerator.Rewind();
            start = DateTime.Now;
            for(var test = 0U; test<keyGenerator.NbKeys;test++ )
            {
                var hash = (int) hashFunction.Search(keyGenerator.Read());
                if(used[hash])
                {
                    Console.WriteLine("FAILED - Collision detected at {0}",test);
                    return;
                }
                used[hash] = true;
            }
            var end = DateTime.Now.Subtract(start).TotalMilliseconds;
            Console.WriteLine("PASS - No collision detected");

            Console.WriteLine("Total scan time : {0:0.000000} s",end/1000.0);
            Console.WriteLine("Average key hash time : {0} ms", end/(double)keyGenerator.NbKeys);
        }
    }
}

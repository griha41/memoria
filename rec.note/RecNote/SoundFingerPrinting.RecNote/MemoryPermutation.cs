using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SoundFingerPrinting.RecNote
{
    public class MemoryPermutation : Soundfingerprinting.Hashing.IPermutations
    {

        private int[][] perm { get; set; }

        public MemoryPermutation(int[][] perm)
        {
            this.perm = perm;
        }


        public int[][] GetPermutations()
        {
            return this.perm;
        }
    }
}

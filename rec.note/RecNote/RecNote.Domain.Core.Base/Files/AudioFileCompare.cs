using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Soundfingerprinting.AudioProxies;
using Soundfingerprinting.Fingerprinting;
using Soundfingerprinting.SoundTools;
using Soundfingerprinting.AudioProxies.Strides;
using Soundfingerprinting.Hashing;

namespace RecNote.Domain.Core.Base.Files
{
    public class AudioFileCompare : Core.Files.IFileCompareProvider<Entities.Files.AudioFile, Core.Files.AudioComparedResult>
    {
        public Core.Files.AudioComparedResult Compare(Entities.Files.AudioFile file1, Entities.Files.AudioFile file2, float similarity)
        {
            var sem = file1.Finger.Select(p => p.Select(a => a ? 1 : 0).ToArray()).ToArray();
            var hasMin = new MinHash(new Audios.MemoryPermutation(sem));
            var dic = new List<RecNote.Domain.Core.Files.AudioComparedResultItem>();

            for (var if1 = 0; if1 < file1.Finger.Length; if1++)
            {
                for (var if2 = 0; if2 < file2.Finger.Length; if2++)
                {
                    dic.Add(new Core.Files.AudioComparedResultItem
                    {
                        MillisecondFile1 = if1 * 11 * 11.6f,
                        MillisecondFile2 = if2 * 11 * 11.6f,
                        Similitary = MinHash.CalculateSimilarity(file1.Finger[if1], file2.Finger[if2])
                    });
                }
            }

            return new Core.Files.AudioComparedResult
            {
                items = dic.Where(p => p.Similitary >= similarity).ToArray()
            };
        }
    }
}

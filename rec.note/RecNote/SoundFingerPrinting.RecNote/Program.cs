using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Files = RecNote.Entities.Files;
using Audios = RecNote.Entities.Audios;

using Spring.Context;
using Spring.Context.Support;

using System.IO;

using RecNote.Domain.Core.Files;
using RecNote.Domain.Core.Audios;

using Soundfingerprinting.AudioProxies.Strides;
using Soundfingerprinting.Hashing;
using Soundfingerprinting.AudioProxies;
using Soundfingerprinting.Fingerprinting;
using Soundfingerprinting.SoundTools;

using System.Threading;

namespace SoundFingerPrinting.RecNote
{
    class Program
    {
        private static readonly IApplicationContext ctx = ContextRegistry.GetContext();
        private static IFileProvider FileProvider { get { return (IFileProvider)ctx.GetObject("FileProvider"); } }
        private static IAudioProvider AudioProvider { get { return (IAudioProvider)ctx.GetObject("AudioProvider"); } }
        static void Main(string[] args)
        {
            while (true)
            {
                var files = FileProvider.FindByType("mp3");
                foreach(var file in files.Where(f => f.GetType() != typeof(Files.AudioFile)) )
                    Process(file);
                Thread.Sleep(5000);
            }
        }


        static List<bool[]> sfinger { get; set; }
       
        static int Skip = 1;
        static int Length = 10;
        static int Block = Skip + Length;
        static int Milliseconds = 64 * Skip;
        static void Process(Files.File file)
        {
            var audio = new Files.AudioFile
            {
                ID = file.ID,
                ContentType = file.ContentType,
                Name = file.Name,
                Size = file.Size,
                Url = file.Url,
                FullPath = file.FullPath
            };

            audio.AlterPath = file.FullPath.ToLower().Replace("mp3", "wav");
            var proxy = new BassProxy();

            if (!File.Exists(audio.AlterPath))
                proxy.RecodeTheFile(file.FullPath, audio.AlterPath, 5512);
            
            FingerprintManager manager = new FingerprintManager();
            manager.FingerprintLength = Length;
            manager.TopWavelets = 150;
            manager.MaxFrequency = 2048;
            manager.MinFrequency = 512;

            float[][] spec = manager.CreateSpectrogram(proxy, audio.AlterPath, 0, 0);

            var StaticStride = new StaticStride(0);

            audio.Finger = manager.CreateFingerprints(proxy, audio.AlterPath, StaticStride).ToArray();
            
            FileProvider.Save(audio);
        }
    }
}

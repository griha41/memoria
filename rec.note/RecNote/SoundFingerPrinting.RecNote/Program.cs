using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Files = RecNote.Entities.Files;
using Audio = RecNote.Entities.Audio;

using Spring.Context;
using Spring.Context.Support;

using System.IO;

using RecNote.Domain.Core.Files;
using RecNote.Domain.Core.Audio;

using Soundfingerprinting.AudioProxies.Strides;
using Soundfingerprinting.Hashing;
using Soundfingerprinting.AudioProxies;
using Soundfingerprinting.Fingerprinting;
using Soundfingerprinting.SoundTools;

using System.Threading;

namespace SoundFingerPrinting.RecNote
{
    /// <summary>
    /// Programa que procesa los archivos de audio sin procesar
    /// </summary>
    class Program
    {
        private static readonly IApplicationContext ctx = ContextRegistry.GetContext();
        private static IFileProvider FileProvider { get { return (IFileProvider)ctx.GetObject("FileProvider"); } }
        private static IAudioProvider AudioProvider { get { return (IAudioProvider)ctx.GetObject("AudioProvider"); } }
        /// <summary>
        /// Loop eterno para revisión de elementos sin procesar
        /// </summary>
        /// <param name="args">permite procesar prioritariamente un archivo</param>
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                foreach (var arg in args)
                {
                    var file = FileProvider.FindByID(arg);
                    if (file == null || file.GetType() == typeof(Files.AudioFile)) { continue; }
                    Process(file);
                }
                return;
            }
            while (true)
            {
                var files = FileProvider.FindByType("mp3");
                foreach(var file in files.Where(f => f.GetType() != typeof(Files.AudioFile)) )
                    Process(file);
                Thread.Sleep(1000);
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

            //float[][] spec = manager.CreateSpectrogram(proxy, audio.AlterPath, 0, 0);

            var StaticStride = new StaticStride(0);

            audio.Finger = manager.CreateFingerprints(proxy, audio.AlterPath, StaticStride).ToArray();
            
            FileProvider.Save(audio);
        }
    }
}

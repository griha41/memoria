using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soundfingerprinting.AudioProxies;
using Soundfingerprinting.Fingerprinting;
using Soundfingerprinting.SoundTools;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Soundfingerprinting.AudioProxies.Strides;
using Soundfingerprinting.Hashing;


namespace Soundfingerprinting.Test
{
    class Program
    {
        static List<bool[]> sfinger { get; set; }
        static void Main(string[] args)
        {
            foreach (var arg in args)
                Process(arg);

            Console.ReadKey();
        }
        static int Skip = 1;
        static int Length = 10;
        static int Block = Skip + Length;
        static int Milliseconds = 64*Skip;
        static void Process(string path)
        {
            Console.WriteLine(path);
            var proxy = new BassProxy();
            proxy.RecodeTheFile(path + ".mp3",  path + ".wav", 5512);
            var data = File.CreateText(path + ".txt");

            FingerprintManager manager = new FingerprintManager();
            manager.FingerprintLength = Length;
            manager.TopWavelets = 150;
            manager.MaxFrequency = 2048;
            manager.MinFrequency = 512;
            float[][] spec = manager.CreateSpectrogram(proxy, path + ".wav", 0, 0);

            var StaticStride = ( path == "7" ) ?  new StaticStride(Milliseconds) : new StaticStride(0);

            var fingerprint = manager.CreateFingerprints(proxy, path + ".wav", StaticStride);
            if (path == "4") { sfinger = fingerprint; }

            foreach (var finger in fingerprint)
            {
                int[] bits = finger.Select(f => f ? 1 : 0).ToArray();
                data.WriteLine(string.Join(";", bits));
            }
            data.Close();

            if(File.Exists( path + ".jpg") )
                File.Delete(path + ".jpg");
            if (spec.Length > 0)
            {
                Bitmap image = Imaging.GetSpectrogramImage(spec, 800, 600);
                image.Save(path + ".jpg", ImageFormat.Jpeg);
            }
            

            if (path == "7")
            {
                var hasMin = new MinHash(new LocalPermutations("2.txt", ";"));
                var result = File.CreateText("r.txt");
                
                Dictionary<int, int> count = new Dictionary<int, int>();

                foreach (var finger in sfinger)
                {
                    //var t = fingerprint.Select(p => MinHash.CalculateSimilarity(finger, p)).OrderByDescending(p => p).ToArray();
                    //var t = fingerprint.Select(p => MinHash.CalculateHammingDistance (finger, p)).OrderByDescending(p => p).ToArray();
                    //var t = sfinger.Select(p => MinHash.CalculateSimilarity(finger, p)).OrderByDescending(p => p).ToArray();

                    for (int i = 0; i < fingerprint.Count; i++)
                    {
                        var similarity = MinHash.CalculateSimilarity(finger, fingerprint[i]);
                        if (similarity > 0.5f)
                        {
                            if (!count.ContainsKey(i))
                                count.Add(i, 0);
                            count[i]++;
                            result.Write("{1}:{0};", similarity.ToString("#.000"), i, i * 11.6f * Block, (i + 1) * 11.6f * Block);
                        }
                    }
                    result.WriteLine();
                }
                foreach (var c in count.OrderByDescending(p => p.Value))
                    result.Write("{0}:{1};", c.Key, c.Value);

                result.WriteLine("-------------------");
                foreach (var c in count.OrderBy(p => p.Key))
                    result.WriteLine("{0}:{1} Time: {2} ms;", c.Key, c.Value, c.Key * 11.6f * Block);

                result.Close();
            }
        }
    }
}

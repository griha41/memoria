using System;

using NAudio.Wave;
using System.Threading;

namespace Usach.Civil.Memoria.AudioEntrevista.Archivo.Mp3
{
    public class Mp3 : IArchivo
    {
        public void Read(string path)
        {
            Console.WriteLine("1");
            var input = new Mp3FileReader(path);
            Console.WriteLine("2");
            var output = new DirectSoundOut();
            Console.WriteLine("3");
            output.Init(new WaveChannel32(input));
            Console.WriteLine("4");

            output.Play();
            Thread.Sleep(1000 * 100);
        }
    }
}

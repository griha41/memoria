using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Usach.Civil.Memoria.AudioEntrevista.Archivo;

namespace Usach.Civil.Memoria.AudioEntrevista.Consola.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            IArchivo arc = new Archivo.Mp3.Mp3();
            arc.Read(args[0]);
        }
    }
}

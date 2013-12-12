using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace RecNote.Data.MongoDB.Files
{
    public class FileData : DataBase<Entities.Files.File>, RecNote.Data.Files.IFileData
    {

        public Entities.Files.File[] FindByType(string type)
        {
            return (
                from e in this.GetCollection().AsQueryable()
                where e.ContentType.ToLower().Contains(type.ToLower())
                select e
             ).ToArray();
        }

        public override Entities.Files.File Save(Entities.Files.File entry)
        {
            //return base.Save(entry);
            if( entry != null && entry.GetType() == typeof(Entities.Files.AudioFile))
            {
                var audio = (Entities.Files.AudioFile)entry;
                var keys = audio.Finger.Select(p => new Key { Length = p.Length, Bytes = this.ToByte(p) });
                var file = File.Create(audio.AlterPath + ".key");

                var count = BitConverter.GetBytes(keys.Count());

                file.Write(count, 0, 4);

                foreach (var k in keys)
                {
                    file.Write(BitConverter.GetBytes(k.Bytes.Length), 0, 4);
                    file.Write(BitConverter.GetBytes(k.Length), 0, 4);
                    file.Write(k.Bytes, 0, k.Bytes.Length);
                }
                file.Close();
                audio.Finger = new bool[][] { };
                return base.Save(audio);
            }
            else
                return base.Save(entry);
        }

        public override Entities.Files.File FindByID(string id)
        {
            var entry = base.FindByID(id);
            if (entry != null && entry.GetType() == typeof(Entities.Files.AudioFile) && File.Exists(((Entities.Files.AudioFile)entry).AlterPath + ".key"))
            {
                var audio = (Entities.Files.AudioFile)entry;
                var fileName = audio.AlterPath + ".key";
                var fs = File.OpenRead(fileName);
                byte[] bytes = new byte[fs.Length];
                fs.Read(bytes, 0, Convert.ToInt32(fs.Length));
                fs.Close();
                audio.Finger = this.ToBools(bytes);
                return audio;
            }
            else
                return entry;
        }

        private class Key
        {
            public int Length { get; set; }
            public byte[] Bytes { get; set; }
        }
       
        private byte[] ToByte(bool[] bools)
        {
            if (bools.Length == 0) return new byte[] { };
            
            int bytes = bools.Length / 8;
            if (bools.Length % 8 != 0) bytes++;
            byte[] arr2 = new byte[bytes];
            int bitIndex = 0, byteIndex = 0;

            for (long i = 0; i < bools.LongLength; i++)
            {
                if (bools[i])
                    arr2[byteIndex] |= (byte)(((byte)1) << bitIndex);

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
                
            }
            return arr2;
        }
        private bool[][] ToBools(byte[] bytes)
        {
            List<List<bool>> bools = new List<List<bool>>();
            int index = 0;
            int countArr = BitConverter.ToInt32(bytes, index);
            index += 4;
            for (int arrI = 0; arrI < countArr; arrI++)
            {
                int countBytes = BitConverter.ToInt32(bytes, index);
                index += 4;
                int countBools = BitConverter.ToInt32(bytes, index);
                index += 4;
                List<bool> list = new List<bool>();
                for(int cii = 0; cii < countBytes; cii++)
                {
                    byte cbyte = bytes[cii + index];
                    for(int cb = 0; cb < 8; cb++)
                    {
                        if(cb < countBools)
                        {
                            list.Add((cbyte & (1 << cb)) != 0);
                        }
                    }

                }
                bools.Add(list);
                index += countBytes;
            }
            return bools.Select(p => p.ToArray()).ToArray();
        }

    }
}

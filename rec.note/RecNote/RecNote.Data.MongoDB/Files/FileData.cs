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
            if (entry != null && entry.GetType() == typeof(RecNote.Entities.Files.AudioFile))
            {
                var audio = (Entities.Files.AudioFile) entry;
                if(string.IsNullOrEmpty(audio.AudioID))
                { 
                    var file = SerializeToStream(audio);
                    var name = audio.AlterPath + ".key";

                    using (FileStream f = new FileStream(name, FileMode.Create, System.IO.FileAccess.Write))
                    {
                        byte[] bytes = new byte[file.Length];
                        file.Read(bytes, 0, (int)file.Length);
                        f.Write(bytes, 0, bytes.Length);
                        //file.Close();
                        f.Close();
                    }


                    var info = this.GenericDataBase.MongoDataBase.GridFS.Upload(name, name);
                    


                    audio.Finger = new bool[][] {};
                    audio.AudioID = info.Id.ToString();
                }
                base.Save(audio);
            }
            return base.Save(entry);
        }

        public override Entities.Files.File FindByID(string id)
        {
            //return base.FindByID(id);
            var entry = base.FindByID(id);
            if (entry != null && entry.GetType() == typeof(RecNote.Entities.Files.AudioFile))
            {
                var audio = (Entities.Files.AudioFile) entry;
                var e = this.GenericDataBase.MongoDataBase.GridFS.FindOne(Query.EQ("_id", new ObjectId(audio.AudioID)));
                audio.Finger = DeserializeFromStream(e).Finger;
                return audio;
            }
            else
                return entry;
        }

       
        private byte[] ToByte(bool[] bools)
        {
            if (bools.Length == 0) return new byte[] { };

            long bytes = bools.LongLength / 8;
            if ((bools.Length % 8) != 0) bytes++;
            byte[] arr2 = new byte[bytes];
            int bitIndex = 0, byteIndex = 0;

            for (long i = 0; i < bools.LongLength; i++)
            {
                if (bools[i])
                    arr2[byteIndex] |= (byte)(((byte)1) << bitIndex);
                
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
                bitIndex++;
            }
            return arr2;
        }


        public static MemoryStream SerializeToStream(RecNote.Entities.Files.AudioFile o)
        {
            MemoryStream stream = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, o);
            return stream;
        }

        public static RecNote.Entities.Files.AudioFile DeserializeFromStream(global::MongoDB.Driver.GridFS.MongoGridFSFileInfo file)
        {

            MemoryStream newFs = new MemoryStream();

            
            using (FileStream f = new FileStream(file.Name, FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[f.Length];
                f.Read(bytes, 0, (int)f.Length);
                newFs.Write(bytes, 0, (int)f.Length);
            }

            /*using (var stream = file.OpenRead())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                newFs.Write(bytes, 0, bytes.Length);
            }*/

            IFormatter formatter = new BinaryFormatter();
            newFs.Seek(0, SeekOrigin.Begin);
            var o = (RecNote.Entities.Files.AudioFile)formatter.Deserialize(newFs);
            return o;
        }

    }
}

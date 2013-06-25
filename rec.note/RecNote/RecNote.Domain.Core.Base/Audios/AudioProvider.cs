using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data.Audios;





using Files = RecNote.Entities.Files;


namespace RecNote.Domain.Core.Base.Audios
{
    public class AudioProvider : ProviderBase<Entities.Audios.Audio>, Core.Audios.IAudioProvider
    {
        IAudioData AudioData { get; set; }
        Core.Files.IFileProvider FileProvider { get; set; }

        public int FingerLength { get; set; }
        public int TopWavelets { get; set; }
        public int MaxFrequency {get;set;}
        public int MinFrequency { get; set; }

        

        public void Append(string projectID, string fileID, string audioName)
        {
            var audio = this.AudioData.Append(projectID, fileID, audioName);
        }

        public Core.Files.AudioComparedResult Compare(string audioID, string fileID, float similarity)
        {
            var audio = this.FindByID(audioID);
            var fileAudio = this.FileProvider.FindByID(audio.FileID);

            if (fileAudio.GetType() != typeof(Entities.Files.AudioFile))
                throw new Exception("error.fileTypeError");

            var file = this.FileProvider.FindByID(fileID);
            if( file.GetType() != typeof(Entities.Files.AudioFile))
                throw new Exception("error.fileTypeError");
            var audioCompare = new Files.AudioFileCompare();

            return audioCompare.Compare((Entities.Files.AudioFile)fileAudio, (Entities.Files.AudioFile)file, similarity);

        }



        public IList<Entities.Audios.Audio> FindByProjectID(string projectID)
        {
            return this.AudioData.GetAudiosByProject(projectID);
        }
    }
}

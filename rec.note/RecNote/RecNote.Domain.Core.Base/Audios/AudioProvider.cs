using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data.Audios;



using System.Threading;

using Files = RecNote.Entities.Files;
using System.Diagnostics;

namespace RecNote.Domain.Core.Base.Audios
{
    public class AudioProvider : ProviderBase<Entities.Audios.Audio>, Core.Audios.IAudioProvider
    {
        IAudioData AudioData { get; set; }
        Core.Files.IFileProvider FileProvider { get; set; }
        string FileName { get; set; }
        string WorkingDirectory { get; set; }

        

        public void Append(string projectID, string fileID, string audioName)
        {
            var file = this.FileProvider.FindByID(fileID);
            if (file.GetType() != typeof(Entities.Files.AudioFile))
            {
                this.Process(file);
            }
            var audio = this.AudioData.Append(projectID, fileID, audioName);
        }

        private void Process(Entities.Files.File file)
        {
            /*
            var p = new System.Diagnostics.Process();
            //p.StartInfo.UseShellExecute = true;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.StartInfo.WorkingDirectory = this.WorkingDirectory;
            p.StartInfo.FileName = this.FileName;
            p.StartInfo.Arguments = "\"" + file.ID + "\"";
            p.StartInfo.EnvironmentVariables.Clear();
            p.Start();
            p.WaitForExit();
            */
            //System.Diagnostics.Process.Start(this.WorkingDirectory + this.FileName + " \"" + file.ID + "\"");
            /*
            ProcessStartInfo info = new ProcessStartInfo(@"A:\SoundRecNote\SoundFingerPrinting.RecNote.exe", file.ID);
            info.UseShellExecute = false;
            info.RedirectStandardError = true;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = true;
            info.CreateNoWindow = true;
            info.ErrorDialog = false;
            info.WindowStyle = ProcessWindowStyle.Hidden;

            Process process = System.Diagnostics.Process.Start(info);
            process.WaitForExit(2000);
            */
            System.Threading.Thread.Sleep(2000);
        }

        public Entities.Files.Compared.AudioComparedResult Compare(string audioID, string fileID, float similarity)
        {
            var audio = this.FindByID(audioID);
            var fileAudio = this.FileProvider.FindByID(audio.FileID);

            if (fileAudio.GetType() != typeof(Entities.Files.AudioFile))
            { this.Process(fileAudio); }

            var file = this.FileProvider.FindByID(fileID);
            if (file.GetType() != typeof(Entities.Files.AudioFile))
            { this.Process(file); }
            var audioCompare = new Files.AudioFileCompare();

            return audioCompare.Compare(
                this.FileProvider.FindByID<Entities.Files.AudioFile>(audio.FileID),
                this.FileProvider.FindByID<Entities.Files.AudioFile>(fileID) , 
                similarity);

        }



        public IList<Entities.Audios.Audio> FindByProjectID(string projectID)
        {
            return this.AudioData.GetAudiosByProject(projectID);
        }

        
    }
}

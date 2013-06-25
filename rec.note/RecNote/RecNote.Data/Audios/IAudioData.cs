using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data.Audios
{
    public interface IAudioData : IDataBase<Entities.Audios.Audio>
    {
        IList<Entities.Audios.Audio> GetAudiosByProject(string projectID);

        Entities.Audios.Audio Append(string projectID, string fileID, string audioName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data.Audios
{
    public interface IAudioProvider : IDataBase<Entities.Audios.Audio>
    {
        IList<Entities.Audios.Audio> GetAudiosByProject(string projectID);
    }
}

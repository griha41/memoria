using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver.Linq;

using RecNote.Data.Audios;

namespace RecNote.Data.MongoDB.Audios
{
    public class AudioProvider : DataBase<Entities.Audios.Audio>, IAudioProvider
    {
        public IList<Entities.Audios.Audio> GetAudiosByProject(string projectID)
        {
            return (from e in this.GetCollection().AsQueryable()
                    where e.Projects.Contains(projectID)
                    select e
                ).ToList();
        }
    }
}

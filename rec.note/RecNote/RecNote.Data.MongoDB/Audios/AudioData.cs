using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver.Linq;

using RecNote.Data.Audios;

namespace RecNote.Data.MongoDB.Audios
{
    public class AudioData : DataBase<Entities.Audios.Audio>, IAudioData
    {
        public IList<Entities.Audios.Audio> GetAudiosByProject(string projectID)
        {
            return (from e in this.GetCollection().AsQueryable()
                    where e.ProjectID == projectID
                    select e
                ).ToList();
        }


        public Entities.Audios.Audio Append(string projectID, string fileID, string audioName)
        {
            return this.Save(new Entities.Audios.Audio
            {
                Date = new DateTime(),
                FileID = fileID,
                ProjectID = projectID,
                Name = audioName
            });
        }
    }
}

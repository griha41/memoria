using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver.Linq;

using RecNote.Data.Audio;

namespace RecNote.Data.MongoDB.Audio
{
    /// <summary>
    /// Implementación en mongo de clase
    /// </summary>
    public class AudioData : DataBase<Entities.Audio.Audio>, IAudioData
    {
        public IList<Entities.Audio.Audio> GetAudioByProject(string projectID)
        {
            return (from e in this.GetCollection().AsQueryable()
                    where e.ProjectID == projectID
                    select e
                ).ToList();
        }


        public Entities.Audio.Audio Append(string projectID, string fileID, string audioName)
        {
            return this.Save(new Entities.Audio.Audio
            {
                Date = new DateTime(),
                FileID = fileID,
                ProjectID = projectID,
                Name = audioName
            });
        }
    }
}

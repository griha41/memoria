using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Audios;

namespace RecNote.Domain.Core.Audios
{
    public interface IAudioProvider : IProviderBase<Entities.Audios.Audio>
    {
        void Append(string projectID, string fileID, string audioName);
        IList<Entities.Audios.Audio> FindByProjectID(string projectID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecNote.Data.Audio
{
    /// <summary>
    /// Interface de manejo de datos de audio, hereda de interface de manejo de datos, especializada en audios
    /// </summary>
    public interface IAudioData : IDataBase<Entities.Audio.Audio>
    {
        /// <summary>
        /// Lista los audios a traves de un proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <returns></returns>
        IList<Entities.Audio.Audio> GetAudioByProject(string projectID);
        /// <summary>
        /// Agrega un audio al proyecto determinado
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="fileID">Identificador del audio</param>
        /// <param name="audioName">Nombre que se dará al audio</param>
        /// <returns></returns>
        Entities.Audio.Audio Append(string projectID, string fileID, string audioName);
    }
}

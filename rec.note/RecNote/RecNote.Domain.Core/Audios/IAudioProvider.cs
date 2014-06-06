using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Entities.Audio;

namespace RecNote.Domain.Core.Audio
{
    /// <summary>
    /// Manejador de Audio
    /// </summary>
    public interface IAudioProvider : IProviderBase<Entities.Audio.Audio>
    {
        /// <summary>
        /// Agrega un audio a sistema
        /// </summary>
        /// <param name="projectID">Identificador del proyecto donde se alojará</param>
        /// <param name="fileID">Identificador del archivo agregado</param>
        /// <param name="audioName">Nombre del audio</param>
        void Append(string projectID, string fileID, string audioName);
        /// <summary>
        /// Genera una lista de audios por proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <returns></returns>
        IList<Entities.Audio.Audio> FindByProjectID(string projectID);
        /// <summary>
        /// Compara dos audios
        /// </summary>
        /// <param name="audioID">Identificador del audio 1</param>
        /// <param name="fileID">Identificador del audio 2</param>
        /// <param name="similarity">Minima similaridad para ser considerado</param>
        /// <returns></returns>
        Entities.Files.Compared.AudioComparedResult Compare(string audioID, string fileID, float similarity);
    }
}

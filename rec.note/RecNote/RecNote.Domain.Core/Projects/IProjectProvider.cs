using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecNote.Entities.Projects;

namespace RecNote.Domain.Core.Projects
{
    /// <summary>
    /// Manejador de proyectos
    /// </summary>
    public interface IProjectProvider : IProviderBase<Project>
    {
        /// <summary>
        /// Obtiene un proyecto temporal para el usuario seleccionado
        /// </summary>
        /// <param name="userID">Identificador de usuario</param>
        /// <returns></returns>
        Project GetTemporalProject(Entities.Users.User userID);
        /// <summary>
        /// Lista los proyectos
        /// </summary>
        /// <returns></returns>
        IList<Project> GetList();
        /// <summary>
        /// Genera un nuevo actor para el proyecto seleccionado
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="actor">Información del actor</param>
        void NewActor(string projectID, Entities.Projects.Actor actor);
        /// <summary>
        /// Elimina el actor del proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="actorID">Identificador del actor</param>
        void RemoveActor(string projectID, string actorID);
        /// <summary>
        /// Lista todos los proyecto con los permisos particulares de un usuario
        /// </summary>
        /// <param name="userID">Usuario</param>
        /// <returns></returns>
        IList<Entities.Projects.RestrictedProject> GetProjectByUser(string userID);

        /// <summary>
        /// Obtiene un concepto de un proyecto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="name">Nombre del concepto</param>
        /// <returns></returns>
        ProjectItem GetItem(string projectID, ProjectItemType type, string name);
        /// <summary>
        /// Bloquea un elemento para que no se permita su edición paralela
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="name">Nombre del conceto</param>
        /// <param name="user">Usuario que bloquea el concepto</param>
        /// <returns></returns>
        ProjectItem BlockItem(string projectID, ProjectItemType type , string name, Entities.Users.User user);
        /// <summary>
        /// Guarda y desbloqueda un concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="item">Elemento</param>
        /// <returns></returns>
        ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item);
        /// <summary>
        /// Cambia el estado de publicación de un concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del concepto</param>
        /// <param name="name">Nombre del concepto</param>
        /// <param name="publish">Publicado: Si\No</param>
        /// <returns></returns>
        ProjectItem PublishItem(string projectID, ProjectItemType type, string name, bool publish);
        /// <summary>
        /// Agrega un comentario al concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="message">Mensaje</param>
        /// <param name="user">Usuario que lo agrega</param>
        void AddComment(string projectID, ProjectItemType type, string name, string message, Entities.Users.User user);
        /// <summary>
        /// Obtiene el rol de un usuario en un proyecto
        /// </summary>
        /// <param name="projectID">Identificaor del proyecto</param>
        /// <param name="userID">Identificador del usuario</param>
        /// <returns></returns>
        RoleType GetRole(string projectID, string userID);
    }
}

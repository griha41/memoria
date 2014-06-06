using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecNote.Entities.Projects;

namespace RecNote.Data.Projects
{
    /// <summary>
    /// Administrador de información de un proyecto
    /// </summary>
    public interface IProjectData : IDataBase<Project>
    {
        /// <summary>
        /// Obtiene el proyecto temporal por un usuario
        /// </summary>
        /// <param name="User">Usuario dueño del proyecto temporal</param>
        /// <returns></returns>
        Project GetTemporalProject(Entities.Users.User User);
        /// <summary>
        /// Obtiene la lista completa de proyectos
        /// </summary>
        /// <returns></returns>
        IList<Entities.Projects.Project> GetList();
        /// <summary>
        /// Obtiene la lista completa de proyecto, donde un usuario es dueño
        /// </summary>
        /// <param name="UserID">Usuario dueño del proyecto</param>
        /// <returns></returns>
        IList<Entities.Projects.Project> GetByOwner(string UserID);
        /// <summary>
        /// Listado de proyecto donde el usuario determinado tiene un rol especifico
        /// </summary>
        /// <param name="UserID">Usuario buscado</param>
        /// <param name="Role">Rol en el proyecto</param>
        /// <returns></returns>
        IList<Entities.Projects.Project> GetByUserRole(string UserID, RoleType Role);
        /// <summary>
        /// Obtiene la información de un concepto según el proyecto, tipo y nombre del concepto
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo del elemento</param>
        /// <param name="name">Nombre</param>
        /// <returns></returns>
        ProjectItem GetItem(string projectID, ProjectItemType type, string name);
        /// <summary>
        /// Guarda un elemento del tipo acordado en proyecto determinado
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="item">Elemento guardado</param>
        /// <returns></returns>
        ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item);
        /// <summary>
        /// Agrega un comentario al elemento guardado
        /// </summary>
        /// <param name="projectID">Identificador del proyecto</param>
        /// <param name="type">Tipo de concepto</param>
        /// <param name="name">Nombre</param>
        /// <param name="message">Mensaje</param>
        void AddComment(string projectID, ProjectItemType type, string name, ProjectItemComment message);
    }
}

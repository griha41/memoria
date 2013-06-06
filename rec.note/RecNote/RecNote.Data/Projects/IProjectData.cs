using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecNote.Entities.Projects;

namespace RecNote.Data.Projects
{
    public interface IProjectData : IDataBase<Project>
    {
        Project GetTemporalProject(Entities.Users.User User);
        IList<Entities.Projects.Project> GetList();
        IList<Entities.Projects.Project> GetByOwner(string UserID);
        IList<Entities.Projects.Project> GetByUserRole(string UserID, RoleType Role);

        ProjectItem GetItem(string projectID, ProjectItemType type, string name);
        ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item);
    }
}

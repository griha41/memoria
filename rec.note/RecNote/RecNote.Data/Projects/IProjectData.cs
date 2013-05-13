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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecNote.Entities.Projects;

namespace RecNote.Domain.Core.Projects
{
    public interface IProjectProvider : IProviderBase<Project>
    {
        Project GetTemporalProject(Entities.Users.User UserID);
        IList<Project> GetList();
        void NewActor(string ProjectID, Entities.Projects.Actor Actor);
    }
}

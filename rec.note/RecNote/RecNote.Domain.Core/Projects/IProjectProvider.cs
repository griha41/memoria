using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecNote.Entities.Projects;

namespace RecNote.Domain.Core.Projects
{
    public interface IProjectProvider : IProviderBase<Project>
    {
        Project GetTemporalProject(Entities.Users.User userID);
        IList<Project> GetList();
        void NewActor(string projectID, Entities.Projects.Actor actor);
        void RemoveActor(string projectID, string actorID);

        IList<Entities.Projects.RestrictedProject> GetProjectByUser(string userID);


        ProjectItem GetItem(string projectID, ProjectItemType type, string name);
        ProjectItem BlockItem(string projectID, ProjectItemType type , string name, Entities.Users.User user);
        ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item);
        ProjectItem PublishItem(string projectID, ProjectItemType type, string name, bool publish);

        void AddComment(string projectID, ProjectItemType type, string name, string message, Entities.Users.User user);
    }
}

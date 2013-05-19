using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using RecNote.Data.Projects;

namespace RecNote.Domain.Core.Base.Projects
{
    class ProjectProvider : ProviderBase<Entities.Projects.Project>, Domain.Core.Projects.IProjectProvider
    {
        IProjectData ProjectData { get; set; }
        Core.Users.IUserProvider UserProvider { get; set; }

        public Entities.Projects.Project GetTemporalProject(Entities.Users.User user)
        {
            var project = this.ProjectData.GetTemporalProject(user);
            
            // No exite temporal
            if(project == null)
                project = this.GenericDataBase.Save(new Entities.Projects.Project
                {
                    Owner = user,
                    IsTemporal = true
                });
            // ahora si =)

            return project;
        }

        public IList<Entities.Projects.Project> GetList()
        {
            return this.ProjectData.GetList();
        }


        public void NewActor(string projectID, Entities.Projects.Actor actor)
        {
            if (string.IsNullOrEmpty(actor.ID))
            {
                actor = (Entities.Projects.Actor)this.UserProvider.New(actor);
            }
            var project = this.ProjectData.FindByID(projectID);
            project.Actors = project.Actors == null ? new[] { actor } : project.Actors.Concat(new[] { actor }).ToArray();
            this.Save(project);
        }

        public void RemoveActor(string projectID, string actorID)
        {
            var project = this.ProjectData.FindByID(projectID);
            project.Actors = project.Actors.Where(p => p.ID != actorID).ToArray();
            this.Save(project);
        }

        public override Entities.Projects.Project Save(Entities.Projects.Project entry)
        {
            entry.IsTemporal = string.IsNullOrEmpty(entry.Name);
            return base.Save(entry);
        }


        public IList<Entities.Projects.RestrictedProject> GetProjectByUser(string userID)
        {
            var listProjects = new List<Entities.Projects.RestrictedProject>();
            var user = this.UserProvider.FindByID(userID);
            listProjects.AddRange( this.ProjectData.GetByOwner(userID).Select(p => new Entities.Projects.RestrictedProject{
                Project = p,
                UserAllow = user,
                IsOwner = true,
                Role = Entities.Projects.RoleType.Developer
            }).ToArray());

            foreach (Entities.Projects.RoleType role in Enum.GetValues(typeof(Entities.Projects.RoleType)))
            {
                listProjects.AddRange(this.ProjectData.GetByUserRole(userID, role).Select(p => new Entities.Projects.RestrictedProject
                {
                    Project = p,
                    UserAllow = user,
                    IsOwner = false,
                    Role = role
                }).ToArray());
            }

            return listProjects.Where(p => !p.Project.IsTemporal).ToList();
        }
    }
}

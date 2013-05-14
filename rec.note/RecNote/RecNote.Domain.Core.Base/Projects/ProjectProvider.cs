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

        public Entities.Projects.Project GetTemporalProject(Entities.Users.User User)
        {
            var project = this.ProjectData.GetTemporalProject(User);
            
            // No exite temporal
            if(project == null)
                project = this.GenericDataBase.Save(new Entities.Projects.Project
                {
                    Owner = User,
                    IsTemporal = true
                });
            // ahora si =)

            return project;
        }

        public IList<Entities.Projects.Project> GetList()
        {
            return this.ProjectData.GetList();
        }


        public void NewActor(string ProjectID, Entities.Projects.Actor Actor)
        {
            if (string.IsNullOrEmpty(Actor.ID))
            {
                var user = this.UserProvider.New(Actor);
                Actor.ID = user.ID;
            }
            var project = this.ProjectData.FindByID(ProjectID);
            project.Actors = project.Actors.Concat(new[] { Actor }).ToArray();
            this.Save(project);
        }
    }
}

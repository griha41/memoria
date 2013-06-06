using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using RecNote.Entities.Projects;

using MongoDB.Driver.Linq;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Bson;

namespace RecNote.Data.MongoDB.Projects
{
    public class ProjectData : DataBase<Entities.Projects.Project>, Data.Projects.IProjectData
    {
        public Entities.Projects.Project GetTemporalProject(Entities.Users.User User)
        {
            return (
                from e in this.GetCollection().AsQueryable()
                        where e.Owner.ID == User.ID
                        && e.IsTemporal
                    select e
             ).FirstOrDefault();
        }
        public IList<Entities.Projects.Project> GetList()
        {
            return (
                from e in this.GetCollection().AsQueryable()
                where !e.IsTemporal
                select (Entities.Projects.Project)e
                ).ToList();
        }


        public IList<Entities.Projects.Project> GetByOwner(string UserID)
        {
            return (
                from e in this.GetCollection().AsQueryable()
                where e.Owner.ID == UserID
                select (Entities.Projects.Project)e
                ).ToList();
        }

        public IList<Entities.Projects.Project> GetByUserRole(string UserID, Entities.Projects.RoleType Role)
        {
            return (
                from e in this.GetCollection().AsQueryable()
                where 
                e.Actors != null &&
                e.Actors.Any(a => a.ID == UserID && a.Role == Role)
                select (Entities.Projects.Project)e
                ).ToList();
        }


        public ProjectItem GetItem(string projectID, ProjectItemType type, string name)
        {
            var project = this.FindByID(projectID);
            if (type == ProjectItemType.Introduction)
                return project.Definition.Introducction;
            if (type == ProjectItemType.CurrentSystem)
                return project.Definition.CurrentSystem;
            switch (type)
            {
                case ProjectItemType.Actors: return project.Definition.Actors.FirstOrDefault(p => p.Name == name); 
                case ProjectItemType.Objetives: return project.Definition.Objetives.FirstOrDefault(p => p.Name == name); 
                case ProjectItemType.ReqFunctionals: return project.Requirements.Functionals.FirstOrDefault(p => p.Name == name); 
                case ProjectItemType.ReqInformations: return project.Requirements.Informations.FirstOrDefault(p => p.Name == name); 
                case ProjectItemType.ReqNotFunctionals: return project.Requirements.NotFunctionals.FirstOrDefault(p => p.Name == name); 
                default: return null;
            }
        }

        public ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item)
        {
            Expression<Func<Project, ProjectItem>> path = null;

            if (type == ProjectItemType.Introduction)
                path = p => p.Definition.Introducction;
            if (type == ProjectItemType.CurrentSystem)
                path = p => p.Definition.CurrentSystem;
            switch (type)
            {
                case ProjectItemType.Actors:
                    path = p => p.Definition.Actors.FirstOrDefault(a => a.Name == item.Name);
                    break;
                case ProjectItemType.Objetives: path = p => p.Definition.Objetives.FirstOrDefault(a => a.Name == item.Name); break;
                case ProjectItemType.ReqFunctionals: path = p => p.Requirements.Functionals.FirstOrDefault(a => a.Name == item.Name); break;
                case ProjectItemType.ReqInformations: path = p => p.Requirements.Informations.FirstOrDefault(a => a.Name == item.Name); break;
                case ProjectItemType.ReqNotFunctionals: path = p => p.Requirements.NotFunctionals.FirstOrDefault(a => a.Name == item.Name); break;
            }

            this.GetCollection().Update(Query<Project>.EQ(p => p.ID, projectID), Update<Project>.Set(path, item));

            return item;
        }


    }
}

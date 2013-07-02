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
using MongoDB.Bson.Serialization;
using MongoDB;

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

        private int GetIndex(string projectID, ProjectItemType type, string name)
        {
            var project = this.FindByID(projectID);
            if (type == ProjectItemType.Introduction)
                return 0;
            if (type == ProjectItemType.CurrentSystem)
                return 0;
            switch (type)
            {
                case ProjectItemType.Actors: return project.Definition.Actors.ToList().FindIndex(p => p.Name == name);
                case ProjectItemType.Objetives: return project.Definition.Objetives.ToList().FindIndex(p => p.Name == name);
                case ProjectItemType.ReqFunctionals: return project.Requirements.Functionals.ToList().FindIndex(p => p.Name == name);
                case ProjectItemType.ReqInformations: return project.Requirements.Informations.ToList().FindIndex(p => p.Name == name);
                case ProjectItemType.ReqNotFunctionals: return project.Requirements.NotFunctionals.ToList().FindIndex(p => p.Name == name);
                default: return 0;
            }
        }

        private Expression<Func<Project, IEnumerable<ProjectItemComment>>> PathComment(string projectID, ProjectItemType type, string name)
        {
            Expression<Func<Project, IEnumerable<ProjectItemComment>>> path = null;

            if (type == ProjectItemType.Introduction)
                path = p => p.Definition.Introducction.Comments;
            if (type == ProjectItemType.CurrentSystem)
                path = p => p.Definition.CurrentSystem.Comments;
            switch (type)
            {
                case ProjectItemType.Actors: path = p => p.Definition.Actors.FirstOrDefault(a => a.Name == name).Comments; break;
                case ProjectItemType.Objetives: path = p => p.Definition.Objetives.FirstOrDefault(a => a.Name == name).Comments; break;
                case ProjectItemType.ReqFunctionals: path = p => p.Requirements.Functionals.FirstOrDefault(a => a.Name == name).Comments; break;
                case ProjectItemType.ReqInformations: path = p => p.Requirements.Informations.FirstOrDefault(a => a.Name == name).Comments; break;
                case ProjectItemType.ReqNotFunctionals: path = p => p.Requirements.NotFunctionals.FirstOrDefault(a => a.Name == name).Comments; break;
            }
            return path;
        }

        private Expression<Func<Project, ProjectItem>> PathItem(string projectID, ProjectItemType type, string name)
        {
            Expression<Func<Project, ProjectItem>> path = null;

            if (type == ProjectItemType.Introduction)
                path = p => p.Definition.Introducction;
            if (type == ProjectItemType.CurrentSystem)
                path = p => p.Definition.CurrentSystem;
            switch (type)
            {
                case ProjectItemType.Actors: path = p => p.Definition.Actors[this.GetIndex(projectID, type, name)]; break;
                case ProjectItemType.Objetives: path = p => p.Definition.Objetives[this.GetIndex(projectID, type, name)]; break;
                case ProjectItemType.ReqFunctionals: path = p => p.Requirements.Functionals[this.GetIndex(projectID, type, name)]; break;
                case ProjectItemType.ReqInformations: path = p => p.Requirements.Informations[this.GetIndex(projectID, type, name)]; break;
                case ProjectItemType.ReqNotFunctionals: path = p => p.Requirements.NotFunctionals[this.GetIndex(projectID, type, name)]; break;
            }
            return path;
        }

        private Expression<Func<Project, IEnumerable<ProjectItem>>> PathSaveItem(string projectID, ProjectItemType type)
        {
            Expression<Func<Project, IEnumerable<ProjectItem>>> path = null;

            if (type == ProjectItemType.Introduction)
                path = null;
            if (type == ProjectItemType.CurrentSystem)
                path = null;
            switch (type)
            {
                case ProjectItemType.Actors: path = p => p.Definition.Actors; break;
                case ProjectItemType.Objetives: path = p => p.Definition.Objetives; break;
                case ProjectItemType.ReqFunctionals: path = p => p.Requirements.Functionals; break;
                case ProjectItemType.ReqInformations: path = p => p.Requirements.Informations; break;
                case ProjectItemType.ReqNotFunctionals: path = p => p.Requirements.NotFunctionals; break;
            }
            return path;
        }

        public ProjectItem SaveItem(string projectID, ProjectItemType type, ProjectItem item)
        {
            var path = this.PathItem(projectID, type, item.Name);
            if (this.GetItem(projectID, type, item.Name) == null)
            {
                this.GetCollection().Update(
                    Query<Project>.EQ(p => p.ID, projectID),
                    Update<Project>.Push<ProjectItem>(this.PathSaveItem(projectID, type), item)
                    );
            }
            else
            {
                if (type == ProjectItemType.Introduction || type == ProjectItemType.CurrentSystem)
                {
                    this.GetCollection().Update(
                        Query<Project>.EQ(p => p.ID, projectID),
                        Update<Project>.Set(path, item)
                        );
                }
                else
                {
                    this.GetCollection().Update(
                        Query.And( Query<Project>.EQ(p => p.ID, projectID) ),
                        Update<Project>.Set<ProjectItem>(path, item)
                        );
                }
            }
            return item;
        }

        public void AddComment(string projectID, ProjectItemType type, string name, ProjectItemComment message)
        {
            var path = this.PathComment(projectID, type, name);
            
            this.GetCollection().Update(Query<Project>.EQ(p => p.ID, projectID),
                Update<Project>.Push<ProjectItemComment>(path, message)
                );
        }
    }
}

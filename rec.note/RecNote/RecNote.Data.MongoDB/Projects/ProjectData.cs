using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MongoDB.Driver.Linq;

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
    }
}

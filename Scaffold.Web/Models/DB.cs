using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class DB : DbContext 
    {
        public DB(): base("DefaultConnection")
        { 
        }

        public virtual IDbSet<Todo> Todos { get; set; }
        public virtual IDbSet<Anu> Anus { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items) 
        {
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
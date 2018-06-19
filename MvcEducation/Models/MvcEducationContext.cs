using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcEducation.Models
{
    public class MvcEducationContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public MvcEducationContext() : base("name=MvcEducationContext")
        {
        }

		public System.Data.Entity.DbSet<MvcEducation.Models.Major> Majors { get; set; }

		public System.Data.Entity.DbSet<MvcEducation.Models.Student> Students { get; set; }

		public System.Data.Entity.DbSet<MvcEducation.Models.Class> Classes { get; set; }

		public System.Data.Entity.DbSet<MvcEducation.Models.Enrolled> Enrolleds { get; set; }
	}
}

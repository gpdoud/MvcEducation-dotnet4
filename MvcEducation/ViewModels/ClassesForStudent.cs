using MvcEducation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcEducation.ViewModels {
	public class ClassesForStudent {
		public Student Student { get; set; }
		public IEnumerable<Class> Classes { get; set; }

		public ClassesForStudent() { }
	}
}
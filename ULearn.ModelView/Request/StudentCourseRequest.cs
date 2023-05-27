using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULearn.DbModel.Models.DB;

namespace ULearn.ModelView.Request
{
	public class StudentCourseRequest
	{
		public int CourseId { get; set; }
		public int StudentId { get; set; }

		public bool IsArchived { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULearn.DbModel.Models.DB;

namespace ULearn.ModelView.ModelView
{
	public class StudentCourseModel
	{
		public int CourseId { get; set; }
		public int StudentId { get; set; }
		[Timestamp]
		public DateTime CreatedDate { get; set; }
		[Timestamp]
		public DateTime UpdatedDate { get; set; }
		public bool IsArchived { get; set; }

		public string CourseName { get; set; }
		public string StudentName { get; set; }
	}
}

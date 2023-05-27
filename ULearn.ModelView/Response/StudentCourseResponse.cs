using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULearn.Common.Extensions;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Result;

namespace ULearn.ModelView.Response
{
	public class StudentCourseResponse
	{
		public PagedResult<StudentCourseModel> pagedResult { get; set; }
		
		public Dictionary<int, CourseResult> Course { get; set; }
	}
}

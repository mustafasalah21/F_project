using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;

namespace ULearn.Core.Manager.Interfaces
{
	public interface ISubscriptionManager
	{
	
		StudentCourse CreateCourseSubscription(StudentCourseRequest request );
		StudentCourseResponse GetSubscriptions(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "");
		List<StudentCourseModel> GetSubscriptionsByStudent(int studentid);
		void DeleteSubscriptions(StudentCourseRequest request);

	}
}

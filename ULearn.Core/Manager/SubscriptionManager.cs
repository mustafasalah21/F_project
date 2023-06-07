using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ULearn.Common.Extensions;
using ULearn.Core.Manager.Interfaces;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;
using ULearn.ModelView.Result;

namespace ULearn.Core.Manager
{
	public class SubscriptionManager : ISubscriptionManager
	{
		public ulearndbContext _ulearndbContext;
		private IMapper _mapper;
        public SubscriptionManager(ulearndbContext ulearndbContext, IMapper mapper)
		{
			_ulearndbContext = ulearndbContext;
			_mapper = mapper;
		}
		public StudentCourse CreateCourseSubscription(StudentCourseRequest request)
		{
			var oldSubscribe = _ulearndbContext.StudentCourses.FirstOrDefault(m=>m.StudentId==request.StudentId && m.CourseId==request.CourseId);
			if (oldSubscribe != null)
			{
				 throw new ServiceValidationException("Student already subscribed to this course");
			}
			var student = _ulearndbContext.Users.Where(m=>m.Id==request.StudentId)
				.Include(m=>m.UserRoles)
				.ThenInclude(m=>m.Role)
				.FirstOrDefault();
			if(student == null)
			{
				throw new ServiceValidationException("there is no student with this id");
			}
			var course = _ulearndbContext.Courses.Where(m => m.Id == request.CourseId).FirstOrDefault();
			if (course == null)
			{
				throw new ServiceValidationException("there is no course with this id");
			}
			var check = _ulearndbContext.StudentCourses.Any(m => (m.StudentId == request.StudentId && m.CourseId == request.CourseId));
            if (check)
            {
                throw new ServiceValidationException("This student already regestered in this course");
            }
            StudentCourse subscribe ;

			subscribe = _ulearndbContext.StudentCourses.Add(new()
			{
				CourseId = request.CourseId,
				CreatedDate =DateTime.Now,
				UpdatedDate = DateTime.Now,
				IsArchived = request.IsArchived,
				StudentId = request.StudentId,
			}).Entity;

			_ulearndbContext.SaveChanges();
			return _mapper.Map<StudentCourse>(subscribe);
		}

		public void DeleteSubscriptions(StudentCourseRequest request)
		{
			var oldSubscribe = _ulearndbContext.StudentCourses.FirstOrDefault(m => m.StudentId == request.StudentId && m.CourseId == request.CourseId);
			if (oldSubscribe == null)
			{
				throw new ServiceValidationException("There is no subscription whith this studentId and courseId");
			}
			_ulearndbContext.StudentCourses.Remove(oldSubscribe);
			_ulearndbContext.SaveChanges();

		}

		public StudentCourseResponse GetSubscriptions(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
		{
			var queryRes = _ulearndbContext.StudentCourses
				.Include(m=>m.Course)
				.Include(m=>m.Student)
										   .Where(a => string.IsNullOrWhiteSpace(searchText)
													   || (a.Course.Name.Contains(searchText)
													   || (a.Student.FirstName+ a.Student.LastName).Contains(searchText)));

			if (!string.IsNullOrWhiteSpace(sortColumn)
				&& sortDirection.Equals("ascending", StringComparison.InvariantCultureIgnoreCase))
			{
				queryRes = queryRes.OrderBy(sortColumn);
			}
			else if (!string.IsNullOrWhiteSpace(sortColumn)
				&& sortDirection.Equals("descending", StringComparison.InvariantCultureIgnoreCase))
			{
				queryRes = queryRes.OrderByDescending(sortColumn);
			}

			var res = queryRes.GetPaged(page, pageSize);

			var courseIds = res.Data
							 .Select(a => a.CourseId)
							 .Distinct()
							 .ToList();

			var courses = _ulearndbContext.Courses
										  .Where(a => courseIds.Contains(a.Id))
										  .ToDictionary(a => a.Id, x => _mapper.Map<CourseResult>(x));

			StudentCourseResponse data = new StudentCourseResponse
			{
				pagedResult = _mapper.Map<PagedResult<StudentCourseModel>>(res),
				Course = courses
			};

			data.pagedResult.Sortable.Add("Name", "Course Name");
			data.pagedResult.Sortable.Add("CreatedDate", "Created Date");

			return data;
		}

		public List<StudentCourseModel> GetSubscriptionsByStudent(int studentid)
		{
			List<StudentCourse> SCs = _ulearndbContext.StudentCourses.Where(m => m.StudentId == studentid)
				.Include(sc=>sc.Student)
				.Include(sc=>sc.Course)
				.ToList();
			List < StudentCourseModel > res=new List<StudentCourseModel>();
			List < StudentCourseModel > res2=new List<StudentCourseModel>();
			foreach (var sc in SCs)
			{
				//var a = _mapper.Map<StudentCourseModel>(sc);
				//res.Add(a);

				res.Add( new()
				{
					CourseId = sc.CourseId,
					StudentId = sc.StudentId,
					StudentName = sc.Student.FirstName+" "+sc.Student.LastName,
					CreatedDate=sc.CreatedDate,
					IsArchived = sc.IsArchived,
					CourseName=sc.Course.Name,
					UpdatedDate=sc.UpdatedDate,
				});
			}


			return res;
		}
	}
}

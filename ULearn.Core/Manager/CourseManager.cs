using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.Core.Manager.Interfaces;
using ULearn.Core.Managers;
using ULearn.DbModel.Models;
using ULearn.DbModel.Models.DB;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;
using ULearn.ModelView.Result;

namespace ULearn.Core.Manager
{
    public class CourseManager : ICourseManager
    {
        public ulearndbContext _ulearndbContext;
        private IMapper _mapper;
		private readonly IHelperManager _helperManager;

		public CourseManager(ulearndbContext ulearndbContext, IMapper mapper, IHelperManager helperManager)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
            _helperManager = helperManager; 
        }

        public CourseModel CreateCourse(UserModel currentUser, CourseRequest courseRequest)
        {
            Course course = null;
			string imgurl = _helperManager.SaveImage(courseRequest.Base64Image, "wwwroot\\images\\courses");
			course = _ulearndbContext.Courses.Add(new Course
            {
                Name = courseRequest.Name,
                Description = courseRequest.Description,
                TeacherId = currentUser.Id,
                Image = imgurl,
            }).Entity;

            _ulearndbContext.SaveChanges();
            return _mapper.Map<CourseModel>(course);
        }

        public CourseModel GetCourse(UserModel currentUser, int id)
        {
            var allowedPermissions = new List<string> { "courses_all_view", "course_view" };

            var hasAccess = currentUser.Permissions.Any(a => allowedPermissions.Contains(a.Code));

            var isAllView = currentUser.Permissions.Any(a => allowedPermissions.Equals("courses_all_view"));

            var res = _ulearndbContext.Courses
                                      .Include("Teacher")
                                      .FirstOrDefault(a => (currentUser.IsSuperAdmin
                                                           || (hasAccess
                                                                && (isAllView || a.TeacherId == currentUser.Id)))
                                                           && a.Id == id)
                                      ?? throw new ServiceValidationException("Invalid course id received");
            res.Image=_helperManager.GetBase64FromImagePath(res.Image);
            return _mapper.Map<CourseModel>(res);
        }

        public CourseResponse GetCourses(int page = 1,
                                     int pageSize = 10,
                                     string sortColumn = "",
                                     string sortDirection = "ascending",
                                     string searchText = "")
        {
            var queryRes = _ulearndbContext.Courses
                                           .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                       || (a.Name.Contains(searchText)
                                                       || (a.Image.Contains(searchText)
                                                       || a.Description.Contains(sortColumn))));
            foreach (var a in queryRes)
            {
                a.Image = _helperManager.GetBase64FromImagePath (a.Image);
            }
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

            var userIds = res.Data
                             .Select(a => a.TeacherId)
                             .Distinct()
                             .ToList();
            var usrs = _ulearndbContext.Users
                                        .Where(a => userIds.Contains(a.Id));
            foreach(var u in usrs)
            {
                u.Image= _helperManager.GetBase64FromImagePath(u.Image);
			}

			var users = usrs
                                        .ToDictionary(a => a.Id, x => _mapper.Map<UserResult>(x));

            var data = new CourseResponse
            {
                Course = _mapper.Map<PagedResult<CourseModel>>(res),
                User = users
            };

            data.Course.Sortable.Add("Title", "Title");
            data.Course.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public CourseModel PutCourse(UserModel currentUser, CourseRequest courseRequest)
        {
            Course course = null;

            course = _ulearndbContext.Courses
                                .FirstOrDefault(a => a.Id == courseRequest.Id)
                                ?? throw new ServiceValidationException("Invalid course id received");
			string imgurl = _helperManager.SaveImage(courseRequest.Base64Image, "wwwroot\\images\\courses");
			course.Name = courseRequest.Name;
            course.Image = imgurl;
            course.Description = courseRequest.Description;

            _ulearndbContext.SaveChanges();
            return _mapper.Map<CourseModel>(course);
        }

        public void ArchiveCourse(UserModel currentUser, int id)
        {
            if (!currentUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to archive course");
            }

            /*  var data = _ulearndbContext.Courses
                                       .FirstOrDefault(a => a.Id == id)
                                       ?? throw new ServiceValidationException("Invalid course id received");
                data.IsArchived = true;

              _ulearndbContext.SaveChanges();*/
            var course = _ulearndbContext.Courses.Find(id);
            if (course == null)
            {
                throw new ArgumentException("Course not found", nameof(id));
            }
            _ulearndbContext.Courses.Remove(course);
            _ulearndbContext.SaveChanges();

        }
       
    }
}

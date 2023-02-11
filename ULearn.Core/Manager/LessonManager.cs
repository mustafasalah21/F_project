using AutoMapper;
using System;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.DbModel.Models.DB;
using ULearn.DbModel.Models;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using Microsoft.EntityFrameworkCore;
using ULearn.ModelView.Response;
using ULearn.ModelView.Result;

namespace ULearn.Core.Manager
{
    public class LessonManager : ILessonManager
    {
        public ulearndbContext _ulearndbContext;
        private IMapper _mapper;

        public LessonManager(ulearndbContext ulearndbContext, IMapper mapper)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
        }

        public LessonModel CreateLesson(LessonRequest lessonRequest)
        {
            Lesson lesson = null;

            lesson = _ulearndbContext.Lessons.Add(new Lesson
            {
                Name = lessonRequest.Name,
                Description = lessonRequest.Description,
                CourseId = lessonRequest.CourseId
            }).Entity;

            _ulearndbContext.SaveChanges();
            return _mapper.Map<LessonModel>(lesson);
        }

        public LessonModel GetLesson(UserModel currentUser, int id)
        {
            var res = _ulearndbContext.Lessons
                                      .Include("Course")
                                      .FirstOrDefault(a => a.Id == id)
                                      ?? throw new ServiceValidationException("Invalid lesson id received");

            return _mapper.Map<LessonModel>(res);
        }

        public LessonResponse GetLessons(int page = 1,
                                     int pageSize = 10,
                                     string sortColumn = "",
                                     string sortDirection = "ascending",
                                     string searchText = "")
        {
            var queryRes = _ulearndbContext.Lessons
                                           .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                       || (a.Name.Contains(searchText)
                                                       || a.Description.Contains(sortColumn)));

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

            var data = new LessonResponse
            {
                Lesson = _mapper.Map<PagedResult<LessonModel>>(res),
                Course = courses
            };

            data.Lesson.Sortable.Add("Name", "Lesson Name");
            data.Lesson.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public LessonModel PutLesson(LessonRequest LessonRequest)
        {
            Lesson Lesson = null;

            Lesson = _ulearndbContext.Lessons
                                .FirstOrDefault(a => a.Id == LessonRequest.Id)
                                ?? throw new ServiceValidationException("Invalid lesson id received");

            Lesson.Name = LessonRequest.Name;
            Lesson.Description = LessonRequest.Description;
            Lesson.Id = LessonRequest.Id;
            Lesson.CourseId = LessonRequest.CourseId;


            _ulearndbContext.SaveChanges();
            return _mapper.Map<LessonModel>(Lesson);
        }

        public void ArchiveLesson(UserModel currentUser, int id)
        {
            if (!currentUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to archive lesson");
            }

            var data = _ulearndbContext.Lessons
                                    .FirstOrDefault(a => a.Id == id)
                                    ?? throw new ServiceValidationException("Invalid lesson id received");
            data.IsArchived = true;
            _ulearndbContext.SaveChanges();
        }
    }
}
﻿using AutoMapper;
using System;
using System.Linq;
using ULearn.Common.Extensions;
using ULearn.DbModel.Models.DB;
using ULearn.DbModel.Models;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;
using ULearn.Core.Manager.Interfaces;
using Microsoft.EntityFrameworkCore;
using ULearn.ModelView.Result;
using ULearn.Core.Managers;

namespace ULearn.Core.Manager
{
    public class VideoManager : IVideoManager
    {
        public ulearndbContext _ulearndbContext;
        private IMapper _mapper;
        private IHelperManager _helperManager;
        public VideoManager(ulearndbContext ulearndbContext, IMapper mapper,IHelperManager helperManager)
        {
            _ulearndbContext = ulearndbContext;
            _mapper = mapper;
            _helperManager = helperManager;
        }

        public VideoModel CreateVideo(VideoRequest videoRequest)
        {
            Video video = null;
            string url  = _helperManager.SaveImage(videoRequest.Base64Image, "wwwroot\\images\\Videos");
            video = _ulearndbContext.Videos.Add(new Video
            {
                Name = videoRequest.Name,
                Description = videoRequest.Description,
                Url = videoRequest.Url,
                LessonId = videoRequest.LessonId,
                Image = url
            }).Entity;

            _ulearndbContext.SaveChanges();
            return _mapper.Map<VideoModel>(video);
        }

        public VideoModel GetVideo(UserModel currentUser, int id)
        {
            var res = _ulearndbContext.Videos
                                      .Include("Lesson")
                                      .FirstOrDefault(a => a.Id == id)
                                      ?? throw new ServiceValidationException("Invalid blog id received");
            res.Image=_helperManager.GetBase64FromImagePath(res.Image);

            return _mapper.Map<VideoModel>(res);
        }

        public VideoResponse GetVideos(int page = 1,
                                     int pageSize = 10,
                                     string sortColumn = "",
                                     string sortDirection = "ascending",
                                     string searchText = "")
        {
            var queryRes = _ulearndbContext.Videos
                                           .Where(a => string.IsNullOrWhiteSpace(searchText)
                                                       || (a.Name.Contains(searchText)
                                                       || a.Description.Contains(sortColumn)));
            foreach (var a in queryRes)
            {
                a.Image = _helperManager.GetBase64FromImagePath(a.Image);
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

            var lessonsIds = res.Data
                             .Select(a => a.LessonId)
                             .Distinct()
                             .ToList();

            var lessons = _ulearndbContext.Lessons
                                        .Where(a => lessonsIds.Contains(a.Id))
                                        ;
            //.ToDictionary(a => a.Id, x => _mapper.Map<LessonResult>(x));
            foreach (var u in lessons)
            {
                u.Image = _helperManager.GetBase64FromImagePath(u.Image);
            }
            var lsns= lessons.ToDictionary(a => a.Id, x => _mapper.Map<LessonResult>(x));

            var data = new VideoResponse
            {
                Video = _mapper.Map<PagedResult<VideoModel>>(res),
                Lesson = lsns
            };

            data.Video.Sortable.Add("Title", "Title");
            data.Video.Sortable.Add("CreatedDate", "Created Date");

            return data;
        }

        public VideoModel PutVideo(UserModel currentUser, VideoRequest videoRequest)
        {
            Video video = null;

            video = _ulearndbContext.Videos
                                .FirstOrDefault(a => a.Id == videoRequest.Id)
                                ?? throw new ServiceValidationException("Invalid video id received");

            video.Name = videoRequest.Name;
            video.Description = videoRequest.Description;
            video.LessonId = videoRequest.LessonId;
            video.Url = videoRequest.Url;
            video.Image = _helperManager.SaveImage(videoRequest.Base64Image, "wwwroot\\images\\Videos");
            _ulearndbContext.SaveChanges();
            return _mapper.Map<VideoModel>(video);
        }

        public void ArchiveVideo(UserModel currentUser, int id)
        {
            if (!currentUser.IsSuperAdmin)
            {
                throw new ServiceValidationException("You don't have permission to archive video");
            }

            /* var data = _ulearndbContext.Videos
                                     .FirstOrDefault(a => a.Id == id)
                                     ?? throw new ServiceValidationException("Invalid video id received");
             data.IsArchived = true;
             _ulearndbContext.SaveChanges();*/
            var Video = _ulearndbContext.Videos.Find(id);
            if (Video == null)
            {
                throw new ArgumentException("Course not found", nameof(id));
            }
            _ulearndbContext.Videos.Remove(Video);
            _ulearndbContext.SaveChanges();
        }

        //public VideoResponse GetCourses(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "")
        //{
        //    throw new NotImplementedException();
        //}

        //public VideoModel PutCourse(UserModel currentUser, VideoRequest VideoRequest)
        //{
        //    throw new NotImplementedException();
        //}
    }
}

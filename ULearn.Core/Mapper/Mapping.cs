using AutoMapper;
using ULearn.Common.Extensions;
using ULearn.DbModel.Models.DB;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Response;
using ULearn.ModelView.Result;

namespace LMS.Core.Mapper
{
    public class Mapping : Profile
    {
        public Mapping()
        {

            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<User, LoginUserResponse>().ReverseMap();
            CreateMap<UserResult, User>().ReverseMap();

            CreateMap<CourseModel, Course>().ReverseMap();
            CreateMap<CourseResult, Course>().ReverseMap();
            CreateMap<PagedResult<CourseModel>, PagedResult<Course>>().ReverseMap();


            CreateMap<LessonModel, Lesson>().ReverseMap();
            CreateMap<LessonResult, Lesson>().ReverseMap();
            CreateMap<PagedResult<LessonModel>, PagedResult<Lesson>>().ReverseMap();


            CreateMap<VideoModel, Video>().ReverseMap();
            CreateMap<PagedResult<VideoModel>, PagedResult<Video>>().ReverseMap();
        }
    }
}

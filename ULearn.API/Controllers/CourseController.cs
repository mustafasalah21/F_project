using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ULearn.API.Attributes;
using ULearn.Core.Manager.Interfaces;
using ULearn.ModelView.Request;

namespace ULearn.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    public class CourseController : ApiBaseController
    {
        private ICourseManager _courseManager;
        private readonly ILogger<UserController> _logger;

        public CourseController(ICourseManager courseManager, ILogger<UserController> logger)
        {
            _courseManager = courseManager;
            _logger = logger;
        }

        [Route("api/v{version:apiVersion}/create")]
        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "course_create")]
        public IActionResult CreateCourse(CourseRequest courseRequest)
        {
            var result = _courseManager.CreateCourse(LoggedInUser, courseRequest);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/getAll")]
        [HttpGet]
        //[ULearnAuthorize(Permissions = "courses_all_view")]
        [MapToApiVersion("1")]
        public IActionResult GetCourses(int page = 1,
                                      int pageSize = 5,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            var result = _courseManager.GetCourses(page, pageSize, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/get/{id}")]
        [HttpGet]
        [MapToApiVersion("1")]
        [ULearnAuthorize(Permissions = "courses_all_view,course_view")]
        public IActionResult GetCourse(int id)
        {
            var result = _courseManager.GetCourse(LoggedInUser, id);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/delete/{id}")]
        [HttpDelete]
        [MapToApiVersion("1")]
        [ULearnAuthorize(Permissions = "course_delete")]
        public IActionResult ArchiveCourse(int id)
        {
            _courseManager.ArchiveCourse(LoggedInUser, id);
            return Ok();
        }

        [Route("api/v{version:apiVersion}/update")]
        [HttpPut]
        [MapToApiVersion("1")]
        [ULearnAuthorize(Permissions = "course_edit")]
        public IActionResult PutCourse(CourseRequest courseRequest)
        {
            var result = _courseManager.PutCourse(LoggedInUser, courseRequest);
            return Ok(result);
        }
    }
}

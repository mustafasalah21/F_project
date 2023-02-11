using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ULearn.API.Attributes;
using ULearn.Core.Manager;
using ULearn.ModelView.Request;

namespace ULearn.API.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    public class LessonController : ApiBaseController
    {
        private ILessonManager _LessonManager;
        private readonly ILogger<UserController> _logger;

        public LessonController(ILessonManager LessonManager, ILogger<UserController> logger)
        {
            _LessonManager = LessonManager;
            _logger = logger;
        }

        [Route("api/v{version:apiVersion}/create")]
        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "lesson_create")]
        public IActionResult CreateLesson(LessonRequest LessonRequest)
        {
            var result = _LessonManager.CreateLesson(LessonRequest);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/getAll")]
        [HttpGet]
        [ULearnAuthorize(Permissions = "lessons_all_view")]
        [MapToApiVersion("1")]
        public IActionResult GetLessons(int page = 1,
                                      int pageSize = 5,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            var result = _LessonManager.GetLessons(page, pageSize, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/get/{id}")]
        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "lessons_all_view,lesson_view")]
        public IActionResult GetLesson(int id)
        {
            var result = _LessonManager.GetLesson(LoggedInUser, id);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/delete/{id}")]
        [HttpDelete]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "lesson_delete")]
        public IActionResult ArchiveLesson(int id)
        {
            _LessonManager.ArchiveLesson(LoggedInUser, id);
            return Ok();
        }

        [Route("api/v{version:apiVersion}/update")]
        [HttpPut]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "lesson_edit")]
        public IActionResult PutLesson(LessonRequest LessonRequest)
        {
            var result = _LessonManager.PutLesson(LessonRequest);
            return Ok(result);
        }
    }
}

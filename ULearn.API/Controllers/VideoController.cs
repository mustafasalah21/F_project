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
    public class VideoController : ApiBaseController
    {
        private IVideoManager _videoManager;
        private readonly ILogger<UserController> _logger;

        public VideoController(IVideoManager videoManager, ILogger<UserController> logger)
        {
            _videoManager = videoManager;
            _logger = logger;
        }

        [Route("api/v{version:apiVersion}/create")]
        [HttpPost]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "video_create")]
        public IActionResult CreateVideo(VideoRequest videoRequest)
        {
            var result = _videoManager.CreateVideo(videoRequest);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/getAll")]
        [HttpGet]
        [ULearnAuthorize(Permissions = "videos_all_view")]
        [MapToApiVersion("1")]
        public IActionResult GetVideos(int page = 1,
                                      int pageSize = 5,
                                      string sortColumn = "",
                                      string sortDirection = "ascending",
                                      string searchText = "")
        {
            var result = _videoManager.GetVideos(page, pageSize, sortColumn, sortDirection, searchText);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/get/{id}")]
        [HttpGet]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "videos_all_view,video_view")]
        public IActionResult GetVideo(int id)
        {
            var result = _videoManager.GetVideo(LoggedInUser, id);
            return Ok(result);
        }

        [Route("api/v{version:apiVersion}/delete/{id}")]
        [HttpDelete]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "video_delete")]
        public IActionResult ArchiveVideo(int id)
        {
            _videoManager.ArchiveVideo(LoggedInUser, id);
            return Ok();
        }

        [Route("api/v{version:apiVersion}/update")]
        [HttpPut]
        [MapToApiVersion("1")]
        [Authorize]
        [ULearnAuthorize(Permissions = "video_edit")]
        public IActionResult PutVideo(VideoRequest videoRequest)
        {
            var result = _videoManager.PutVideo(LoggedInUser, videoRequest);
            return Ok(result);
        }
    }
}

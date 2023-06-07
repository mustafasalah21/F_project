using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ULearn.API.Attributes;
using ULearn.Core.Manager.Interfaces;
using ULearn.ModelView.Request;

namespace ULearn.API.Controllers
{
	public class SubscriptionController : ApiBaseController
	{
		private ISubscriptionManager _subscriptionManager;
		private readonly ILogger<UserController> _logger;

		public SubscriptionController(ISubscriptionManager subscriptionManager, ILogger<UserController> logger)
		{
			_subscriptionManager = subscriptionManager;
			_logger = logger;
		}
		[Route("api/v{version:apiVersion}/create")]
		[HttpPost]
		[MapToApiVersion("1")]
		//[Authorize]
		//[ULearnAuthorize(Permissions = "student_course_create")]
		public IActionResult CreateSubscription(StudentCourseRequest Request)
		{
			var result = _subscriptionManager.CreateCourseSubscription(Request);
			return Ok(result);
		}
		[Route("api/v{version:apiVersion}/Delete")]
		[HttpDelete]
		[MapToApiVersion("1")]
		[Authorize]
		[ULearnAuthorize(Permissions = "student_course_Delete")]
		public IActionResult DeleteSubscription(StudentCourseRequest Request)
		{
			 _subscriptionManager.DeleteSubscriptions(Request);
			return Ok();
		}

		[Route("api/v{version:apiVersion}/getAll")]
		[HttpGet]
		[ULearnAuthorize(Permissions = "student_course_all_view")]
		[MapToApiVersion("1")]
		public IActionResult GetSubscriptions(int page = 1,
									  int pageSize = 5,
									  string sortColumn = "",
									  string sortDirection = "ascending",
									  string searchText = "")
		{
			var result = _subscriptionManager.GetSubscriptions(page, pageSize, sortColumn, sortDirection, searchText);
			return Ok(result);
		}

		[Route("api/v{version:apiVersion}/getByStudentId")]
		[HttpGet]
		[ULearnAuthorize(Permissions = "student_course_all_view")]
		[MapToApiVersion("1")]
		public IActionResult GetLessons(int studentId)
		{
			var result = _subscriptionManager.GetSubscriptionsByStudent(studentId);
			return Ok(result);
		}





	}
}

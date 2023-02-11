using System.Collections.Generic;
using ULearn.Common.Extensions;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Result;

namespace ULearn.ModelView.Response
{
    public class CourseResponse
    {
        public PagedResult<CourseModel> Course { get; set; }

        public Dictionary<int, UserResult> User { get; set; }
    }
}

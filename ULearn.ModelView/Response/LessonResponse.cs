using System.Collections.Generic;
using ULearn.Common.Extensions;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Result;

namespace ULearn.ModelView.Response
{
    public class LessonResponse
    {
        public PagedResult<LessonModel> Lesson { get; set; }

        public Dictionary<int, CourseResult> Course { get; set; }

    }
}

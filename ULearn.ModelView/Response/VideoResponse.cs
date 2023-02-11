using System.Collections.Generic;
using ULearn.Common.Extensions;
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Result;

namespace ULearn.ModelView.Response
{
    public class VideoResponse
    {
        public PagedResult<VideoModel> Video { get; set; }

        public Dictionary<int, LessonResult> Lesson { get; set; }
    }
}

using System.ComponentModel;

namespace ULearn.ModelView.Request
{
    public class VideoRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DefaultValue("")]
        public string Url { get; set; }
        public int LessonId { get; set; }
    }
}

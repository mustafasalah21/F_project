using System.ComponentModel;

namespace ULearn.ModelView.ModelView
{
    public class VideoModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DefaultValue("")]
        public string Url { get; set; }
        public int LessonId { get; set; }
    }
}

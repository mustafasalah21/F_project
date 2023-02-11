using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ULearn.DbModel.Models.DB
{
    public partial class Video
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [DefaultValue("")]
        public string Url { get; set; }
        public int LessonId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }
        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual Lesson Lesson { get; set; }
    }
}

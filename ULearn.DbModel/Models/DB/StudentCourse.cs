using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ULearn.DbModel.Models.DB
{
    public partial class StudentCourse
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }
        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }
    }
}

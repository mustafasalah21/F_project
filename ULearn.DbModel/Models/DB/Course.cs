using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ULearn.DbModel.Models.DB
{
    public partial class Course
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Rate { get; set; }
        public int TeacherId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }
        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual User Teacher { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}

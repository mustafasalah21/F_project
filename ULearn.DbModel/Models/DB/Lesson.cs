using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ULearn.DbModel.Models.DB
{
    public partial class Lesson
    {
        public Lesson()
        {
            Videos = new HashSet<Video>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }
        [Timestamp]
        public DateTime CreatedDate { get; set; }
        [Timestamp]
        public DateTime UpdatedDate { get; set; }
        public bool IsArchived { get; set; }

        public virtual Course Course { get; set; }
        public virtual ICollection<Video> Videos { get; set; }
    }
}

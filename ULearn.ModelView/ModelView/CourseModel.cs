﻿namespace ULearn.ModelView.ModelView
{
    public class CourseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public int Rate { get; set; }
        public int TeacherId { get; set; }

        public byte[] CourseFile{get;set;}
    }
}

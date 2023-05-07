namespace ULearn.ModelView.Request
{
    public class CourseRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

         public byte[] CourseFile { get; set; }

    }
}

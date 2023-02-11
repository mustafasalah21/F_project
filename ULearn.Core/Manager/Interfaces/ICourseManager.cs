using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;

namespace ULearn.Core.Manager.Interfaces
{
    public interface ICourseManager
    {
        void ArchiveCourse(UserModel currentUser, int id);
        CourseModel CreateCourse(UserModel currentUser, CourseRequest courseRequest);
        CourseModel GetCourse(UserModel currentUser, int id);
        CourseResponse GetCourses(int page = 1, 
                                  int pageSize = 10, 
                                  string sortColumn = "", 
                                  string sortDirection = "ascending", 
                                  string searchText = "");
        CourseModel PutCourse(UserModel currentUser, CourseRequest courseRequest);
    }
}
using ULearn.ModelView.ModelView;
using ULearn.ModelView.Request;
using ULearn.ModelView.Response;

namespace ULearn.Core.Manager.Interfaces
{
    public interface IVideoManager
    {
        void ArchiveVideo(UserModel currentUser, int id);
        VideoModel CreateVideo(VideoRequest VideoRequest);
        VideoResponse GetVideos(int page = 1, int pageSize = 10, string sortColumn = "", string sortDirection = "ascending", string searchText = "");
        VideoModel GetVideo(UserModel currentUser, int id);
        VideoModel PutVideo(UserModel currentUser, VideoRequest VideoRequest);
    }
}
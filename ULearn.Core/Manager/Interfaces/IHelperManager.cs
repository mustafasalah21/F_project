using ULearn.Core.Manager.Interfaces;

namespace ULearn.Core.Managers
{
    public interface IHelperManager : IManager
    {
        string SaveImage(string base64img, string baseFolder);
        string Base64ToString(string base64String);
    }
}

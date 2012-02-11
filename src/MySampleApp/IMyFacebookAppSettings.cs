
namespace MySampleApp
{

    public interface IMyFacebookAppSettings
    {
        string AppId { get; }
        string AppSecret { get; }

        string CanvasUrl { get; }
        string SecureCanvasUrl { get; }

        string CanvasPageName { get; }
    }
}

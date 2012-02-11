
namespace MySampleApp
{
    public class MyFacebookAppSettings : IMyFacebookAppSettings
    {
        public string AppId { get; set; }
        public string AppSecret { get; set; }
        public string CanvasUrl { get; set; }
        public string SecureCanvasUrl { get; set; }
        public string CanvasPageName { get; set; }
    }
}

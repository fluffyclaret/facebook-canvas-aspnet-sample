
namespace MySampleApp
{
    public interface IMyFacebookCanvasContext : IMyFacebookAppSettings
    {
        MyFacebookSignedRequest SignedRequest { get; }
    }
}

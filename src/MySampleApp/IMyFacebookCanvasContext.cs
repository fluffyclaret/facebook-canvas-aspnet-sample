
namespace MySampleApp
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Facebook;

    public interface IMyFacebookCanvasContext
    {
        MyFacebookSignedRequest SignedRequest { get; }
        IMyFacebookAppSettings AppSettings { get; }
        IEnumerable<string> Permissions { get; }
        FacebookClient FacebookClient { get; }

        string GetCanvasUrl(HttpRequestBase httpRequest);

        Uri GetLoginUrl(string redirectUri, string[] scope = null, string state = null, bool isMobile = false);
    }
}

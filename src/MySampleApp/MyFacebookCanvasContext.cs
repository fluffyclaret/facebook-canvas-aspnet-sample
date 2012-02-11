
namespace MySampleApp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using Facebook;

    public class MyFacebookCanvasContext : IMyFacebookCanvasContext
    {
        public FacebookClient FacebookClient { get; set; }

        public string GetCanvasUrl(HttpRequestBase httpRequest)
        {
            if (httpRequest == null)
                throw new ArgumentNullException("httpRequest");

            var sb = new StringBuilder();
            if (httpRequest.UrlReferrer == null)
                sb.AppendFormat("{0}://apps.facebook.com/", httpRequest.Url.Scheme);
            else
                sb.AppendFormat("{0}://{1}/", httpRequest.UrlReferrer.Scheme, httpRequest.UrlReferrer.Host);

            sb.Append(AppSettings.CanvasPageName);

            return sb.ToString();
        }

        public Uri GetLoginUrl(string redirectUri, string[] scope = null, string state = null, bool isMobile = false)
        {
            if (redirectUri == null)
                throw new ArgumentNullException("redirectUri");

            var parameters = new Dictionary<string, object>();
            parameters["client_id"] = AppSettings.AppId;
            parameters["redirect_uri"] = redirectUri;

            if (scope != null && scope.Length > 0)
                parameters["scope"] = scope;
            if (!string.IsNullOrWhiteSpace(state))
                parameters["state"] = state;
            if (isMobile)
                parameters["mobile"] = true;

            return FacebookClient.GetLoginUrl(parameters);
        }

        public MyFacebookSignedRequest SignedRequest { get; set; }
        public IMyFacebookAppSettings AppSettings { get; set; }

        public IEnumerable<string> Permissions { get; set; }

    }
}

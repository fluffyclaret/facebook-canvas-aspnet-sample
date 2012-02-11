
namespace MySampleApp
{
    using System;
    using System.Collections.Generic;

    public class MyFacebookSignedRequest
    {
        private readonly object _signedRequest;
        private readonly string _accessToken;

        public MyFacebookSignedRequest(object signedRequest)
        {
            _signedRequest = signedRequest;
            if (signedRequest == null)
                throw new ArgumentNullException("signedRequest");
            var dict = signedRequest as IDictionary<string, object>;
            if (dict != null)
            {
                if (dict.ContainsKey("oauth_token"))
                    _accessToken = (string)dict["oauth_token"];
            }
        }

        public object Data
        {
            get { return _signedRequest; }
        }

        public string AccessToken
        {
            get { return _accessToken; }
        }
    }
}

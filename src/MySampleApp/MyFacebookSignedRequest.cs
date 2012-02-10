
namespace MySampleApp
{
    using System;

    public class MyFacebookSignedRequest
    {
        private readonly object _signedRequest;

        public MyFacebookSignedRequest(object signedRequest)
        {
            _signedRequest = signedRequest;
            if (signedRequest == null)
                throw new ArgumentNullException("signedRequest");
        }

        public object Data
        {
            get { return _signedRequest; }
        }
    }
}

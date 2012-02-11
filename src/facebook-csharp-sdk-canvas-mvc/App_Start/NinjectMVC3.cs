[assembly: WebActivator.PreApplicationStartMethod(typeof(facebook_csharp_sdk_canvas_mvc.App_Start.NinjectMVC3), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(facebook_csharp_sdk_canvas_mvc.App_Start.NinjectMVC3), "Stop")]

namespace facebook_csharp_sdk_canvas_mvc.App_Start
{
    using System.Collections.Generic;
    using System.Web;
    using Facebook;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using MySampleApp;
    using Ninject;
    using Ninject.Web.Mvc;

    public static class NinjectMVC3
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestModule));
            DynamicModuleUtility.RegisterModule(typeof(HttpApplicationInitializationModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            RegisterFacebookServices(kernel);
            // register other services here
        }

        private static void RegisterFacebookServices(IKernel kernel)
        {
            kernel.Bind<IMyFacebookAppSettings>()
                .ToConstant(new MyFacebookAppSettings
                                {
                                    AppId = ""
                                    AppSecret = ""
                                    CanvasPageName = ""  /* only the canvas page name and not the full url */
                                    CanvasUrl = "http://localhost:2408/",
                                    SecureCanvasUrl = "https://localhost:44300/"
                                })
                .InSingletonScope();

            kernel.Bind<IMyFacebookCanvasContext>()
                .ToMethod(
                    ctx =>
                    {
                        var fb = new FacebookClient();

                        var request = HttpContext.Current.Request;
                        if (request.UrlReferrer != null)
                        {
                            fb.IsSecureConnection = request.UrlReferrer.Scheme == "https";
                            fb.UseFacebookBeta = request.UrlReferrer.Host == "apps.beta.facebook.com";
                        }

                        var canvasContext = new MyFacebookCanvasContext
                                                {
                                                    AppSettings = ctx.Kernel.Get<IMyFacebookAppSettings>(),
                                                    FacebookClient = fb
                                                };

                        object signedRequest;
                        if (fb.TryParseSignedRequest(canvasContext.AppSettings.AppSecret, HttpContext.Current.Request["signed_request"], out signedRequest))
                        {
                            canvasContext.SignedRequest = new MyFacebookSignedRequest(signedRequest);
                            if (!string.IsNullOrWhiteSpace(canvasContext.SignedRequest.AccessToken))
                            {
                                fb.AccessToken = canvasContext.SignedRequest.AccessToken;

                                try
                                {
                                    dynamic result = fb.Get("me/permissions");
                                    canvasContext.Permissions = result.data[0].Keys as IEnumerable<string>;
                                }
                                catch (FacebookOAuthException)
                                {
                                    // log exception
                                    // oauth token expired or invalid
                                    canvasContext.SignedRequest = null;
                                    fb.AccessToken = null;
                                }
                            }
                        }

                        return canvasContext;
                    })
                .InRequestScope();

            kernel.Bind<FacebookClient>()
                .ToMethod(ctx => ctx.Kernel.Get<IMyFacebookCanvasContext>().FacebookClient)
                .InRequestScope();
        }
    }
}

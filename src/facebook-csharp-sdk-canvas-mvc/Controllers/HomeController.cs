
namespace facebook_csharp_sdk_canvas_mvc.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Facebook;
    using MySampleApp;

    public class HomeController : Controller
    {
        private readonly FacebookClient _fb;
        private readonly IMyFacebookCanvasContext _fbCanvasContext;

        private readonly string[] ExtendedPermissions = new[] { "user_about_me", "publish_stream", "read_stream" };

        public HomeController(FacebookClient fb, IMyFacebookCanvasContext fbCanvasContext)
        {
            _fb = fb;
            _fbCanvasContext = fbCanvasContext;
        }

        //
        // GET: /Home/

        public ActionResult Index()
        {
            var perms = _fbCanvasContext.Permissions;
            ViewBag.perms = perms;
            if (perms == null)
            {
                ViewBag.fbLoginUrl = _fbCanvasContext.GetLoginUrl(_fbCanvasContext.GetCanvasUrl(Request) + Request.Url.AbsolutePath, ExtendedPermissions);
            }
            else
            {
                dynamic result = _fb.Get("me", new { fields = new[] { "name", "picture" } });
                ViewBag.name = result.name;
                ViewBag.picture = result.picture;

                if (perms.Contains("read_stream"))
                {
                    var feed = _fb.Get("me/feed", new { limit = 3 });
                    ViewBag.feed = feed;
                }
                else
                {
                    ViewBag.fbLoginUrl = _fbCanvasContext.GetLoginUrl(_fbCanvasContext.GetCanvasUrl(Request) + Request.Url.AbsolutePath, ExtendedPermissions);
                }
            }

            return View();
        }
    }
}

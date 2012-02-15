# Facebook Canvas App Sample using Facebook C# SDK with ASP.NET MVC3
This sample demonstrates the use of Facebook C# SDK v6 in an ASP.NET MVC3 Facebook Canvas App.

_Note: The sample does not necessarily demonstrate the best use but rather sample on using Facebook C# SDK on the server side. It is highly recommended to use a mixture of both Facebook Javascript SDK and Facebook C# SDK for best performance rather then using your ASP.NET MVC3 app as a proxy to Facebook server._

# Getting started

Set the appropriate AppId, AppSecret and CanvasPageName before running the sample.

```csharp
new MyFacebookAppSettings
    {
        AppId = "app_id"
        AppSecret = "app_secret"
        CanvasPageName = "mycanvasappname"  /* only the canvas page name and not the full url */
        CanvasUrl = "http://localhost:2408/",
        SecureCanvasUrl = "https://localhost:44300/"
    })
```

# License
Apache 2.0 License

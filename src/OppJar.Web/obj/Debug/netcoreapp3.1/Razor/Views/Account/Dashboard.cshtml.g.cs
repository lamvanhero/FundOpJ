#pragma checksum "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "fc3512b246b64436310c7d78d1db59b1e3108841"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Account_Dashboard), @"mvc.1.0.view", @"/Views/Account/Dashboard.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\_ViewImports.cshtml"
using OppJar.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\_ViewImports.cshtml"
using OppJar.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\_ViewImports.cshtml"
using OppJar.Common.Enum;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fc3512b246b64436310c7d78d1db59b1e3108841", @"/Views/Account/Dashboard.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"660b510b08a90efe8b7102005c7200e96fb27b00", @"/Views/_ViewImports.cshtml")]
    public class Views_Account_Dashboard : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<OppJar.Web.Models.UserDetailViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
  
    ViewData["Title"] = "Children";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"app-body white\">\r\n    <div class=\"row\">\r\n        <div class=\"subdivmenu\">\r\n            <div class=\"p-t submenu\"");
            BeginWriteAttribute("style", " style=\"", 230, "\"", 238, 0);
            EndWriteAttribute();
            WriteLiteral(">\r\n                <a");
            BeginWriteAttribute("href", " href=\"", 260, "\"", 301, 1);
#nullable restore
#line 11 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
WriteAttributeValue("", 267, Url.Action("Dashboard","Account"), 267, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("><h6 style=\"color: white\">Dashboard</h6></a>\r\n                <div><h6 style=\"color:#ccc\"><a");
            BeginWriteAttribute("href", " href=\"", 394, "\"", 463, 1);
#nullable restore
#line 12 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
WriteAttributeValue("", 401, Url.Action("Settings","Account", new { id = ViewBag.UserId }), 401, 62, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Settings</a></h6></div>\r\n            </div>\r\n        </div>\r\n        <table class=\"table\">\r\n            <thead>\r\n                <tr>\r\n                    <th>\r\n                        ");
#nullable restore
#line 19 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                   Write(Html.DisplayNameFor(model => model.Avatar));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 22 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                   Write(Html.DisplayNameFor(model => model.DisplayName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th>\r\n                        ");
#nullable restore
#line 25 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                   Write(Html.DisplayNameFor(model => model.Slug));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </th>\r\n                    <th></th>\r\n                </tr>\r\n            </thead>\r\n            <tbody>\r\n");
#nullable restore
#line 31 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 1219, "\"", 1237, 1);
#nullable restore
#line 35 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
WriteAttributeValue("", 1225, item.Avatar, 1225, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1238, "\"", 1261, 1);
#nullable restore
#line 35 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
WriteAttributeValue("", 1244, item.DisplayName, 1244, 17, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" width=\"50\" />\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 38 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                       Write(Html.DisplayFor(modelItem => item.DisplayName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 41 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Slug));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
#nullable restore
#line 44 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                       Write(Html.ActionLink("Edit", "Settings", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" |\r\n                            <a");
            BeginWriteAttribute("href", " href=\"", 1728, "\"", 1751, 2);
            WriteAttributeValue("", 1735, "/user/", 1735, 6, true);
#nullable restore
#line 45 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
WriteAttributeValue("", 1741, item.Slug, 1741, 10, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Memories</a> |\r\n                            ");
#nullable restore
#line 46 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                       Write(Html.ActionLink("Public page", "PublicPage", new { slug = item.Slug }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 49 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Views\Account\Dashboard.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</div>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<OppJar.Web.Models.UserDetailViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "88a30562c46404fd7bff56a74e6ed80c7aca9a7c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admins_Views_Shared__MainHeader), @"mvc.1.0.view", @"/Areas/Admins/Views/Shared/_MainHeader.cshtml")]
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
#line 1 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\_ViewImports.cshtml"
using OppJar.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\_ViewImports.cshtml"
using OppJar.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\_ViewImports.cshtml"
using DataTables.AspNetCore.Mvc;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\_ViewImports.cshtml"
using OppJar.Web.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"88a30562c46404fd7bff56a74e6ed80c7aca9a7c", @"/Areas/Admins/Views/Shared/_MainHeader.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a45afae8fc67f1670e9a7bf966633e0ba9e3098", @"/Areas/Admins/_ViewImports.cshtml")]
    public class Areas_Admins_Views_Shared__MainHeader : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-item p-y-sm"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/Account/SignOut"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("navbar-right"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<div class=""app-header box-shadow"" style=""background-color:#4f87b8"">
    <div class=""navbar"">
        <a class=""navbar-brand"" href=""/"">
            <img src=""/assets/images/oppjar-logo.PNG"" class=""m-t-sm oppjar-logo"">
        </a>
        <!-- navbar right -->
        <ul class=""nav navbar-nav pull-right"">
");
#nullable restore
#line 8 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
             if (User.Identity.IsAuthenticated)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                <li class=""nav-item dropdown"">
                    <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" aria-expanded=""false"" style=""color:white"">

                        <span class=""hidden-md-down nav-text m-r-sm text-right text-white"">
                            <span>");
#nullable restore
#line 14 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
                             Write(ViewBag.DisplayName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </span>\r\n                        <span class=\"avatar w-30 white\">\r\n                            <img");
            BeginWriteAttribute("src", " src=\"", 831, "\"", 855, 1);
#nullable restore
#line 17 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
WriteAttributeValue("", 837, ViewBag.AvatarUrl, 837, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 856, "\"", 862, 0);
            EndWriteAttribute();
            WriteLiteral(" />\r\n                        </span>\r\n                    </a>\r\n                    <div class=\"dropdown-menu pull-right dropdown-menu-scale\" style=\"border-bottom-left-radius: 10px; border-bottom-right-radius: 10px\">\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "88a30562c46404fd7bff56a74e6ed80c7aca9a7c6925", async() => {
                WriteLiteral("\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "88a30562c46404fd7bff56a74e6ed80c7aca9a7c7211", async() => {
                    WriteLiteral("Sign Out");
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                    </div>\r\n                </li>\r\n");
#nullable restore
#line 26 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <li class=\"nav-item\">\r\n                    <a class=\"nav-link\"");
            BeginWriteAttribute("href", " href=\"", 1443, "\"", 1481, 1);
#nullable restore
#line 30 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
WriteAttributeValue("", 1450, Url.Action("SignIn","Account"), 1450, 31, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <span class=""btn rounded btn-outline text-sm _600"" style=""border:1px solid white; color:white; background-color:#4f87b8"">
                            Login
                        </span>
                    </a>
                </li>
                <li class=""nav-item"">
                    <a class=""nav-link""");
            BeginWriteAttribute("href", " href=\"", 1827, "\"", 1867, 1);
#nullable restore
#line 37 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
WriteAttributeValue("", 1834, Url.Action("Register","Account"), 1834, 33, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <span class=""btn rounded btn-outline text-sm _600"" style=""border:1px solid white; color:white; background-color:#4f87b8"">
                            Sign Up
                        </span>
                    </a>
                </li>
");
#nullable restore
#line 43 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_MainHeader.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </ul>\r\n        <!-- navbar collapse -->\r\n        <div class=\"collapse navbar-toggleable-sm\" id=\"navbar-1\">\r\n        </div>\r\n        <!-- / navbar collapse -->\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
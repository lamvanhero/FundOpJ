#pragma checksum "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_Loader.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "db9323dae69ce6a10a96ded501159752f3ab7e49"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admins_Views_Shared__Loader), @"mvc.1.0.view", @"/Areas/Admins/Views/Shared/_Loader.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"db9323dae69ce6a10a96ded501159752f3ab7e49", @"/Areas/Admins/Views/Shared/_Loader.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8a45afae8fc67f1670e9a7bf966633e0ba9e3098", @"/Areas/Admins/_ViewImports.cshtml")]
    public class Areas_Admins_Views_Shared__Loader : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<style>
     #preloader {
            display: none;
            position: fixed;
            left: 0;
            top: 0;
            z-index: 9999999;
            width: 100%;
            height: 100%;
            overflow: visible;
            background: rgba(51, 51, 51, 0.35) url('");
#nullable restore
#line 11 "D:\ProjectSS\OppJar\oppjar\src\OppJar.Web\Areas\Admins\Views\Shared\_Loader.cshtml"
                                               Write(Url.Content("~/assets/images/ajax-loading.gif"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\') no-repeat center center;\r\n        }\r\n</style>\r\n<div id=\"preloader\"></div>");
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

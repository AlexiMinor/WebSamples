﻿#pragma checksum "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\Details2.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "7ca1266d7cdb24ba814016895454eac8aa707de363f17dfa9c60dbc58604a14a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Articles_Details2), @"mvc.1.0.view", @"/Views/Articles/Details2.cshtml")]
namespace AspNetCoreGeneratedDocument
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line (1,2)-(1,18) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\_ViewImports.cshtml"
using WebApp.MVC

#nullable disable
    ;
#nullable restore
#line (2,2)-(2,25) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\_ViewImports.cshtml"
using WebApp.MVC.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("Identifier", "/Views/Articles/Details2.cshtml")]
    [global::System.Runtime.CompilerServices.CreateNewOnMetadataUpdateAttribute]
    #nullable restore
    internal sealed class Views_Articles_Details2 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line (1,8)-(1,20) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\Details2.cshtml"
ArticleModel

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line (3,3)-(6,1) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\Details2.cshtml"

    ViewBag.Title = "Article";
    Layout = "_Layout";

#line default
#line hidden
#nullable disable

            WriteLiteral("\r\n<div>\r\n    ");
            Write(
#nullable restore
#line (9,6)-(9,41) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\Details2.cshtml"
Html.LabelFor(model => model.Title)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n    ");
            Write(
#nullable restore
#line (10,6)-(10,43) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\Details2.cshtml"
Html.DisplayFor(model => model.Title)

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n");
            WriteLiteral("</div>");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ArticleModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

﻿#pragma checksum "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "a686e88d1e8bb76959067073c4a7aa37020dd1f9d6daab4a915e55b75a13803a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCoreGeneratedDocument.Views_Articles_ArticlePreview), @"mvc.1.0.view", @"/Views/Articles/ArticlePreview.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemMetadataAttribute("Identifier", "/Views/Articles/ArticlePreview.cshtml")]
    [global::System.Runtime.CompilerServices.CreateNewOnMetadataUpdateAttribute]
    #nullable restore
    internal sealed class Views_Articles_ArticlePreview : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<
#nullable restore
#line (1,8)-(1,45) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml"
WebApp.Mvc.Models.Models.ArticleModel

#line default
#line hidden
#nullable disable
    >
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Articles", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n\r\n<div class=\"card col-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 col-xxl-3 mt-3 mb-3 m-5 article-preview-card article-preview\" style=\"background-color:white !important;\">\r\n    <img");
            BeginWriteAttribute("src", " src=\"", 226, "\"", 247, 1);
            WriteAttributeValue("", 232, 
#nullable restore
#line (5,16)-(5,30) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml"
Model.ImageUrl

#line default
#line hidden
#nullable disable
            , 232, 15, false);
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\" alt=\"Article picture\">\r\n    <div class=\"card-body\">\r\n        <h5 class=\"card-title\">");
            Write(
#nullable restore
#line (7,33)-(7,44) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml"
Model.Title

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("</h5>\r\n        <p class=\"card-text\">Model.Description</p>\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "a686e88d1e8bb76959067073c4a7aa37020dd1f9d6daab4a915e55b75a13803a5502", async() => {
                WriteLiteral("Read details");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
            WriteLiteral(
#nullable restore
#line (9,74)-(9,82) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml"
Model.Id

#line default
#line hidden
#nullable disable
            );
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n    <div class=\"card-footer text-muted\">\r\n        ");
            Write(
#nullable restore
#line (12,10)-(12,28) "C:\Users\AlexiMinor\Desktop\ItAcademy-samples\WebSamples\WebApp.MVC\Views\Articles\ArticlePreview.cshtml"
Model.CreationDate

#line default
#line hidden
#nullable disable
            );
            WriteLiteral("\r\n    </div>\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebApp.Mvc.Models.Models.ArticleModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

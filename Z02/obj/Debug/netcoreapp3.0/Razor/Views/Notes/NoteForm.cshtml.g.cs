#pragma checksum "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "08dd4bbe6609a4cdf67736c11e05b78a7e2d88be"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Notes_NoteForm), @"mvc.1.0.view", @"/Views/Notes/NoteForm.cshtml")]
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
#line 1 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/_ViewImports.cshtml"
using Z02;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/_ViewImports.cshtml"
using Z02.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"08dd4bbe6609a4cdf67736c11e05b78a7e2d88be", @"/Views/Notes/NoteForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"34b78150f76aecaa90e94e9f349d622f009b3cbd", @"/Views/_ViewImports.cshtml")]
    public class Views_Notes_NoteForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Z02.Models.Note>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control col-sm-6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rows", new global::Microsoft.AspNetCore.Html.HtmlString("8"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("textarea"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline col-lg-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"form-group\">\n    <label class=\" control-label\" for=\"titleInput\">Title of the note:</label>\n    ");
#nullable restore
#line 5 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.EditorFor(model => model.title, new { htmlAttributes = new { @class="form-control", @id="titleInput"} }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    ");
#nullable restore
#line 6 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.ValidationMessageFor(model => model.title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</div>\n<div class=\"form-group\">\n    <div class=\"col-sm-12 checkbox\">\n            ");
#nullable restore
#line 10 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
       Write(Html.EditorFor(model => model.markdown, new { htmlAttributes = new {@id="markdown"}}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n            <label class=\"control-label\">\n                Markdown\n            </label>\n    </div>\n</div>\n\n<div class=\"form-group\">\n    <label class=\"control-label\" for=\"textarea\">Content of the note: </label>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08dd4bbe6609a4cdf67736c11e05b78a7e2d88be5919", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 19 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.content);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n</div>\n\n<div class=\"form-group\">\n    ");
#nullable restore
#line 23 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.LabelFor(model => model.date, htmlAttributes: new { @class = "control-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <div>\n        ");
#nullable restore
#line 25 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
   Write(Html.EditorFor(model => model.date, new { htmlAttributes = new { @class = "form-control" } }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
    </div>
</div>

<div class=""container"" style=""padding:0"">
    <div class=""row"">
        <div class=""col-lg-2"">
            <label for=""category-list"">In categories:</label>
        </div>
        <div id=""category-list"" class=""col-lg-5"" style=""height:150px;max-width:300px;overflow:auto;margin:20px 0px;"">
            <table class=""table"">
                <tbody>
");
#nullable restore
#line 37 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                     for (int i = 0; i < Model.categories.Count(); ++i) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <th>\n                                ");
#nullable restore
#line 40 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                           Write(Html.TextBoxFor(item => Model.categories[i], new { @class = "form-control", @readonly = "readonly" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                            </th>\n                        </tr>\n");
#nullable restore
#line 43 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "08dd4bbe6609a4cdf67736c11e05b78a7e2d88be9590", async() => {
                WriteLiteral(@"
            <div class=""form-group"">
                <label for=""category-input"">Category name:</label>
                <input type=""text"" id=""category-input"" name=""category""/>
                <input type=""submit"" class=""btn btn-default"" name=""btnSubmit"" value=""Add""/>
                <input type=""submit"" class=""btn btn-default"" name=""btnSubmit"" value=""Remove""/>
            </div>
        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n    </div>\n</div>\n\n<input class=\"btn btn-primary\" type=\"submit\" value=\"OK\"/>\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Z02.Models.Note> Html { get; private set; }
    }
}
#pragma warning restore 1591

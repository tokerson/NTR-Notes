#pragma checksum "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02f0bd3572cdb34160dca49a6e7c4c7ed39dcd80"
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
using Z02.Model;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02f0bd3572cdb34160dca49a6e7c4c7ed39dcd80", @"/Views/Notes/NoteForm.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"72fe3f27f693228d1b756fa3a1feef8cb26b8fd6", @"/Views/_ViewImports.cshtml")]
    public class Views_Notes_NoteForm : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Z02.Model.Note>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control col-sm-6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rows", new global::Microsoft.AspNetCore.Html.HtmlString("8"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("textarea"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-inline col-lg-5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n<div class=\"form-group\">\n    <label class=\" control-label\" for=\"titleInput\">Title of the note:</label>\n    ");
#nullable restore
#line 5 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.EditorFor(model => model.Title, new { htmlAttributes = new { @class="form-control", @id="titleInput"} }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    ");
#nullable restore
#line 6 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.ValidationMessageFor(model => model.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</div>\n");
#nullable restore
#line 8 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.HiddenFor(m => m.NoteCategories));

#line default
#line hidden
#nullable disable
            WriteLiteral(";\n<div class=\"form-group\">\n    <label class=\"control-label\" for=\"textarea\">Content of the note: </label>\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("textarea", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02f0bd3572cdb34160dca49a6e7c4c7ed39dcd806090", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.TextAreaTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 11 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_TextAreaTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Description);

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
#line 15 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
Write(Html.LabelFor(model => model.NoteDate, htmlAttributes: new { @class = "control-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n    <div>\n        ");
#nullable restore
#line 17 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
   Write(Html.EditorFor(model => model.NoteDate, new { htmlAttributes = new { @class = "form-control" } }));

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
#line 29 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                      var NoteCategories = Model.NoteCategories.ToList().Select(nc => nc.Category.Title);

#line default
#line hidden
#nullable disable
#nullable restore
#line 30 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                     for (int i = 0; i < Model.NoteCategories.Count; ++i) {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\n                            <th>\n                                <input type=\"hidden\"");
            BeginWriteAttribute("name", " name=\"", 1453, "\"", 1474, 3);
            WriteAttributeValue("", 1460, "categories[", 1460, 11, true);
#nullable restore
#line 33 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
WriteAttributeValue("", 1471, i, 1471, 2, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1473, "]", 1473, 1, true);
            EndWriteAttribute();
            BeginWriteAttribute("value", " value=\"", 1475, "\"", 1531, 1);
#nullable restore
#line 33 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
WriteAttributeValue("", 1483, Model.NoteCategories.ToList()[i].Category.Title, 1483, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\n                                ");
#nullable restore
#line 34 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                           Write(Html.TextBoxFor(item => Model.NoteCategories.ToList()[i].Category.Title, new { @class = "form-control", @readonly = "readonly" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n                            </th>\n                        </tr>\n");
#nullable restore
#line 37 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </tbody>\n            </table>\n        </div>\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "02f0bd3572cdb34160dca49a6e7c4c7ed39dcd8010939", async() => {
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
            WriteLiteral("\n    </div>\n</div>\n\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "02f0bd3572cdb34160dca49a6e7c4c7ed39dcd8012672", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
#nullable restore
#line 52 "/home/tokarz/Documents/Studia/NTR-Notes/Z02/Views/Notes/NoteForm.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.RowVersion);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n<input class=\"btn btn-primary\" type=\"submit\" value=\"OK\"/>\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Z02.Model.Note> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "3adc601426f9a2ee28ef1b1e93bf6f620da90ca8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Index), @"mvc.1.0.view", @"/Views/Home/Index.cshtml")]
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
#line 1 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\_ViewImports.cshtml"
using CadastroCurriculo;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\_ViewImports.cshtml"
using CadastroCurriculo.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\_ViewImports.cshtml"
using CadastroCurriculo.Enums;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"3adc601426f9a2ee28ef1b1e93bf6f620da90ca8", @"/Views/Home/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fd0086641ac32e5e424cd2299cfb7c7ecd2a7428", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<CurriculoViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("cb-area"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "Id_Area", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Pesquisa", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";
    ViewData["Title"] = "Início";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
 if (ViewBag.Mensagem != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <script>\r\n        alert(\"");
#nullable restore
#line 10 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
          Write(ViewBag.Mensagem);

#line default
#line hidden
#nullable disable
            WriteLiteral("\");\r\n    </script>\r\n");
#nullable restore
#line 12 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"

}

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

<div>
    <div class=""col-lg-12"" id=""div-btn-criar"">
        <a id=""btn-criar"" class=""btn btn-success btn-redondo"" href=""/Home/Create?"">Criar Currículo</a>
    </div>
</div>


<div class=""container container-pesquisa"">
    <h3 id=""titulo-home"">Currículos cadastrados</h3>
    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3adc601426f9a2ee28ef1b1e93bf6f620da90ca86287", async() => {
                WriteLiteral("\r\n        <div class=\"row\">\r\n            <div class=\"col-lg-6\">\r\n                <label for=\"Nome\">Nome</label>\r\n                <input type=\"text\" name=\"Nome\" class=\"form-control\"");
                BeginWriteAttribute("value", " value=\"", 727, "\"", 793, 1);
#nullable restore
#line 29 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 735, !string.IsNullOrEmpty(ViewBag.Nome) ? ViewBag.Nome : "", 735, 58, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n\r\n\r\n            <div class=\"col-lg-6\">\r\n                <label for=\"Area\">Categoria</label>\r\n                <div id=\"div-btn-pesquisar\">\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("select", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3adc601426f9a2ee28ef1b1e93bf6f620da90ca87382", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.SelectTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Name = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 36 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items = ViewBag.Areas;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-items", __Microsoft_AspNetCore_Mvc_TagHelpers_SelectTagHelper.Items, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    <input id=\"btn-pesquisar\" type=\"submit\" class=\"btn btn-primary\" value=\"Pesquisar\">\r\n                </div>\r\n            </div>\r\n\r\n        </div>\r\n    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n\r\n<div class=\"container-fluid\">\r\n    <div class=\"row\">\r\n");
#nullable restore
#line 47 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
         if (Model?.Count == 0)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"container text-center alert alert-info\" role=\"alert\">\r\n                <p>Nenhum currículo encontrado.</p>\r\n            </div>\r\n");
#nullable restore
#line 52 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
        }
        else
        {
            

#line default
#line hidden
#nullable disable
#nullable restore
#line 55 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"col-md-4 mb-4\">\r\n                    <div class=\"card box-shadow\">\r\n                        <div class=\"card-body\">\r\n                            <img");
            BeginWriteAttribute("id", " id=\"", 1780, "\"", 1785, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"foto-perfil mb-2 mx-2\"");
            BeginWriteAttribute("src", " src=\"", 1816, "\"", 1860, 1);
#nullable restore
#line 60 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 1822, $"/lib/img/icon{item.Id_Icone}.png", 1822, 38, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("alt", " alt=\"", 1861, "\"", 1901, 1);
#nullable restore
#line 60 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 1867, $"Ícone do usuário {item.Nome}", 1867, 34, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                            <h5 class=\"card-title\">");
#nullable restore
#line 61 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
                                              Write(item.Nome);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                            <h6 class=\"card-subtitle mb-3\">CPF: ");
#nullable restore
#line 62 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
                                                           Write(item.CPF);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                            <p class=\"card-text\">");
#nullable restore
#line 63 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
                                            Write(item.Cargo_Pretendido);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                            <p class=\"card-text text-secondary\">");
#nullable restore
#line 64 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
                                                           Write(item.Area.Desc_Area);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        </div>\r\n                        <div class=\"card-buttons\">\r\n                            <a class=\"btn btn-outline-primary btn-redondo\"");
            BeginWriteAttribute("href", " href=\"", 2378, "\"", 2419, 2);
            WriteAttributeValue("", 2385, "/Home/Editar?id=", 2385, 16, true);
#nullable restore
#line 67 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 2401, item.Id_Curriculo, 2401, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Editar</a>\r\n                            <a class=\"btn btn-outline-danger btn-redondo\"");
            BeginWriteAttribute("href", " href=\"", 2506, "\"", 2559, 3);
            WriteAttributeValue("", 2513, "javascript:apagarCurriculo(", 2513, 27, true);
#nullable restore
#line 68 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 2540, item.Id_Curriculo, 2540, 18, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 2558, ")", 2558, 1, true);
            EndWriteAttribute();
            WriteLiteral(">Deletar</a>\r\n                        </div>\r\n                        <div class=\"card-buttons\">\r\n                            <a class=\"btn btn-outline-success btn-redondo\"");
            BeginWriteAttribute("href", " href=\"", 2732, "\"", 2776, 2);
            WriteAttributeValue("", 2739, "/Home/Impressao?id=", 2739, 19, true);
#nullable restore
#line 71 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
WriteAttributeValue("", 2758, item.Id_Curriculo, 2758, 18, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Visualizar</a>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n");
#nullable restore
#line 75 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
            }

#line default
#line hidden
#nullable disable
#nullable restore
#line 75 "C:\Users\HP\Downloads\CadastroCurriculo\CadastroCurriculo\CadastroCurriculo\Views\Home\Index.cshtml"
             
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n<script>\r\n    function apagarCurriculo(id) {\r\n        if (confirm(\'Deseja mesmo excluir esse currículo?\'))\r\n            location.href = \'/Home/Deletar?Id_Curriculo=\' + id;\r\n    }\r\n</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<CurriculoViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591

#pragma checksum "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "f0643235b9674ad3fde7d74db4418b976e313178d4225bae48f471da60ea487f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_Cadastro), @"mvc.1.0.view", @"/Views/Usuario/Cadastro.cshtml")]
namespace AspNetCore
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
#line 1 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\_ViewImports.cshtml"
using PBL_EC5

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\_ViewImports.cshtml"
using PBL_EC5.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"f0643235b9674ad3fde7d74db4418b976e313178d4225bae48f471da60ea487f", @"/Views/Usuario/Cadastro.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"6a99188d751bb4ea9743e1a2f342bb6f5dd47110bdeb19570b8158773beae304", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Usuario_Cadastro : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<UsuarioViewModel>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form p-4"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("action", new global::Microsoft.AspNetCore.Html.HtmlString("Salvar"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable

            WriteLiteral(@"<style>
    .form {
        border: 1px solid #00a1a1;
        border-radius: 15px;
    }

    .form-group {
        display: flex;
        justify-content: space-between; /* labels à esquerda e inputs à direita */
        align-items: center;
        margin-bottom: 12px; /* espaçamento vertical entre grupos */
    }

        .form-group label {
            width: 35%; /* largura das labels */
            font-weight: bold;
            padding-right: 10px; /* espaço à direita para separar visualmente */
            text-align: right; /* alinha labels à direita para estética */
        }

        .form-group input {
            width: 65%; /* largura dos inputs */
            padding: 6px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }

    .btn-submit {
        width: 100px;
        margin-left: auto; /* botão alinhado à direita */
        padding: 8px;
        border: none;
        border-radius: 5px;
       ");
            WriteLiteral(" background-color: #00a1a1;\r\n        color: white;\r\n        cursor: pointer;\r\n    }\r\n\r\n        .btn-submit:hover {\r\n            background-color: #008181;\r\n        }\r\n\r\n</style>\r\n\r\n<div class=\"text-start\">\r\n\r\n    <h2>Cadastro de Usuário</h2>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f0643235b9674ad3fde7d74db4418b976e313178d4225bae48f471da60ea487f5902", async() => {
                WriteLiteral("\r\n        <div class=\"form-group\">\r\n            <label for=\"Id\" class=\"control-label\">Id</label>\r\n");
#nullable restore
#line 56 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
             if (Model.Id == 0)
            {

#line default
#line hidden
#nullable disable

                WriteLiteral("                <input type=\"text\" Name=\"Id\"");
                BeginWriteAttribute("value", " value=\"", 1593, "\"", 1601, 0);
                EndWriteAttribute();
                WriteLiteral(" hidden disabled />\r\n");
#nullable restore
#line 59 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable

                WriteLiteral("                <input type=\"text\" Name=\"Id\"");
                BeginWriteAttribute("value", " value=\"", 1715, "\"", 1732, 1);
                WriteAttributeValue("", 1723, 
#nullable restore
#line 62 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                     Model.Id

#line default
#line hidden
#nullable disable
                , 1723, 9, false);
                EndWriteAttribute();
                WriteLiteral(" hidden />\r\n");
#nullable restore
#line 63 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
            }

#line default
#line hidden
#nullable disable

                WriteLiteral("        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Nome\" class=\"control-label\">Nome</label>\r\n            <input type=\"text\" Name=\"Nome\"");
                BeginWriteAttribute("value", " value=\"", 1920, "\"", 1939, 1);
                WriteAttributeValue("", 1928, 
#nullable restore
#line 68 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                   Model.Nome

#line default
#line hidden
#nullable disable
                , 1928, 11, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Telefone\" class=\"control-label\">Telefone</label>\r\n            <input type=\"text\" Name=\"Telefone\"");
                BeginWriteAttribute("value", " value=\"", 2126, "\"", 2149, 1);
                WriteAttributeValue("", 2134, 
#nullable restore
#line 73 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                       Model.Telefone

#line default
#line hidden
#nullable disable
                , 2134, 15, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Cpf\" class=\"control-label\">CPF</label>\r\n            <input type=\"text\" Name=\"Cpf\"");
                BeginWriteAttribute("value", " value=\"", 2321, "\"", 2339, 1);
                WriteAttributeValue("", 2329, 
#nullable restore
#line 78 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                  Model.Cpf

#line default
#line hidden
#nullable disable
                , 2329, 10, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Cep\" class=\"control-label\">Cep</label>\r\n            <input type=\"text\" Name=\"Cep\"");
                BeginWriteAttribute("value", " value=\"", 2511, "\"", 2529, 1);
                WriteAttributeValue("", 2519, 
#nullable restore
#line 83 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                  Model.Cep

#line default
#line hidden
#nullable disable
                , 2519, 10, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"DataNascimento\" class=\"control-label\">Data Nascimento</label>\r\n            <input type=\"date\"  Name=\"DataNascimento\"");
                BeginWriteAttribute("value", " value=\"", 2736, "\"", 2765, 1);
                WriteAttributeValue("", 2744, 
#nullable restore
#line 88 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                              Model.DataNascimento

#line default
#line hidden
#nullable disable
                , 2744, 21, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"Email\" class=\"control-label\">Email</label>\r\n            <input type=\"text\"  Name=\"Email\"");
                BeginWriteAttribute("value", " value=\"", 2944, "\"", 2964, 1);
                WriteAttributeValue("", 2952, 
#nullable restore
#line 93 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                     Model.Email

#line default
#line hidden
#nullable disable
                , 2952, 12, false);
                EndWriteAttribute();
                WriteLiteral(" required />\r\n        </div>\r\n\r\n        <div class=\"form-group\">\r\n            <label for=\"SenhaHash\" class=\"control-label\">Senha</label>\r\n            <input type=\"password\" \" Name=\"SenhaHash\" value=\"");
                Write(
#nullable restore
#line 98 "C:\Users\Guzac\source\repos\PBL_EC5\PBL_EC5\Views\Usuario\Cadastro.cshtml"
                                                              Model.SenhaHash

#line default
#line hidden
#nullable disable
                );
                WriteLiteral(@""" required />
        </div>

        <div class=""d-flex justify-content-center align-items-center mt-5"">
            <a href=""/Usuario/Index"">Cancelar</a>
            <label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</label>
            <input class=""btn btn-success"" type=""submit"" value=""Salvar"" />
        </div>
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<UsuarioViewModel> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

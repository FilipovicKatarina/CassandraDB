#pragma checksum "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef1a9a1e943a2106480c66e6ea02dc2f9b577fe4"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Preporuka_prikaziPreporukeKncerata), @"mvc.1.0.view", @"/Views/Preporuka/prikaziPreporukeKncerata.cshtml")]
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
#line 1 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\_ViewImports.cshtml"
using CassandraProj;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\_ViewImports.cshtml"
using CassandraProj.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef1a9a1e943a2106480c66e6ea02dc2f9b577fe4", @"/Views/Preporuka/prikaziPreporukeKncerata.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"69dc6f3c7880ed9b3509a61a2b0599816dba696c", @"/Views/_ViewImports.cshtml")]
    public class Views_Preporuka_prikaziPreporukeKncerata : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CassandraProj.Models.Koncert>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
  
    ViewData["Title"] = "prikaziPreporukeKncerata";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1 style=\"width:650px; background-color:palevioletred\">Preporuke koncerata</h1>\r\n<br />\r\n<br />\r\n\r\n<table border=\"2\" style=\"background-color:lightgreen;text-align:center\">\r\n\r\n");
#nullable restore
#line 13 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
     if (Model != null)
    {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"        <thead>
            <tr>
                <th width=""200"">Koncert ID</th>
                <th width=""200"">Ime</th>
                <th width=""200"">Organizator</th>
                <th width=""200"">Sponzor</th>
                <th width=""200"">Opis</th>
                <th width=""200"">Tip</th>

            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 28 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>");
#nullable restore
#line 31 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.KoncertID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 32 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.Ime);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 33 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.Organizator);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 34 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.Sponzor);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 35 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.Opis);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 36 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
                   Write(item.Tip);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n\r\n                </tr>\r\n");
#nullable restore
#line 39 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n");
#nullable restore
#line 41 "C:\Users\User\source\repos\CassandraProj13\CassandraProj\Views\Preporuka\prikaziPreporukeKncerata.cshtml"

    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n</table>\r\n<br />\r\n<br />\r\n<br />\r\n\r\n<div>\r\n    <button>\r\n        <a href=\"../Koncert/PretragaKoncerataPoTipu\" class=\"this-page-style\">Vrati se na prethodnu stranu</a>\r\n    </button>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CassandraProj.Models.Koncert>> Html { get; private set; }
    }
}
#pragma warning restore 1591
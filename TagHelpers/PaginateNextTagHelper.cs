using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("paginate-next", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PaginateNextTagHelper:TagHelper
    {
        [HtmlAttributeName("url")]
        public string Url { get; set; }
        [HtmlAttributeName("target")]
        public string Target { get; set; }

        [HtmlAttributeName("disabled")]
        public bool Disabled { get; set; } = false;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("hx-get", Url);
            output.Attributes.SetAttribute("hx-target", Target);
            output.Attributes.SetAttribute("type", "button");
            if (!Disabled)
            {
                output.Attributes.SetAttribute("disabled", null);
            }
            output.Attributes.SetAttribute("class", "flex min-h-[38px] min-w-[38px] items-center justify-center rounded-lg px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 disabled:opacity-50 disabled:pointer-events-none dark:text-white dark:hover:bg-white/10 dark:focus:bg-white/10");
            var content = @"<span class=""sr-only"">Next</span>
                            <svg class=""shrink-0 size-3.5"" xmlns=""http://www.w3.org/2000/svg"" width=""24"" height=""24"" viewBox=""0 0 24 24"" fill=""none"" stroke=""currentColor"" stroke-width=""2"" stroke-linecap=""round"" stroke-linejoin=""round"">
                                <path d=""m9 18 6-6-6-6""></path>
                            </svg>";
            output.Content.SetHtmlContent(content);

        }
    }
}

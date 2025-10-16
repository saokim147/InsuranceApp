using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("paginate-button")]
    public class PaginateButtonTagHelper : TagHelper
    {
        [HtmlAttributeName("page")]
        public int PageNumber { get; set; }
        [HtmlAttributeName("is-selected")]
        public bool IsSelected { get; set; } = false;
        [HtmlAttributeName("target")]
        public string Target { get; set; } = string.Empty;
        [HtmlAttributeName("url")]
        public string Url { get; set; } = string.Empty;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var activeClass = IsSelected ? "bg-red-100 dark:bg-neutral-800" : string.Empty;
            output.TagName = "button";
            output.Attributes.SetAttribute("type", "button");
            var classes = $"{activeClass} flex min-h-[38px] min-w-[38px] items-center justify-center rounded-lg px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 disabled:opacity-50 disabled:pointer-events-none dark:text-white dark:hover:bg-white/10 dark:focus:bg-white/10";
            output.Attributes.SetAttribute("class", classes);
            output.Attributes.SetAttribute("hx-get", Url);
            output.Attributes.SetAttribute("hx-target", $"{Target}");

            output.Content.SetHtmlContent($"{PageNumber}");

        }

    }
}

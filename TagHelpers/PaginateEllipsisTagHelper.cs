using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("paginate-ellipsis", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class PaginateEllipsisTagHelper:TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            var classes = "hs-tooltip-toggle group flex min-h-[38px] min-w-[38px] items-center justify-center rounded-lg px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 disabled:opacity-50 disabled:pointer-events-none dark:text-white dark:hover:bg-white/10 dark:focus:bg-white/10";
            output.Attributes.SetAttribute("type", "button");
            output.Attributes.SetAttribute("class", classes);
            var content = @"
                <span class='group-hover:hidden text-[10px]'>...</span>
                <svg class='group-hover:block shrink-0 hidden size-5' xmlns='http://www.w3.org/2000/svg' width='24'
                    height='24' viewBox='0 0 24 24' fill='none' stroke='currentColor' stroke-width='2'
                    stroke-linecap='round' stroke-linejoin='round'>
                    <path d='m6 17 5-5-5-5'></path>
                    <path d='m13 17 5-5-5-5'></path>
                </svg>
            ";
            output.Content.SetHtmlContent(content);
        }
    }
}

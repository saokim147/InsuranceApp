using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("paginate-list")]
    public class PaginateListTagHelper:TagHelper
    {
        [HtmlAttributeName("start")]
        public int Start { get; set; }
        [HtmlAttributeName("end")]
        public int End { get; set; }

        [HtmlAttributeName("target")]
        public string Target { get; set; } = string.Empty;

        private string PaginateButton(int number,int currentPage,string url)
        {
            var active = number == currentPage ? "bg-red-100 dark:bg-neutral-800" : " ";
            return $@"<button type='button' class='{active} flex min-h-[38px] min-w-[38px] items-center justify-center rounded-lg px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 focus:outline-none focus:bg-gray-100 disabled:opacity-50 disabled:pointer-events-none dark:text-white dark:hover:bg-white/10 dark:focus:bg-white/10'
                            hx-get='{url}'
                            hx-target='#hospital-table'>{number}
                  </button>";
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "ul";
            output.Attributes.SetAttribute("class", "pagination");
            var stringBuilder= new StringBuilder();
            for (int i = Start; i <= End; i++)
            {
                stringBuilder.Append(PaginateButton(i, 1, Target));
            }
            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}

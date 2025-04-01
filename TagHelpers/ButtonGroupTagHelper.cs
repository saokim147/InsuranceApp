using System.Text;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    public class ButtonGroupTagHelper:TagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("class", "inline-flex rounded-lg shadow-2xs");
            var childContent = await output.GetChildContentAsync();
            var html= InjectFirstLastAttributes(childContent.GetContent());
            output.Content.SetHtmlContent(html);
        }

        private string InjectFirstLastAttributes(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var buttons = doc.DocumentNode.Descendants("button").ToList();
            if (buttons.Count == 0)
            {
                return html;
            }

            buttons.First().Attributes.Add("is-first", "true");
            buttons.Last().Attributes.Add("is-last", "true");

            var sb = new StringBuilder();
            foreach (var node in doc.DocumentNode.ChildNodes)
            {
                sb.Append(node.OuterHtml);
            }

            return sb.ToString();
        }
    }
}

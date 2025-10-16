using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("hx-button")] 
    public class ButtonTagHelper:TagHelper
    {
        [HtmlAttributeName("get")]
        public string HxGet { get; set; }
        [HtmlAttributeName("target")]
        public string HxTarget { get; set; }
        [HtmlAttributeName("indicator")]
        public string HxIndicator { get; set; }
        [HtmlAttributeName("swap")]
        public string HxSwap { get; set; }
        [HtmlAttributeName("trigger")]
        public string HxTrigger { get; set; } = "click";
        [HtmlAttributeName("include")]
        public string HxInclude { get; set; }
        [HtmlAttributeName("exclude")]
        public string HxExclude { get; set; }

        [HtmlAttributeName("text")]
        public string Text { get; set; }= "Button";

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "button";
            output.Attributes.SetAttribute("type", "button");
            if(!string.IsNullOrEmpty(HxGet))
            {
                output.Attributes.SetAttribute("hx-get", HxGet);
            }
            if(!string.IsNullOrEmpty(HxTarget))
            {
                output.Attributes.SetAttribute("hx-target", HxTarget);  
            }
            if (!string.IsNullOrEmpty(HxIndicator))
            {
                output.Attributes.SetAttribute("hx-indicator", HxIndicator);
            }
            if (!string.IsNullOrEmpty(HxSwap))
            {
                output.Attributes.SetAttribute("hx-swap", HxSwap);
            }
            if (!string.IsNullOrEmpty(HxTrigger))
            {
                output.Attributes.SetAttribute("hx-trigger", HxTrigger);
            }
            if (!string.IsNullOrEmpty(HxInclude))
            {
                output.Attributes.SetAttribute("hx-include", HxInclude);
            }
            if (!string.IsNullOrEmpty(HxExclude))
            {
                output.Attributes.SetAttribute("hx-exclude", HxExclude);
            }
            output.Content.SetHtmlContent(Text);
            var childContent = await output.GetChildContentAsync();
            output.Content.SetHtmlContent(childContent.GetContent());
        }
    }
}

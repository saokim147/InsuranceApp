using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace InsuranceWebApp.TagHelpers
{
    public class CheckBoxTagHelper : InputTagHelper
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public CheckBoxTagHelper(IHtmlGenerator generator, IStringLocalizer<SharedResource> localizer) : base(generator) 
        {
            _localizer = localizer;
        }
        [HtmlAttributeName("label")]
        public string Label { get; set; } = string.Empty;

        [HtmlAttributeName("id")]
        public string Id { get; set; } = string.Empty;
        [HtmlAttributeName("value")]
        public string Value {get; set; }=string.Empty;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var checkbox = new TagBuilder("input");
            checkbox.Attributes.Add("type", "checkbox");
            checkbox.Attributes.Add("name", Name);
            checkbox.Attributes.Add("id", Id);
            checkbox.Attributes.Add("value", Value);
            checkbox.Attributes.Add("class", "size-4 accent-primary-500");
            output.Content.AppendHtml(checkbox);

            if (!string.IsNullOrEmpty(Label))
            {
                var label = new TagBuilder("label");
                label.Attributes.Add("for", Id);
                label.Attributes.Add("class", "ms-2 text-sm text-gray-900 dark:text-neutral-200");
                var textLabel = _localizer[Label].ResourceNotFound ? Label : _localizer[Label];
                label.InnerHtml.Append(textLabel);
                output.Content.AppendHtml(label);
            }
        }

    }
}

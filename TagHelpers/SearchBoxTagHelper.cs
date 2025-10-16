using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.Extensions.Localization;
using InsuranceWebApp.Helper;

namespace InsuranceWebApp.TagHelpers
{
    [HtmlTargetElement("search-box")]
    public class SearchBoxTagHelper : InputTagHelper
    {
        private readonly IStringLocalizer<SharedResource> _localizer;
        public SearchBoxTagHelper(IHtmlGenerator generator, IStringLocalizer<SharedResource> localizer) : base(generator)
        {
            _localizer = localizer;
        }

        [HtmlAttributeName("id")]
        public required string InputId { get; set; }
        [HtmlAttributeName("name")]
        public string Name { get; set; }
        [HtmlAttributeName("list-id")]
        public required string ListId { get; set; }
        [HtmlAttributeName("placeholder")]
        public string PlaceHolder { get; set; } = string.Empty;

        [HtmlAttributeName("get")]
        public string Url { get; set; }
        [HtmlAttributeName("min")]
        public string MinChar { get; set; } = string.Empty;
        [HtmlAttributeName("max")]
        public string MaxChar { get; set; } = string.Empty;
        [HtmlAttributeName("include")]
        public string Include { get; set; } = string.Empty;
        [HtmlAttributeName("label")]
        public string Label { get; set; } = string.Empty;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var condition = string.Empty;
            if (!string.IsNullOrEmpty(MinChar))
            {
                condition += $"this.value.length>={MinChar}";
            }
            if (!string.IsNullOrEmpty(MaxChar))
            {
                condition += $" && this.value.length<={MaxChar}";
            }
            if (!string.IsNullOrEmpty(Label))
            {
                var label = new TagBuilder("label");
                label.Attributes.Add("for", InputId);
                label.InnerHtml.Append(Label);
                output.Content.AppendHtml(label);
            }
            var container = new TagBuilder("div");
            container.Attributes.Add("class", "relative");

            var displayInput = new TagBuilder("input");
            displayInput.Attributes.Add("type", "text");
            displayInput.Attributes.Add("name", Name);
            displayInput.Attributes.Add("id", InputId);
            displayInput.Attributes.Add("class", Css.Input);
            var localizedPlaceHolderText = _localizer[PlaceHolder];
            var placeHolderText = localizedPlaceHolderText.ResourceNotFound ? PlaceHolder : localizedPlaceHolderText;
            displayInput.Attributes.Add("placeholder", placeHolderText);
            displayInput.Attributes.Add("autocomplete", "off");
            displayInput.Attributes.Add("hx-get", Url);
            displayInput.Attributes.Add("hx-target", $"#{ListId}");
            if (!string.IsNullOrEmpty(Include))
            {
                displayInput.Attributes.Add("hx-include", Include);
            }
            if (condition != string.Empty)
            {
                //var unescapedString = new HtmlString($"input changed delay:250ms[{condition}]");
                displayInput.Attributes.Add("hx-trigger", $"input[{condition}] changed delay:250ms");
            }
            else
            {
                displayInput.Attributes.Add("hx-trigger", $"input changed delay:250ms");
            }
            container.InnerHtml.AppendHtml(displayInput);

            var listContainer = new TagBuilder("div");
            listContainer.Attributes.Add("id", ListId);
            listContainer.Attributes.Add("class", "absolute top-full left-0 right-0 z-[9999] mt-1 max-h-60 w-full overflow-y-auto rounded-lg border border-gray-200 bg-white shadow-lg dark:bg-neutral-800 dark:border-neutral-700 hidden");
            listContainer.Attributes.Add("role", "listbox");
            listContainer.Attributes.Add("tabindex", "-1");

            if (For != null)
            {
                var hiddenInput = new TagBuilder("input");
                hiddenInput.Attributes.Add("type", "hidden");
                hiddenInput.Attributes.Add("name", For.Name.ToString());
                var model = For.Model != null ? For.Model.ToString() : string.Empty;
                hiddenInput.Attributes.Add("value", model);
                container.InnerHtml.AppendHtml(hiddenInput);
            }
            // Icon
            var buttonBuilder = new TagBuilder("button");
            buttonBuilder.Attributes.Add("type", "button");
            buttonBuilder.Attributes.Add("class", "absolute inset-y-0 right-0 flex items-center px-2");
            buttonBuilder.Attributes.Add("tabindex", "-1");
            var childContent = await output.GetChildContentAsync();
            buttonBuilder.InnerHtml.AppendHtml(childContent);

            container.InnerHtml.AppendHtml(buttonBuilder);

            output.Content.AppendHtml(container);
            output.Content.AppendHtml(listContainer);

        }
    }
}
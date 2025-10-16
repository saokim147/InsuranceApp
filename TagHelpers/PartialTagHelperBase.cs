using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace InsuranceWebApp.TagHelpers
{
    public class PartialTagHelperBase : TagHelper
    {
        private readonly IHtmlHelper _htmlHelper;
        public PartialTagHelperBase(IHtmlHelper htmlHelper)
        {
            _htmlHelper = htmlHelper;
        }
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        protected async Task<IHtmlContent> RenderPartial<T>(string partialName, T model)
        {
            ((IViewContextAware)_htmlHelper).Contextualize(ViewContext);

            return await _htmlHelper.PartialAsync(partialName, model);
        }
    }
}

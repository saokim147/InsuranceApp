using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InsuranceWebApp.TagHelpers
{
    public class SelectTagHelper : PartialTagHelperBase
    {
        public ModelExpression modelExpression { get; set; }

        public SelectTagHelper(IHtmlHelper htmlHelper) : base(htmlHelper)
        {

        }

    
    }
}

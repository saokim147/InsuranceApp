using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace InsuranceWebApp.Models
{
    public class AutocompleteViewModel
    {
        public ModelExpression AspFor { get; set; }
        public string Name { get; set; }
        public string Id { get; set; }
        public string Placeholder { get; set; }
        public int MinLength { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Delay { get; set; }
        public object HtmlAttributes { get; set; }
    }
}

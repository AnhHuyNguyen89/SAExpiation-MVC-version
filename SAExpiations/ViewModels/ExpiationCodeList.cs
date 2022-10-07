using SAExpiations.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Collections;

namespace SAExpiations.ViewModels
{
    public class ExpiationCodeList
    {
        public string? SearchText { get; set; }
        public List<ExpiationOffence> Items { get; set; }
        public List<Expiation> expiations { get; set; }
        public List<String> expiationCodes { get; set; }
        public List<OffenceStatus> StatusList { get; set; }

        [Display(Name = "Number of expiation offences for selected year is: ")]
        public int? Count { get; set; }
        public int SelectedYear { set; get; }
        public List<int> Years { set; get; }

        [Display(Name = "Selected expiation offence code: ")]
        public string offenceCode { get; set; }

        [Display(Name = "Expiation offence code's description: ")]
        public string offenceDescription { get; set; }
        public Dictionary<string, Dictionary<string, int>> report { get; set; }
        public int? expCount { get; set; }

    }
}

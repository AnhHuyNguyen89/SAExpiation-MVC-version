using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SAExpiations.Models
{
    public partial class OffenceStatus
    {
        public string OffenceStatusCode { get; set; } = null!;
        public string? OffenceStatusDescription { get; set; }
        
        //public virtual Expiation? Expiation { get; set; }
        [NotMapped]
        [Display(Name ="Notice Status Description")]
        public string NoticeName { get; set; }

        [NotMapped]
        [Display(Name ="Status Count")]
        public int StatusCount { get; set; }

        [NotMapped]
        [Display(Name ="Month")]
        public string Month { get; set; }
    }
}

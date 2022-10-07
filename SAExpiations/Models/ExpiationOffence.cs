using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace SAExpiations.Models
{
    public partial class ExpiationOffence
    {
        public string ExpiationOffenceCode { get; set; } = null!;
        public string? ExpiationOffenceDescription { get; set; }

        public virtual ExpiationCategory ExpiationCategory { get; set; } = null!;

       

    }
}

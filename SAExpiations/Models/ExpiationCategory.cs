using System;
using System.Collections.Generic;

namespace SAExpiations.Models
{
    public partial class ExpiationCategory
    {
        public string ExpiationOffenceCode { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public int Category { get; set; }

        public virtual ExpiationOffence ExpiationOffenceCodeNavigation { get; set; } = null!;
    }
}

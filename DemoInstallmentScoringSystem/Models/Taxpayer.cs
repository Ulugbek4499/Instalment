
using Installment.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installment.Domain
{
    public class Taxpayer : IPassportId
    {
        public required string PassportId { get; init; }
        public SocialStatus SocialStatus { get; set; }
        public required decimal Income { get; set; }
        public decimal Debt { get; set; }

    }
}

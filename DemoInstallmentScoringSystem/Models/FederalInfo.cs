using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installment.Domain
{
    public class FederalInfo : IPassportId
    {
        public required string PassportId { get; init; }
        public bool IsConvicted { get; set; }
        public bool IsDivorced { get; set; }

    }
}

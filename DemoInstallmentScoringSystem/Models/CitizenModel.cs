using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Installment.Domain
{
    public class CitizenModel : IPassportId
    {
        public required string PassportId { get; init; }
        public required string FirstName { get; init; } = string.Empty;
        public required string LastName { get; init; } = string.Empty;
        public required string MidName { get; init; } = string.Empty;
        public required string Region { get; init; } = string.Empty;
        public required DateOnly BirthDate { get; init; }

    }
}

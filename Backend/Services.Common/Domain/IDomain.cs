using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Domain
{
    public interface IDomain
    {
        Guid Id { get; }
    }
}

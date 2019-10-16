using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Domain
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}

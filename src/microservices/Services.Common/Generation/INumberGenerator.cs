using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Generation
{
    public interface INumberGenerator
    {
        int GenerateNumber(int length);
    }
}

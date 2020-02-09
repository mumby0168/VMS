using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common.Generation
{
    public class NumberGenerator : INumberGenerator
    {
        public int GenerateNumber(int length)
        {
            var random = new Random(DateTime.Now.Millisecond);
            var number = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                number.Append(random.Next(0, 9));
            }

            return int.Parse(number.ToString());
        }
    }
}

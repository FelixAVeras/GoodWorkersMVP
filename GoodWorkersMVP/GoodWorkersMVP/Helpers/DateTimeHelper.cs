using System;
using System.Collections.Generic;
using System.Text;

namespace GoodWorkersMVP.Helpers
{
    public class DateTimeHelper
    {
        public static DateTime GetMinimumDate => DateTime.Today.AddYears(-65);
        public static DateTime GetMaximumDate => new DateTime(2006, 12, 31);
    }
}

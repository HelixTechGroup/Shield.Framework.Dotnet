using System;
using System.Collections;
using System.Collections.Generic;

namespace Shield.Framework.Validation
{
    public class TimeGuard
    {
        public static ValidationRule<DateTime> DateTime
        {
            get { return new ValidationRule<DateTime>(); }
        }

        public static ValidationRule<DateTimeOffset> DateTimeOffset
        {
            get { return new ValidationRule<DateTimeOffset>(); }
        }

        public static ValidationRule<TimeSpan> TimeSpan
        {
            get { return new ValidationRule<TimeSpan>(); }
        }
    }
}
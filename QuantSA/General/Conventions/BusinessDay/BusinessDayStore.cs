﻿using QuantSA.General.Dates;

namespace QuantSA.General.Conventions.BusinessDay
{
    /// <summary>
    /// </summary>
    /// <remarks>
    /// These values are taken from QLNET (https://github.com/amaggiulli/QLNet)
    /// </remarks>
    public class BusinessDayStore
    {
        public static readonly Following Following = Following.Instance;
        public static readonly ModifiedFollowing ModifiedFollowing = ModifiedFollowing.Instance;
        public static readonly Preceding Preceding = Preceding.Instance;
        public static readonly ModifiedPreceding ModifiedPreceding = ModifiedPreceding.Instance;
        public static readonly Unadjusted Unadjusted = Unadjusted.Instance;
    }

    /// <summary>
    /// Choose the first business day after the given holiday.
    /// </summary>
    /// <seealso cref="QuantSA.General.Conventions.BusinessDay.BusinessDayConvention" />
    public class Following : BusinessDayConvention
    {
        public static readonly Following Instance = new Following();
        private Following() { }
        public Date Adjust(Date date, Calendar calendar)
        {
            Date newDate = new Date(date);
            while (!calendar.isBusinessDay(newDate))
                newDate = newDate.AddDays(1);
            return newDate;
        }
    }

    /// <summary>
    /// Choose the first business day after the given holiday unless it belongs to a different month, in which 
    /// case choose the first business day before the holiday.
    /// </summary>
    /// <seealso cref="QuantSA.General.Conventions.BusinessDay.BusinessDayConvention" />
    public class ModifiedFollowing : BusinessDayConvention
    {
        public static readonly ModifiedFollowing Instance = new ModifiedFollowing();
        private ModifiedFollowing() { }
        public Date Adjust(Date date, Calendar calendar)
        {
            Date newDate = new Date(date);
            while (!calendar.isBusinessDay(newDate))
            {
                newDate = newDate.AddDays(1);
                if (newDate.Month != date.Month)
                    return BusinessDayStore.Preceding.Adjust(date, calendar);
            }
            return newDate;
        }
    }

    /// <summary>
    /// Choose the first business day beforeb the given holiday.
    /// </summary>
    /// <seealso cref="QuantSA.General.Conventions.BusinessDay.BusinessDayConvention" />
    public class Preceding : BusinessDayConvention
    {
        public static readonly Preceding Instance = new Preceding();
        private Preceding() { }
        public Date Adjust(Date date, Calendar calendar)
        {
            Date newDate = new Date(date);
            while (!calendar.isBusinessDay(newDate))
            {
                newDate = newDate.AddDays(-1);
            }
            return newDate;
        }
    }

    /// <summary>
    /// Choose the first business day before the given holiday unless it belongs to a 
    /// different month, in which case choose the first business day after the holiday
    /// </summary>
    /// <seealso cref="QuantSA.General.Conventions.BusinessDay.BusinessDayConvention" />
    public class ModifiedPreceding : BusinessDayConvention
    {
        public static readonly ModifiedPreceding Instance = new ModifiedPreceding();
        private ModifiedPreceding() { }
        public Date Adjust(Date date, Calendar calendar)
        {
            Date newDate = new Date(date);
            while (!calendar.isBusinessDay(newDate))
            {
                newDate = newDate.AddDays(-1);
                if (newDate.Month != date.Month)
                    return BusinessDayStore.Following.Adjust(date, calendar);
            }
            return newDate;
        }
    }

    /// <summary>
    /// Do not adjust.
    /// </summary>
    /// <seealso cref="QuantSA.General.Conventions.BusinessDay.BusinessDayConvention" />
    public class Unadjusted : BusinessDayConvention
    {
        public static readonly Unadjusted Instance = new Unadjusted();
        private Unadjusted() { }
        public Date Adjust(Date date, Calendar calendar)
        {
            return new Date(date);
        }
    }
}

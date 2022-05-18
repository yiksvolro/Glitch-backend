using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace Glitch.Extensions
{
    public static class DateTimeExtensions
    {
        private static readonly string timeZone = "FLE Standard Time";
        public static DateTime GetUkrainianDateTime(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.FindSystemTimeZoneById(timeZone));
        }
    }
}

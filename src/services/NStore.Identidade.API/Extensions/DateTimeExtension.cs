using System;

namespace NStore.Identidade.API.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToEpochDate(this DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 1, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}

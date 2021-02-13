using System;

namespace NStore.Identidade.API.Extensions
{
    public static class DateTimeExtension
    {
        public static long ToEpochDate(this DateTime date) => DateTimeOffset.Now.ToUnixTimeSeconds();
    }
}

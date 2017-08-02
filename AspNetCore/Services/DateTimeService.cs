using System;

namespace AspNetCore.Services
{
    public class DateTimeService
    {
        public string GetTime() => DateTime.Now.ToString();     
    }
}

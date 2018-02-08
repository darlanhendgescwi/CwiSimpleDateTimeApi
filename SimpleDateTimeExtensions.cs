using System;

namespace JefersonBueno
{
    public static class CwiSimpleDateTimeExtensions
    {
        public static CwiSimpleDateTime FromDateTimeString(string data)
        {
            var split = data.Split(' ');
            var (year, month, day) = FromDateString(split[0]);      
            var (hour, minute) = FromTimeString(split[1]);
            
            return new CwiSimpleDateTime(year, month, day, hour, minute);
        }
        
        private static (int year, int month, int day) FromDateString(string date)
        {
            var splittedDate = date.Split('/');
            
            if(splittedDate.Length != 3) 
                return (0, 0, 0);
                
            return (Convert.ToInt32(splittedDate[2]), Convert.ToInt32(splittedDate[1]), Convert.ToInt32(splittedDate[0]));
        }
        
        private static (int hour, int minute) FromTimeString(string time)
        {
            var splittedTime = time.Split(':');
            
            if(splittedTime.Length != 2)
                return (0, 0);
            
            return (Convert.ToInt32(splittedTime[0]), Convert.ToInt32(splittedTime[1]));
        }
    }
}
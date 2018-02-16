using System;

namespace JefersonBueno
{
    /* 
    ** Estrutura que representa uma data simples. Sem anos bissextos e fusos horários.
    ** Cada objeto deste tipo tem um campo chamado 'dateMillis', que contém a data e o tempo 
    ** (horas e minutos apenas) contando em milissegundos a partir do dia 01/01/0001 às 00:00. 
    */    
    public struct CwiSimpleDateTime
    {
        private enum DatePortion
        {
            Year, Month, Day
        }

        private static readonly int[] MaxDaysOfMonths = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        private static readonly int[] DaysToMonth = { 0, 31, 59, 90, 120, 151, 181, 212, 243, 273, 304, 334, 365 };
        
        private const int DaysPerYear = 365; 
        private const int MillisPerSecond = 1000;
        private const int MillisPerMinute = 60 * MillisPerSecond;
        private const int MillisPerHour = 60 * MillisPerMinute;
        private const int MillisPerDay = 24 * MillisPerHour;
        
        private ulong dateMillis;

        public int Year => GetDateFraction(DatePortion.Year);  
        public int Month => GetDateFraction(DatePortion.Month);
        public int Day => GetDateFraction(DatePortion.Day);
        public int Hour => (int)((dateMillis / MillisPerHour) % 24);
        public int Minute => (int)((dateMillis / MillisPerMinute) % 60); 

        private CwiSimpleDateTime(ulong millis) => dateMillis = millis;
        
        public CwiSimpleDateTime(int year, int month, int day, int hour, int minute) => dateMillis = DateToMillis(year, month, day) + (ulong)TimeToMillis(hour, minute);        
                
        public CwiSimpleDateTime(int year, int month, int day) : this(year, month, day, 0, 0) { }
        
        public CwiSimpleDateTime AddMinutes(long minutes) => Add(minutes, MillisPerMinute);

        private CwiSimpleDateTime Add(long value, int multiplier) => AddMilliseconds(value * multiplier);

        private CwiSimpleDateTime AddMilliseconds(long millis) => new CwiSimpleDateTime((ulong)millis + dateMillis);

        public override string ToString() => $"{Day:d2}/{Month:d2}/{Year:d4} {Hour:d2}:{Minute:d2}";

        private int GetDateFraction(DatePortion part)
        {
            var qtDays = (int)(dateMillis / MillisPerDay);
            var qtYears = qtDays / DaysPerYear;
            
            if(part == DatePortion.Year)
                return qtYears + 1;
                
            qtDays -= qtYears * DaysPerYear;
            
            int month = 0;
            while (qtDays >= DaysToMonth[month]) {
                month++;
            }
            
            if(part == DatePortion.Month)
                return month;
            
            return qtDays - DaysToMonth[month - 1] + 1;
        }

        private static ulong DateToMillis(int year, int month, int day)
        {
            if(year < 1 || year > 9999 || month < 1 || month > 12) {
                throw new ArgumentOutOfRangeException("O ano precisa estar entre 1 e 9999 e o mês entre 1 e 12");
            }
            
            if(day > MaxDaysOfMonths[month - 1]) {
                throw new ArgumentOutOfRangeException($"Quantidade de dias ({day}) inválida para o mês {month}");
            }
            
            int y = year - 1;
            int m = month - 1;
            ulong qtDays = (ulong)((day - 1) + DaysToMonth[month - 1] + ((year - 1) * DaysPerYear));
                    
            return qtDays * MillisPerDay;
        }
        
        private static long TimeToMillis(int hour, int minute)
        {
            if(hour < 0 || hour > 23 || minute < 0 || minute > 59) {
                throw new ArgumentOutOfRangeException($"Intervalo inválido para horas e minutos ({hour}:{minute})");
            }
            
            return hour * MillisPerHour + minute * MillisPerMinute;
        }
    }
}

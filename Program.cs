using System;
using System.Globalization;

namespace JefersonBueno
{
    public class Program
    {
        public static string ChangeDate(string date, char op, long value)
        {
            if(op != '+' && op != '-')
                throw new ArgumentException("Operação é inválida", nameof(op));

            var toAdd = Math.Abs(value);
            if(op == '-')
                toAdd *= -1;
                
            var dt = CwiSimpleDateTimeExtensions.FromDateTimeString(date);
            var newDate = dt.AddMinutes(toAdd);

            return newDate.ToString();
        }
    }	
}
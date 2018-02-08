using System;
using System.Globalization;

namespace JefersonBueno
{
    public class Program2
    {
        public static void Main()
        {
            var testes = new[]
            {
                new { Input = "01/03/2010 23:00", Op = '+', Val = 4_000L },
                new { Input = "01/01/0030 00:00", Op = '+', Val = 18_000L },
                new { Input = "01/05/2018 14:00", Op = '+', Val = 50L },
                new { Input = "02/08/2014 00:08", Op = '-', Val = -8L },
                new { Input = "01/05/0001 00:50", Op = '-', Val = 50L },
                new { Input = "01/05/0001 00:50", Op = 'a', Val = 50L }
            };
            
            foreach(var teste in testes)
            {
                var result = Program.ChangeDate(teste.Input, teste.Op, teste.Val);
                var toAdd = Math.Abs(teste.Val);
                if(teste.Op == '-')
                    toAdd *= -1;               
                
                var expected = DateTime.ParseExact(teste.Input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture).AddMinutes(toAdd).ToString("dd/MM/yyyy HH:mm");
                Console.WriteLine($"Input: {teste.Input} {teste.Op} {teste.Val}\nResult: {result}\nExpected: {expected} | {result == expected}\n");
            }
        }
    }	
}
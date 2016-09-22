using System;
using System.Threading.Tasks;
using HvA.API.NetStandard1;
using HvA.API.NetStandard1.Util;
using HvA.API.NetStandard1.Extensions;

namespace HvA.API.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run(args).GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static async Task Run(string[] args)
        {
            var client = new HvAClient(args[0], args[1]);

            if (await client.SignInAsync())
            {
                var schedule = (await client.GetSchedulesAsync("IS101"))[0];

                foreach (var timetableItem in await client.GetOtherTimeTableAsync(schedule.Value, DateTime.Now.GetIso8601WeekOfYear()))
                {
                    var beginDate = DateTimeHelper.LongDateToDateTime(timetableItem.StartDate).ToLocalTime();
                    var endDate = DateTimeHelper.LongDateToDateTime(timetableItem.EndDate).ToLocalTime();

                    Console.WriteLine($"{beginDate:ddd dd/MM HH:mm} - {endDate:dd/MM HH:mm}: " + timetableItem.ActivityDescription);
                }
            }
            else
            {
                Console.WriteLine("Couldn't sign in, wrong credentials specified.");
            }

            await client.SignOutAsync();
        }
    }
}

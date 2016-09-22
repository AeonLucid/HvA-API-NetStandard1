﻿using System;
using System.Threading.Tasks;
using HvA.API.Extensions;
using HvA.API.Util;

namespace HvA.API.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Run("username", "password").GetAwaiter().GetResult();

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        private static async Task Run(string username, string password)
        {
            var client = new HvAClient(username, password);

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

            await client.SignOutAsync();
        }
    }
}

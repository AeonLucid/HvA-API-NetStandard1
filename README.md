# HvA-API-NetStandard1

[![Build status](https://ci.appveyor.com/api/projects/status/8ci60nhsjxawhmbt?svg=true)](https://ci.appveyor.com/project/AeonLucid/hva-api-netstandard1)

A C# API to authenticate with and request data from the Hogeschool van Amsterdam.

[Click here for the java version.](https://github.com/AeonLucid/HvA-API-Java)

## Installation

Soon.

## Example

```csharp
using System;
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
            else
            {
                Console.WriteLine("Couldn't sign in, wrong credentials specified.");
            }

            await client.SignOutAsync();
        }
    }
}
```

Output:
```
Mon 19/09 14:30 - 19/09 16:10: FYS Coaching
Tue 20/09 08:30 - 20/09 12:40: Project Fasten Your Seatbelts
Tue 20/09 12:50 - 20/09 17:00: Project Fasten Your Seatbelts
Wed 21/09 10:20 - 21/09 12:00: Audit 1
Wed 21/09 12:00 - 21/09 14:30: Personal Skills
Thu 22/09 08:30 - 22/09 11:50: Essential Skills Wiskunde
Thu 22/09 12:00 - 22/09 13:40: Programming
Thu 22/09 13:40 - 22/09 15:20: Programming
Fri 23/09 08:30 - 23/09 11:00: User Interaction
Press any key to exit.
```

## Implemented API calls

- [x] signIn
- [x] signOut
- [x] getCurrentUser
- [x] updateProfile
- [x] getDomains
- [x] getProgrammes
- [x] getAbsentees
- [ ] getAnnouncements (Response data unknown)
- [x] getNews
- [x] getLocations
- [x] getStudylocations
- [x] getStudylocationPage
- [ ] getPeople (Server throws an exception, also broken in the official 'MijnHvApp')
- [x] getMyTimeTable
- [x] getSchedules
- [x] otherSchedule
- [x] getAZUrlsForEmployees
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HvA.API.Data;
using HvA.API.Util;
using Newtonsoft.Json;

namespace HvA.API
{
    public class HvAClient : IDisposable
    {
        private readonly string _username;
        private readonly string _password;
        
        private readonly HttpClient _httpClient;

        public HvAClient(string username, string password)
        {
            _username = username;
            _password = password;
            

            _httpClient = new HttpClient(new HttpClientHandler
            {
                CookieContainer = new CookieContainer()
            });
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(Configuration.UserAgent);
        }

        public bool IsAuthenticated { get; private set; }

        /// <summary>
        ///     Sends an authentication request to the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Determines whether authentication was successful.</returns>
        public async Task<bool> SignInAsync()
        {
            var response = await PerformRequestAsync<AuthenticationUser>("/auth/signin", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"username", _username},
                {"password", _password}
            }));

            return IsAuthenticated = !(response == null || !response.IsAuthenticated);
        }

        /// <summary>
        ///     Sends a sign out request to the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Determines whether signing out was successful.</returns>
        public async Task<bool> SignOutAsync()
        {
            return !IsAuthenticated || (IsAuthenticated = await PerformRequestAsync("/auth/signout"));
        }

        /// <summary>
        ///     Gets the current <see cref="AuthenticationUser"/> of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns the current <see cref="AuthenticationUser"/> of the Hogeschool van Amsterdam.</returns>
        public async Task<AuthenticationUser> GetCurrentUserAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<AuthenticationUser>("/auth/getCurrentUser");
        }

        /// <summary>
        ///     Updates the <see cref="Profile"/> on the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="profile">The modified <see cref="Profile"/> you want to synchronize.</param>
        /// <returns>Determines whether updating was successful.</returns>
        public async Task<bool> UpdateProfileAsync(Profile profile)
        {
            if (!IsAuthenticated) return false;

            var response = await PerformRequestAsync<Profile>("/auth/updateprofile", new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"Domain", profile.Domain},
                {"DomainAZUrl", profile.DomainAzUrl},
                {"Language", profile.Language},
                // TODO: Courses
                // TODO: Teachers
                {"AZProgrammeTitle", profile.AzProgrammeTitle},
                {"AZType", profile.AzType},
                {"AZPrefix", profile.AzPrefix},
                {"IsComplete", profile.IsComplete}
            }));

            return response != null;
        }

        /// <summary>
        ///     Gets an array containing all <see cref="Domain"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="Domain"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<Domain[]> GetDomainsAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<Domain[]>("/api/domains");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="Programme"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="Programme"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<Programme[]> GetProgrammesAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<Programme[]>("/api/programmes");
        }

        /// <summary>
        ///     Gets the <see cref="Absentee"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns the <see cref="Absentee"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<Absentee[]> GetAbsenteesAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<Absentee[]>("/api/absentees");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="News"/> of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="News"/> of the Hogeschool van Amsterdam.</returns>
        public async Task<News[]> GetNewsAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<News[]>("/api/news");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="Location"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="Location"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<Location[]> GetLocationsAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<Location[]>("/api/locations");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="StudyLocation"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="StudyLocation"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<StudyLocation[]> GetStudyLocationAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<StudyLocation[]>("/api/studylocations");
        }

        /// <summary>
        ///     Gets the <see cref="StudyLocationPage"/> of the specified url of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="url">The url property of <see cref="StudyLocation"/>.</param>
        /// <returns>Returns the <see cref="StudyLocationPage"/> of the specified url of the Hogeschool van Amsterdam.</returns>
        public async Task<StudyLocationPage> GetStudyLocationsPageAsync(string url)
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<StudyLocationPage>($"/api/studylocations/page?url={url}&main=true&serviceUrl={Configuration.ApiUrl}/");
        }

        /// <summary>
        ///     Gets the <see cref="TimetableItem"/>s of the specified week number of the currently signed in user of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="weekNumber">The week number to retrieve <see cref="TimetableItem"/>s.</param>
        /// <returns>Returns the <see cref="TimetableItem"/>s of the specified week number of the currently signed in user of the Hogeschool van Amsterdam.</returns>
        public Task<TimetableItem[]> GetMyTimetableAsync(int weekNumber)
        {
            var dateTime = DateTimeHelper.FirstDateOfWeek(weekNumber);

            return GetMyTimetableAsync(dateTime, dateTime.AddDays(7));
        }

        /// <summary>
        ///     Gets the <see cref="TimetableItem"/>s of the specified start <see cref="DateTime"/> and end <see cref="DateTime"/> (exclusive) of the currently signed in user of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="startDate">The start <see cref="DateTime"/> to retrieve <see cref="TimetableItem"/>s from.</param>
        /// <param name="endDate">The end <see cref="DateTime"/> to retrieve <see cref="TimetableItem"/>s from.</param>
        /// <returns></returns>
        public async Task<TimetableItem[]> GetMyTimetableAsync(DateTime startDate, DateTime endDate)
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<TimetableItem[]>($"/timetable/my?startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        }

        /// <summary>
        ///     Gets the <see cref="TimetableItem"/>s of the specified week number of the specified studentSetId of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="studentSetId">StudentSet ID which is retrieve-able from <see cref="Schedule"/>.</param>
        /// <param name="weekNumber">The week number to retrieve <see cref="TimetableItem"/>s.</param>
        /// <returns>Returns the <see cref="TimetableItem"/>s of the specified week number of the currently signed in user of the Hogeschool van Amsterdam.</returns>
        public Task<TimetableItem[]> GetOtherTimeTableAsync(string studentSetId, int weekNumber)
        {
            var dateTime = DateTimeHelper.FirstDateOfWeek(weekNumber);

            return GetOtherTimeTableAsync(studentSetId, dateTime, dateTime.AddDays(7));
        }

        /// <summary>
        ///     Gets the <see cref="TimetableItem"/>s of the specified start <see cref="DateTime"/> and end <see cref="DateTime"/> (exclusive) of the specified studentSetId of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="studentSetId">StudentSet ID which is retrieve-able from <see cref="Schedule"/>.</param>
        /// <param name="startDate">The start <see cref="DateTime"/> to retrieve <see cref="TimetableItem"/>s from.</param>
        /// <param name="endDate">The end <see cref="DateTime"/> to retrieve <see cref="TimetableItem"/>s from.</param>
        /// <returns></returns>
        public async Task<TimetableItem[]> GetOtherTimeTableAsync(string studentSetId, DateTime startDate, DateTime endDate)
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<TimetableItem[]>($"/timetable/other?id={studentSetId}&startDate={startDate:yyyy-MM-dd}&endDate={endDate:yyyy-MM-dd}");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="Schedule"/>s of the specified filter of the Hogeschool van Amsterdam.
        /// </summary>
        /// <param name="filter">Filter to search for <see cref="Schedule"/>s.</param>
        /// <returns>Returns an array containing all <see cref="Schedule"/>s of the specified filter of the Hogeschool van Amsterdam.</returns>
        public async Task<Schedule[]> GetSchedulesAsync(string filter)
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<Schedule[]>($"/timetable/schedules?filter={filter}");
        }

        /// <summary>
        ///     Gets an array containing all <see cref="AzUrl"/>s of the Hogeschool van Amsterdam.
        /// </summary>
        /// <returns>Returns an array containing all <see cref="AzUrl"/>s of the Hogeschool van Amsterdam.</returns>
        public async Task<AzUrl[]> GetAzUrlsForEmployeesAsync()
        {
            if (!IsAuthenticated) return null;

            return await PerformRequestAsync<AzUrl[]>("/api/az");
        }

        // GET
        private async Task<bool> PerformRequestAsync(string path)
        {
            using (var response = await _httpClient.GetAsync($"{Configuration.ApiUrl}{path}"))
            {
                return response.IsSuccessStatusCode;
            }
        }

        // GET & POST
        private async Task<T> PerformRequestAsync<T>(string path, HttpContent content = null)
        {
            var url = $"{Configuration.ApiUrl}{path}";
            var responseTask = content != null ? _httpClient.PostAsync(url, content) : _httpClient.GetAsync(url);

            using (var response = await responseTask)
            {
                return !response.IsSuccessStatusCode
                    ? default(T)
                    : JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}

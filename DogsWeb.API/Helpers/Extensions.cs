using System;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace DogsWeb.API.Helpers
{
    public static class Extensions
    {
     public static void AddApplicationError(this HttpResponse response, string message)
        {
            response.Headers.Add("Application-Error", WebUtility.UrlEncode(message));
            response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Content-type", "text/json");
            response.Headers.Add("Content-type", "application/json");
            
        
        }
        public static int CalculateAge(this DateTime theDateTime){

            var age = DateTime.Today.Year - theDateTime.Year;
            if(theDateTime.AddYears(age) > DateTime.Today)
                    age--;

            return age;
        }
    }
}
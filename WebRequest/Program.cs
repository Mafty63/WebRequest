using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        try
        {
            string userAuthenticationURI = "http://universities.hipolabs.com/search?country=Indonesia";

            if (!string.IsNullOrEmpty(userAuthenticationURI))
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(userAuthenticationURI);
                request.Method = "GET";
                request.ContentType = "application/json";

                using (WebResponse response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
                        dynamic obj = JsonConvert.DeserializeObject(responseString);
                       // Console.WriteLine(responseString);

                        foreach (var university in obj)
                        {
                            Console.WriteLine("----------------------------------------------------");
                            Console.WriteLine("Name   : " + university.name); 
                            Console.WriteLine("Country: " + university.country);
                            Console.WriteLine("Website: " + String.Join(",",university.web_pages));
                            Console.WriteLine("----------------------------------------------------");
                        }
                    }
                }
            }
        }
        catch (WebException ex)
        {
            // Handle exceptions like network errors, invalid URL, etc.
            Console.WriteLine($"Error: {ex.Message}");

            if (ex.Response != null)
            {
                using (var errorResponse = (HttpWebResponse)ex.Response)
                {
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        string errorText = reader.ReadToEnd();
                        Console.WriteLine($"API Error: {errorText}");
                    }
                }
            }
        }
    }
}

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    class RegularRESTResponse
    {
        static void Main(string[] args)
        {
            const string RequestUriString = "URI PLACEHOLDER STRING";
            WebRequest request = WebRequest.Create(RequestUriString);
            request.Method = "GET";
            WebResponse response = request.GetResponse();//REST response of type GET
            string responseString = "";
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                responseString = reader.ReadToEnd();//Read JSON Response into string          
            }
            responseString = responseString.Insert(responseString.IndexOf("1") + 3, "[");
            responseString = responseString.Insert(responseString.LastIndexOf("}"), "]");// append "[]" to JSON to create a list

            MatchCollection col = Regex.Matches(responseString, @"""\d+"":");
            string[] fields = new string[col.Count];
            for (int i = 1; i < fields.Length; i++)
            {
                fields[i] = col[i].Groups[1].Value; // (Index 1 is the first group)
                responseString = Regex.Replace(responseString, col[i].ToString(), ""); //remove unnecessary record IDs
            }// Match all quoted fields, replace with blank string            
            Console.WriteLine(responseString);
            Console.ReadLine();
        }
    }
}

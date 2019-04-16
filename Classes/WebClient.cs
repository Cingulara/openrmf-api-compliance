using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using openstig_api_compliance.Models.Artifact;
using openstig_api_compliance.Models.Compliance;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace openstig_api_compliance.Classes
{
    public static class WebClient 
    {
        public static async Task<List<Artifact>> GetChecklistsBySystem(string systemId)
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try	
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    string hosturl = Environment.GetEnvironmentVariable("openrmf-api-read-server");
                    HttpResponseMessage response = await client.GetAsync(hosturl + "/systems/" + System.Uri.EscapeUriString(systemId));
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<Artifact>>(responseBody);
                    return result;
                }
                catch(HttpRequestException e)
                {
                    // log something here
                    throw e;
                }
                catch (Exception ex) {
                    // log something here
                    throw ex;
                }
            }
        }
 
        public static async Task<Artifact> GetChecklistAsync(string artifactId)
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try	
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/xml");
                    string hosturl = Environment.GetEnvironmentVariable("openrmf-api-read-server");
                    Console.WriteLine("URL: {0}", hosturl + "/" + artifactId);
                    HttpResponseMessage response = await client.GetAsync(hosturl + "/" + artifactId);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    XmlSerializer serializer = new XmlSerializer(typeof(Artifact));
                    Artifact art;
                    using (TextReader reader = new StringReader(responseBody))
                    {
                        art = (Artifact)serializer.Deserialize(reader);
                    }
                    return art;
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nHTTP Exception Caught!");
                    Console.WriteLine("Message :{0}", e.Message );
                    Console.WriteLine("Stack :{0}", e.StackTrace);
                    //throw e;
                    return null;
                }
                catch (Exception ex) {
                    Console.WriteLine("\nGeneral  exception Caught!");	
                    Console.WriteLine("Message :{0}", ex.Message);
                    //throw ex;
                    return null;
                }
            }
        }


        public static async Task<List<ControlSet>> GetControlRecords()
        {
            // Create a New HttpClient object and dispose it when done, so the app doesn't leak resources
            using (HttpClient client = new HttpClient())
            {
                // Call asynchronous network methods in a try/catch block to handle exceptions
                try	
                {
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Add("Accept", "application/json");
                    string hosturl = Environment.GetEnvironmentVariable("openrmf-api-controls-server");
                    Console.WriteLine("URL: {0}", hosturl + "/");
                    HttpResponseMessage response = await client.GetAsync(hosturl + "/");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<ControlSet>>(responseBody);
                    if (result.Count > 0)
                        return result;
                    else
                        return null;
                }
                catch(HttpRequestException e)
                {
                    Console.WriteLine("\nHTTP Exception Caught!");
                    Console.WriteLine("Message :{0}", e.Message );
                    Console.WriteLine("Stack :{0}", e.StackTrace);
                    //throw e;
                    return null;
                }
                catch (Exception ex) {
                    Console.WriteLine("\nGeneral  exception Caught!");	
                    Console.WriteLine("Message :{0}", ex.Message);
                    //throw ex;
                    return null;
                }
            }
        }
    }
}
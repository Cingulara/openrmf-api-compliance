using System;
using System.Collections.Generic;
using System.Text;
using NATS.Client;
using openstig_api_compliance.Models.Artifact;
using Newtonsoft.Json;

namespace openstig_api_compliance.Classes
{
    public static class NATSClient
    {        
        /// <summary>
        /// Decompresses the string.
        /// </summary>
        /// <param name="system">The system ID for all the checklists to return.</param>
        /// <returns></returns>
        public static List<Artifact> GetChecklistsBySystem(string system)
        {
            List<Artifact> arts = new List<Artifact>();
            // Create a new connection factory to create a connection.
            ConnectionFactory cf = new ConnectionFactory();

            // Creates a live connection to the default NATS Server running locally
            IConnection c = cf.CreateConnection(Environment.GetEnvironmentVariable("natsserverurl"));

            // publish to get this list of Artifact checklists back via system
            Msg reply = c.Request("openrmf.system.checklists.read", Encoding.UTF8.GetBytes(system), 30000); 
            c.Flush();
            // save the reply and get back the checklist score
            if (reply != null) {
                arts = JsonConvert.DeserializeObject<List<Artifact>>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                c.Close();
                return arts;
            }
            c.Close();
            return arts;
        }
        public static Artifact GetChecklist(string id){
            Artifact art = new Artifact();
            // Create a new connection factory to create a connection.
            ConnectionFactory cf = new ConnectionFactory();

            // Creates a live connection to the default NATS Server running locally
            IConnection c = cf.CreateConnection(Environment.GetEnvironmentVariable("natsserverurl"));
            Msg reply = c.Request("openrmf.checklist.read", Encoding.UTF8.GetBytes(id), 3000); // publish to get this Artifact checklist back via ID
            // save the reply and get back the checklist to score
            if (reply != null) {
                art = JsonConvert.DeserializeObject<Artifact>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                c.Close();
                return art;
            }
            c.Close();
            return art;
        }
    }
}
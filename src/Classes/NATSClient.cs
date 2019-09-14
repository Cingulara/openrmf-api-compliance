using System;
using System.Collections.Generic;
using System.Text;
using NATS.Client;
using openstig_api_compliance.Models.Artifact;
using openstig_api_compliance.Models.Compliance;
using openstig_api_compliance.Models.NISTtoCCI;
using Newtonsoft.Json;

namespace openstig_api_compliance.Classes
{
    public static class NATSClient
    {        
        /// <summary>
        /// Return a list of checklists based on the system.
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
 
        /// <summary>
        /// Return a single checklist based on the unique ID.
        /// </summary>
        /// <param name="id">The checklist ID for the checklist to return.</param>
        /// <returns></returns>
        public static Artifact GetChecklist(string id){
            Artifact art = new Artifact();
            // Create a new connection factory to create a connection.
            ConnectionFactory cf = new ConnectionFactory();

            // Creates a live connection to the default NATS Server running locally
            IConnection c = cf.CreateConnection(Environment.GetEnvironmentVariable("natsserverurl"));
            // publish to get this Artifact checklist back via ID
            Msg reply = c.Request("openrmf.checklist.read", Encoding.UTF8.GetBytes(id), 3000);
            // save the reply and get back the checklist to score
            if (reply != null) {
                art = JsonConvert.DeserializeObject<Artifact>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                art.CHECKLIST = ChecklistLoader.LoadChecklist(art.rawChecklist);
                c.Close();
                return art;
            }
            c.Close();
            return art;
        }

        /// <summary>
        /// Return a list of controls based on filter and PII
        /// </summary>
        /// <param name="impactlevel">The impact level of the controls to return.</param>
        /// <param name="pii">Boolean to return the PII elements or not.</param>
        /// <returns></returns>
        public static List<ControlSet> GetControlRecords(string impactlevel, bool pii){
            // get the result ready to receive the info and send on
            List<ControlSet> controls = new List<ControlSet>();
            // setup the filter for impact level and PII for controls
            Filter controlFilter = new Filter() {impactLevel = impactlevel, pii = pii};
            // Create a new connection factory to create a connection.
            ConnectionFactory cf = new ConnectionFactory();
            // Creates a live connection to the default NATS Server running locally
            IConnection c = cf.CreateConnection(Environment.GetEnvironmentVariable("natsserverurl"));
            // send the message with data of the filter serialized
            Msg reply = c.Request("openrmf.controls", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(controlFilter)), 30000);
            // save the reply and get back the checklist to score
            if (reply != null) {
                controls = JsonConvert.DeserializeObject<List<ControlSet>>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                c.Close();
                return controls;
            }
            c.Close();
            return controls;
        }

        /// <summary>
        /// Return a list of CCI Items to use for generating compliance
        /// </summary>
        /// <returns></returns>
        public static List<CciItem> GetCCIListing(){
            // get the result ready to receive the info and send on
            List<CciItem> cciItems = new List<CciItem>();
            // Create a new connection factory to create a connection.
            ConnectionFactory cf = new ConnectionFactory();
            // Creates a live connection to the default NATS Server running locally
            IConnection c = cf.CreateConnection(Environment.GetEnvironmentVariable("natsserverurl"));
            // send the message with the subject, no data needed
            Msg reply = c.Request("openrmf.compliance.cci", Encoding.UTF8.GetBytes(JsonConvert.SerializeObject("")), 30000);
            // save the reply and get back the checklist to score
            if (reply != null) {
                cciItems = JsonConvert.DeserializeObject<List<CciItem>>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                c.Close();
                return cciItems;
            }
            c.Close();
            return cciItems;
        }
    }
}
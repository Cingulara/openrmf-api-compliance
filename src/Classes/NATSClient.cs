// Copyright (c) Cingulara 2019. All rights reserved.
// Licensed under the GNU GENERAL PUBLIC LICENSE Version 3, 29 June 2007 license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;
using NATS.Client;
using openrmf_api_compliance.Models.Artifact;
using openrmf_api_compliance.Models.Compliance;
using openrmf_api_compliance.Models.NISTtoCCI;
using Newtonsoft.Json;

namespace openrmf_api_compliance.Classes
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
            // add the options for the server, reconnecting, and the handler events
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.MaxReconnect = -1;
            opts.ReconnectWait = 1000;
            opts.Name = "openrmf-api-compliance";
            opts.Url = Environment.GetEnvironmentVariable("NATSSERVERURL");
            opts.AsyncErrorEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("NATS client error. Server: {0}. Message: {1}. Subject: {2}", events.Conn.ConnectedUrl, events.Error, events.Subscription.Subject));
            };

            opts.ServerDiscoveredEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("A new server has joined the cluster: {0}", events.Conn.DiscoveredServers));
            };

            opts.ClosedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Closed: {0}", events.Conn.ConnectedUrl));
            };

            opts.ReconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Reconnected: {0}", events.Conn.ConnectedUrl));
            };

            opts.DisconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Disconnected: {0}", events.Conn.ConnectedUrl));
            };
            
            // Creates a live connection to the NATS Server with the above options
            IConnection c = cf.CreateConnection(opts);

            // publish to get this list of Artifact checklists back via system
            Msg reply = c.Request("openrmf.system.checklists.read", Encoding.UTF8.GetBytes(system), 30000); 
            c.Flush();
            // save the reply and get back the checklist score
            if (reply != null) {
                arts = JsonConvert.DeserializeObject<List<Artifact>>(Compression.DecompressString(Encoding.UTF8.GetString(reply.Data)));
                c.Publish("openrmf.system.compliance", Encoding.UTF8.GetBytes(system));
                c.Flush();
                c.Close();
                return arts;
            }
            // publish that we just ran a compliance update
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
            // add the options for the server, reconnecting, and the handler events
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.MaxReconnect = -1;
            opts.ReconnectWait = 1000;
            opts.Name = "openrmf-api-compliance";
            opts.Url = Environment.GetEnvironmentVariable("NATSSERVERURL");
            opts.AsyncErrorEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("NATS client error. Server: {0}. Message: {1}. Subject: {2}", events.Conn.ConnectedUrl, events.Error, events.Subscription.Subject));
            };

            opts.ServerDiscoveredEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("A new server has joined the cluster: {0}", events.Conn.DiscoveredServers));
            };

            opts.ClosedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Closed: {0}", events.Conn.ConnectedUrl));
            };

            opts.ReconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Reconnected: {0}", events.Conn.ConnectedUrl));
            };

            opts.DisconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Disconnected: {0}", events.Conn.ConnectedUrl));
            };
            
            // Creates a live connection to the NATS Server with the above options
            IConnection c = cf.CreateConnection(opts);

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
            // add the options for the server, reconnecting, and the handler events
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.MaxReconnect = -1;
            opts.ReconnectWait = 1000;
            opts.Name = "openrmf-api-compliance";
            opts.Url = Environment.GetEnvironmentVariable("NATSSERVERURL");
            opts.AsyncErrorEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("NATS client error. Server: {0}. Message: {1}. Subject: {2}", events.Conn.ConnectedUrl, events.Error, events.Subscription.Subject));
            };

            opts.ServerDiscoveredEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("A new server has joined the cluster: {0}", events.Conn.DiscoveredServers));
            };

            opts.ClosedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Closed: {0}", events.Conn.ConnectedUrl));
            };

            opts.ReconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Reconnected: {0}", events.Conn.ConnectedUrl));
            };

            opts.DisconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Disconnected: {0}", events.Conn.ConnectedUrl));
            };
            
            // Creates a live connection to the NATS Server with the above options
            IConnection c = cf.CreateConnection(opts);

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
            // add the options for the server, reconnecting, and the handler events
            Options opts = ConnectionFactory.GetDefaultOptions();
            opts.MaxReconnect = -1;
            opts.ReconnectWait = 1000;
            opts.Name = "openrmf-api-compliance";
            opts.Url = Environment.GetEnvironmentVariable("NATSSERVERURL");
            opts.AsyncErrorEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("NATS client error. Server: {0}. Message: {1}. Subject: {2}", events.Conn.ConnectedUrl, events.Error, events.Subscription.Subject));
            };

            opts.ServerDiscoveredEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("A new server has joined the cluster: {0}", events.Conn.DiscoveredServers));
            };

            opts.ClosedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Closed: {0}", events.Conn.ConnectedUrl));
            };

            opts.ReconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Reconnected: {0}", events.Conn.ConnectedUrl));
            };

            opts.DisconnectedEventHandler += (sender, events) =>
            {
                Console.WriteLine(string.Format("Connection Disconnected: {0}", events.Conn.ConnectedUrl));
            };
            
            // Creates a live connection to the NATS Server with the above options
            IConnection c = cf.CreateConnection(opts);
            
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
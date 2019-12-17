using GraphQL.Client;
using GraphQL.Common.Request;
using HandPApis_GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Data;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace HandPApis_GraphQL
{
    public class OwnerConsumer
    {
        private readonly GraphQLClient _client;

        public OwnerConsumer(GraphQLClient client)
        {
            _client = client;
        }

        public async Task<List<Appointment>> GetAppointments()
        {
            _client.DefaultRequestHeaders.Add("Authorization", "SFMyNTY.g3QAAAACZAAEZGF0YW0AAAAHNTA6MTgxOGQABnNpZ25lZG4GAHI8YElsAQ.1t0AfDf1vU1u4DV4rbv4j1VYa7WHaIw3kyHG2B8jm38");
            var query = new GraphQLRequest
            {
                Query = @"query getAppointments 
                        { 
                            appointments 
                            { 
                                start 
                                insertedAt 
                                type 
                                { 
                                    id 
                                    encounterType 
                                    { 
                                        id 
                                        name 
                                    }
                                    name 
                                } 
                                status 
                                location 
                                { 
                                    name 
                                } 
                                provider 
                                { 
                                    isActive 
                                    name 
                                } 
                                patient 
                                { 
                                    clientId
                                } 
                            }
                        }"
            };
            try
            {
                var response = await _client.PostAsync(query);
                if (response.Errors?.Any() ?? false)
                {
                    var unprocessedIds = response.Errors.Select(x => x.AdditonalEntries)
                        .Where(y => y.ContainsKey("extensions")).Where(z => z.ContainsKey("unprocessedIds"));
                }
                // to get the json string in data set
                //string jsonstr = JsonConvert.SerializeObject(response);

                List<Appointment> result = response.GetDataFieldAs<List<Appointment>>("appointments");

                //string jsonstr = JsonConvert.SerializeObject(response);

                Dictionary<TestClass, int> output = new Dictionary<TestClass, int>();
                foreach (Appointment app in result)
                {
                    if (app.Start.HasValue && app.Type != null && app.Location!=null)
                    {
                        //service type
                        string serviceType = app.Type.Name.ToLower().Contains("care") || app.Type.Name.ToLower().Contains("play") ? app.Type.Name : "Style";

                        TestClass test = new TestClass(app.Start.Value.Date, serviceType, app.Location.Name);
                        if (!output.ContainsKey(test))
                            output.Add(test, 1);
                        else
                            output[test] = output[test] + 1;
       
                        // Aggregation - Total Care
                        if(serviceType.ToLower().Contains("care"))
                        {
                            TestClass totalCare = new TestClass(app.Start.Value.Date, "Total Care", app.Location.Name);
                            if (!output.ContainsKey(totalCare))
                                output.Add(totalCare, 1);
                            else
                                output[totalCare] = output[totalCare]+1;
                        }

                        // Aggregation - Total Appointments
                        TestClass totalAppointments = new TestClass(app.Start.Value.Date, "Total Appointments", app.Location.Name);
                        if (!output.ContainsKey(totalAppointments))
                            output.Add(totalAppointments, 1);
                        else
                            output[totalAppointments] = output[totalAppointments] + 1;
                    }
                }

                output.OrderBy(x => x.Key.Location);
                Debug.WriteLine(output.Count);
                foreach (KeyValuePair<TestClass, int> kvp in output.OrderBy(x => x.Key.Location))
                {
                    Debug.WriteLine("{0}, -> {1}", kvp.Key, output[kvp.Key].ToString(), kvp.Value);
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

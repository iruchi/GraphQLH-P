using GraphQL.Client;
using GraphQL.Common.Request;
using HandPApis_GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;

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
                var result =  response.GetDataFieldAs<List<Appointment>>("appointments");
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }     
        }
    }
}

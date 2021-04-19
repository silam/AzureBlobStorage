using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace cosmos_metrics
{
    class Program
    {
        static string cosmosdb_connection = "AccountEndpoint=https://az204cosmodb2021.documents.azure.com:443/;AccountKey=9LXNSvchdo5bZnrQDt37DSKtOkE8xyzVOriySHcBDQLCEhPxQJhvahJX3Rf6MPZCxZFPMN7t4ssIKmn8p4fVYQ==;";
       
        
        static string database = "az204cosmo";
        static string containername = "monitor";
        static string endpoint = "https://az204cosmodb2021.documents.azure.com:443/";
        static string accountkeys = "9LXNSvchdo5bZnrQDt37DSKtOkE8xyzVOriySHcBDQLCEhPxQJhvahJX3Rf6MPZCxZFPMN7t4ssIKmn8p4fVYQ==";


        static void Main(string[] args)
        {
            ReadItem().Wait();
            Console.ReadLine();
        }

        static async Task ReadItem()
        {

            CosmosClient cosmosClient = new CosmosClient(
            cosmosdb_connection,
            new CosmosClientOptions()
            {
                ApplicationRegion = Regions.EastUS2
            });

            Database db_conn = cosmosClient.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);
                                                
                string id = "71628e8d-1ee7-466b-bd78-b19660bdbacc";
                PartitionKey key =new PartitionKey("Administrative");
                ItemResponse<dynamic> p_response = await container_conn.ReadItemAsync<dynamic>(id,key);

                
                string output_string = JValue.Parse(p_response.Diagnostics.ToString()).ToString(Formatting.Indented);
                Console.WriteLine(output_string);
                Console.WriteLine("Operation complete");
                }
            }
    }


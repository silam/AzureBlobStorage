using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace cosmos_metrics
{
    class Program
    {
        
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
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {
                
                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);
                                                
                string id = "7f267444-67be-42ab-9e8e-60de5f7719ed";
                PartitionKey key=new PartitionKey("Administrative");
                ItemResponse<dynamic> p_response = await container_conn.ReadItemAsync<dynamic>(id,key);

                
                string output_string = JValue.Parse(p_response.Diagnostics.ToString()).ToString(Formatting.Indented);
                Console.WriteLine(output_string);
                Console.WriteLine("Operation complete");
                }
            }
    }
}

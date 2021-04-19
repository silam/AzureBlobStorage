using Microsoft.Azure.Cosmos;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace cosmosdb___sp
{
    class Program
    {

        static string database = "az204cosmo";
        static string containername = "customer";
        static string endpoint = "https://az204cosmodb2021.documents.azure.com:443/";
        static string accountkeys = "9LXNSvchdo5bZnrQDt37DSKtOkE8xyzVOriySHcBDQLCEhPxQJhvahJX3Rf6MPZCxZFPMN7t4ssIKmn8p4fVYQ==";

        static void Main(string[] args)
        {
            //CallSP();
            SP_CreateItem().Wait();
            Console.ReadLine();
        }

        private static async Task CallSP()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                var stored_procedures = container_conn.Scripts;

                PartitionKey key = new PartitionKey(string.Empty);

                var response = await stored_procedures.ExecuteStoredProcedureAsync<string>("demo", key, null);

                Console.WriteLine(response.Resource);
            }
            }

        private static async Task SP_CreateItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                var stored_procedures = container_conn.Scripts;
                        

                dynamic[] obj = new dynamic[]
                {
                    new {
                    id = Guid.NewGuid().ToString(),
                    customerid = 6,
                    customername = "James",
                    city = "Miami"}
                };

               PartitionKey key = new PartitionKey(obj[0].city);

                var response = await stored_procedures.ExecuteStoredProcedureAsync<string>("CreateItem", key,obj);
                Console.WriteLine(response.Resource);
                Console.WriteLine("Item added");

            }
        }
        }
}

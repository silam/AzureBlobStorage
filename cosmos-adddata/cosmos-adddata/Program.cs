using LumenWorks.Framework.IO.Csv;
using Microsoft.Azure.Cosmos;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace cosmos_adddata
{
    class Program
    {
        static DataTable dt_table;

        static string database = "az204cosmo";
        static string containername = "monitor";
        static string endpoint = "https://az204cosmodb2021.documents.azure.com:443/";
        static string accountkeys = "9LXNSvchdo5bZnrQDt37DSKtOkE8xyzVOriySHcBDQLCEhPxQJhvahJX3Rf6MPZCxZFPMN7t4ssIKmn8p4fVYQ==";


        static async Task Main(string[] args)
        {
            LoadData();
           CreateNewItem().Wait();
            ReadItem().Wait();
            Console.WriteLine("Operation complete");
            Console.ReadLine();
        }

        private static void LoadData()
        {
            dt_table = new DataTable();
            using (var csvReader = new CsvReader(new StreamReader(System.IO.File.OpenRead("QueryResult.csv")), true))
            {
                dt_table.Load(csvReader);
            }


        }
        private static async Task CreateNewItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                foreach (DataRow row in dt_table.Rows)
                {
                    ActivityData obj = new ActivityData();
                    obj.Correlationid = row[0].ToString();
                    obj.Operationname = row[1].ToString();
                    obj.status = row[2].ToString();
                    obj.EventCategory = row[3].ToString();
                    obj.Level = row[4].ToString();
                    obj.dttime = DateTime.Parse(row[5].ToString());
                    Console.WriteLine(obj.dttime);
                    obj.subscription = row[6].ToString();
                    obj.InitiatedBy = row[7].ToString();
                    obj.resourcetype = row[8].ToString();
                    obj.resourcegroup = row[9].ToString();
                    obj.resource = row[10].ToString();
                    obj.id = Guid.NewGuid().ToString();
                    ItemResponse<ActivityData> response = await container_conn.CreateItemAsync(obj);
                }
            }
        }
        private static async Task ReadItem()
        {
            using (CosmosClient cosmos_client = new CosmosClient(endpoint, accountkeys))
            {

                Database db_conn = cosmos_client.GetDatabase(database);

                Container container_conn = db_conn.GetContainer(containername);

                QueryDefinition query =
                    new QueryDefinition("select c.Operationname, c.Level from c where c.EventCategory = @category")
                    .WithParameter("@category", "Administrative");

                
                while (true) { 
                    FeedIterator<dynamic> iterator_obj = container_conn.GetItemQueryIterator<dynamic>(query);
                    while (iterator_obj.HasMoreResults)
                    {
                        FeedResponse<dynamic> activity_obj = await iterator_obj.ReadNextAsync();
                        foreach (var obj in activity_obj)
                        {
                            Console.WriteLine("operation name = {0}", obj["Operationname"]);
                        }
                    }
                }
            }
        }
    }
}
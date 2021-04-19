using Microsoft.Azure.Cosmos.Table;
using System;
using System.Threading.Tasks;

namespace cosmos_table
{
    class Program
    {
        static string connection_string= "DefaultEndpointsProtocol=https;AccountName=apptable2000;AccountKey=d4F8ADURL0CiIArwNN2sYcqx8sxnJ6mXLKS8MlJKgTuNFfGyCvQttNd5LjnJm1RUiUBxcGH8ZFECv3GkR9HOrw==;TableEndpoint=https://apptable2000.table.cosmos.azure.com:443/;";
        static void Main(string[] args)
        {
            //NewItem().Wait();
            //ReadItem().Wait();
            //UpdateItem().Wait();
            DeleteItem().Wait();
            Console.ReadLine();
        }

        static async Task NewItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);
            
            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();
            
            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            Customer obj = new Customer("2", "James", "New York");
            TableOperation p_operation = TableOperation.Insert(obj);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Console.WriteLine("Entity added");
        }

        static async Task ReadItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "1";
            string rowkey = "John";

            TableOperation p_operation = TableOperation.Retrieve<Customer>(partition_key, rowkey);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Customer return_obj = (Customer)response.Result;

            Console.WriteLine("Customer ID is {0}", return_obj.PartitionKey);
            Console.WriteLine("Customer Name is {0}", return_obj.RowKey);
            Console.WriteLine("Customer City is {0}", return_obj.city);


        }

        static async Task UpdateItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "1";
            string rowkey = "John";

            Customer updated_obj = new Customer(partition_key, rowkey, "Chicago");

            TableOperation p_operation = TableOperation.InsertOrReplace(updated_obj);
            TableResult response = await p_table.ExecuteAsync(p_operation);
            Console.WriteLine("Entity updated");

        }

        static async Task DeleteItem()
        {
            CloudStorageAccount p_account = CloudStorageAccount.Parse(connection_string);

            CloudTableClient p_tableclient = p_account.CreateCloudTableClient();

            CloudTable p_table = p_tableclient.GetTableReference("Customer");

            string partition_key = "1";
            string rowkey = "John";

            TableOperation p_operation = TableOperation.Retrieve<Customer>(partition_key, rowkey);
            TableResult response = await p_table.ExecuteAsync(p_operation);

            Customer return_obj = (Customer)response.Result;


            TableOperation p_delete = TableOperation.Delete(return_obj);

            response = await p_table.ExecuteAsync(p_delete);
            Console.WriteLine("Entity deleted");

        }
    }
    }

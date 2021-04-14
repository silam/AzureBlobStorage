using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blobdetails
{
    class Program
    {
        static string connstring = "DefaultEndpointsProtocol=https;AccountName=demostore2020;AccountKey=6HnNV0qvUoTTJkEiv5e0oJ0iOlsCWOVtqWo2XGpoepXCfJSMxtQbU+9DOjVPP8NGwiK/zbepL4moGFfKdTxn2A==;EndpointSuffix=core.windows.net";
        static void Main(string[] args)
        {
            WorkwithBlob().GetAwaiter().GetResult();
            Console.ReadKey();

        }
        private static async Task WorkwithBlob()
        {
            CloudStorageAccount l_storageAccount;
            if (CloudStorageAccount.TryParse(connstring, out l_storageAccount))
            {
                CloudBlobClient l_cloudBlobClient = l_storageAccount.CreateCloudBlobClient();

                CloudBlobContainer l_cloudBlobContainer =
                l_cloudBlobClient.GetContainerReference("demo");

                
                var l_blockBlob = l_cloudBlobContainer.GetBlockBlobReference("audio.log");
                TimeSpan? l_leaseTime = TimeSpan.FromSeconds(60);
                string leaseID = l_blockBlob.AcquireLease(l_leaseTime, null);
                l_blockBlob.FetchAttributes();
                Console.WriteLine("The lease state is " + l_blockBlob.Properties.LeaseState);
                Console.WriteLine("The lease duration is " + l_blockBlob.Properties.LeaseDuration);
            }
        }
    }
}

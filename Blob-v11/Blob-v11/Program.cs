using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Data;
using System.Threading.Tasks;

namespace Blob_v11
{
    class Program
    {
        static string _conenction_string = "DefaultEndpointsProtocol=https;AccountName=demostore40000;AccountKey=oKSUsm+5HsyIVxORQAZoWqYvxOVR81F3ZCowrNe/gUvVl409q8E2TMQtZlycuN9fI/OxVBu/qKDhNNSQd09LZw==;EndpointSuffix=core.windows.net";
        static string _container_name = "datav11";
        static string _filename = "sample.txt";
        static string _filelocation = "C:\\Work\\sample.txt";
        static void Main(string[] args)
        {
            //CreateContainer().Wait();
            //UploadBlob().Wait();
            //ListBlobs().Wait();
            GetBlob().Wait();
            Console.WriteLine("Completed");
        }

        static async Task CreateContainer()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);
                await _container.CreateAsync();

            }
        }

        static async Task UploadBlob()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);

                CloudBlockBlob _blob = _container.GetBlockBlobReference(_filename);
                await _blob.UploadFromFileAsync(_filelocation);
            }
        }
        static async Task ListBlobs()
        {
            CloudStorageAccount _storageAccount;
            if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
            {
                CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                CloudBlobContainer _container = _client.GetContainerReference(_container_name);

                BlobContinuationToken _blobContinuationToken = null;
                do
                {
                    var _results = await _container.ListBlobsSegmentedAsync(null, _blobContinuationToken);

                    _blobContinuationToken = _results.ContinuationToken;

                    foreach (IListBlobItem _blob in _results.Results)
                    {
                        Console.WriteLine(_blob.Uri);
                    }
                }
                while (_blobContinuationToken != null);
            }
        }

            static async Task GetBlob()
            {
                CloudStorageAccount _storageAccount;
                if (CloudStorageAccount.TryParse(_conenction_string, out _storageAccount))
                {
                    CloudBlobClient _client = _storageAccount.CreateCloudBlobClient();
                    CloudBlobContainer _container = _client.GetContainerReference(_container_name);
                    
                    CloudBlockBlob _blob = _container.GetBlockBlobReference(_filename);
                    string str=await _blob.DownloadTextAsync();
                    Console.WriteLine(str);
                }
                }
            }
        }


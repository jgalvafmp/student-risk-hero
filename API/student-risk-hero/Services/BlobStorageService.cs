using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using student_risk_hero.Contracts;

namespace student_risk_hero.Services
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly string connectionString;
        private readonly string containerName;

        public BlobStorageService(IConfiguration config)
        {
            connectionString = config.GetValue<string>("StorageSettings:StorageConnection");
            containerName = config.GetValue<string>("StorageSettings:ContainerName");
        }

        public List<string> GetAllDocuments()
        {
            var container = BlobExtensions.GetContainer(connectionString, containerName);

            if (!container.Exists())
            {
                return new List<string>();
            }

            List<string> blobs = new();

            foreach (BlobItem blobItem in container.GetBlobs())
            {
                blobs.Add(blobItem.Name);
            }
            return blobs;
        }

        public void UploadDocument(string fileName, Stream fileContent)
        {
            var container = BlobExtensions.GetContainer(connectionString, containerName);

            if (!container.Exists())
            {
                BlobServiceClient blobServiceClient = new(connectionString);
                blobServiceClient.CreateBlobContainer(containerName);
                container = blobServiceClient.GetBlobContainerClient(containerName);
            }

            var bobclient = container.GetBlobClient(fileName);
            if (!bobclient.Exists())
            {
                fileContent.Position = 0;
                container.UploadBlob(fileName, fileContent);
            }
            else
            {
                fileContent.Position = 0;
                bobclient.Upload(fileContent, overwrite: true);
            }
        }

        public Stream GetDocument(string fileName)
        {
            var container = BlobExtensions.GetContainer(connectionString, containerName);
            if (container.Exists())
            {
                var blobClient = container.GetBlobClient(fileName);
                if (blobClient.Exists())
                {
                    var content = blobClient.DownloadStreaming();
                    return content.Value.Content;
                }
                else
                {
                    throw new FileNotFoundException();
                }
            }
            else
            {
                throw new FileNotFoundException();
            }

        }

        public bool DeleteDocument(string fileName)
        {
            var container = BlobExtensions.GetContainer(connectionString, containerName);
            if (!container.Exists())
            {
                return false;
            }

            var blobClient = container.GetBlobClient(fileName);

            if (blobClient.Exists())
            {
                blobClient.DeleteIfExists();
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public static class BlobExtensions
    {
        public static BlobContainerClient GetContainer(string connectionString, string containerName)
        {
            BlobServiceClient blobServiceClient = new(connectionString);
            return blobServiceClient.GetBlobContainerClient(containerName);
        }
    }
}

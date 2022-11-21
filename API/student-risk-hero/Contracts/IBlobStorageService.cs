using Azure.Storage.Blobs.Models;

namespace student_risk_hero.Contracts
{
    public interface IBlobStorageService
    {
        public List<string> GetAllDocuments();
        void UploadDocument(string fileName, Stream fileContent);
        Stream GetDocument(string fileName);
        bool DeleteDocument(string fileName);
    }
}

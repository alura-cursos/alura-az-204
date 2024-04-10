using System.IO;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace BlobFunctionApp
{
    public class FunctionBlobTrigger
    {
        private readonly ILogger<FunctionBlobTrigger> _logger;

        public FunctionBlobTrigger(ILogger<FunctionBlobTrigger> logger)
        {
            _logger = logger;
        }

        [Function(nameof(FunctionBlobTrigger))]
        public void Run([BlobTrigger("dados/{name}", Connection = "blobConnection")] string blob, string name)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetByteCount(blob));
            StreamWriter writer = new StreamWriter(stream, Encoding.UTF8, -1,true);
            writer.Write(blob);
            writer.Flush();
            stream.Position = 0;

            string message = $"Processamento do Blob:" +
                $"\n Name: {name}" +
                $"\n Size: {stream.Length} Bytes";

            _logger.LogInformation(message);

            StreamWriter writer1 = new StreamWriter(@"D:\Azure\az204\blob.txt", false);
            writer1.WriteLine(message);
            writer1.Close();
        }
    }
}

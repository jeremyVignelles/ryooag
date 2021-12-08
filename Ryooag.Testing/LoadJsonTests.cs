using NUnit.Framework;
using Ooak.SystemTextJson;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ryooag.Testing
{
    public class Tests
    {
        [Test]
        [TestCase("TestFiles/api-with-examples.json")]
        [TestCase("TestFiles/callback-example.json")]
        [TestCase("TestFiles/link-example.json")]
        [TestCase("TestFiles/petstore.json")]
        [TestCase("TestFiles/petstore-expanded.json")]
        [TestCase("TestFiles/uspto.json")]
        public async Task LoadDefinition(string fileName)
        {
            var fileContent = await File.ReadAllBytesAsync(fileName);
            JsonSerializer.Deserialize<OpenApiDocument>(fileContent, new JsonSerializerOptions
            {
                Converters = {
                    new AnyOfJsonConverter<OpenApiSchema, OpenApiReference>()// TODO: temp hack because it doesn't support nested converters
                }
            });
            Assert.Pass();
        }
    }
}

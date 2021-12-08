using NUnit.Framework;
using System;
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
            var document = JsonSerializer.Deserialize<OpenApiDocument>(fileContent)!;

            Console.WriteLine("Models:");
            foreach (var model in document.GetModels())
            {
                Console.WriteLine(model.name);
            }

            Console.WriteLine("Operations:");
            // Operations
            foreach (var (path, pathItem) in document.paths)
            {
                // Do something for each path item
                if(pathItem.get is not null)
                {
                    Console.WriteLine("GET " + path);
                }
                if(pathItem.put is not null)
                {
                    Console.WriteLine("PUT " + path);
                }
                if(pathItem.post is not null)
                {
                    Console.WriteLine("POST " + path);
                }
                if(pathItem.delete is not null)
                {
                    Console.WriteLine("DELETE " + path);
                }
                if(pathItem.options is not null)
                {
                    Console.WriteLine("OPTIONS " + path);
                }
                if(pathItem.head is not null)
                {
                    Console.WriteLine("HEAD " + path);
                }
                if(pathItem.patch is not null)
                {
                    Console.WriteLine("PATCH " + path);
                }
                if(pathItem.trace is not null)
                {
                    Console.WriteLine("TRACE " + path);
                }
            }
            Assert.Pass();
        }
    }
}

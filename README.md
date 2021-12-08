# Ryooag - Roll-Your-Own OpenApi Generator

Ryooag, (pronounced "re-wag") is a set of tools that allows you to parse [OpenApi documents](https://swagger.io/specification/) (formerly known as "swagger").

It can be used to can write your own code generators for the language/framework you like.


# Why ?

// TODO : a lot to say here


# How to use it ?

```cs
var fileContent = await File.ReadAllBytesAsync(fileName);
var document = JsonSerializer.Deserialize<OpenApiDocument>(fileContent)!;

// Models
foreach (var model in document.GetModels())
{
    Console.WriteLine(model.name);
}

// Operations
foreach (var (path, pathItem) in document.paths)
{
    // Do something for each path item
    if(pathItem.get is not null)
    {
        Console.WriteLine("GET " + path);
    }
    // ...
}
```

NOTE : helpers are expected to be added to Ryooag to help simplify the process of generating this kind of code

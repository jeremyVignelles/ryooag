namespace Ryooag;

public record OpenApiModelType(string name, OpenApiSchema schema);

public static class OpenApiDocumentExtensions
{
    public static IEnumerable<OpenApiModelType> GetSchemaComponentModels(this OpenApiDocument document)
    {
        var schemaComponents = document.components?.schemas;
        if (schemaComponents is not null)
        {
            foreach (var (key, value) in schemaComponents)
            {
                if (value.TryGetLeft(out var schema))
                {
                    yield return new OpenApiModelType(key, schema);
                }
            }
        }
    }

    public static IEnumerable<OpenApiModelType> GetResponseComponentModels(this OpenApiDocument document)
    {
        var responseComponents = document.components?.responses;
        if (responseComponents is not null)
        {
            foreach (var (responseKey, responseValue) in responseComponents)
            {
                if (responseValue.TryGetLeft(out var response))
                {
                    if (response.headers is not null)
                    {
                        foreach (var (headerKey, headerValue) in response.headers)
                        {
                            if (headerValue.TryGetLeft(out var header) && header.schema is not null && header.schema.TryGetLeft(out var schema))
                            {
                                yield return new OpenApiModelType(headerKey, schema);
                            }
                        }
                    }

                    if (response.content is not null)
                    {
                        foreach (var (contentKey, contentValue) in response.content)
                        {
                            if (contentValue.schema != null && contentValue.schema.TryGetLeft(out var schema))
                            {
                                yield return new OpenApiModelType(contentKey, schema);
                            }
                        }
                    }
                }
            }
        }
    }

    public static IEnumerable<OpenApiModelType> GetParameterComponentModels(this OpenApiDocument document)
    {
        var parameterComponents = document.components?.parameters;
        if (parameterComponents is not null)
        {
            foreach (var (parameterKey, parameterValue) in parameterComponents)
            {
                if (parameterValue.TryGetLeft(out var parameter))
                {
                    if (parameter.schema is not null && parameter.schema.TryGetLeft(out var schema))
                    {
                        yield return new OpenApiModelType(parameterKey, schema);
                    }

                    if (parameter.content is not null)
                    {
                        foreach (var (contentKey, contentValue) in parameter.content)
                        {
                            if (contentValue.schema != null && contentValue.schema.TryGetLeft(out var contentSchema))
                            {
                                yield return new OpenApiModelType(contentKey, contentSchema);
                            }
                        }
                    }
                }
            }
        }
    }

    public static IEnumerable<OpenApiModelType> GetModels(this OpenApiDocument document)
    {
        return document.GetSchemaComponentModels()
        .Union(document.GetResponseComponentModels())
        .Union(document.GetParameterComponentModels());
    }
}

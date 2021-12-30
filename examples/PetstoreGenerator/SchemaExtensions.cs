using Ooak;
using Ryooag;

namespace PetstoreGenerator;

/// <summary>
/// Extensions methods that allows to manipulate schema objects easily
/// </summary>
public static class SchemaExtensions
{
    /// <summary>
    /// Simple, non-bullet-proof method to get a flat list of all properties in a schema, specifically tailored for this sample project.
    /// </summary>
    /// <returns>Properties infos, like name, type, format, nullability...</returns>
    public static IEnumerable<SchemaPropertyInfo> GetAllProperties(this OpenApiSchema schema, OpenApiDocument document)
    {
        // sc: The petstore schema only have `type: "object"` and `allOf`, no need for other stuff
        if (schema.allOf is not null)
        {
            foreach (var child in schema.allOf)
            {
                if (child.GetOrResolveSchema(document) is { } childSchema)
                {
                    foreach (var childProp in childSchema.GetAllProperties(document))
                    {
                        yield return childProp;
                    }
                }
            }
        }

        if (schema.type == "object" && schema.properties is not null)
        {
            var requiredProps = schema.required ?? Array.Empty<string>();
            foreach (var (name, value) in schema.properties)
            {
                if (value.GetOrResolveSchema(document) is { } propSchema)
                {
                    var resolvedType = propSchema.type switch
                    {
                        "string" => "string",
                        "integer" =>
                            propSchema.format switch {
                                "int32" => "int",
                                "int64" => "long",
                                _ => throw new NotSupportedException($"Unsupported integer format {propSchema.format}") // sc: We don't support every possible type here
                            },
                        _ => throw new NotSupportedException($"Unsupported property type {propSchema.type}") // sc: We don't support every possible type here
                    };

                    var required = requiredProps.Contains(name);
                    yield return new SchemaPropertyInfo(name, resolvedType, required);
                }
            }
        }
    }

    /// <summary>
    /// Resolve internal schema reference inside the document.
    /// The schemaReference must begin with `#/components/schema/`
    /// </summary>
    /// <param name="document">The document to look into</param>
    /// <param name="schemaReference">The schema reference to find</param>
    /// <returns>The found reference</returns>
    public static OpenApiSchema? ResolveSchema(this OpenApiDocument document, string schemaReference)
    {
        var prefix = "#/components/schemas/";
        if (!schemaReference.StartsWith(prefix))
        {
            throw new NotSupportedException("invalid schema reference"); // sc: Only support schema reference, otherwise we'd need to support JSON Pointers https://datatracker.ietf.org/doc/html/draft-ietf-appsawg-json-pointer-04
        }

        if (document.components?.schemas?.TryGetValue(schemaReference[prefix.Length..], out var result) == true
            && result?.TryGetLeft(out var schema) == true)
        {
            return schema;
        }

        return null;
    }

    /// <summary>
    /// Gets the OpenApiSchema represented by this entity, either directly or through a schema reference
    /// </summary>
    /// <param name="schemaOrReference">The direct schema definition or the schema reference</param>
    /// <param name="document">The document used to resolve the references</param>
    /// <returns>The resolved schema</returns>
    public static OpenApiSchema? GetOrResolveSchema(this TypeUnion<OpenApiSchema, OpenApiReference> schemaOrReference, OpenApiDocument document)
    {
        if (schemaOrReference.TryGetLeft(out var schema))
        {
            return schema;
        }

        if (schemaOrReference.TryGetRight(out var reference))
        {
            return document.ResolveSchema(reference._ref);
        }

        return null;
    }
}

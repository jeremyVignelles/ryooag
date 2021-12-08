using Ooak;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ryooag.Internal;
using Ooak.SystemTextJson;

namespace Ryooag;

public record OpenApiDocument(
    string openapi,
    OpenApiInfo info,
    OpenApiServer[]? servers,
    Dictionary<string, OpenApiPathItem> paths,
    OpenApiComponents? components,
    Dictionary<string, string[]>? security,
    OpenApiTag[]? tags,
    OpenApiExternalDocumentation? externalDocs
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiInfo(
    string title,
    string? description,
    string? termsOfService,
    OpenApiContact? contact,
    OpenApiLicense? license,
    string version
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiContact(
    string? name,
    string? url,
    string? email
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiLicense(
    string name,
    string? url
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiServer(
    string url,
    string? description,
    Dictionary<string, OpenApiServerVariable>? variables
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiServerVariable(
    string[]? @enum,
    string @default,
    string? description
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiComponents(
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<OneOfOpenApiSchemaOpenApiReferenceConverter, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? schemas,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<OneOfOpenApiResponseOpenApiReferenceConverter, TypeUnion<OpenApiResponse, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiResponse, OpenApiReference>>? responses,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiParameter, OpenApiReference>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiParameter, OpenApiReference>>? parameters,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiExample, OpenApiReference>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiRequestBody, OpenApiReference>, TypeUnion<OpenApiRequestBody, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiRequestBody, OpenApiReference>>? requestBodies,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiHeader, OpenApiReference>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiSchema, OpenApiReference>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? securitySchemes,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiLink, OpenApiReference>, TypeUnion<OpenApiLink, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiLink, OpenApiReference>>? links,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<Dictionary<string, OpenApiPathItem>, OpenApiReference>, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>))]
    Dictionary<string, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>? callbacks
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiPathItem(
    string? summary,
    string? description,
    OpenApiOperation? get,
    OpenApiOperation? put,
    OpenApiOperation? post,
    OpenApiOperation? delete,
    OpenApiOperation? options,
    OpenApiOperation? head,
    OpenApiOperation? patch,
    OpenApiOperation? trace,
    OpenApiServer[]? servers,

    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<AnyOfJsonConverter<OpenApiParameter, OpenApiReference>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    TypeUnion<OpenApiParameter, OpenApiReference>[]? parameters,

    [property:JsonPropertyName("$ref")] string? _ref
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiOperation(
    string[]? tags,
    string? summary,
    string? description,
    OpenApiExternalDocumentation? externalDocs,
    string? operationId,

    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<AnyOfJsonConverter<OpenApiParameter, OpenApiReference>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    TypeUnion<OpenApiParameter, OpenApiReference>[]? parameters,

    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiRequestBody, OpenApiReference>))]
    TypeUnion<OpenApiRequestBody, OpenApiReference>? requestBody,
    Dictionary<string, OpenApiResponse> responses,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<Dictionary<string, OpenApiPathItem>, OpenApiReference>, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>))]
    Dictionary<string, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>? callbacks,
    bool? deprecated,
    Dictionary<string, string[]>[]? security,
    OpenApiServer[]? servers
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiExternalDocumentation(string? description, string url)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiParameter(
    string name,
    string @in,
    string? description,
    bool? required,
    bool? deprecated,
    bool? allowEmptyValue,
    string? style,
    bool? explode,
    bool? allowReserved,
    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiSchema, OpenApiReference>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema,
    JsonElement? example,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiExample, OpenApiReference>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples,
    Dictionary<string, OpenApiMediaType> content
    )
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiRequestBody(
    string? description,
    Dictionary<string, OpenApiMediaType> content,
    bool? required
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiMediaType(
    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiSchema, OpenApiReference>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema,
    JsonElement? example,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiExample, OpenApiReference>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples,
    Dictionary<string, OpenApiEncoding>? encoding
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiEncoding(
    string? contentType,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiHeader, OpenApiReference>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers,
    string? style,
    bool? explode,
    bool? allowReserved
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiResponse(
    string? description,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiHeader, OpenApiReference>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers,
    Dictionary<string, OpenApiMediaType>? content,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiLink, OpenApiReference>, TypeUnion<OpenApiLink, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiLink, OpenApiReference>>? links
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiExample(
    string? summary,
    string? description,
    JsonElement? value,
    string? externalValue
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiLink(
    string? operationRef,
    string? operationId,
    Dictionary<string, JsonElement>? parameters,
    JsonElement? requestBody,
    string? description,
    OpenApiServer? server
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiHeader(
    string? description,
    bool? required,
    bool? deprecated,
    bool? allowEmptyValue,
    string? style,
    bool? explode,
    bool? allowReserved,
    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiSchema, OpenApiReference>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema,
    JsonElement? example,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiExample, OpenApiReference>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples,
    Dictionary<string, OpenApiMediaType> content
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiTag(
    string name,
    string? description,
    OpenApiExternalDocumentation? externalDocs
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiReference([property: JsonPropertyName("$ref")] string _ref)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiSchema(
    string? title,
    string? description,
    string? type,
    string? format,
    int? multipleOf,
    double? maximum,
    bool? exclusiveMaximum,
    double? minimum,
    bool? exclusiveMinimum,
    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiSchema, OpenApiReference>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? items,
    int? maxLength,
    int? minLength,
    string? pattern,
    int? maxItems,
    int? minItems,
    bool? uniqueItems,
    int? maxProperties,
    int? minProperties,
    string[]? required,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<OpenApiSchema, OpenApiReference>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? properties,
    [property:JsonConverter(typeof(AnyOfJsonConverter<bool, TypeUnion<OpenApiSchema, OpenApiReference>>))]// TODO: nested items are supported ?
    TypeUnion<bool, TypeUnion<OpenApiSchema, OpenApiReference>>? additionalProperties,
    JsonElement[]? @enum,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<AnyOfJsonConverter<OpenApiSchema, OpenApiReference>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? allOf,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<AnyOfJsonConverter<OpenApiSchema, OpenApiReference>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? anyOf,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<AnyOfJsonConverter<OpenApiSchema, OpenApiReference>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? oneOf,
    [property:JsonConverter(typeof(AnyOfJsonConverter<OpenApiSchema, OpenApiReference>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? not,
    JsonElement? @default,
    bool? nullable,
    OpenApiDiscriminator? discriminator,
    bool? readOnly,
    bool? writeOnly,
    OpenApiXml? xml,
    OpenApiExternalDocumentation? externalDocs,
    JsonElement? example,
    bool? deprecated
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiDiscriminator(
    string propertyName,
    Dictionary<string, string>? mapping
);

public record OpenApiXml(
    string? name,
    string? @namespace,
    string? prefix,
    bool? attribute,
    bool? wrapped
)
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiSecurityScheme(
    string type,
    string? description,
    string? name,
    string? @in,
    string? scheme,
    string? bearerFormat,
    OpenApiOAuthFlows? flows,
    string? openIdConnectUrl
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiOAuthFlows(
    OpenApiOAuthFlow? @implicit,
    OpenApiOAuthFlow? password,
    OpenApiOAuthFlow? clientCredentials,
    OpenApiOAuthFlow? authorizationCode
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

public record OpenApiOAuthFlow(
    string? authorizationUrl,
    string? tokenUrl,
    string? refreshUrl,
    Dictionary<string, string>? scopes
)
{

    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
};

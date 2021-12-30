using Ooak;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ryooag.Internal;
using Ooak.SystemTextJson;

namespace Ryooag;

public record SchemaPartWithExtension
{
    [JsonExtensionData]
    public Dictionary<string, JsonElement>? extensions { get; init; }
}

public record OpenApiDocument(
    string openapi,
    OpenApiInfo info,
    Dictionary<string, OpenApiPathItem> paths,
    OpenApiServer[]? servers = null,
    OpenApiComponents? components = null,
    Dictionary<string, string[]>? security = null,
    OpenApiTag[]? tags = null,
    OpenApiExternalDocumentation? externalDocs = null
) : SchemaPartWithExtension;

public record OpenApiInfo(
    string title,
    string version,
    string? description = null,
    string? termsOfService = null,
    OpenApiContact? contact = null,
    OpenApiLicense? license = null
) : SchemaPartWithExtension;

public record OpenApiContact(
    string? name = null,
    string? url = null,
    string? email = null
) : SchemaPartWithExtension;

public record OpenApiLicense(
    string name,
    string? url = null
) : SchemaPartWithExtension;

public record OpenApiServer(
    string url,
    string? description = null,
    Dictionary<string, OpenApiServerVariable>? variables = null
) : SchemaPartWithExtension;

public record OpenApiServerVariable(
    string @default,
    string[]? @enum = null,
    string? description = null
) : SchemaPartWithExtension;

public record OpenApiComponents(
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? schemas = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiResponse>, TypeUnion<OpenApiResponse, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiResponse, OpenApiReference>>? responses = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiParameter>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiParameter, OpenApiReference>>? parameters = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiExample>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiRequestBody>, TypeUnion<OpenApiRequestBody, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiRequestBody, OpenApiReference>>? requestBodies = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiHeader>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? securitySchemes = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiLink>, TypeUnion<OpenApiLink, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiLink, OpenApiReference>>? links = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<Dictionary<string, OpenApiPathItem>, OpenApiReference>, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>))]
    Dictionary<string, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>? callbacks = null
) : SchemaPartWithExtension;

public record OpenApiPathItem(
    string? summary = null,
    string? description = null,
    OpenApiOperation? get = null,
    OpenApiOperation? put = null,
    OpenApiOperation? post = null,
    OpenApiOperation? delete = null,
    OpenApiOperation? options = null,
    OpenApiOperation? head = null,
    OpenApiOperation? patch = null,
    OpenApiOperation? trace = null,
    OpenApiServer[]? servers = null,

    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiParameter>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    TypeUnion<OpenApiParameter, OpenApiReference>[]? parameters = null,

    [property:JsonPropertyName("$ref")] string? _ref = null
) : SchemaPartWithExtension;

public record OpenApiOperation(
    Dictionary<string, OpenApiResponse> responses,
    string[]? tags = null,
    string? summary = null,
    string? description = null,
    OpenApiExternalDocumentation? externalDocs = null,
    string? operationId = null,
    
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiParameter>, TypeUnion<OpenApiParameter, OpenApiReference>>))]
    TypeUnion<OpenApiParameter, OpenApiReference>[]? parameters = null,

    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiRequestBody>))]
    TypeUnion<OpenApiRequestBody, OpenApiReference>? requestBody = null,

    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<AnyOfJsonConverter<Dictionary<string, OpenApiPathItem>, OpenApiReference>, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>))]
    Dictionary<string, TypeUnion<Dictionary<string, OpenApiPathItem>, OpenApiReference>>? callbacks = null, // TODO : Dictionary<string, OpenApiPathItem> is a "callback object" that can have extensions. Don't know how to tell that
    bool? deprecated = null,
    Dictionary<string, string[]>[]? security = null,
    OpenApiServer[]? servers = null
) : SchemaPartWithExtension;

public record OpenApiExternalDocumentation(string url, string? description = null) : SchemaPartWithExtension;

public record OpenApiParameter(
    string name,
    string @in,
    Dictionary<string, OpenApiMediaType> content,
    string? description = null,
    bool? required = null,
    bool? deprecated = null,
    bool? allowEmptyValue = null,
    string? style = null,
    bool? explode = null,
    bool? allowReserved = null,
    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiSchema>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema = null,
    JsonElement? example = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiExample>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples = null
    ) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiParameter() : this(string.Empty, string.Empty, new Dictionary<string, OpenApiMediaType>()) { }
}

public record OpenApiRequestBody(
    Dictionary<string, OpenApiMediaType> content,
    string? description = null,
    bool? required = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiRequestBody() : this(new Dictionary<string, OpenApiMediaType>()) { }
}

public record OpenApiMediaType(
    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiSchema>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema = null,
    JsonElement? example = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiExample>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples = null,
    Dictionary<string, OpenApiEncoding>? encoding = null
) : SchemaPartWithExtension;

public record OpenApiEncoding(
    string? contentType = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiHeader>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers = null,
    string? style = null,
    bool? explode = null,
    bool? allowReserved = null
) : SchemaPartWithExtension;

public record OpenApiResponse(
    string? description = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiHeader>, TypeUnion<OpenApiHeader, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiHeader, OpenApiReference>>? headers = null,
    Dictionary<string, OpenApiMediaType>? content = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiLink>, TypeUnion<OpenApiLink, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiLink, OpenApiReference>>? links = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiResponse() : this(null, null) { }
}

public record OpenApiExample(
    string? summary = null,
    string? description = null,
    JsonElement? value = null,
    string? externalValue = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiExample() : this(null, null) { }
}

public record OpenApiLink(
    string? operationRef = null,
    string? operationId = null,
    Dictionary<string, JsonElement>? parameters = null,
    JsonElement? requestBody = null,
    string? description = null,
    OpenApiServer? server = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiLink() : this(null, null) { }
}

public record OpenApiHeader(
    Dictionary<string, OpenApiMediaType> content,
    string? description = null,
    bool? required = null,
    bool? deprecated = null,
    bool? allowEmptyValue = null,
    string? style = null,
    bool? explode = null,
    bool? allowReserved = null,
    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiSchema>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? schema = null,
    JsonElement? example = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiExample>, TypeUnion<OpenApiExample, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiExample, OpenApiReference>>? examples = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiHeader() : this(new Dictionary<string, OpenApiMediaType>()) { }
}

public record OpenApiTag(
    string name,
    string? description = null,
    OpenApiExternalDocumentation? externalDocs = null
) : SchemaPartWithExtension;

public record OpenApiReference([property: JsonPropertyName("$ref")] string _ref) : SchemaPartWithExtension;

public record OpenApiSchema(
    string? title = null,
    string? description = null,
    string? type = null,
    string? format = null,
    int? multipleOf = null,
    double? maximum = null,
    bool? exclusiveMaximum = null,
    double? minimum = null,
    bool? exclusiveMinimum = null,
    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiSchema>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? items = null,
    int? maxLength = null,
    int? minLength = null,
    string? pattern = null,
    int? maxItems = null,
    int? minItems = null,
    bool? uniqueItems = null,
    int? maxProperties = null,
    int? minProperties = null,
    string[]? required = null,
    [property:JsonConverter(typeof(DictionarySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    Dictionary<string, TypeUnion<OpenApiSchema, OpenApiReference>>? properties = null,
    [property:JsonConverter(typeof(RecursiveAnyOfJsonConverter<bool, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<bool, TypeUnion<OpenApiSchema, OpenApiReference>>? additionalProperties = null,
    JsonElement[]? @enum = null,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? allOf = null,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? anyOf = null,
    [property:JsonConverter(typeof(ArraySystemTextJsonConverter<SchemaOrOpenApiReferenceConverter<OpenApiSchema>, TypeUnion<OpenApiSchema, OpenApiReference>>))]
    TypeUnion<OpenApiSchema, OpenApiReference>[]? oneOf = null,
    [property:JsonConverter(typeof(SchemaOrOpenApiReferenceConverter<OpenApiSchema>))]
    TypeUnion<OpenApiSchema, OpenApiReference>? not = null,
    JsonElement? @default = null,
    bool? nullable = null,
    OpenApiDiscriminator? discriminator = null,
    bool? readOnly = null,
    bool? writeOnly = null,
    OpenApiXml? xml = null,
    OpenApiExternalDocumentation? externalDocs = null,
    JsonElement? example = null,
    bool? deprecated = null
) : SchemaPartWithExtension
{
    // Hack to get a real default constructor for SchemaOrOpenApiReferenceConverter
    public OpenApiSchema(): this(null, null) { }
}

public record OpenApiDiscriminator(
    string propertyName,
    Dictionary<string, string>? mapping = null
);

public record OpenApiXml(
    string? name = null,
    string? @namespace = null,
    string? prefix = null,
    bool? attribute = null,
    bool? wrapped = null
) : SchemaPartWithExtension;

public record OpenApiSecurityScheme(
    string type,
    string? description = null,
    string? name = null,
    string? @in = null,
    string? scheme = null,
    string? bearerFormat = null,
    OpenApiOAuthFlows? flows = null,
    string? openIdConnectUrl = null
) : SchemaPartWithExtension;

public record OpenApiOAuthFlows(
    OpenApiOAuthFlow? @implicit = null,
    OpenApiOAuthFlow? password = null,
    OpenApiOAuthFlow? clientCredentials = null,
    OpenApiOAuthFlow? authorizationCode = null
) : SchemaPartWithExtension;

public record OpenApiOAuthFlow(
    string? authorizationUrl = null,
    string? tokenUrl = null,
    string? refreshUrl = null,
    Dictionary<string, string>? scopes = null
) : SchemaPartWithExtension;

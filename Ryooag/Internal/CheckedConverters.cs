using Ooak.SystemTextJson;

namespace Ryooag.Internal;

// Internal subclasses of converters that checks if deserialized values are actually valid
// because System.Text.Json says it properly deserialized a model even when no property was filled.
// So the JSON `{}` will be allowed in models like OpenApiResponse where all properties are optional

internal class OneOfOpenApiSchemaOpenApiReferenceConverter : OneOfJsonConverter<OpenApiSchema, OpenApiReference>
{
    protected override bool LeftIsValid(OpenApiSchema value) => value with { extensions = null } != new OpenApiSchema();
    protected override bool RightIsValid(OpenApiReference value) => value._ref is not null;
}

internal class OneOfOpenApiResponseOpenApiReferenceConverter : OneOfJsonConverter<OpenApiResponse, OpenApiReference>
{
    protected override bool LeftIsValid(OpenApiResponse value) => value with { extensions = null } != new OpenApiResponse();
    protected override bool RightIsValid(OpenApiReference value) => value._ref is not null;
}

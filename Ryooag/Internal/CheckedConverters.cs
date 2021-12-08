using Ooak.SystemTextJson;

namespace Ryooag.Internal;

// Internal subclasses of converters that checks if deserialized values are actually valid
// because System.Text.Json says it properly deserialized a model even when no property was filled.
// So the JSON `{}` will be allowed in models like OpenApiResponse where all properties are optional

internal class OneOfOpenApiSchemaOpenApiReferenceConverter : OneOfJsonConverter<OpenApiSchema, OpenApiReference>
{
    protected override bool LeftIsValid(OpenApiSchema value) => value.type is not null || value.allOf is not null || value.anyOf is not null || value.oneOf is not null || value.not is not null;
    protected override bool RightIsValid(OpenApiReference value) => value._ref is not null;
}

internal class OneOfOpenApiResponseOpenApiReferenceConverter : OneOfJsonConverter<OpenApiResponse, OpenApiReference>
{
    // TODO : rewrite this entierly. Every model should have its own validator...
    protected override bool LeftIsValid(OpenApiResponse value) => value.description is not null || value.headers is not null || value.content is not null || value.links is not null;
    protected override bool RightIsValid(OpenApiReference value) => value._ref is not null;
}
using Ooak.SystemTextJson;

namespace Ryooag.Internal;

// Internal subclasses of converters that checks if deserialized values are actually valid
// because System.Text.Json says it properly deserialized a model even when no property was filled.
// So the JSON `{}` will be allowed in models like OpenApiResponse where all properties are optional

internal class SchemaOrOpenApiReferenceConverter<TSchema> : OneOfJsonConverter<TSchema, OpenApiReference> where TSchema: SchemaPartWithExtension, new()
{
    protected override bool LeftIsValid(TSchema value) => value with { extensions = null } != new TSchema();
    protected override bool RightIsValid(OpenApiReference value) => value._ref is not null;
}

namespace PetstoreGenerator;

public record SchemaPropertyInfo(string name, string type, bool required)
{
    public string ToCSharp() => $"{this.type}{(this.required ? "" : "?")} {name}";
}

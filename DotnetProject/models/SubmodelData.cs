// SubmodelData.cs
namespace AasApi.Models
{
    public class SubmodelData
    {
        public string? ModelType { get; set; }
        public string? Kind { get; set; }
        public SemanticId? SemanticId { get; set; }
        public string? Id { get; set; }
        public string? IdShort { get; set; }
        public List<SubmodelElementCollection>? SubmodelElements { get; set; }
    }

    public class SemanticId
    {
        public List<Key>? Keys { get; set; }
        public string? Type { get; set; }
    }

    public class Key
    {
        public string? Type { get; set; }
        public string? Value { get; set; }
    }

    public class SubmodelElementCollection
    {
        public string? ModelType { get; set; }
        public string? IdShort { get; set; }
        public List<SubmodelElementCollectionValue>? Value { get; set; }
    }

    public class SubmodelElementCollectionValue
    {
        public string? ModelType { get; set; }
        public string? IdShort { get; set; }
        public List<Property>? Value { get; set; }
    }

    public class Property
    {
        public string? ModelType { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
        public string? IdShort { get; set; }
    }
}

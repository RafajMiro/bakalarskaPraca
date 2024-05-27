// AasData.cs
namespace AasApi.Models
{
    public class AasListResponse
    {
        public PagingMetadata? PagingMetadata { get; set; }
        public List<AasData>? Result { get; set; }
    }

    public class PagingMetadata
    {
        
    }
    public class AasData
    {
        public string? ModelType { get; set; }
        public AssetInformation? AssetInformation { get; set; }
        public List<SubmodelReference>? Submodels { get; set; }
        public string? Id { get; set; }
        public string? IdShort { get; set; }
    }

    public class AssetInformation
    {
        public string? AssetKind { get; set; }
        public string? GlobalAssetId { get; set; }
    }

    public class SubmodelReference
    {
        public List<Key>? Keys { get; set; }
        public string? Type { get; set; }
    }
    
}


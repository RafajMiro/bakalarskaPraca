// models/NodeData.cs
namespace OpcUaApi.Models
{
    public class NodeData
    {
        public string? NodeId { get; set; }
        public string? DisplayName { get; set; }
        public string? NodeClass { get; set; }
        public List<NodeData>? Children { get; set; }
    }
}
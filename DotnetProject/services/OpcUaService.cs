// services/OpcUaService.cs
using Opc.Ua;
using Opc.Ua.Client;
using OpcUaApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OpcUaApi.Services
{
    public class OpcUaService
    {
        private readonly ApplicationConfiguration _config;

        public OpcUaService()
        {
            _config = new ApplicationConfiguration
            {
                ApplicationName = "OPC UA Client",
                ApplicationType = ApplicationType.Client,
                SecurityConfiguration = new SecurityConfiguration { ApplicationCertificate = new CertificateIdentifier() },
                TransportConfigurations = new TransportConfigurationCollection(),
                TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 }
            };
        }

        private async Task<Session> CreateSession(string endpointUrl)
        {
            await _config.Validate(ApplicationType.Client);
            var endpoint = CoreClientUtils.SelectEndpoint(endpointUrl, false, 15000);
            return await Session.Create(_config, new ConfiguredEndpoint(null, endpoint, EndpointConfiguration.Create(_config)), false, "OPC UA Client", 60000, null, null);
        }

        public async Task<string> Connect(string endpointUrl)
        {
            try
            {
                await CreateSession(endpointUrl);
                return $"Connected to OPC UA Server at {endpointUrl}";
            }
            catch (Exception ex)
            {
                return $"Failed to connect: {ex.Message}";
            }
        }

        public async Task<List<NodeData>> BrowseNodes(string endpoint, string nodeId)
        {
            var session = await CreateSession(endpoint);
            var browseNodeId = string.IsNullOrEmpty(nodeId) ? ObjectIds.RootFolder : new NodeId(nodeId);

            var browseDescription = new BrowseDescription
            {
                NodeId = browseNodeId,
                BrowseDirection = BrowseDirection.Forward,
                ReferenceTypeId = ReferenceTypeIds.HierarchicalReferences,
                IncludeSubtypes = true,
                NodeClassMask = (uint)NodeClass.Object | (uint)NodeClass.Variable,
                ResultMask = (uint)BrowseResultMask.All
            };

            ReferenceDescriptionCollection references;
            byte[] continuationPoint;
            session.Browse(
                null,
                null,
                browseNodeId,
                0,
                BrowseDirection.Forward,
                ReferenceTypeIds.HierarchicalReferences,
                true,
                (uint)NodeClass.Object | (uint)NodeClass.Variable,
                out continuationPoint,
                out references
            );

            var nodeDataList = new HashSet<string>();
            var uniqueNodes = new List<NodeData>();
            foreach (var reference in references)
            {
                var nodeIdString = reference.NodeId.ToString();
                if (!nodeDataList.Contains(nodeIdString))
                {
                    nodeDataList.Add(nodeIdString);
                    uniqueNodes.Add(new NodeData
                    {
                        NodeId = nodeIdString,
                        DisplayName = reference.DisplayName.Text,
                        NodeClass = reference.NodeClass.ToString(),
                        Children = null
                    });
                }
            }
            return uniqueNodes;
        }
    }
}
using System.ComponentModel.DataAnnotations;

namespace IceGate_Demo.Entities
{
    public class IntegrationRequest
    {
        [Key]
        public int IntegrationRequestId { get; set; }
        [Key]
        public Guid AckId { get; set; }
        public string? RequestorId { get; set; }
        public char? RecordType { get; set; }
        public string? VoyageCallNumber { get; set; }
        public string? ModeOfTransport { get; set; }
        public string? TypeOfTransportMeans { get; set; }
        public string? IdentityOfTransportMeans { get; set; }
        public string? VesselCode { get; set; }
        public string? VoyageNumber { get; set; }
        public string? TypeOfVessel { get; set; }
        public string? PurposeOfCall { get; set; }
        public string? ShippingAgentCode { get; set; }
        public string? LineCode { get; set; }
        public string? TerminalOperatorCode { get; set; }
        public string? PortCode { get; set; }
        public DateTimeOffset? ExpectedDateTimeOfArrival { get; set; }
        public DateTimeOffset? ExpectedDateTimeOfDeparture { get; set; }
        public DateTimeOffset? RequestePostedDate { get; set; }
        public string? ServiceName { get; set; }
        public DateTimeOffset? AllotmentDate { get; set; }
        public string? ManifestNumberOrRotationNumber { get; set; }
        public DateTimeOffset? ManifestDateOrRotationDate { get; set; }
    }
    public class DemoIntegrationRequestJson
    {
        public IntegrationRequest? Text { get; set; }
    }
}

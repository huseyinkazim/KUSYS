namespace KUSYS.Api.Model
{
    public class ClaimInfo
    {
        public ClaimInfo(string type, string value)
        {
            Type = type;
            Value = value;
        }
        public string Type { get; init; }
        public string Value { get; init; }
    }
}

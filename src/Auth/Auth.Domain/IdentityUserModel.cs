using System.Text.Json.Serialization;

namespace Auth.Domain;
 
public class IdentityUserModel
{
    [JsonPropertyName("sub")]
    public string sub { get; set; }
    
    [JsonPropertyName("name")]
    public string name { get; set; }
    
    [JsonPropertyName("email")]
    public string email { get; set; }
    [JsonPropertyName("preferred_username")]
    public string preferred_username { get; set; }
    [JsonPropertyName("email_verified")]
    public string email_verified { get; set; }
}


namespace Auth.Domain;

[Serializable]
public class SecurityConfiguration
{
    public const string SecuritySectionName = "Identity";
    
    public string IdentityUrl { get; set; }
    public string AdminPolicyRole { get; set; }
    public string Audience { get; set; }
    public string AudienceDescription { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string AuthorizationUrl { get; set; }
    public string TokenUrl { get; set; }
    public string SwaggerClientId { get; set; }
}
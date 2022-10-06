namespace Document.Domain;

public class UserDocument
{
    public string UserId { get; set; } = "";

    public List<Document> Items { get; set; } = new List<Document>();

    public UserDocument()
    {

    }

    public UserDocument(string userId)
    {
        UserId = userId;
    }
}
using System.Text.Json.Serialization;

namespace ChatService.Domain.Models;

public class Chat
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonIgnore]public ICollection<Message> Messages { get; set; } 
}
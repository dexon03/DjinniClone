using Newtonsoft.Json;

namespace ChatService.Domain.Models;

public class User
{
    public Guid Id { get; init; }
    public string UserName { get; init; }
    public ICollection<Message> Messages { get; init; }
}

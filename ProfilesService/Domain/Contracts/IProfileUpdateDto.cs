namespace ProfilesService.Domain.Contracts;

public interface IProfileUpdateDto<T>
{
    public Guid Id { get; set; }
}
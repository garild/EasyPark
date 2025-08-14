namespace EasyPark.Domain;

public abstract class BaseEntity<TIdType> where TIdType: struct
{
    public required TIdType Id { get; init; }
    public required DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.UtcNow;
}
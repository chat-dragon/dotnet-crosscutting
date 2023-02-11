namespace CrossCutting.Domain.SeedWork;

public interface IAggregateRoot 
{
}

public interface IAggregateIntRoot : IAggregateRoot
{
    int Id { get; }
}

public interface IAggregateGuidRoot : IAggregateRoot
{
    Guid Uuid { get; }
}
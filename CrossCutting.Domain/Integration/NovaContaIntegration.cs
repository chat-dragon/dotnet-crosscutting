using CrossCutting.Domain.ValueObjects;

namespace CrossCutting.Domain.Integration;

public record NovaContaIntegration(Guid Id, string Username, string Nome, string Email, string? Celular, string? Nickname, string? TimeType)
{
    public Guid CorrelationId => new Guid("c7b4f743-1992-4be1-957e-0b987f8c5b91");
}

using CrossCutting.Domain.Extensions;
using CrossCutting.Domain.SeedWork;

namespace CrossCutting.Domain.ValueObjects;

public class Email : ValueObject
{
    private readonly string _address;

    public Email(string address)
    {
        AddNotifications(address, "Email inválido", $"Endreço de email {_address} inválido",
            ValidationExtensions.EmailEndereco(nameof(Address)));
        _address = address;
    }

    public string Address => _address;

    public static implicit operator Email(string email)
    {
        return new Email(email);
    }
    public override string ToString()
    {
        return _address;
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}

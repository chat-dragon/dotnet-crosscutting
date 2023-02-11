using CrossCutting.Domain.Extensions;
using CrossCutting.Domain.SeedWork;

namespace CrossCutting.Domain.ValueObjects;

public class Contato : ValueObject
{
    private readonly NumeroTelefone _telefone;
    private readonly NumeroTelefone _celular;
    private readonly NumeroTelefone? _whatsapp;
    private readonly string _nomeContato;

    public string NomeContato => _nomeContato;

    public NumeroTelefone Telefone => _telefone;

    private Contato() { }

    public Contato(string telefone, string celular, string nomeContato, bool celularWhatsapp = false)
    {
        _telefone = telefone;
        _celular = celular;
        _whatsapp = celularWhatsapp ? new(celular, true) : null;
        _nomeContato = nomeContato;
    }

    public Contato(string telefone, string celular, string whatsapp, string nomeContato)
    {
        _telefone = telefone;
        _celular = celular;
        _whatsapp = new(whatsapp, true);
        _nomeContato = nomeContato;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return NomeContato;
        yield return Telefone;
        yield return _celular;
        yield return _whatsapp ?? string.Empty;
    }

    public void ValidateTelefone()
    {
        AddNotifications(Telefone, "Telefone inválido", $"Telefone {Telefone} inválido", 
            ValidationExtensions.Telefone());
    }

    public void ValidateCelular()
    {
        AddNotifications(_celular, "Celular inválido", $"Celular {_celular} inválido", 
            ValidationExtensions.Celular());
        if(_whatsapp != null)
        {
            AddNotifications(_whatsapp, "Whatsapp inválido", $"Whatsapp {_whatsapp} inválido", 
                ValidationExtensions.Celular());
        }
    }
}


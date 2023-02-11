using CrossCutting.Domain.SeedWork;

namespace CrossCutting.Domain.ValueObjects;

public class Endereco : ValueObject
{
    public Endereco(string logradouro, string cidade, string uf, string pais, string cep, string numero)
    {
        Logradouro = logradouro;
        Cidade = cidade;
        UF = uf;
        Pais = pais;
        Cep = cep;
        Numero = numero;
    }

    public String Logradouro { get; }
    public String Numero { get; }
    public String Cidade { get; }
    public String UF { get; }
    public String Pais { get; }
    public String Cep { get; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        // Using a yield return statement to return each element one at a time
        yield return Logradouro;
        yield return Cidade;
        yield return UF;
        yield return Pais;
        yield return Cep;
    }
}


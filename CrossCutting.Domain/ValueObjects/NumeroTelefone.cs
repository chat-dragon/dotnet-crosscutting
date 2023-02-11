using CrossCutting.Domain.SeedWork;
using System.Text.RegularExpressions;

namespace CrossCutting.Domain.ValueObjects;

public class NumeroTelefone : ValueObject
{
    private readonly string _numero1 = String.Empty;
    private readonly string _numero2 = String.Empty;
    private readonly string _ddd = String.Empty;
    private readonly string _pais = String.Empty;
    private readonly bool _whatsapp = false;

    public NumeroTelefone(string telefone, bool whatsapp)
    {
        _whatsapp = whatsapp;

        string t = telefone.Trim();
        t = t.Replace("(", String.Empty).Replace(")", String.Empty).Replace("-", String.Empty);

        //Busca por um + seguido por dois números
        var matchPais = Regex.Match(t, @"(?:(^\+\d{2})?)"); // Código pais
        if(matchPais.Success) _pais = matchPais.Groups[1].Value;
        t = t[_pais.Length..]; // Removendo pais

        //Busca por dois números de 1 à 9, ou três números de 0 à 9, isso faz a diferença entre o DDD com zero e DDD sem zero, lembrando que nenhum código de cidade tem o digito 0.
        var matchDDD = Regex.Match(t, @"(?:([1-9]{2})|([0-9]{3})?)"); // DDD
        if (matchDDD.Success) _ddd = matchDDD.Groups[1].Value;
        t = t[_ddd.Length..]; // Removendo DDD

        //Segue buscando por dois grupos de números, um de 5 ou 4 digitos, que diferencia números de celular e telefones fixos (celulares tem o digito 9 a mais no começo), e os últimos números que são sempre 4 digitos segundo o padrão de números de telefones do Brasil.
        var matchNumero = Regex.Match(t, @"(\d{4,5})(\d{4})"); // Números
        if (matchNumero.Success)
        {
            _numero1 = matchNumero.Groups[1].Value;
            _numero2 = matchNumero.Groups[2].Value;
        }
    }

    public string Numero1 => _numero1;

    public string Numero2 => _numero2;

    public string DDD => _ddd;

    public string Pais => _pais;

    public bool IsWhatsapp => _whatsapp;

    public override string ToString()
    {
        string whatsPrefixo = _whatsapp ?
            "W" : string.Empty;
        return $"{whatsPrefixo}{_ddd}{_numero1}{_numero2}";
    }

    public static implicit operator NumeroTelefone(string telefone)
    {
        bool whatsapp = telefone.IndexOf('W', 0) >= 0;
        return new NumeroTelefone(telefone, whatsapp);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        throw new NotImplementedException();
    }
}

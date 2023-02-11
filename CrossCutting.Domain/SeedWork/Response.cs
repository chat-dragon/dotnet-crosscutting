using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CrossCutting.Domain.SeedWork;

public class Response
{
    private readonly IList<string> _messages = new List<string>();

    public IEnumerable<string> Errors { get; }

    [JsonProperty("resultado")]
    public object Resultado { get; set; }
    [JsonProperty("motivo")]
    public string Motivo { get; set; }
    [JsonProperty("motivoAdicional")]
    public string MotivoAdicional { get; set; }

    public Response() => Errors = new ReadOnlyCollection<string>(_messages);

    public Response(object resultado) : this() => Resultado = resultado;

    public Response AddError(string message)
    {
        _messages.Add(message);
        return this;
    }
}

using System.Net;

namespace CrossCutting.Api;

public record HttpError
{
    public HttpStatusCode Status { get; set; }
    public string Title { get; set; }
    public string Detail { get; set; }
    public string Documentation { get; set; }
    public string? MessageId { get; set; }
    public string? ConversationId { get; }
    public IEnumerable<HttpError>? Problems { get; set; }
    public HttpError(HttpStatusCode status, string title, string detail, string documentation, string? messageId, string? conversationId, IEnumerable<HttpError>? problems = null)
    {
        Status = status;
        Title = title;
        Detail = detail;
        Documentation = documentation;
        MessageId = messageId;
        ConversationId = conversationId;
        Problems = problems;
    }
}

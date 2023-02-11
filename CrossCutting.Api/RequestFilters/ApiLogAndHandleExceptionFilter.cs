using CrossCutting.Api;
using CrossCutting.Domain.Exceptions;
using CrossCutting.Domain.Support;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Net;

namespace DragonRoomPlatform.Application.Request.Filters;

public class ApiLogAndHandleExceptionFilter<T> : IFilter<ConsumeContext<T>>
    where T : class
{
    private readonly ILogger<ApiLogAndHandleExceptionFilter<T>> _logger;

    public ApiLogAndHandleExceptionFilter(ILogger<ApiLogAndHandleExceptionFilter<T>> logger)
    {
        _logger = logger;
    }

    public void Probe(ProbeContext context) { }

    public async Task Send(ConsumeContext<T> context, IPipe<ConsumeContext<T>> next)
    {
        string requestName = typeof(T).Name;
        try
        {
            _logger.LogInformation($"Inicio request {requestName} - Conversation Id: {context.ConversationId} - content: {context.Message.ToJson()}");
            await next.Send(context);
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogWarning($"Arquivo {ex.FileName} não encontrado - Request {requestName} - Conversation Id: {context.ConversationId}");
            await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<ApiLogAndHandleExceptionFilter<T>>.ShortName);
            await context.RespondAsync(new HttpError(HttpStatusCode.InternalServerError, "Error interno", String.Empty, String.Empty, context.MessageId?.ToString(), context.ConversationId?.ToString()));
        }
        catch (ConsumerCanceledException ex)
        {
            _logger.LogWarning($"Request {requestName} cancelado - {ex.Message} - Conversation Id: {context.ConversationId}");
            await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<ApiLogAndHandleExceptionFilter<T>>.ShortName);
            await context.RespondAsync(new HttpError(HttpStatusCode.InternalServerError, "Operação cancelada", String.Empty, String.Empty, context.MessageId?.ToString(), context.ConversationId?.ToString()));
        }
        catch (ValidateException ex)
        {
            _logger.LogWarning($"Request {requestName} inválido - Notificações: {ex.Notifications} - Conversation Id: {context.ConversationId}");
            await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<ApiLogAndHandleExceptionFilter<T>>.ShortName);
            await context.RespondAsync(new HttpError(HttpStatusCode.BadRequest, "Operação inválida", String.Empty, String.Empty, context.MessageId?.ToString(), context.ConversationId?.ToString()));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error request Conversation id: {0}", context.ConversationId);
            await context.NotifyConsumed(context.ReceiveContext.ElapsedTime, TypeCache<ApiLogAndHandleExceptionFilter<T>>.ShortName);
            await context.RespondAsync(new HttpError(HttpStatusCode.InternalServerError, "Erro interno", String.Empty, String.Empty, context.MessageId?.ToString(), context.ConversationId?.ToString()));
        }
        finally
        {
            _logger.LogInformation($"Fim request {requestName} - Conversation Id: {context.ConversationId}");
        }
    }
}

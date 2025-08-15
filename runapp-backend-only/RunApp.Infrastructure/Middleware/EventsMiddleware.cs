using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RunApp.Domain.Common;
using RunApp.Infrastructure.Common.Persistence;

namespace RunApp.Infrastructure.Middleware
{
    public class EventsMiddleware
    {
        private readonly RequestDelegate _next;

        public EventsMiddleware(RequestDelegate next) => _next = next;

        public async Task InvokeAsync(
            HttpContext httpContext,
            AppStoreDbContext appDbContext,
            IPublisher publisher,
            ILogger<EventsMiddleware> log)
        {
            // Execute the request first
            await _next(httpContext);

            // After the request has been handled, check if there are domain events
            if (!httpContext.Items.TryGetValue("DomainEvents", out var eventsObj)
                || eventsObj is not Queue<IDomainEvent> domainEvents
                || domainEvents.Count == 0)
            {
                return;
            }

            var ct = httpContext.RequestAborted;

            try
            {
                // Start a transaction only if we have events to publish
                using var transaction = await appDbContext.Database.BeginTransactionAsync(ct);

                while (domainEvents.TryDequeue(out var domainEvent))
                {
                    try
                    {
                        await publisher.Publish(domainEvent, ct);
                    }
                    catch (Exception exEvent)
                    {
                        // Log the event publication error and continue (or decide to abort)
                        log.LogError(exEvent, "Failed to publish domain event {@Event}", domainEvent);
                    }
                }

                await transaction.CommitAsync(ct);
            }
            catch (OperationCanceledException)
            {
                log.LogWarning("Event publishing cancelled (request aborted).");
            }
            catch (Exception ex)
            {
                // Log full exception so you can see it in Azure logs
                log.LogError(ex, "Failed to process domain events after request.");
            }
        }
    }
}


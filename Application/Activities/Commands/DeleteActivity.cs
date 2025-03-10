using System;
using System.Net;
using Application.Core;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class DeleteActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.Id], cancellationToken);
            
            if (activity == null)
            {
                return Result<Unit>.Failure("Activity not found", (int)HttpStatusCode.NotFound);
            }

            context.Activities.Remove(activity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;

            if (!result) return Result<Unit>.Failure("Fail to delete the activity", (int)HttpStatusCode.BadRequest);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

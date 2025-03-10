using System;
using System.Net;
using Application.Activities.DTOs;
using Application.Core;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Commands;

public class EditActivity
{
    public class Command : IRequest<Result<Unit>>
    {
        public required EditActivityDto EditActivityDto { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var activity = await context.Activities.FindAsync([request.EditActivityDto.Id], cancellationToken);

            if (activity == null)
            {
                return Result<Unit>.Failure("Activity not found", (int)HttpStatusCode.NotFound);
            }

            mapper.Map(request.EditActivityDto, activity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            
            if (!result) return Result<Unit>.Failure("Fail to update the activity", (int)HttpStatusCode.BadRequest);
            return Result<Unit>.Success(Unit.Value);
        }
    }
}

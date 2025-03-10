using System;
using System.Net;
using Application.Core;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Result<Activity>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Activity>>
    {
        public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
        {
            var data =  await context.Activities.FindAsync([request.Id], cancellationToken);

            if (data == null)
            {
                return Result<Activity>.Failure("Activity not found", (int)HttpStatusCode.NotFound);
            }

            return Result<Activity>.Success(data);
        }
    }
}

using System;
using System.Net;
using Application.Activities.DTOs;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Activities.Queries;

public class GetActivityDetails
{
    public class Query : IRequest<Result<ActivityDto>>
    {
        public required string Id { get; set; }
    }

    public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Query, Result<ActivityDto>>
    {
        public async Task<Result<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var data = await context.Activities
                        .ProjectTo<ActivityDto>(mapper.ConfigurationProvider,
                            new { currentUserId = userAccessor.GetUserId() })
                        .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (data == null)
            {
                return Result<ActivityDto>.Failure("Activity not found", (int)HttpStatusCode.NotFound);
            }

            return Result<ActivityDto>.Success(mapper.Map<ActivityDto>(data));
        }
    }
}

using System;
using System.Net;
using Application.Core;
using Application.Interfaces;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries;

public class GetProfile
{
    public class Query : IRequest<Result<UserProfile>>
    {
        public required string UserId { get; set; }
    }

    public class Handler(AppDbContext dbContext, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Query, Result<UserProfile>>
    {
        public async Task<Result<UserProfile>> Handle(Query request, CancellationToken cancellationToken)
        {
            var profile = await dbContext.Users
                .ProjectTo<UserProfile>(mapper.ConfigurationProvider,
                    new { currentUserId = userAccessor.GetUserId() })
                .FirstOrDefaultAsync(x => x.Id == request.UserId);

            return profile == null
                ? Result<UserProfile>.Failure("Profile not found", (int)HttpStatusCode.BadRequest)
                : Result<UserProfile>.Success(profile);
        }
    }
}

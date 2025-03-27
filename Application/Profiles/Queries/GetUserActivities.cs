using System;
using Application.Core;
using Application.Profiles.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries;

public class GetUserActivities
{
    public class Query : IRequest<Result<List<UserActivityDto>>>
    {
        public required string UserId { get; set; }
        public required string Filter { get; set; }
    }

    public class Handler(AppDbContext dbContext, IMapper mapper) : IRequestHandler<Query, Result<List<UserActivityDto>>>
    {
        public async Task<Result<List<UserActivityDto>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userActivities = dbContext.ActivityAttendees
                .Where(x => x.UserId == request.UserId)
                .OrderBy(x => x.Activity.Date)
                .Select(x => x.Activity)
                .AsQueryable();

            userActivities = request.Filter switch
            {
                "past" => userActivities.Where(x => x.Date <= DateTime.UtcNow && x.Attendees.Any(a => a.UserId == request.UserId)),
                "hosting" => userActivities.Where(x => x.Attendees.Any(a => a.UserId == request.UserId && a.IsHost)),
                _ => userActivities.Where(x => x.Date >= DateTime.UtcNow && x.Attendees.Any(a => a.UserId == request.UserId))
            };

            var projectedQuery = userActivities.ProjectTo<UserActivityDto>(mapper.ConfigurationProvider);
            var result = await projectedQuery.ToListAsync(cancellationToken);

            return Result<List<UserActivityDto>>.Success(result);
        }
    }
}

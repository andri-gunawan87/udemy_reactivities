using System;
using Application.Core;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles.Queries;

public class GetProfilePhotos
{
    public class Query : IRequest<Result<List<Photo>>>
    {
        public required string UserId { get; set; }
    }

    public class Handler(AppDbContext dbContext) : IRequestHandler<Query, Result<List<Photo>>>
    {
        public async Task<Result<List<Photo>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var photos = await dbContext.Users
                            .Where(x => x.Id == request.UserId)
                            .SelectMany(x => x.Photo)
                            .ToListAsync(cancellationToken);

            return Result<List<Photo>>.Success(photos);
        }
    }
}

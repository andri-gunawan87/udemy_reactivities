using System;
using System.Net;
using Application.Core;
using Application.Interfaces;
using Domain;
using MediatR;
using Persistence;

namespace Application.Profiles.Commands;

public class FollowToggle
{
    public class Command : IRequest<Result<Unit>>
    {
        public required string TargetUserId { get; set; }
    }

    public class Handler(AppDbContext dbContext, IUserAccessor userAccessor) : IRequestHandler<Command, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var observer = await userAccessor.GetUserAsync();
            var target = await dbContext.Users.FindAsync([request.TargetUserId], cancellationToken);

            if (target == null) return Result<Unit>.Failure("Target user not found", (int)HttpStatusCode.BadRequest);

            var following = await dbContext.UserFollowings
                .FindAsync([observer.Id, target.Id], cancellationToken);

            if (following == null)
            {
                dbContext.UserFollowings.Add(new UserFollowing
                {
                    ObeserverId = observer.Id,
                    TargetId = target.Id
                });
            }
            else
            {
                dbContext.UserFollowings.Remove(following);
            }

            var result = await dbContext.SaveChangesAsync() > 0;

            return result
                ? Result<Unit>.Success(Unit.Value)
                : Result<Unit>.Failure("Problem updating following", (int)HttpStatusCode.BadRequest);
        }
    }
}

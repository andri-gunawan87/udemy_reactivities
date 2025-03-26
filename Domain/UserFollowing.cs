using System;

namespace Domain;

public class UserFollowing
{
    public required string ObeserverId { get; set; }
    public User Oberserver { get; set; } = null!;
    public required string TargetId { get; set; }
    public User Target { get; set; } = null!;
}

﻿using System;

namespace Toggl.Multivac.Models
{
    public interface ITask : IIdentifiable, ILastChangeDatable
    {
        string Name { get; }

        long ProjectId { get; }

        long WorkspaceId { get; }

        long? UserId { get; }

        long EstimatedSeconds { get; }

        bool Active { get; }

        long TrackedSeconds { get; }
    }
}

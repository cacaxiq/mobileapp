﻿using Toggl.PrimeRadiant.Models;

namespace Toggl.PrimeRadiant
{
    public interface ITogglDatabase
    {
        IRepository<IDatabaseUser> User { get; }
        IRepository<IDatabaseClient> Clients { get; }
        IRepository<IDatabaseProject> Projects { get; }
        IRepository<IDatabaseTag> Tags { get; }
        IRepository<IDatabaseTask> Tasks { get; }
        IRepository<IDatabaseTimeEntry> TimeEntries { get; }
        IRepository<IDatabaseWorkspace> Workspaces { get; }
    }
}
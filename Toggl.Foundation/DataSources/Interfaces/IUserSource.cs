using System;
using Toggl.Foundation.DataSources.Interfaces;
using Toggl.Foundation.DTOs;
using Toggl.Foundation.Models.Interfaces;
using Toggl.PrimeRadiant.Models;

namespace Toggl.Foundation.DataSources
{
    public interface IUserSource : ISingletonDataSource<IThreadSafeUser, IDatabaseUser>
    {
        IObservable<IThreadSafeUser> UpdateWorkspace(long workspaceId);

        IObservable<IThreadSafeUser> Update(EditUserDTO dto);
    }
}

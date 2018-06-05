using System;
using System.Linq;
using System.Reactive.Linq;
using Toggl.Foundation.Exceptions;
using Toggl.Foundation.Sync.States.Pull;
using Toggl.Multivac;
using Toggl.Multivac.Models;

namespace Toggl.Foundation.Sync.States
{
    internal sealed class NoWorkspaceExceptionsCatchingPersistState : IPersistState
    {
        private readonly IPersistState internalState;

        public StateResult<IFetchObservables> FinishedPersisting { get; } = new StateResult<IFetchObservables>();

        public StateResult<Exception> Failed { get; } = new StateResult<Exception>();

        public NoWorkspaceExceptionsCatchingPersistState(IPersistState internalState)
        {
            Ensure.Argument.IsNotNull(internalState, nameof(internalState));

            this.internalState = internalState;
        }

        public IObservable<ITransition> Start(IFetchObservables fetch)
            => fetch.GetList<IWorkspace>()
                .SelectMany(workspaces => workspaces.Any() 
                    ? handlePresenceOfWorkspaces(fetch)
                    : Observable.Throw<ITransition>(new NoWorkspaceException()));

        private IObservable<ITransition> handlePresenceOfWorkspaces(IFetchObservables fetch)
            => internalState.Start(fetch)
                .Select(_ => FinishedPersisting.Transition(fetch))
                .Catch((Exception exception) => processError(exception));

        private IObservable<ITransition> processError(Exception exception)
            => Observable.Return(Failed.Transition(exception));
    }
}

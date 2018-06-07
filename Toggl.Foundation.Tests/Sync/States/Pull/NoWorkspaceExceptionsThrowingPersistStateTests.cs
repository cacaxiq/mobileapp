using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NSubstitute;
using Toggl.Foundation.Sync.States;
using Toggl.Foundation.Sync.States.Pull;
using Toggl.Foundation.Tests.Mocks;
using Toggl.Multivac.Models;
using Xunit;
using Toggl.Foundation.Exceptions;

namespace Toggl.Foundation.Tests.Sync.States.Pull
{
    public sealed class NoWorkspaceExceptionsThrowingPersistStateTests
    {
        private readonly IFetchObservables fetchObservables = Substitute.For<IFetchObservables>();

        private readonly IPersistState internalState = Substitute.For<IPersistState>();

        private readonly NoWorkspaceExceptionsThrowingPersistState state;

        public NoWorkspaceExceptionsThrowingPersistStateTests()
        {
            state = new NoWorkspaceExceptionsThrowingPersistState(internalState);
        }

        [Fact]
        public async Task ReturnsSuccessResultWhenWorkspacesArePresent()
        {
            var arrayWithWorkspace = new List<IWorkspace>(new[] { new MockWorkspace() });
            fetchObservables.GetList<IWorkspace>().Returns(Observable.Return<List<IWorkspace>>(arrayWithWorkspace));
            var transition = await state.Start(fetchObservables);

            transition.Result.Should().Be(state.FinishedPersisting);
        }

        [Fact]
        public async void ThrowsWhenExceptionsWhenNoWorkspacesAreAvailable()
        {
            Exception caughtException = null;

            var arrayWithWorkspace = new List<IWorkspace>();
            fetchObservables.GetList<IWorkspace>().Returns(Observable.Return<List<IWorkspace>>(arrayWithWorkspace));

            try
            {
                var transition = await state.Start(fetchObservables);
            }
            catch (Exception e)
            {
                caughtException = e;
            }

            caughtException.Should().NotBeNull();
            caughtException.Should().BeAssignableTo<NoWorkspaceException>();
        }
    }
}

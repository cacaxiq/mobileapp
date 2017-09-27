﻿using System;
using Xunit;
using FluentAssertions;
using Toggl.Foundation.Sync.ConflictResolution;
using Toggl.PrimeRadiant;

namespace Toggl.Foundation.Tests.Sync.ConflictResolution
{
    public sealed class AlwaysOverwriteTests
    {
        [Fact]
        public void ThrowsWhenIncomingEntityIsNull()
        {
            var existingEntity = new TestModel();

            Action resolving = () => resolver.Resolve(null, null);
            Action resolvingWithExistingLocalEntity = () => resolver.Resolve(existingEntity, null);

            resolving.ShouldThrow<ArgumentNullException>();
            resolvingWithExistingLocalEntity.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void UpdateWhenThereIsAnExistingEntityLocally()
        {
            var existingEntity = new TestModel();
            var incomingEntity = new TestModel();

            var mode = resolver.Resolve(existingEntity, incomingEntity);

            mode.Should().Be(ConflictResolutionMode.Update);
        }

        [Fact]
        public void CreateNewWhenThereIsNoExistingEntity()
        {
            var incomingEntity = new TestModel();

            var mode = resolver.Resolve(null, incomingEntity);

            mode.Should().Be(ConflictResolutionMode.Create);
        }

        private sealed class TestModel
        {
        }

        private AlwaysOverwrite<TestModel> resolver { get; }
            = new AlwaysOverwrite<TestModel>();
    }
}
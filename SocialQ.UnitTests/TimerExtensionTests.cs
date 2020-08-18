using System;
using System.Reactive.Subjects;
using FluentAssertions;
using Microsoft.Reactive.Testing;
using ReactiveUI.Testing;
using Xunit;

namespace SocialQ.UnitTests
{
    public class TimerExtensionTests
    {
        [Fact]
        public void RemainingTime()
        {
            // Given
            TimeSpan result = TimeSpan.Zero;
            var something = DateTimeOffset.Parse("2020-08-15").AddHours(2);
            var testScheduler = new TestScheduler();
            Subject<DateTimeOffset> fixture = new Subject<DateTimeOffset>();
            fixture.RemainingTime(testScheduler).Subscribe(x => result = x);

            // When
            fixture.OnNext(something);
            testScheduler.AdvanceByMs(2001);

            // Then
            result.Should().Be(TimeSpan.Zero);
        }
    }
}
﻿namespace Dawn.Tests
{
    using System;
    using Xunit;

    public sealed class EnumTests : BaseTests
    {
        [Theory(DisplayName = T + "Enum: Defined.")]
        [InlineData(null, null)]
        [InlineData(Colors.Red, Colors.All + 1)]
        public void Defined(Colors? defined, Colors? undefined)
        {
            var nullableDefinedArg = Guard.Argument(() => defined).Defined();
            var nullableUndefinedArg = Guard.Argument(() => undefined);
            if (defined.HasValue)
            {
                ThrowsArgumentException(
                    nullableUndefinedArg,
                    arg => arg.Defined(),
                    (arg, message) => arg.Defined(c =>
                    {
                        Assert.Equal(undefined, c);
                        return message;
                    }));

                var definedArg = Guard.Argument(defined.Value, nameof(defined)).Defined();
                var undefinedArg = Guard.Argument(undefined.Value, nameof(undefined));
                ThrowsArgumentException(
                    undefinedArg,
                    arg => arg.Defined(),
                    (arg, message) => arg.Defined(c =>
                    {
                        Assert.Equal(undefined, c);
                        return message;
                    }));
            }
        }

        [Theory(DisplayName = T + "Enum: HasFlag/DoesNotHaveFlag.")]
        [InlineData(null, Colors.All, Colors.All)]
        [InlineData(Colors.Red, Colors.None, Colors.Green)]
        [InlineData(Colors.Red | Colors.Green, Colors.Red | Colors.Green, Colors.Blue)]
        [InlineData(Colors.Red | Colors.Blue, Colors.Blue, Colors.Green)]
        public void HasFlag(Colors? value, Colors setFlags, Colors unsetFlags)
        {
            var nullableValueArg = Guard.Argument(() => value).HasFlag(setFlags).DoesNotHaveFlag(unsetFlags);
            if (value.HasValue)
            {
                ThrowsArgumentException(
                    nullableValueArg,
                    arg => arg.HasFlag(unsetFlags),
                    (arg, message) => arg.HasFlag(unsetFlags, (v, f) =>
                    {
                        Assert.Equal(value, v);
                        Assert.Equal(unsetFlags, f);
                        return message;
                    }));

                ThrowsArgumentException(
                    nullableValueArg,
                    arg => arg.DoesNotHaveFlag(setFlags),
                    (arg, message) => arg.DoesNotHaveFlag(setFlags, (v, f) =>
                    {
                        Assert.Equal(value, v);
                        Assert.Equal(setFlags, f);
                        return message;
                    }));

                var valueArg = Guard.Argument(value.Value, nameof(value)).HasFlag(setFlags).DoesNotHaveFlag(unsetFlags);
                ThrowsArgumentException(
                    valueArg,
                    arg => arg.HasFlag(unsetFlags),
                    (arg, message) => arg.HasFlag(unsetFlags, (v, f) =>
                    {
                        Assert.Equal(value, v);
                        Assert.Equal(unsetFlags, f);
                        return message;
                    }));

                ThrowsArgumentException(
                    valueArg,
                    arg => arg.DoesNotHaveFlag(setFlags),
                    (arg, message) => arg.DoesNotHaveFlag(setFlags, (v, f) =>
                    {
                        Assert.Equal(value, v);
                        Assert.Equal(setFlags, f);
                        return message;
                    }));
            }
        }

        [Flags]
        public enum Colors
        {
            None = 0,

            Red = 1,

            Green = 2,

            Blue = 4,

            All = Red | Green | Blue
        }
    }
}
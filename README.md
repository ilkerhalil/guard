# Guard

![Logo](media/guard-64.png)

Guard is a fluent argument validation library that is intuitive, fast and extensible.

[![NuGet](https://img.shields.io/nuget/v/Dawn.Guard.svg?style=flat)](https://www.nuget.org/packages/Dawn.Guard/)
[![Build](https://dev.azure.com/safakgur/Guard/_apis/build/status/Guard-CI?label=build)](https://dev.azure.com/safakgur/Guard/_build/latest?definitionId=1)
[![Coverage](https://codecov.io/gh/safakgur/guard/branch/dev/graph/badge.svg)](https://codecov.io/gh/safakgur/guard/branch/dev)

* [Introduction](#introduction)
* [What's Wrong with Vanilla?](#whats-wrong-with-vanilla)
* [Requirements](#requirements)
* [Standard Validations](#standard-validations)
* [Design Decisions](#design-decisions)
* [Extensibility](#extensibility)
* [What's Next](#whats-next)

## Introduction

Here is a sample constructor that validates its arguments without Guard:

```c#
public Person(string name, int age)
{
    if (name == null)
        throw new ArgumentNullException(nameof(name), "Name cannot be null.");

    if (name.Length == 0)
        throw new ArgumentException("Name cannot be empty.", nameof(name));

    if (age < 0)
        throw new ArgumentOutOfRangeException(nameof(age), age, "Age cannot be negative.");

    Name = name;
    Age = age;
}
```

And this is how we write the same constructor with Guard:

```c#
public Person(string name)
{
    Name = Guard.Argument(name, nameof(name)).NotNull().NotEmpty();
    Age = Guard.Argument(age, nameof(age)).NotNegative();
}
```

If this looks like too much allocations to you, fear not. The arguments are read-only structs that
are passed by reference, see the [design decisions](#design-decisions) for details.

## What's Wrong with Vanilla?

There is nothing wrong with writing your own checks but when you have lots of types you need to
validate, the task gets very tedious, very quickly.

Let's analyze the string validation in the example without Guard:

* We have an argument (name) that we need to be a non-null, non-empty string.
* We check if it's null and throw an `ArgumentNullException` if it is.
* We then check if it's empty and throw an `ArgumentException` if it is.
* We specify the same parameter name for each validation.
* We write an error message for each validation.
* `ArgumentNullException` accepts the parameter name as its first argument and error message as its
second while it's the other way around for the `ArgumentException`. An inconsistency that many of us
sometimes find it hard to remember.

In reality, all we need to express should be the first bullet, that we want our argument non-null
and non-empty.

With Guard, if you want to guard an argument against null, you just write `NotNull` and that's it.
If the argument is passed null, you'll get an `ArgumentNullException` thrown with the correct
parameter name and a clear error message out of the box. The [standard validations](#standard-validations)
have fully documented, meaningful defaults that get out of your way and let you focus on your project.

## Requirements

**C# 7.2 or later is required.** Guard takes advantage of almost all the new features introduced in
C# 7.2. So in order to use Guard, you need to make sure your Visual Studio is up to date and you
have `<LangVersion>7.2</LangVersion>` or later added in your .csproj file.

**.NET Standard 1.0** and above are supported. [Microsoft Docs][2] lists the following platform
versions as .NET Standard 1.0 compliant but keep in mind that currently, the unit tests are only
targeting .NET Core 1.0 and 2.0.

| Platform                   | Version |
| -------------------------- | ------- |
| .NET Core                  | `1.0`   |
| .NET Framework             | `4.5`   |
| Mono                       | `4.6`   |
| Xamarin.iOS                | `10.0`  |
| Xamarin.Mac                | `3.0`   |
| Xamarin.Android            | `7.0`   |
| Universal Windows Platform | `10.0`  |
| Windows                    | `8.0`   |
| Windows Phone              | `8.1`   |
| Windows Phone Silverlight  | `8.0`   |

## More

### Standard Validations

[Click here][3] for a list of the validations that are included in the library.

### Design Decisions

[Click here][1] for the document that explains the motives behind the Guard's API design.

### Extensibility

[Click here][4] to see how to add custom validations to Guard by writing simple extension methods.

### What's Next

Right now the following are on the horizon:

* More validations
* Online documentation
* Performance benchmarks

[1]: docs/design-decisions.md
[2]: https://docs.microsoft.com/dotnet/standard/net-standard
[3]: docs/standard-validations.md
[4]: docs/extensibility.md
[5]: https://github.com/safakgur/guard/tree/dev
[6]: https://github.com/safakgur/guard/tree/master

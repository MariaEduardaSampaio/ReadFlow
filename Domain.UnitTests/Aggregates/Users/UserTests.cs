using Domain.Aggregates.Users;
using Domain.Common.Enums;
using FluentAssertions;

namespace Domain.UnitTests.Aggregates.Users;

[TestFixture]
public sealed class UserTests
{
    [Test]
    public void Constructor_WhenParamsAreDefined_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var user = new User(guid, "Maria", "maria@email.com", EUserRole.User);

        // Assert
        user.Id.Should().Be(guid);
        user.Name.Should().Be("Maria");
        user.Email.Should().Be("maria@email.com");
        user.Role.Should().Be(EUserRole.User);
    }

    [Test]
    public void Constructor_WhenRoleIsAdmin_ShouldSetRoleCorrectly()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var user = new User(guid, "Admin", "admin@email.com", EUserRole.Admin);

        // Assert
        user.Role.Should().Be(EUserRole.Admin);
    }

    [Test]
    public void ProtectedParameterlessConstructor_ShouldCreateInstance()
    {
        // Act
        var user = (User)Activator.CreateInstance(typeof(User), true)!;

        // Assert
        user.Should().NotBeNull();
    }
}
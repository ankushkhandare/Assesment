using SampleAPI.Application.Features.Order.Commands;
using FluentAssertions;

namespace SampleAPI.Tests.Validators;

    public class CreateOrderCommandValidatorTest
    {
        private readonly CreateOrderCommandValidator _validator;

        public CreateOrderCommandValidatorTest()
        {
            _validator = new CreateOrderCommandValidator();
        }

        [Fact]
        public void Validate_ShouldPass_WhenValidData()
        {
            // Arrange
            var command = new CreateOrderCommand(){Name = "Valid Name", Description = "Valid Description"};

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeTrue();
            result.Errors.Should().BeEmpty();
        }

        [Theory]
        [InlineData(null, "Valid Description Data", "'Name' must not be empty.")]
        [InlineData("", "Valid Description Data", "'Name' must not be empty.")]
        [InlineData("This name is way too long and exceeds the maximum allowed length of one hundred characters which should result in a validation error being thrown.", "Valid Description", "The length of 'Name' must be 100 characters or fewer. You entered 146 characters.")]
        public void Validate_ShouldFail_WhenInvalidName(string name, string description, string expectedErrorMessage)
        {
            // Arrange
            var command = new CreateOrderCommand(){Name = name, Description = description};

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be(expectedErrorMessage);
        }

        [Theory]
        [InlineData("Valid Name", null, "'Description' must not be empty.")]
        [InlineData("Valid Name", "", "'Description' must not be empty.")]
        [InlineData("Valid Name", "This description is way too long and exceeds the maximum allowed length of one hundred characters which should result in a validation error being thrown.", "The length of 'Description' must be 100 characters or fewer. You entered 153 characters.")]
        public void Validate_ShouldFail_WhenInvalidDescription(string name, string description, string expectedErrorMessage)
        {
            // Arrange
            var command = new CreateOrderCommand(){Name = name, Description = description};

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle()
                .Which.ErrorMessage.Should().Be(expectedErrorMessage);
        }

        [Fact]
        public void Validate_ShouldFail_WhenBothNameAndDescriptionAreInvalid()
        {
            // Arrange
            var command = new CreateOrderCommand(){Name = "", Description = ""};

            // Act
            var result = _validator.Validate(command);

            // Assert
            result.IsValid.Should().BeFalse();
            result.Errors.Should().HaveCount(2);
            result.Errors.Should().Contain(e => e.ErrorMessage == "'Name' must not be empty.");
            result.Errors.Should().Contain(e => e.ErrorMessage == "'Description' must not be empty.");
        }
    }

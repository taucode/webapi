using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;

namespace TauCode.WebApi.Tests
{
    [TestFixture]
    public class ValidationErrorResponseDtoTest
    {
        [Test]
        public void Deserialize_NoArguments_DeserializesAsExpected()
        {
            // Arrange
            var validationErrorResponse = ValidationErrorDto.CreateStandard("Foo validation error");
            validationErrorResponse.Failures = new Dictionary<string, ValidationFailureDto>
            {
                {"age", new ValidationFailureDto("bad_age", "Age must be positive")},
            };

            validationErrorResponse.AddFailure("weight", "bad_weight", "Too thin");

            // Act
            var json = JsonConvert.SerializeObject(validationErrorResponse);
            var deserialized = JsonConvert.DeserializeObject<ValidationErrorDto>(json);

            // Assert
            Assert.That(deserialized.Code, Is.EqualTo("ValidationError"));
            Assert.That(deserialized.Message, Is.EqualTo("Foo validation error"));

            var error = deserialized.Failures["age"];
            Assert.That(error.Code, Is.EqualTo("bad_age"));
            Assert.That(error.Message, Is.EqualTo("Age must be positive"));

            error = deserialized.Failures["weight"];
            Assert.That(error.Code, Is.EqualTo("bad_weight"));
            Assert.That(error.Message, Is.EqualTo("Too thin"));
        }
    }
}

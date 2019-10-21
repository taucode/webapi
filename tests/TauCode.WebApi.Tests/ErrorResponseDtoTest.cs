using Newtonsoft.Json;
using NUnit.Framework;

namespace TauCode.WebApi.Tests
{
    [TestFixture]
    public class ErrorResponseDtoTest
    {
        [Test]
        public void Serialize_NoArguments_SerializesCorrectly()
        {
            // Arrange
            var response = new ErrorDto
            {
                Code = "some_code",
                Message = "Some message"
            };

            // Act
            var json = JsonConvert.SerializeObject(response);

            // Assert
            Assert.That(json, Is.EqualTo(@"{""Code"":""some_code"",""Message"":""Some message""}"));
        }
    }
}

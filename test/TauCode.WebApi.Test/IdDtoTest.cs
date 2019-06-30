using Newtonsoft.Json;
using NUnit.Framework;
using System;

namespace TauCode.WebApi.Test
{
    [TestFixture]
    public class IdDtoTest
    {
        [Test]
        public void Equals_SameGuids_ReturnsTrue()
        {
            // Arrange

            // Act
            var id1 = new IdDto(Guid.Parse("5C231189-B80F-4B40-AD46-3CAFD1FBEC61"));
            var id2 = new IdDto("5c231189-b80f-4b40-ad46-3cafd1fbec61");

            var equal = id1 == id2;

            // Assert
            Assert.That(equal, Is.True);
        }

        [Test]
        public void Json_Serialize_SerializesCorrectly()
        {
            // Arrange
            var id = new IdDto("5C231189-B80F-4B40-AD46-3CAFD1FBEC61");

            // Act
            var text = JsonConvert.SerializeObject(id);

            // Assert
            Assert.That(text, Is.EqualTo("\"5c231189-b80f-4b40-ad46-3cafd1fbec61\""));
        }

        [Test]
        public void Json_Deserialize_SerializesCorrectly()
        {
            // Arrange
            var json = "{Id : \"044210e9-bee8-4638-8fe6-0f28929905b6\"}";

            // Act
            var foo = JsonConvert.DeserializeObject<FooDto>(json);

            // Assert
            Assert.That(foo.Id, Is.EqualTo(new IdDto("044210e9-bee8-4638-8fe6-0f28929905b6")));
        }

        public class FooDto
        {
            public IdDto Id { get; set; }
        }
    }
}

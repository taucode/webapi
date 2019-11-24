using NUnit.Framework;

namespace TauCode.WebApi.Host.Tests
{
    [TestFixture]
    public class HostDummyFixture
    {
        [Test]
        public void DummyTest()
        {
            Assert.Pass("Passed for Jenkins.");
        }
    }
}
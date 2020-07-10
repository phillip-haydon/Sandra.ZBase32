using Xunit;

namespace Sandra.ZBase32.Tests
{
    public class EncodeDecodeTests
    {
        private ZBase32 encoder;

        public EncodeDecodeTests()
        {
            encoder = new ZBase32();
        }

        [Theory]
        [InlineData("banana", "cjoshamqcr")]
        [InlineData("谢谢", "7nakf4fowe")]
        [InlineData("!@#$%^&(*()*{}|\"':<>?", "rfyngjbfmaunoktefri8s9mhreuuwxb68h")]
        [InlineData("4235232-655344-fw7f3gre", "go3dgpj1gc3n4ptigw3uepbpc35uq3tuc73gk")]
        [InlineData("          ", "ryonyebyryonyeby")]
        [InlineData("", "")]
        public void EncodeDecideScenarios(string input, string expected)
        {
            var actual = encoder.Encode(input);
            var decoded = encoder.Decode(actual);
            
            Assert.Equal(expected, actual);
            Assert.Equal(input, decoded);
        }
    }
}
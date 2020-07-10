using Xunit;

namespace Sandra.ZBase32.Tests
{
    public class EncodingTests
    {
        private ZBase32 encoder;

        public EncodingTests()
        {
            encoder = new ZBase32();
        }

        [Fact]
        public void ChineseCharacterEncoding()
        {
            var input = "谢谢";
            var expected = "7nakf4fowe";
            
            var actual = encoder.Encode(input);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void LongStringEncoding()
        {
            var input = "4235232-655344-fw7f3gre";
            var expected = "go3dgpj1gc3n4ptigw3uepbpc35uq3tuc73gk";

            var actual = encoder.Encode(input);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SingleCharEncoding()
        {
            var input = "Z";
            var expected = "me";

            var actual = encoder.Encode(input);
            
            Assert.Equal(expected, actual);
        }
    }
}
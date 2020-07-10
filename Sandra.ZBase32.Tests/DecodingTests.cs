using Xunit;

namespace Sandra.ZBase32.Tests
{
    public class DecodingTests
    {
        private ZBase32 decoder;

        public DecodingTests()
        {
            decoder = new ZBase32();
        }

        [Fact]
        public void ChineseCharacterDecoding()
        {
            var input = "7nakf4fowe";
            var expected = "谢谢";
            
            var actual = decoder.Decode(input);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void LongStringDecoding()
        {
            var input = "go3dgpj1gc3n4ptigw3uepbpc35uq3tuc73gk";
            var expected = "4235232-655344-fw7f3gre";

            var actual = decoder.Decode(input);
            
            Assert.Equal(expected, actual);
        }
        
        [Fact]
        public void SingleCharDecoding()
        {
            var input = "me";
            var expected = "Z";

            var actual = decoder.Decode(input);
            
            Assert.Equal(expected, actual);
        }
    }
}
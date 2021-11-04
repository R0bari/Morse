using NUnit.Framework;

namespace NoVowels
{
    [TestFixture]
    public class KataTest
    {
        [Test]
        public void BaseTest()
        {
            Assert.AreEqual("HEY JUDE", MorseDecoder.DecodeMorse(MorseDecoder.DecodeBits("1100110011001100000011000000111111001100111111001111110000000000000011001111110011111100111111000000110011001111110000001111110011001100000011")));
        }

        [Test]
        public void TestExtraZerosHandling()
        {
            Assert.AreEqual("", MorseDecoder.DecodeMorse(MorseDecoder.DecodeBits("0000")));
        }

        [Test]
        public void TestBasicBitsDecoding()
        {
            Assert.AreEqual("M", MorseDecoder.DecodeMorse(MorseDecoder.DecodeBits("11111100111111")));
        }
    }
}
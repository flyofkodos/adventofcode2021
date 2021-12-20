using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;
namespace testing;

[TestClass]
public class PacketTreeTests
{
    [TestMethod()]
    public void SingleLiteralPacketTest()
    {
        var test = new PacketTree("110100101111111000101000");
        Assert.AreEqual("Literal", test.TypeStr);
        Assert.AreEqual(6, test.Version);
        Assert.AreEqual(0, test.SubPacketCount);
        Assert.AreEqual(3, test.LiteralValueCount);
    }

    [TestMethod]
    public void LiteralSubPacketTest()
    {
        var test = new PacketTree("00111000000000000110111101000101001010010001001000000000");
        Assert.AreEqual(1, test.Version);
        Assert.AreEqual("LessThan", test.TypeStr);
        Assert.AreEqual(2, test.SubPacketCount);
    }

    [TestMethod]
    public void LiteralSubPacketTest2()
    {
        var test = new PacketTree("11101110000000001101010000001100100000100011000001100000");
        Assert.AreEqual(7, test.Version);
        Assert.AreEqual("Maximum", test.TypeStr);
        Assert.AreEqual(3, test.SubPacketCount);
    }

    [TestMethod()]
    public void LiteralToBinaryStringTest()
    {
        var test = new PacketTree("110100101111111000101000");
        Assert.AreEqual("110100101111111000101000", test.ToBinaryString());
    }
    [TestMethod()]
    public void LiteralSubPacketToBinaryStringTest()
    {
        var test = new PacketTree("00111000000000000110111101000101001010010001001000000000");
        Assert.AreEqual("00111000000000000110111101000101001010010001001000000000", test.ToBinaryString());
    }
    [TestMethod()]
    public void LiteralSubPacket2ToBinaryStringTest()
    {
        var test = new PacketTree("11101110000000001101010000001100100000100011000001100000");
        Assert.AreEqual("11101110000000001101010000001100100000100011000001100000", test.ToBinaryString());
    }
}
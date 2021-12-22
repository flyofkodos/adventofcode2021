using AdventOfCode;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests;

[TestClass]
public class SnailFishTreeTests
{
    private readonly string[] _testData =
    {
        "[[[0,[5,8]],[[1,7],[9,6]]],[[4,[1,2]],[[1,4],2]]]",
        "[[[5,[2,8]],4],[5,[[9,9],0]]]",
        "[6,[[[6,2],[5,6]],[[7,6],[4,7]]]]",
        "[[[6,[0,7]],[0,9]],[4,[9,[9,0]]]]",
        "[[[7,[6,4]],[3,[1,3]]],[[[5,5],1],9]]",
        "[[6,[[7,3],[3,2]]],[[[3,8],[5,7]],4]]",
        "[[[[5,4],[7,7]],8],[[8,3],8]]",
        "[[9,3],[[9,9],[6,[4,9]]]]",
        "[[2,[[7,7],7]],[[5,8],[[9,3],[0,2]]]]",
        "[[[[5,2],5],[8,[3,7]]],[[5,[7,5]],[4,4]]]"
    };

    [TestMethod]
    public void SnailFishTreeTest()
    {
        Assert.AreEqual(143, new SnailFishTree("[[1,2],[[3,4],5]]").Magnitude);
        Assert.AreEqual(1384, new SnailFishTree("[[[[0,7],4],[[7,8],[6,0]]],[8,1]]").Magnitude);
        Assert.AreEqual(791, new SnailFishTree("[[[[3,0],[5,3]],[4,4]],[5,5]]").Magnitude);
        Assert.AreEqual(1137, new SnailFishTree("[[[[5,0],[7,4]],[5,5]],[6,6]]").Magnitude);
        Assert.AreEqual(3488, new SnailFishTree("[[[[8,7],[7,7]],[[8,6],[7,7]]],[[[0,7],[6,6]],[8,7]]]").Magnitude);
    }

    [TestMethod]
    public void TestAddition()
    {
        var tree1 = new SnailFishTree("[1,2]");
        var tree2 = new SnailFishTree("[[3,4],5]");
        Assert.AreEqual("[[1,2],[[3,4],5]]", tree1 + "[[3,4],5]");
        Assert.AreEqual("[[1,2],[[3,4],5]]", tree1 + tree2);
    }

    [TestMethod]
    public void TestSum()
    {
        var tempClass = new SnailFishTree(_testData[0]);
        for (var l = 1; l < _testData.Length; l++) tempClass = new SnailFishTree(tempClass.RawString + _testData[l]);
        Assert.AreEqual(4140, tempClass.Magnitude);
    }
}
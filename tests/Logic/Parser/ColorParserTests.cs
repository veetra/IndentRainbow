using IndentRainbow.Logic.Parser;
using NUnit.Framework;
using System;
using System.Windows.Media;

namespace IndentRainbow.LogicTests.Colors
{
    [TestFixture]
    public class ColorParserTests
    {
        private static readonly Brush[][] solutions = new Brush[][]
        {
            //First test case
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(255, 255, 255, 255))
            },
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(0x40, 255, 255, 0)),
                new SolidColorBrush(Color.FromArgb(0x40, 102, 255, 51)),
                new SolidColorBrush(Color.FromArgb(0x40, 0, 204, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 153, 51, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 0)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 170, 0))
            },
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(0x40, 153, 51, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 255)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 0, 0)),
                new SolidColorBrush(Color.FromArgb(0x40, 255, 170, 0))
            },
            new Brush[]
            {
                new SolidColorBrush(Color.FromArgb(0x20,255,255,0)),
                new SolidColorBrush(Color.FromArgb(0x20,0,255,255)),
                new SolidColorBrush(Color.FromArgb(0x20,255,0,255)),
            },
            Array.Empty<Brush>(),
            new Brush[]{null}
        };


        [Test]
        [TestCase("#FFFFFFFF", 1.0, 0)]
        [TestCase("#40FFFF00,#4066FF33,#4000CCFF,#409933FF,#40FF00FF,#40FF0000,#40FFAA00", 1.0, 1)]
        [TestCase("#40FFFF004066FF33,#F,#409933FF,#40FF00FF,#40FF0000,#40FFAA00", 1.0, 2)]
        [TestCase("#40FFFF00,#4000FFFF,#40FF00FF", 0.5, 3)]
        [TestCase("#60FFFF00,#6000FFFF,#60FF00FF", 1.0 / 3.0, 3)]
        [TestCase(null, 0, 4)]
        [TestCase("", 0, 4)]
        public void ConvertStringToBrushArray_ExpectedBehavior(string input, double opacityMultiplier, int solutionIndex)
        {
            Brush[] result = ColorParser.ConvertStringToBrushArray(input, opacityMultiplier);
            Brush[] solution = solutions[solutionIndex];

            Assert.AreEqual(solution.Length, result.Length);
            for (int i = 0; i < solution.Length; i++)
            {
                Assert.AreEqual(solution[i].ToString(), result[i].ToString());
            }
        }

        [Test]
        [TestCase("#FFFFFFFF", 1.0, 0)]
        [TestCase("#40FFFF00", 1.0, 1)]
        [TestCase("#409933FF", 1.0, 2)]
        [TestCase("#40FFFF00", 0.5, 3)]
        [TestCase("#60FFFF00", 1.0 / 3.0, 3)]
        [TestCase(null, 0, 5)]
        [TestCase("", 0, 5)]
        public void ConvertStringToBrush_ExpectedBehavior(string input, double opacityMultiplier, int solutionIndex)
        {
            Brush result = ColorParser.ConvertStringToBrush(input, opacityMultiplier);
            Brush solution = solutions[solutionIndex][0];
            if (result is null && solution is null)
            {
                return;
            }
            Assert.AreEqual(solution.ToString(), result.ToString());
        }
    }
}

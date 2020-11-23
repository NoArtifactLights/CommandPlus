using CommandPlus;
using CommandPlus.Commanding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CommandPlus.Test
{
    public static class Common
    {
        internal static bool paramLessSuccess;
    }

    [TestClass]
    public class CommandParserTest
    {
        [TestMethod]
        public void CommandRegisterTest()
        {
            System.Console.WriteLine("Starting Command Register Test");
            TestCommand tc = new TestCommand();
            CommandControl cc = new CommandControl();
            System.Console.WriteLine("Instances created");
            cc.Register("test", tc);
            System.Console.WriteLine("Registered!");
            cc.ParseAndRun("test true");
            System.Console.WriteLine("Test complete!");
        }

        [TestMethod]
        public void ComplexCommandTest()
        {
            System.Console.WriteLine("Starting Complex Command Register Test");
            ComplexTestCommand tc = new ComplexTestCommand();
            CommandControl cc = new CommandControl();
            System.Console.WriteLine("Instances created");
            cc.Register("test", tc);
            System.Console.WriteLine("Registered!");
            cc.ParseAndRun("test yeah false 20");
            System.Console.WriteLine("Test complete!");
        }

        [TestMethod]
        public void ParamLessCommandTest()
        {
            System.Console.WriteLine("Starting Parameter-less Command Register Test");
            ParamLessTestCommand tc = new ParamLessTestCommand();
            CommandControl cc = new CommandControl();
            System.Console.WriteLine("Instances created");
            cc.Register("test", tc);
            System.Console.WriteLine("Registered!");
            cc.ParseAndRun("test");
            Assert.IsTrue(Common.paramLessSuccess);
            System.Console.WriteLine("Test complete!");
        }

        [TestMethod]
        public void StringParserTest()
        {
            string exc = "true false 1 2 3";
            StringParser parser = new StringParser(exc);

            bool var1 = parser.ReadBoolean();
            parser.SkipWhitespace();
            bool var2 = parser.ReadBoolean();
            parser.SkipWhitespace();
            int var3 = parser.ReadInt32();
            parser.SkipWhitespace();
            int var4 = parser.ReadInt32();
            parser.SkipWhitespace();
            int var5 = parser.ReadInt32();

            Assert.IsTrue(var1);
            Assert.IsFalse(var2);
            Assert.AreEqual(1, var3);
            Assert.AreEqual(2, var4);
            Assert.AreEqual(3, var5);
        }
    }
}

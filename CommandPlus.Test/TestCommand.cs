using CommandPlus.Commanding;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommandPlus.Test
{
    public class ParamLessTestCommand : Command
    {
        public override CommandResult Executed(object[] arguments)
        {
            return new CommandResult("Success", CommandResultStatus.Success);
        }
    }

    public class ComplexTestCommand : Command
    {
        public ComplexTestCommand()
        {
            this.ArgumentTypes.Add(typeof(string));
            this.ArgumentTypes.Add(typeof(bool));
            this.ArgumentTypes.Add(typeof(int));
        }

        public override CommandResult Executed(object[] arguments)
        {
            string str = this.VerifyAndConstruct<string>(0, arguments[0]);
            bool b = this.VerifyAndConstruct<bool>(1, arguments[1]);
            int i = this.VerifyAndConstruct<int>(2, arguments[2]);

            Assert.AreEqual("yeah", str);
            Assert.IsFalse(b);
            Assert.AreEqual(20, i);

            return new CommandResult("Success", CommandResultStatus.Success);
        }
    }

    public class TestCommand : Command
    {
        public TestCommand()
        {
            this.ArgumentTypes.Add(typeof(bool));
        }

        public override CommandResult Executed(object[] arguments)
        {
            Console.WriteLine("executed!");
            if (arguments.Length != 1) throw new InvalidOperationException("It transfers invalid to our method!");
            Console.WriteLine((bool)arguments[0]);
            Assert.IsTrue((bool)arguments[0]);

            return new CommandResult("Success", CommandResultStatus.Success);
        }
    }
}

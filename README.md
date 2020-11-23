# Command Plus

A stupidly-simple command parser library meant for beginners.

## Usage

First, create a class inherits `CommandPlus.Commanding.Command`:

```c#
using CommandPlus.Commanding;

// ... after namespace

public class BeginnerCommand : Command
	{
        public BeginnerCommand()
        {
        }

        public override void Executed(object[] arguments)
        {
        }
    }
```

Commands usually had two types of them.

### Argument-less commands

Argument less commands means commands without arguments. For example:

```c#
public class ParamLessCommand : Command
    {
        public override void Executed(object[] arguments)
        {
            // do something
        }
    }
```

They usually didn't have constructors and do something directly in `Executed` method.

### Commands has Arguments

For commands has arguments, the `Command` class has a `ArgumentTypes` field which is a list can be used to define `Type`s of parameters.

**NOTE: Arguments only accepts `string`, `bool`, `int`, `long`, `float`, `double` as their types! Custom types are not supported!**

```c#
public class ParametersCommand : Command
    {
        public ParametersCommand()
        {
            this.ArgumentTypes.Add(typeof(string));
            this.ArgumentTypes.Add(typeof(bool));
            this.ArgumentTypes.Add(typeof(int));
        }

        public override void Executed(object[] arguments)
        {
            // Verify and construct does the following thing:
            // 1. ensure the given type is correct (if not, there's an exception).
            // 2. cast them to the given type.
            // 3. return the argument.
            string str = this.VerifyAndConstruct<string>(0, arguments[0]);
            bool b = this.VerifyAndConstruct<bool>(1, arguments[1]);
            int i = this.VerifyAndConstruct<int>(2, arguments[2]);
			
            // do something
        }
    }
```

Since arguments can be in various types (such as `int`, `long`...), they are passed in a general `object` type. To cast them, you can either cast them directly or use `VerifyAndConstruct` method which verifies them for you.

For more examples, check out the `TestCommand` file in unit test project. It contains various types of commands.
using System;

namespace DotNetStudy.Topics
{
  public class Operators
  {
    public static void DisplayMenu()
    {
      bool backToMain = false;
      while (!backToMain)
      {
        Console.Clear();
        Console.WriteLine("--- OPERATORS MODULE ---");
        Console.WriteLine("1. Overview: Arithmetic Operators");
        Console.WriteLine("2. Overview: Logic Operators");
        Console.WriteLine("3. Game: Guess the Arithmetic Operator");
        Console.WriteLine("4. Game: Guess the Logic Operator");
        Console.WriteLine("0. Back to Main Menu");
        Console.Write("\nSelection: ");

        switch (Console.ReadLine())
        {
          case "1":
            ArithmeticOverview();
            break;
          case "2":
            LogicOperatorsOverview();
            break;
          case "3":
            GuessArithmeticOperator();
            break;
          case "4":
            GuessLogicOperator();
            break;
          case "0":
            backToMain = true;
            break;
        }
      }
    }

    private static void ArithmeticOverview()
    {
      Console.Clear();
      Console.WriteLine("--- Arithmetic Operators in C# ---");
      Console.WriteLine("");

      Console.WriteLine("* [+] Addition: Adds two operands. (e.g., 5 + 2 = 7)");
      Console.WriteLine("* [-] Subtraction: Subtracts second operand from the first. (e.g., 5 - 2 = 3)");
      Console.WriteLine("* [*] Multiplication: Multiplies both operands. (e.g., 5 * 2 = 10)");
      Console.WriteLine("* [/] Division: Divides numerator by denominator. (e.g., 10 / 2 = 5)");
      Console.WriteLine("      Note: Integer division truncates (e.g., 5 / 2 = 2).");
      Console.WriteLine("* [%] Modulo: Returns the remainder of a division. (e.g., 5 % 2 = 1)");
      Console.WriteLine("* [++] Increment: Increases integer value by one. (e.g., x++)");
      Console.WriteLine("* [--] Decrement: Decreases integer value by one. (e.g., x--)");

      Console.WriteLine("\n--- Quick Example ---");
      int a = 10;
      int b = 3;
      Console.WriteLine($"If a={a} and b={b}:");
      Console.WriteLine($"Addition: {a + b} | Division: {a / b} | Remainder: {a % b}");

      Console.WriteLine("\nPress any key to return...");
      Console.ReadKey();
    }

    private static void GuessArithmeticOperator()
    {
      Console.Clear();
      Random rand = new Random();

      int num1 = rand.Next(1, 21);
      int num2 = rand.Next(1, 21);

      string[] ops = { "+", "-", "*", "/" };
      string correctOp = ops[rand.Next(ops.Length)];
      int result = 0;

      switch (correctOp)
      {
        case "+": result = num1 + num2; break;
        case "-": result = num1 - num2; break;
        case "*": result = num1 * num2; break;
        case "/":
          result = num1 / num2;
          break;
      }

      Console.WriteLine("--- Game: Guess the Operator ---");
      Console.WriteLine($"Problem: {num1} [ ? ] {num2} = {result}");
      Console.Write("Enter the missing operator (+, -, *, /): ");

      string? userGuess = Console.ReadLine();

      if (userGuess == correctOp)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nCorrect! You're a math wizard.");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nWrong! The correct operator was '{correctOp}'.");
        Console.WriteLine($"Logic: {num1} {correctOp} {num2} = {result}");
      }

      Console.ResetColor();
      Console.WriteLine("\nPress any key to return...");
      Console.ReadKey();
    }

    private static void LogicOperatorsOverview()
    {
      Console.Clear();
      Console.WriteLine("--- Logical Operators in C# ---");
      Console.WriteLine("");

      Console.WriteLine("* [&&] Logical AND: True if both conditions are true. (e.g., true && true is true)");
      Console.WriteLine("* [||] Logical OR: True if at least one condition is true. (e.g., true || false is true)");
      Console.WriteLine("* [!]  Logical NOT: Reverses the state. (e.g., !true is false)");
      Console.WriteLine("* [^]  Logical XOR: True if exactly one condition is true, but not both.");



      Console.WriteLine("\n--- Example ---");
      bool isSunny = true;
      bool isWarm = false;
      Console.WriteLine($"Sunny: {isSunny}, Warm: {isWarm}");
      Console.WriteLine($"Is it Sunny AND Warm? (isSunny && isWarm) -> {isSunny && isWarm}");
      Console.WriteLine($"Is it Sunny OR Warm? (isSunny || isWarm) -> {isSunny || isWarm}");

      Console.WriteLine("\nPress any key to return...");
      Console.ReadKey();
    }

    private static void GuessLogicOperator()
    {
      Console.Clear();
      Random rand = new Random();

      // Generate two numbers to compare
      int val1 = rand.Next(1, 11); // e.g., 5
      int val2 = rand.Next(1, 11); // e.g., 8

      // Generate a second set of comparisons
      int val3 = rand.Next(1, 11);
      int val4 = rand.Next(1, 11);

      // We create two boolean facts
      bool fact1 = val1 > val2;
      bool fact2 = val3 < val4;

      string[] ops = { "&&", "||" };
      string correctOp = ops[rand.Next(ops.Length)];

      // Calculate final boolean result
      bool finalResult = (correctOp == "&&") ? (fact1 && fact2) : (fact1 || fact2);

      Console.WriteLine("--- Game: Guess the Logic Operator ---");
      Console.WriteLine($"Fact A: ({val1} > {val2}) is {fact1.ToString().ToLower()}");
      Console.WriteLine($"Fact B: ({val3} < {val4}) is {fact2.ToString().ToLower()}");
      Console.WriteLine($"\nQuestion: Fact A [ ? ] Fact B results in: {finalResult.ToString().ToLower()}");
      Console.Write("Enter the missing operator (&&, ||): ");

      string? userGuess = Console.ReadLine();

      if (userGuess == correctOp)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nCorrect! Your logic is sound.");
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\nWrong! The operator was '{correctOp}'.");
      }

      Console.ResetColor();
      Console.WriteLine("\nPress any key to return...");
      Console.ReadKey();
    }
  }
}
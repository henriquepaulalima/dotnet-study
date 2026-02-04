using System;

namespace DotNetStudy.Topics
{
  public class DataTypes
  {
    public static void DisplayMenu()
    {
      bool backToMain = false;
      while (!backToMain)
      {
        Console.Clear();
        Console.WriteLine("--- DATA TYPES MODULES ---");
        Console.WriteLine("1. Overview: Static Types");
        Console.WriteLine("2. Game: TypeAType");
        Console.WriteLine("0. Back to Main Menu");
        Console.Write("\nSelection: ");

        switch (Console.ReadLine())
        {
          case "1":
            RunExample();
            break;
          case "2":
            TypeAType();
            break;
          case "0":
            backToMain = true;
            break;
        }
      }
    }

    public static void RunExample()
    {
      Console.Clear();
      Console.WriteLine("--- Data Types Deep Dive ---");

      int age = 25;
      double price = 19.99;
      decimal precision = 1000.254m;
      bool isActive = true;

      string message = "C# is powerful";
      int[] scores = { 90, 85, 88 };
      DateTime dateTime = new DateTime(2026, 2, 1, 22, 54, 50);
      DateTime dateOnly = dateTime.Date;

      Console.WriteLine($"Integer: {age}");
      Console.WriteLine($"Double: {price}");
      Console.WriteLine($"Decimal (Money): {precision}");
      Console.WriteLine($"Boolean: {isActive}");
      Console.WriteLine($"String: {message}");

      Console.WriteLine($"DateTime: {dateTime}");
      Console.WriteLine($"DateTime (formated): {dateOnly.ToString("dd/MM/yyyy HH:mm")}");

      Console.WriteLine($"Array Count: {scores.Length}");

      Console.WriteLine("\nPress any key to return to menu...");
      Console.ReadKey();
    }

    public static void TypeAType()
    {
      Console.Clear();
      Console.WriteLine("--- Game: Type a Type ---");

      // List of types we want to test
      string[] types = { "int", "double", "decimal", "bool", "string", "datetime", "array", "var" };
      Random rand = new Random();
      string selectedType = types[rand.Next(types.Length)];

      Console.WriteLine($"\nTask: Type a valid value for the type: **{selectedType}**");
      Console.Write("Input: ");
      string? input = Console.ReadLine();

      bool isValid = false;

      // Validation Logic
      switch (selectedType)
      {
        case "int":
          isValid = int.TryParse(input, out _);
          break;
        case "double":
          isValid = double.TryParse(input, out _);
          break;
        case "decimal":
          string cleanDecimal = input!.Replace("m", "").Replace("M", "");
          isValid = decimal.TryParse(cleanDecimal, out _);
          break;
        case "bool":
          isValid = bool.TryParse(input, out _);
          break;
        case "string":
          isValid = true;
          break;
        case "datetime":
          isValid = DateTime.TryParse(input, out _);
          break;
        case "array":
          isValid = input!.Contains("{") && input.Contains("}");
          if (isValid)
          {
            bool hasMutiple = input.Contains(" ");
            if (hasMutiple)
            {
              isValid = input.Contains(",");
            }
          }
          break;
        case "var":
          isValid = true;
          break;
      }

      if (isValid)
      {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success! That is a valid " + selectedType);
      }
      else
      {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Invalid! '{input}' is not a valid {selectedType}.");
      }

      Console.ResetColor();
      Console.WriteLine("\nPress any key to return...");
      Console.ReadKey();
    }
  }
}
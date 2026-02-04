using System;
using DotNetStudy.Topics;

namespace DotNetStudy
{
  class Program
  {
    static void Main(string[] args)
    {
      bool keepRunning = true;

      while (keepRunning)
      {
        Console.Clear();
        Console.WriteLine("=== C# MASTER LEARNING APP ===");
        Console.WriteLine("1. Data Types Modules");
        Console.WriteLine("2. Operators");
        Console.WriteLine("0. Exit");
        Console.Write("\nSelect Topic: ");

        switch (Console.ReadLine())
        {
          case "1":
            DataTypes.DisplayMenu();
            break;
          case "2":
            Operators.DisplayMenu();
            break;
          case "0":
            keepRunning = false;
            break;
          default:
            Console.WriteLine("Invalid option.");
            System.Threading.Thread.Sleep(800);
            break;
        }
      }
    }
  }
}
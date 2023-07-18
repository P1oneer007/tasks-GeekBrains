// Decompiled with JetBrains decompiler
// Type: Program
// Assembly: lesson_4_4, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B99391BD-F514-45E9-86CA-A7AEFB37E32E
// Assembly location: C:\Users\root\source\repos\lesson_4_4\lesson_4_4\bin\Release\net6.0\lesson_4_4.dll

using System;
using System.Runtime.CompilerServices;


#nullable enable
internal class Program
{
  private static void Main(string[] args)
  {
    Console.Write("Введите номер числа Фибоначчи: ");
    int n = int.Parse(Console.ReadLine());
    int num = Program.Fibonacci(n);
    DefaultInterpolatedStringHandler interpolatedStringHandler = new DefaultInterpolatedStringHandler(34, 2);
    interpolatedStringHandler.AppendLiteral("Число Фибоначчи для ");
    interpolatedStringHandler.AppendFormatted<int>(n);
    interpolatedStringHandler.AppendLiteral("-го элемента: ");
    interpolatedStringHandler.AppendFormatted<int>(num);
    Console.WriteLine(interpolatedStringHandler.ToStringAndClear());
  }

  private static int Fibonacci(int n) => n <= 1 ? n : Program.Fibonacci(n - 1) + Program.Fibonacci(n - 2);
}

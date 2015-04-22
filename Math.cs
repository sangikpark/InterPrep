using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Diagnostics;

namespace InterPrep
{
    class MathEx
    {
        // Fibonacci numbers are 0, 1, 1, 2, 3, 5, 8, 13, ... (add the last two to get the next) 
        // Fibonacci - iterative
        public static int FibonacciIterative(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            int prevPrev = 0;
            int prev = 1;
            int result = 0;

            for (int i = 2; i <= n; i++)
            {
                result = prev + prevPrev;
                prevPrev = prev;
                prev = result;
            }
            return result;
        }
   
        // Fibonacci - recursive
        public static int FibonacciRecursive(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            
            return FibonacciRecursive(n - 1) + FibonacciRecursive(n - 2);
        }

        // Fibonacci - dynamic programming (bottom-up)
        public static int FibonacciDynamic(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;

            // Memoization
            int[] f = new int[n+1];

            f[0] = 0;
            f[1] = 1;

            for (int i = 2; i <= n; i++)
            {
                f[i] = f[i - 1] + f[i - 2];
            }

            return f[n];
        }


        // Implement factorial
        // n! = n (n-1)!
        // 0! = 1! = 1

    	// Iterative solution
        public static int FactorialIterative(int n)
        {
	        int f = 1;
	        for (int i = n; i > 1; i--)
	            f *= i;
	        return f;
        }

        // Recursive solution
        public static int FactorialRecursive(int n)
        {
            if (n > 1) // Recursive case
                return n * FactorialRecursive(n - 1);
            else // Base case
                return 1;
        }


        // Prime number - A prime number is divisible only by 1 and itself
	// 2, 3, 5, 7, 11, 13, 17, 19...
        public static bool IsPrime(int num)
        {
            bool isPrime = true;

            // check for even number
            if (num % 2 == 0)
            {
                if (num == 2)
                {
                    return true;
                }
                return false;
            }

            // don't need to check past the square root
            for (int i = 3; i <= (int) (Math.Sqrt(num)); i += 2)
            {
                if (num % i == 0)
                {
                    isPrime = false;
                    break;
                }
            }
            return isPrime;
        }

        // Add two very large numbers that cannot be stored in int
        public static int[] AddLargeNumbers(string num1, string num2)
        {
            int min = (num1.Length < num2.Length) ? num1.Length : num2.Length;
            int max = (num1.Length < num2.Length) ? num2.Length : num1.Length;

            int[] n1 = new int[max];
            int[] n2 = new int[max];

            for (int i = 0; i < num1.Length; i++)
                n1[i] = num1[num1.Length - i - 1] - 48; // 48 = (int) '0'

            for (int i = 0; i < num2.Length; i++)
                n2[i] = num2[num2.Length - i - 1] - 48;


            int carry = 0;
            int[] sum = new int[max+1];

            for (int i = 0; i < max; i++)
            {
                int s = n1[i] + n2[i] + carry;                

                if (s >= 10)
                {
                    sum[i] =  s % 10;
                    carry = 1;
                }
                else
                {
                    sum[i] = s;
                    carry = 0;
                }
            }

            sum[max] = carry;

            return sum;
        }

        // Q: Implement divide method without using * operator
        public static int DivideUsingMultiplication(int numerator, int denominator)
        {
            int result = 0;

            if (numerator == 0)
                return result;
            
            if (denominator == 0)
                throw new DivideByZeroException("Error: Divided by 0");

            int sign = 1;

            if (numerator < 0)
            {
                numerator *= -1;
                sign = -1;
            }
            if (denominator < 0)
            {
                denominator *= -1;
                sign *= -1;
            }

            // using multiplication
            for (int i = 1; i * denominator <= numerator; i++)
            {
                result++;
            }

            return sign * result;
        }

        // Q: Implement divide method without using - operator
        public static int DivideUsingSubtraction(int numerator, int denominator)
        {
            int result = 0;

            if (numerator == 0)
                return result;

            if (denominator == 0)
                throw new DivideByZeroException("Error: Divided by 0");

            int sign = 1;

            if (numerator < 0)
            {
                numerator *= -1;
                sign = -1;
            }
            if (denominator < 0)
            {
                denominator *= -1;
                sign *= -1;
            }

            // using subtraction
            while ((numerator -= denominator) > 0)
            {
                result++;
            }

            return sign * result;
        }

        // Q: Multiply two integers without using multiplication, division and bitwise operators, and no loops
        // Time Complexity: O(y) where y is the second argument to function Multiply().
        public static int Multiply(int x, int y)
        {   
            if (x == 0 || y == 0)
                return 0;

            if (y > 0) // Add x one by one
                return (x + Multiply(x, y - 1));
            else // the case where y is negative
                return -Multiply(x, -y);
        }

        // Q: Add two numbers without using arithmetic operators
        public int Add(int x, int y)
        {
            // Iterate till there is no carry
            while (y != 0)
            {
                // carry now contains common set bits of x and y
                int carry = x & y;
                // Sum of bits of x and y where at least one of the bits is not set
                x = x ^ y;
                // Carry is shifted by one so that adding it to x gives the required sum
                y = carry << 1;
            }
            return x;
        }

        // Q: Find square root of an integer. The answer should be an integer closest to the actual square root.
        // For example: Input = 16, Output = 4. Input = 17, Output = 4, Input = 24, Output = 5
        // You cannot use math functions such as sqrt and pow
        public static int SqrtRoot(int input)
        {
            if (input < 0)
                return -1;

            double sqrt = 0;

            for (int i = 1; i <= (input/2) + 1; i++)
            {
                sqrt = i * i;

                if (sqrt == input)
                {
                    return i;
                }
                else if (sqrt > input)
                {
                    if ((sqrt - input) > (input - ((i - 1) * (i - 1))))
                    {
                        return i - 1;
                    }
                    else
                    {
                        return i;
                    }
                }
            }

            return -1;
        }

        public void SwapTwoNumbers(int a, int b)
        {
            a = a + b;

            b = a - b;

            a = a - b;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int result1 = MathEx.DivideUsingSubtraction(100, 3);
            result1 = MathEx.DivideUsingMultiplication(100, 3);

            result1 = MathEx.Multiply(-2, -3);

            System.Console.WriteLine(MathEx.FibonacciIterative(30));
            System.Console.WriteLine(MathEx.FibonacciRecursive(30));
            System.Console.WriteLine(MathEx.FibonacciDynamic(30));

            int[] sum = MathEx.AddLargeNumbers("9999", "9999");
            for (int i = sum.Length-1; i >= 0; i--)
            {
                System.Console.Write(sum[i]);
            }

            bool result = MathEx.IsPrime(1);

            Debug.Assert(MathEx.SqrtRoot(16) == 4);
            Debug.Assert(MathEx.SqrtRoot(17) == 4);
            Debug.Assert(MathEx.SqrtRoot(24) == 5);

            System.Console.ReadKey();
        }
    }
}

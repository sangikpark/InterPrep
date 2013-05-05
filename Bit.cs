using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterPrep
{
    class BitOperations
    {
        // Reverses bits in a byte 
        public static byte Reverse(byte b)
        {
            int rev = (b >> 4) | ((b & 0xf) << 4);
            rev = ((rev & 0xcc) >> 2) | ((rev & 0x33) << 2); // 0xcc = 11001100, 0x33 = 00110011
            rev = ((rev & 0xaa) >> 1) | ((rev & 0x55) << 1); // 0xaa = 10101010, 0x55 = 01010101

            return (byte)rev;
        }

        // Counts the number of ones in a variable
        public static int CountOnes(int input)
        {
            int sum = 0;

            while (input != 0)
            {
                sum += input & 0x01;
                input >>= 1;
            }

            return sum;
        }

        // Swap two integers without temp variable
        public static void Swap(int a, int b)
        {
            // Use XOR
            a = a ^ b;
            b = a ^ b;
            a = a ^ b;
        }
    }

    class Program
    {
        const byte b00000000 = 0x00; 
        const byte b00000001 = 0x01; 
        const byte b00000010 = 0x02; 
        const byte b00000011 = 0x03; 
        const byte b00000100 = 0x04;

        static void Main(string[] args)
        {
            BitOperations.Swap(1, 4);
            byte input = b00000001;
            byte output = BitOperations.Reverse(input);

            string hexValue = output.ToString("x");

            int countOnes = BitOperations.CountOnes(5);
        }
    }
}

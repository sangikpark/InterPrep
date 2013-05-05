using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace InterPrep
{
    public class BaseClass
    {
        public void NonVirtualMethod() { Console.WriteLine("NonVirtulMethod from BaseClass"); }

        public virtual void VirtulMethod() { Console.WriteLine("VirtulMethod from BaseClass"); }

    }
    public class DerivedClass : BaseClass
    {
        public new void NonVirtualMethod() { Console.WriteLine("NonVirtulMethod from DerivedClass"); }

        public override void VirtulMethod() { Console.WriteLine("VirtulMethod from DerivedClass"); }
    }


    class Program
    {
        static void Main(string[] args)
        {
            DerivedClass D = new DerivedClass();
            BaseClass B = (BaseClass)D;

            // Compile-time type of the instance
            D.NonVirtualMethod();
            B.NonVirtualMethod();

            // Run-time type of the instance
            D.VirtulMethod();
            B.VirtulMethod(); // VirtulMethod from DerivedClass!
            
            Debug.Assert(D.GetType() == typeof(DerivedClass));
            Debug.Assert(B.GetType() == typeof(DerivedClass));

            Console.ReadLine();
        }
    }
}

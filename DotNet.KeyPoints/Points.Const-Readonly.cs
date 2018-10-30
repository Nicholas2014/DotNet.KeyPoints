using System;

namespace DotNet.KeyPoints
{
    /// <summary>
    /// 静态常量 所谓静态常量就是在编译期间会对变量进行解析，再将常量的值替换成初始化的值。
    /// 动态常量 所谓动态常量就是编译期间会将变量标记只读常量，而不用常量的值代替，
    /// 这样在声明时可以不初始化，可以延迟到构造函数初始化
    /// </summary>
    public class ConstReadOnly
    {
        /*
         *  const修饰的常量是上述中的第一种，即静态常量，而readonly是上述中第二种即动态常量。
         *  他们的区别可以从静态常量和动态常量的特性来说明：
            const修饰的常量在声明时必须初始化值；readonly修饰的常量可以不初始化值，且可以延迟到构造函数。
            cons修饰的常量在编译期间会被解析，并将常量的值替换成初始化的值；而readonly延迟到运行的时候。
            const修饰的常量注重的是效率；readonly修饰的常量注重灵活。
            const修饰的常量没有内存消耗；readonly因为需要保存常量，所以有内存消耗。
            const只能修饰基元类型、枚举类、或者字符串类型;readonly却没有这个限制         
         *
         */

        static readonly int A = B * 10;
        static readonly int B = 10;
        public static void Test()
        {
            // readonly是动态常量，在编译期间是不会解析的，所以开始就是默认值， 
            // A和B都是int类型，值都是0，所以A=0*10=0,程序接着执行到B=10，才会真正的B的初值10赋给B
            Console.WriteLine("A is {0},B is {1} ", A, B);
            Console.ReadLine();
        }
    }
}

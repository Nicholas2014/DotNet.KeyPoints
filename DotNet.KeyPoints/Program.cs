using System;
using System.Threading;

namespace DotNet.KeyPoints
{
    class Program
    {
        #region Lock2

        const string firstOrderId = "001";
        const string secondOrderId = "002";
        const string thirdOrderId = "003";

        #endregion


        static void Main(string[] args)
        {
            try
            {
                #region Lock1

                //var test = new LockTest();
                //var t = new Thread(test.LockMe);
                //t.Start(false);
                //Thread.Sleep(100);

                //lock (test)
                //{
                //    // 调用没有被lock的方法
                //    test.DoNotLockMe();   
                //    // 调用被lock的方法，并试图将deadlock解除                                        
                //    test.LockMe(false);
                //}

                #endregion

                #region Lock2

                //test(LockType.LockThis);
                //test(LockType.LockString);
                //test(LockType.LockObject);
                //test(LockType.LockStaticObject);

                #endregion

                #region Singleton

                //for (int i = 0; i < 10; i++)
                //{
                //    Console.WriteLine(Singleton.GetInstance().GetHashCode());
                //    Thread.Sleep(500);
                //}

                #endregion

                ConstReadOnly.Test();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Thread.Sleep(5000);
            }

            Console.WriteLine($"Press any key to exit...");
            Console.ReadKey();
        }

        #region Lock

        static void test(LockType lockType)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------测试相同订单------------");
            Console.ForegroundColor = ConsoleColor.White;
            OrderPay(firstOrderId, 1, lockType);
            OrderPay(firstOrderId, 2, lockType);
            OrderPay(firstOrderId, 3, lockType);
            Thread.Sleep(10000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------测试不同订单------------");
            Console.ForegroundColor = ConsoleColor.White;
            OrderPay(firstOrderId, 1, lockType);
            OrderPay(secondOrderId, 1, lockType);
            OrderPay(thirdOrderId, 1, lockType);
        }

        static void OrderPay(string orderId, int threadNo, LockType lockType)
        {
            new Thread(() => new Payment(orderId, threadNo).Pay(lockType)).Start();

            Thread.Sleep(10);
        }

        #endregion
    }
}

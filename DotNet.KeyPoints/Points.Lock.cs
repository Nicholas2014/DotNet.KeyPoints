using System;
using System.Threading;

namespace DotNet.KeyPoints
{
    /// <summary>
    /// lock关键字
    /// 
    /// lock(objectA){codeB}看似简单，实际上有三个意思，这对于适当地使用它至关重要：
    /// 1.objectA被lock了吗？没有则由我来lock，否则一直等待，直至objectA被释放。
    /// 2.lock以后在执行codeB的期间其他线程不能调用codeB，也不能使用objectA。
    /// 3.执行完codeB之后释放objectA，并且codeB可以被其他线程访问。
    /// 
    /// 关键点：
    /// 1.lock(this)的缺点就是在一个线程锁定某对象之后导致整个对象无法被其他线程访问。
    /// 2.锁定的不仅仅是lock段里的代码，锁本身也是线程安全的。
    /// 3.我们应该使用不影响其他操作的私有对象作为locker。
    /// 4.在使用lock的时候，被lock的对象(locker)一定要是引用类型的，如果是值类型，
    /// 将导致每次lock的时候都会将该对象装箱为一个新的引用对象(事实上如果使用值类型，c#编译器在编译时会给出个错误）
    /// </summary>
    public class LockTest
    {
        private bool deadlocked = true;
        private static object locker = new object();

        //这个方法用到了lock，我们希望lock的代码在同一时刻只能由一个线程访问
        public void LockMe(object o)
        {
            //lock (this)
            lock (locker)
            {
                while (deadlocked)
                {
                    deadlocked = (bool)o;
                    Console.WriteLine("Foo: I am locked :(");
                    Thread.Sleep(500);
                }
            }
        }

        //所有线程都可以同时访问的方法
        public void DoNotLockMe()
        {
            Console.WriteLine("I am not locked :)");
        }
    }

    /// <summary>
    /// 单例模式
    /// </summary>
    public class Singleton
    {
        private Singleton()
        {

        }
        private static readonly object obj = new object();
        private static Singleton _instance;

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                lock (obj)
                {
                    if (_instance == null)
                    {
                        _instance = new Singleton();
                    }
                }
            }

            return _instance;
        }
    }

    public class Payment
    {
        private readonly string LockString;
        public readonly int ThreadNo;
        private readonly Object LockObj = new object();
        private static readonly Object StaticLockObj = new object();

        public Payment(string orderID, int threadNo)
        {
            LockString = orderID;
            ThreadNo = threadNo;
        }

        public void Pay(LockType lockType)
        {
            ShowMessage("等待锁资源");
            switch (lockType)
            {
                case LockType.LockThis:
                    lock (this)
                    {
                        showAction();
                    }
                    break;
                case LockType.LockString:
                    lock (LockString)
                    {
                        showAction();
                    }
                    break;
                case LockType.LockObject:
                    lock (LockObj)
                    {
                        showAction();
                    }
                    break;
                case LockType.LockStaticObject:
                    lock (StaticLockObj)
                    {
                        showAction();
                    }
                    break;
            }
            ShowMessage("释放锁");
        }

        private void showAction()
        {
            ShowMessage("进入锁并开始操作");
            Thread.Sleep(2000);
            ShowMessage("操作完成,完成时间为" + DateTime.Now);
        }

        private void ShowMessage(string message)
        {
            Console.WriteLine(String.Format("订单{0}的第{1}个线程 {2}", LockString, ThreadNo, message));
        }

    }

    public enum LockType
    {
        LockThis = 0,
        LockString = 1,
        LockObject = 2,
        LockStaticObject = 3
    }
}

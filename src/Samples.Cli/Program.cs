using System;

namespace Samples.Cli
{
    class Program
    {
        static void Main(string[] args)
        {
            Test(options =>
            {
                options.Option1 = "1";
                options.Option2 = "2";
                options.Option3 = "3";
            });

            Console.WriteLine("Hello World!");
        }

        static void Test(Action<MyOptions> options)
        {
            var t = new MyOptions();
            options(t);
        }
    }

    public class MyOptions
    {
        public string Option1 { get; set; }

        public string Option2 { get; set; }

        public string Option3 { get; set; }
    }
}

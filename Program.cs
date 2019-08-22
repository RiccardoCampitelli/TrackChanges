using System;
using test_app.TestCompareValues;

namespace Tracking
{
    class Program
    {
        static void Main(string[] args)
        {

            var firstEntity = new TestEntity(){
                one = 1,
                two = 2,
                three = 3,
                four = 4
            };

            var secondEntity = new TestEntity(){
                one = 1,
                two = 20,
                three = 3,
                four = 4
            };

            var changedProperties = firstEntity.GetChangedProperties<TestEntity>(secondEntity);

            Console.WriteLine("Hello World!");
        }
    }
}

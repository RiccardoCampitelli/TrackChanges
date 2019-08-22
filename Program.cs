using System;
using Newtonsoft.Json;
using test_app.TestCompareValues;
using Tracking.TestCompareValues.Models;

namespace Tracking
{
    class Program
    {
        static void Main(string[] args)
        {

            var nestedProp1 = new ExampleNestedProperty(){
                NestedOne = 1,
                NestedTwo = DateTime.Now
            };

            var nestedProp2 = new ExampleNestedProperty(){
                NestedOne = 1,
                NestedTwo = DateTime.Now
            };

            var firstEntity = new TestEntity(){
                one = 1,
                two = 2,
                three = 3,
                four = nestedProp1
            };

            var secondEntity = new TestEntity(){
                one = 1,
                two = 20,
                three = 3,
                four = nestedProp2
            };

            var changedProperties = firstEntity.GetChangedProperties<TestEntity>(secondEntity);

            var isEqual = firstEntity.UserVariablesEqual(secondEntity);

            Console.WriteLine(JsonConvert.SerializeObject(changedProperties));
            Console.WriteLine("IsEqual: {0}", isEqual);
            
        }
    }
}

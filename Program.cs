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
                NestedTwo = nestedProp1.NestedTwo
            };

            var firstEntity = new TestEntity(){
                one = 1,
                two = 22,
                three = 3,
                four = nestedProp1
            };

            var secondEntity = new TestEntity(){
                one = 1,
                two = 22,
                three = 33,
                four = nestedProp2
            };

            var changedProperties = firstEntity.GetChangedTrackedProperties<TestEntity>(secondEntity);

            var trackedPropertiesEqual = firstEntity.EntityPropertiesEqual(secondEntity, true);
            var allPropertiesEqual = firstEntity.EntityPropertiesEqual(secondEntity, false);

            Console.WriteLine(JsonConvert.SerializeObject(changedProperties));
            Console.WriteLine("Tracked Properties Equal: {0}", trackedPropertiesEqual);
            Console.WriteLine("All Properties Equal: {0}", allPropertiesEqual);
            
        }
    }
}

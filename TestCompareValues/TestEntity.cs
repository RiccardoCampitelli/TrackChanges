using test_app.TestCompareValues.Attributes;
using Tracking.TestCompareValues.Models;

namespace test_app.TestCompareValues
{
    public class TestEntity : TrackedEntity
    {
        public int one { get; set; }

        [CompareValues]
        public int two { get; set; }

        public int three { get; set; }
        [CompareValues]
        public ExampleNestedProperty four { get; set; }
    }
}
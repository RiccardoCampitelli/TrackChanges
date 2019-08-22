using test_app.TestCompareValues.Attributes;

namespace test_app.TestCompareValues
{
    public class TestEntity : TrackedEntity
    {
        public int one { get; set; }

        [CompareValues]
        public int two { get; set; }

        public int three { get; set; }
        [CompareValues]
        public int four { get; set; }
    }
}
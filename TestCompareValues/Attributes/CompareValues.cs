using System;

namespace test_app.TestCompareValues.Attributes {
    [AttributeUsage ( AttributeTargets.Property | 
                      AttributeTargets.Field,
                      Inherited = true)]
    public class CompareValues : Attribute {
        public CompareValues () { }
    }
}
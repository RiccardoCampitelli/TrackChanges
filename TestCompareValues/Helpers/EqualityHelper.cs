using Newtonsoft.Json;

namespace Tracking.TestCompareValues.Helpers {

    //Thank you StackOverflow 🎉
    public static class EqualityHelper {
        public static bool JsonCompare (object obj, object another) {
            if (ReferenceEquals (obj, another)) return true;
            if ((obj == null) || (another == null)) return false;
            if (obj.GetType () != another.GetType ()) return false;

            var objJson = JsonConvert.SerializeObject (obj);
            var anotherJson = JsonConvert.SerializeObject (another);

            return objJson == anotherJson;
        }
    }
}
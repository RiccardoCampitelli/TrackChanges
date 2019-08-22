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

    
    //TODO: Try fix this one ?
    // public static bool DeepCompare (this object obj, object another) {
    //     if (ReferenceEquals (obj, another)) return true;
    //     if ((obj == null) || (another == null)) return false;
    //     //Compare two object's class, return false if they are difference
    //     if (obj.GetType () != another.GetType ()) return false;

    //     var result = true;
    //     //Get all properties of obj
    //     //And compare each other
    //     foreach (var property in obj.GetType ().GetProperties ()) {
    //         var objValue = property.GetValue (obj);
    //         var anotherValue = property.GetValue (another);
    //         if (!objValue.Equals (anotherValue)) result = false;
    //     }

    //     return result;
    // }

    // public static bool FancyCompare (this object obj, object another) {
    //     if (ReferenceEquals (obj, another)) return true;
    //     if ((obj == null) || (another == null)) return false;
    //     if (obj.GetType () != another.GetType ()) return false;

    //     //properties: int, double, DateTime, etc, not class
    //     if (!obj.GetType ().IsClass) return obj.Equals (another);

    //     var result = true;
    //     foreach (var property in obj.GetType ().GetProperties ()) {
    //         var objValue = property.GetValue (obj);
    //         var anotherValue = property.GetValue (another);
    //         //Recursion 🤯
    //         if (!objValue.DeepCompare (anotherValue)) result = false;
    //     }
    //     return result;
    // }


    }
}
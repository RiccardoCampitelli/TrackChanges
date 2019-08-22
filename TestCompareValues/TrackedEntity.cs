using System;
using System.Collections.Generic;
using System.Linq;
using test_app.TestCompareValues.Attributes;

namespace test_app.TestCompareValues {
    public class TrackedEntity {
        private TrackedEntityProperty[] GetOwnTrackedProperties () {

            var type = this.GetType ();
            var properties = type.GetProperties ();
            var result = new List<TrackedEntityProperty> ();

            foreach (var property in properties) {

                var x = (CompareValues[]) property.GetCustomAttributes (typeof (CompareValues), false);

                var hasAttribute = Attribute.IsDefined (property, typeof (CompareValues));

                if (hasAttribute) {
                    result.Add (new TrackedEntityProperty {
                        value = property.GetValue (this, null),
                            name = property.Name
                    });
                }

            }

            return result.ToArray ();
        }

        private TrackedEntityProperty[] GetTrackedProperties<T> (T values) {

            var type = values.GetType ();
            var properties = type.GetProperties ();
            var result = new List<TrackedEntityProperty> ();

            foreach (var property in properties) {

                var x = (CompareValues[]) property.GetCustomAttributes (typeof (CompareValues), false);

                var hasAttribute = Attribute.IsDefined (property, typeof (CompareValues));

                if (hasAttribute) {
                    result.Add (new TrackedEntityProperty {
                        value = property.GetValue (values, null),
                            name = property.Name
                    });
                }

            }

            return result.ToArray ();

        }

        public bool UserVariablesEqual<T> (T newValues) {

            var currentTrackedValues = GetOwnTrackedProperties ();

            var newTrackedValues = GetTrackedProperties<T> (newValues);

            if (currentTrackedValues.Length != newTrackedValues.Length)
                return false;

            foreach (var newTrackedValue in newTrackedValues) {
                var exists = currentTrackedValues.Any (x => x.value == newTrackedValue.value && x.name == newTrackedValue.name);
                if (exists)
                    return false; //prop changed

                // not changed
            }

            return true;

        }

        public ChangedProperty[] GetChangedProperties<T> (T newValues) {
            var changedProperties = new List<ChangedProperty> ();

            var oldTrackedValues = GetOwnTrackedProperties ();

            var newTrackedValues = GetTrackedProperties<T> (newValues);

            // if(currentTrackedValues.Count != newTrackedValues.Count)
            //     return ;

            foreach (var newTrackedValue in newTrackedValues) {
                var changedValue = oldTrackedValues.FirstOrDefault (x => (x.name == newTrackedValue.name) && (x.value != newTrackedValue.value));

                if (changedValue != null)
                    changedProperties.Add (new ChangedProperty () {
                        NewValue = newTrackedValue.value,
                            OldValue = changedValue.value,
                            PropertyName = changedValue.name
                    });
            }

            //  currentTrackedValues.FirstOrDefault(y => y.name == x.name && y.value == )

            // foreach (var newTrackedValue in newTrackedValues)
            // {
            //     foreach (var currentTrackedValue in currentTrackedValues)
            //     {
            //         if(!(newTrackedValue.value == currentTrackedValue.value && newTrackedValue.name == currentTrackedValue.name))
            //             changedProperties.Add(
            //                 new ChangedProperty{
            //                 NewValue=newTrackedValue.value, 
            //                 OldValue = currentTrackedValue.value, 
            //                 PropertyName = newTrackedValue.name
            //                 });
            //     }
            //     // not changed
            // }

            return changedProperties.ToArray ();

        }

        // public void UpdateUserVariables<T>(T newValues){

        //     var currentTrackedValues = GetOwnTrackedProperties();

        //     var newTrackedValues = GetTrackedProperties<T>(newValues);

        //     if(currentTrackedValues.Count != newTrackedValues.Count)
        //         return;

        //     foreach (var newTrackedValue in newTrackedValues)
        //     {
        //         var index = currentTrackedValues.FindIndex(x => x.value == newTrackedValue.value && x.name == newTrackedValue.name);
        //         if(index == -1)
        //             DoSomething(); //prop changed

        //         // not changed
        //     }

        //     //assign new values
        // }

        public void DoSomething () {

        }
    }
}
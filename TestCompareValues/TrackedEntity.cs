using System;
using System.Collections.Generic;
using System.Linq;
using test_app.TestCompareValues.Attributes;
using Tracking.TestCompareValues.Helpers;

namespace test_app.TestCompareValues {
    public class TrackedEntity {
       

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

        public bool UserVariablesEqual(){


            return true;
        }

        public bool TrackedPropertiesEqual<T> (T newValues) {

            var currentTrackedValues = GetTrackedProperties (this);

            var newTrackedValues = GetTrackedProperties<T> (newValues);

            if (currentTrackedValues.Length != newTrackedValues.Length)
                return false;

            foreach (var newTrackedValue in newTrackedValues) {
                var exists = currentTrackedValues.Any (x => x.name == newTrackedValue.name &&
                    !EqualityHelper.JsonCompare (x.value, newTrackedValue.value));

                if (exists)
                    return false;

            }

            return true;

        }

        public ChangedProperty[] GetChangedProperties<T> (T newValues) {
            var changedProperties = new List<ChangedProperty> ();

            var oldTrackedValues = GetTrackedProperties (this);

            var newTrackedValues = GetTrackedProperties<T> (newValues);

            // TODO: Maybe wrap class and return error messages ?
            // if(currentTrackedValues.Count != newTrackedValues.Count) 
            //     return ;

            foreach (var newTrackedValue in newTrackedValues) {
                var changedValue = oldTrackedValues.FirstOrDefault (x => (x.name == newTrackedValue.name) &&
                    (!EqualityHelper.JsonCompare (x.value, newTrackedValue.value)));

                if (changedValue != null)
                    changedProperties.Add (new ChangedProperty () {
                        NewValue = newTrackedValue.value,
                            OldValue = changedValue.value,
                            PropertyName = changedValue.name
                    });
            }

            return changedProperties.ToArray ();

        }


        // public void UpdateUserVariables<T>(T newValues){

     

        public void DoSomething () {

        }
    }
}
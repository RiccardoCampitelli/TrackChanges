using System;
using System.Collections.Generic;
using System.Linq;
using test_app.TestCompareValues.Attributes;
using Tracking.TestCompareValues.Helpers;

namespace test_app.TestCompareValues {
    public class TrackedEntity {

        private EntityProperty[] GetEntityProperties<T> (T values, bool onlyTracked) {

            var type = values.GetType ();
            var properties = type.GetProperties ();
            var result = new List<EntityProperty> ();

            foreach (var property in properties) {

                //   var x = (CompareValues[]) property.GetCustomAttributes (typeof (CompareValues), false);

                var hasAttribute = Attribute.IsDefined (property, typeof (CompareValues));

                if (onlyTracked) {
                    if (hasAttribute) {
                        result.Add (new EntityProperty {
                            value = property.GetValue (values, null),
                                name = property.Name
                        });
                    }
                } else {
                    result.Add (new EntityProperty {
                        value = property.GetValue (values, null),
                            name = property.Name
                    });
                }

            }

            return result.ToArray ();

        }

    
        public bool EntityPropertiesEqual<T> (T newValues, bool onlyTracked) {

            var currentTrackedValues = GetEntityProperties (this, onlyTracked);

            var newTrackedValues = GetEntityProperties<T> (newValues, onlyTracked);

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

        public ChangedProperty[] GetChangedTrackedProperties<T> (T newValues) {
            var changedProperties = new List<ChangedProperty> ();

            var oldTrackedValues = GetEntityProperties (this, true);

            var newTrackedValues = GetEntityProperties<T> (newValues, true);

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
    }
}
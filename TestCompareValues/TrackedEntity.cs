using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using test_app.TestCompareValues.Attributes;
using Tracking.TestCompareValues.Helpers;

namespace test_app.TestCompareValues {
    public class TrackedEntity {

        private EntityProperty[] GetEntityProperties<T> (T values, bool onlyTracked) {

            var type = values.GetType ();
            var properties = type.GetProperties ();
            var result = new List<EntityProperty> ();

            foreach (var property in properties) {

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

        public bool EntityPropertiesEqual (object values, bool onlyTracked) {

            var currentValues = GetEntityProperties (this, onlyTracked);

            var newValues = GetEntityProperties (values, onlyTracked);

            if (currentValues.Length != newValues.Length)
                return false;

            foreach (var newValue in newValues) {
                var exists = currentValues.Any (x => x.name == newValue.name &&
                    !EqualityHelper.JsonCompare (x.value, newValue.value));

                if (exists)
                    return false;

            }

            return true;

        }

        public ChangedProperty[] GetChangedProperties<T> (T values, bool onlyTracked) {
            var changedProperties = new List<ChangedProperty> ();

            var oldTrackedValues = GetEntityProperties (this, onlyTracked);

            var newTrackedValues = GetEntityProperties<T> (values, onlyTracked);

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

        public void Update<T> (T newValues) {

            Type type = this.GetType();

            var changedProperties = GetChangedProperties (newValues, false);

            var localProperties = GetEntityProperties(this,false);

            var propertiesToUpdate = localProperties.Where(prop => changedProperties.Any(x => x.PropertyName == prop.name));

            foreach (var propertyToUpdate in propertiesToUpdate)
            {
                PropertyInfo prop = type.GetProperty(propertyToUpdate.name);
                
                prop.SetValue(this, changedProperties.First(x => x.PropertyName == propertyToUpdate.name).NewValue, null);
            }

        }
    }
}
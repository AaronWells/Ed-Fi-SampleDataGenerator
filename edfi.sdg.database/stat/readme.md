#Stat Tables
Stat tables contain statistical lookup information used by value providers to populate objects.

##Columns
1. Id - The Id column's value should correspond to the value. That is to say that a unique value will always have the same Id, no matter how many entries exist for it in the table.

1. Value - This varchar(max) value contains the value for the property that may be assigned as a result. 

1. Attribute - one of the property values to match 

1. Prop100k - the number of instances of Value if 100,000 entities sharing Attribute were surveyed

##Process
1. The Stat lookup value provider is configured with the name of a Stat table, the property to be set and properties required to set that property.

1. An object is presented requiring a property to be set.

1. The actual values (attribute values) of the object are extracted and presented to the Stat lookup value provider

1. The stat table is queried for all values matching the attribute values

1. The weights (Prop100k) of all matching attributes are summed, and a list of Ids with their cumulative sums is collected

1. A random number between 0 and max(cumulative sum) is selected

1. The corresponding Value is returned and assigned to the property on the object.

##Notes
The value column may contain a serialized object

Creating a row with a unique Id, Value, and Attribute has the effect of creating a 100% possibility that the value will be selected when the Attribute is matched.
# AutoComparer
This library aims to provide a simple extension method which compares properties across objects without having to define a custom hashcode override.

The library will map those properties with a matching name and type and compare their values to see if they are equal.

**The main advantage and reason why this was built is not having to update the hashcode override function when a new field is changed**

## Usage
In its simplest form
```
using AutoComparer;

bool equal = obj1.ValueEquals(obj2);
```

## FAQ
Let's cover some corner cases...

Q: what if there is no overlap of any field of the two objects?
A: `false`, at least 1 property should match


Q: property name matches in the other object, but the type is different, what happens?
A: It will be a `false`, as the mapping will be valid, but types are incompatible. No implicit conversion is made.


Q: In case of nested objects, what happens to the nested object? Will it use the default `Equals` method?
A: There is an optional parameter that you can set called `recursivelyCheckInnerObjects`. By default it does, but if the parameter is set to `true` it will instead recursively call `ValueEquals` for all properties which are Classes (structures are treated with `Equals`).


Q: Mapping nullables and non-nullables?
A: Implicitly handled

## Roadmap
More features could be added, like
* custom type transformers
* custom value transformers
* custom name overrides (for matching properties even if the name is different)
A virtual, infinite array. The default value (null, none, 0, false, etc) acts as if it exists and is subject to all transformations.

Allows you to transform elements, even into other types. Allows you to translate an entire axis. Allows elementwise operations with two arrays.

Mostly pure-ish, still allows in place value changes like a regular array. Use at your own risk.

Current implementation is slow (using dictionaries), which performs badly on dense data.

Uses include: Intersections of arrays of unknown size. Boolean tricks in many dimensions. Something else, probably.

Base class is for one dimension, can be used for any number of dimensions by nesting them a-la a jagged array. Accessing dimensions is very easy, just do array[a][b][c][d][etc]...

Operations in higher dimensions may be kind of cumbersome as equality functions have to be provided for certain operations.

The default Func<> must always return a newly constructed value if you dont want weird shared state errors.

Included are 3 helper functions to make building a new 2d, 3d or 4d array slightly less cumbersome.

Use at your own risk.

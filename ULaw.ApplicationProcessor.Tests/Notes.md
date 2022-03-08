General issues with code:
 - Enums which include the "Enum" in the name
 - Incorrect/inconsistent capitalisation on the Enum members
 - Multiple Enums in same file
 - Enums specify `int` as the type which is unnecessary as that is the default base type of an enum
 - Due to the default value of an `int` being zero, uninstantiated use of these enums will result in the first value being used which could cause problems. A guard member should be added at position zero to ensure correct use of the Enum.
 - File location does not fit with Namespace declarations (i.e. the enums declare themselves to be in the `Enums` subnamespace but are in the base folder with the rest of the code)
 - Superfluous use of "Attribute" on "DescriptionAttribute" (should just be "Description")
 - Use of Enum for DegreeSubject seems overly restrictive and would result in a requirement to re-compile should a new subject be required
 - Unrequired "Using" statements in files
 - Inconsistent naming used for method declarations
 - DateTime could cause issues with timezones when/if performing calculations
 - Variables have been defined using specifically named types where the keyword `var` is generally preferred these days

Issues with Application class:
 - Constructor takes a mix of parameters which are not directly related to one another in an order which harms clarity
 - Constructor relies on Guid - should not be instantiating a dependency directly
 - No requirement for `private set` - the properties are only set within the constructor. Removing `private set` would allow them to be read-only preventing possible future mistakes.
 - `string.Format` prevents clarity on what is being used where. String interpolation would be clearer
 - `string.Format` used in cases where no formatting is required
 - There doesn't seem to be value in `DegreeGrade` and `DegreeSubject` being externally settable properties.
 - The `Process` method has sections of the response duplicated
 - The logic for determining the response is obscured due to the nesting of `if` statements
 - Handrolled HTML with StringBuilder is almost certainly not the right way to generate the response. If an HTML response is required it'd be better to build up using a class specifically designed to create HTML, or have a templating system used so that the output is not hardcoded into the system and can be end-user editable.
 - The HTML has errors/formatting issues. (i.e. `<p/>` is not a good construct, whitespace is included which may be ignored when rendered)
 - The logic for accepting `1st`s and `2:2`s in specific subjects if overly restrictive/uses hardcoded values. Changes in business requirements to acceptable degrees would result in a requirement to redevelop/recompile the code.
 - Use of `this` is not specifically required in most instances for the code as-is and following style guides for naming would have eliminated it entirely
 - The line `this.RequiresVisa = RequiresVisa` is ignoring the provided parameter and assigning the default `bool` which `RequiresVisa` would have instantiated with back to itself.
 - `ApplicationId`, `Faculty`, `Title`, `LastName`, `DateOfBirth`, and `RequiresVisa` are not used within the code.
 - Use of `ToLongDateString()` is locale dependent and will produce different results depending on system settings. Unit tests which pass on one machine may fail on another and what is sent to the applicant will differ.
 - Use of hardcoded deposit value would, again, cause issues if business requirements change/evolve.
 - One of the `Yours sincerely` lines is inconsistent with the rest. The test case has been updated to account for this.
 - There is no validation being performed on the parameters being passed into the method. It could be the case that there isn't a requirement to ensure the values fit within some criteria, but this seems unlikely.

Issues in Unit Tests:
 - Expected and actual results are the wrong way round in the `Assert`s. Although this is sufficient to ascertain success/failure it makes debugging problematic as the wrong thing may end up being adjusted.
 - Testing could have been changed to make use of a set of input options and output expectations rather than having the same boilerplate written out each time.
 - Unit tests do not sufficiently cover the expected uses. e.g. Each test case uses `Test` as the `FirstName` of the applicant - this would continue to pass if the name `Test` was hardcoded into the class under test and code coverage systems would show that all the code is being tested.

Approach used:
 - I have grouped together the data which is being transferred to the result processor into objects which encapsulate the relevant parts of the data together. e.g. all characteristics of the applicant go in one class, the details of course applied for into another, etc.
 - The logical grouping of the different pieces of data is done via interfaces so that the actual implementation of the classes is abstracted away. This helps both in terms of being able to define different logic at implementation and aids testing as the details passed to the class under test can be mocked without needing to consider dependencies of the concrete classes.
 - The test processor has been reimplemented to use the new interface dependencies and had the culture/locale sensitive date methods removed in favour of format strings which then cause it to match the data within the tests. The format string is something which should be moved out into a settings system so that there can be consistency over the whole codebase as to how dates are formatted.
 - Beyond the changes required due to renaming some types, the text inconsistency mentioned above, and reordering the parameters in the `Assert`, I have pretty much left the `OfferTests` file alone. This would need to be updated at some stage.
 - A new `Application` class has been added to the `Tests` project to act as a shim to allow the existing tests to function with the newly defined logic.
 - `Moq` has been used to allow the mocking of the interfaces that the Processor now utilises.
 - Only "in-use" properties have been added to the interfaces to allow the unit tests to pass and to demonstrate the required refactoring.
 - The class under test has been renamed for greater clarity on what it is doing
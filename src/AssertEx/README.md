
# MSTest.TestFramework.AssertExtensions

## ShouldThrow

This class holds additional functionality that can be validated on an exception.


### M:MSTest.TestFramework.AssertExtensions.#ctor(exception)

The constructor.

| Name | Description |
| ---- | ----------- |
| exception | *System.Exception*<br>The exception context. |

### .Exception

The exception Thrown.


### M:MSTest.TestFramework.AssertExtensions.VerifyContains(propertyName, propertyValue, substring)

Verify if a string contains another string. Throws exception if not.

| Name | Description |
| ---- | ----------- |
| propertyName | *System.String*<br>The name of the string/property being verified. |
| propertyValue | *System.String*<br>Its value. |
| substring | *System.String*<br>The substring to verify for. |

### M:MSTest.TestFramework.AssertExtensions.WithExactMessage(message)

Ensures that the exception has exactly the specified message.

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>The expected exception message. |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  When the message is not exactly similar to the exception message. 


#### Returns

The current instance of the ShouldThrow class so this can be chained with other asserts.


### M:MSTest.TestFramework.AssertExtensions.WithInnerException``1

Ensures that the exception contains an inner exception of the specified type.


#### Returns

The current instance of the ShouldThrow class so this can be chained with other asserts.


### M:MSTest.TestFramework.AssertExtensions.WithMessage(message)

Ensures that the exception contains the specified message.

| Name | Description |
| ---- | ----------- |
| message | *System.String*<br>The expected exception message. |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  When the message is not part of the exception message. 


#### Returns

The current instance of the ShouldThrow class so this can be chained with other asserts.


### M:MSTest.TestFramework.AssertExtensions.WithStackTrace(stackTrace)

Ensures that the exception contains the specified stack trace.

| Name | Description |
| ---- | ----------- |
| stackTrace | *System.String*<br>The stack trace. |


#### Returns




#### Returns

The current instance of the ShouldThrow class so this can be chained with other asserts.


## ThrowsEx

Extensions for the MSTest Assertions.


### M:MSTest.TestFramework.AssertExtensions.DoesNotThrow(assert, action)

Asserts that the delegate does not throw an exception.

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br>The assert class. |
| action | *System.Action*<br>Delegate to code to be tested and which is expected to throw exception. |

### M:MSTest.TestFramework.AssertExtensions.DoesNotThrow(assert, action)

Asserts that the delegate does not throw an exception.

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br>The assert class. |
| action | *System.Func{System.Object}*<br>Delegate to code to be tested and which is expected to throw exception. |

### M:MSTest.TestFramework.AssertExtensions.Throws``1(assert, action)

Tests whether the code specified by delegate throws exception of type 'T'

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br> The assert class.  |
| action | *System.Action*<br> Delegate to code to be tested and which is expected to throw exception.  |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  Thrown if does not throws exception of type . 


#### Returns

 The context assertion for further actions. 


### M:MSTest.TestFramework.AssertExtensions.Throws``1(assert, action)

Tests whether the code specified by delegate throws exception of type 'T'

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br> The assert class.  |
| action | *System.Func{System.Object}*<br> Delegate to code to be tested and which is expected to throw exception.  |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  Thrown if does not throws exception of type . 


#### Returns

 The context assertion for further actions. 


### M:MSTest.TestFramework.AssertExtensions.ThrowsInnerException``1(assert, action)

Tests whether the code specified by delegate throws exact given exception/has inner exception of type (and not of derived type).

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br> The assert class.  |
| action | *System.Action*<br> Delegate to code to be tested and which is expected to throw exception.  |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  Thrown if does not throws exception of type . 


#### Returns

 The type of exception expected to be thrown. 


### M:MSTest.TestFramework.AssertExtensions.ThrowsInnerException``1(assert, action)

Tests whether the code specified by delegate throws exact exception/has inner exception of type (and not of derived type).

| Name | Description |
| ---- | ----------- |
| assert | *Microsoft.VisualStudio.TestTools.UnitTesting.Assert*<br> The assert class.  |
| action | *System.Func{System.Object}*<br> Delegate to code to be tested and which is expected to throw exception.  |

*Microsoft.VisualStudio.TestTools.UnitTesting.AssertFailedException:*  Thrown if does not throws exception/contain inner-exception of type . 


#### Returns

 The type of exception expected to be thrown. 




# MSTest.TestFramework.TestRunExtensions


## FlakyTestClassAttribute

Add this on top of a class, so all test methods in it are gifted a retry logic that executes tests 'n' number of times until they pass.


### M:MSTest.TestFramework.TestRunExtensions.#ctor(retryCount)

The constructor.

| Name | Description |
| ---- | ----------- |
| retryCount | *System.Int32*<br>The maximum number of times to re-try to make the test pass. |

### M:MSTest.TestFramework.TestRunExtensions.GetTestMethodAttribute(testMethodAttribute)

Gets the FlakyTestMethodAttribute if not already.

| Name | Description |
| ---- | ----------- |
| testMethodAttribute | *Microsoft.VisualStudio.TestTools.UnitTesting.TestMethodAttribute*<br> |


#### Returns

The FlakyTestMethodAttribute attribute.


## FlakyTestMethodAttribute

Add this on top of Flaky tests to enable a retry logic that executes tests 'n' number of times until they pass.


### M:MSTest.TestFramework.TestRunExtensions.#ctor(retryCount)

The constructor.

| Name | Description |
| ---- | ----------- |
| retryCount | *System.Int32*<br>The maximum number of times to re-try making the test pass. |

### M:MSTest.TestFramework.TestRunExtensions.Execute(testMethod)

Runs the flaky test method.

| Name | Description |
| ---- | ----------- |
| testMethod | *Microsoft.VisualStudio.TestTools.UnitTesting.ITestMethod*<br>The test method to run. |


#### Returns

The test results.



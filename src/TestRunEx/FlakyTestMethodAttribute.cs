
namespace MSTest.TestFramework.TestRunExtensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Linq;

    /// <summary>
    /// Add this on top of Flaky tests to enable a retry logic that executes tests 'n' number of times until they pass.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class FlakyTestMethodAttribute : TestMethodAttribute
    {
        private int retryCount;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="retryCount">The maximum number of times to re-try making the test pass.</param>
        public FlakyTestMethodAttribute(int retryCount)
        {
            this.retryCount = retryCount;
        }

        /// <summary>
        /// Runs the flaky test method.
        /// </summary>
        /// <param name="testMethod">The test method to run.</param>
        /// <returns>The test results.</returns>
        public override TestResult[] Execute(ITestMethod testMethod)
        {
            var currentCount = 0;
            TestResult[] result = null;
            do
            {
                try
                {
                    result = base.Execute(testMethod);
                }
                catch (Exception)
                {
                    currentCount++;
                }

                if(result.Any((tr) => tr.Outcome == UnitTestOutcome.Failed))
                {
                    currentCount++;
                }
                else
                {
                    break;
                }
            } while (currentCount < this.retryCount);

            return result;
        }
    }
}

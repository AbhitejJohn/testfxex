
namespace MSTest.TestFramework.TestRunExtensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Add this on top of a class, so all test methods in it are gifted a retry logic that executes tests 'n' number of times until they pass.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class FlakyTestClassAttribute : TestClassAttribute
    {
        private int retryCount;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="retryCount">The maximum number of times to re-try to make the test pass.</param>
        public FlakyTestClassAttribute(int retryCount)
        {
            this.retryCount = retryCount;
        }

        /// <summary>
        /// Gets the FlakyTestMethodAttribute if not already.
        /// </summary>
        /// <param name="testMethodAttribute"></param>
        /// <returns>The FlakyTestMethodAttribute attribute.</returns>
        public override TestMethodAttribute GetTestMethodAttribute(TestMethodAttribute testMethodAttribute)
        {
            if (testMethodAttribute is FlakyTestMethodAttribute) return testMethodAttribute;

            return new FlakyTestMethodAttribute(this.retryCount);
        }
    }
}

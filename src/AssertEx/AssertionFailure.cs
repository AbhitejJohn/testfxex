
namespace MSTest.TestFramework.AssertExtensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Central place to handle all assertion failures.
    /// </summary>
    internal class AssertionFailure
    {
        public static void Handle(string message)
        {
            throw new AssertFailedException(message);
        }

        internal static void Handle(string message, Exception exception)
        {
            throw new AssertFailedException(message, exception);
        }
    }
}


namespace MSTest.TestFramework.AssertExtensions
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;

    /// <summary>
    /// Central place to handle all assertion failures.
    /// </summary>
    internal class AssertionFailure
    {
        /// <summary>
        /// Throws a AssertFailedException with the message provided.
        /// </summary>
        /// <param name="message"></param>
        public static void Handle(string message)
        {
            throw new AssertFailedException(message);
        }

        /// <summary>
        /// Throws a AssertFailedException with the message and exception provided.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        internal static void Handle(string message, Exception exception)
        {
            throw new AssertFailedException(message, exception);
        }
    }
}

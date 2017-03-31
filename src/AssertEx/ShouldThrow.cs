
namespace MSTest.TestFramework.AssertExtensions
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// This class holds additional functionality that can be validated on an exception.
    /// </summary>
    public class ShouldThrow
    {
        private Exception exception;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="exception">The exception context.</param>
        public ShouldThrow(Exception exception)
        {
            this.exception = exception;
        }

        /// <summary>
        /// Ensures that the exception contains the specified message.
        /// </summary>
        /// <param name="message">The expected exception message.</param>
        /// <exception cref="AssertFailedException">
        /// When the message is not part of the exception message.
        /// </exception>
        public void WithMessage(string message)
        {
            StringAssert.Contains(this.exception.Message, message);
        }

        /// <summary>
        /// Ensures that the exception has exactly the specified message.
        /// </summary>
        /// <param name="message">The expected exception message.</param>
        /// <exception cref="AssertFailedException">
        /// When the message is not exactly similar to the exception message.
        /// </exception>
        public void WithExactMessage(string message)
        {
            Assert.AreEqual(this.exception.Message, message);
        }
    }
}

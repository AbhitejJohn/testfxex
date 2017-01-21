
namespace Microsoft.VisualStudio.TestTools.UnitTesting.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;
    
    public static class AssertEx
    {
        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact given exception of type <typeparamref name="T"/> (and not of derived type)
        /// and throws 
        /// <code>
        /// AssertFailedException
        /// </code>
        /// if code does not throws exception or throws exception of type other than <typeparamref name="T"/>.
        /// </summary>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <param name="message">
        /// The message expected from the thrown exception.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The type of exception expected to be thrown.
        /// </returns>
        public static void ThrowsExceptionWithMessage<T>(Action action, string message) where T : Exception
        {
            var exception = Assert.ThrowsException<T>(action);

            StringAssert.Contains(exception.Message, message);
        }

        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact given exception/has inner exception of type <typeparamref name="T"/> (and not of derived type)
        /// and throws.
        /// <code>
        /// AssertFailedException
        /// </code>
        /// if code does not throws exception or throws exception of type other than <typeparamref name="T"/>.
        /// </summary>
        /// <param name="action">
        /// Delegate to code to be tested and which is expected to throw exception.
        /// </param>
        /// <param name="message">
        /// The message to include in the exception when <paramref name="action"/>
        /// does not throws exception of type <typeparamref name="T"/>.
        /// </param>
        /// <typeparam name="T">
        /// Type of exception expected to be thrown.
        /// </typeparam>
        /// <exception cref="AssertFailedException">
        /// Thrown if <paramref name="action"/> does not throws exception of type <typeparamref name="T"/>.
        /// </exception>
        /// <returns>
        /// The type of exception expected to be thrown.
        /// </returns>
        public static T ThrowsInnerException<T>(Action action, string message) where T : Exception
        {
            string finalMessage;

            try
            {
                action();
            }
            catch (Exception ex)
            {
                Exception innerException = ex;
                StringBuilder exTypeBuilder = new StringBuilder();

                do
                {
                    var exType = innerException.GetType();
                    if (typeof(T).Equals(exType))
                    {
                        return (T)ex;
                    }
                    exTypeBuilder.Append(exType);
                    innerException = ex.InnerException;
                } while (innerException != null);

                finalMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Could not find Exception of type {1} in exception chain {2}. {0} \r\n Exception Message {3} \r\n Stack Trace {4}",
                        message ?? "(null)",
                        typeof(T).Name,
                    exTypeBuilder.ToString(),
                    ex.Message,
                    ex.StackTrace);

                Assert.Fail("Assert.ThrowsException {0}", finalMessage);
            }

            finalMessage = string.Format(
                CultureInfo.CurrentCulture,
                FrameworkMessages.NoExceptionThrown,
                message ?? "(null)",
                typeof(T).Name);
            Assert.Fail("Assert.ThrowsException {0}", finalMessage);

            // This will not hit, but need it for compiler.
            return null;
        }
    }
}


namespace Microsoft.VisualStudio.TestTools.UnitTesting.Extensions
{
    using System;
    using System.Globalization;
    using System.Text;
    
    public static class AssertEx
    {
        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact given exception of type <typeparamref name="T"/> (and not of derived type)
        /// with the specified message and throws 
        /// <code>
        /// AssertFailedException
        /// </code>
        /// if code does not throw exception that contains the specified message or throws exception of type other than <typeparamref name="T"/>.
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
        public static T ThrowsExceptionWithMessage<T>(Action action, string message) where T : Exception
        {
            var exception = Assert.ThrowsException<T>(action);

            StringAssert.Contains(exception.Message, message);

            return exception;
        }

        /// <summary>
        /// Tests whether the code specified by delegate <paramref name="action"/> throws exact given exception of type <typeparamref name="T"/> (and not of derived type)
        /// with the specified message and throws 
        /// <code>
        /// AssertFailedException
        /// </code>
        /// if code does not throw exception that contains the specified message or throws exception of type other than <typeparamref name="T"/>.
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
        public static T ThrowsExceptionWithMessage<T>(Func<object> action, string message) where T : Exception
        {
            return ThrowsExceptionWithMessage<T>(() => { action(); }, message);
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
        public static T ThrowsInnerException<T>(Func<object> action) where T : Exception
        {
            return ThrowsInnerException<T>(() => { action(); });
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
        public static T ThrowsInnerException<T>(Action action) where T : Exception
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
                        return (T)innerException;
                    }
                    if(exTypeBuilder.Length != 0)
                    {
                        exTypeBuilder.Append(", ");
                    }

                    exTypeBuilder.Append(exType);
                    innerException = innerException.InnerException;
                } while (innerException != null);

                finalMessage = string.Format(
                        CultureInfo.CurrentCulture,
                        "Could not find Exception of type {0} in exception chain {1}. \r\n Exception Message {2} \r\n Stack Trace {3}",
                        typeof(T).Name,
                    exTypeBuilder.ToString(),
                    ex.Message,
                    ex.StackTrace);

                Assert.Fail("Assert.ThrowsException {0}", finalMessage);
            }

            finalMessage = string.Format(
                CultureInfo.CurrentCulture,
                "No exception thrown. {0} exception was expected",
                typeof(T).Name);

            Assert.Fail("Assert.ThrowsException {0}", finalMessage);

            // This will not hit, but need it for compiler.
            return null;
        }
    }
}

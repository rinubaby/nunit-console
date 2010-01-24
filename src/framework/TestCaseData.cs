// ***********************************************************************
// Copyright (c) 2008 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections;
using System.Collections.Specialized;

// TODO: Remove conditional code
namespace NUnit.Framework
{
    /// <summary>
    /// The TestCaseData class represents a set of arguments
    /// and other parameter info to be used for a parameterized
    /// test case. It provides a number of instance modifiers
    /// for use in initializing the test case.
    /// 
    /// Note: Instance modifiers are getters that return
    /// the same instance after modifying it's state.
    /// </summary>
    public class TestCaseData : ITestCaseData
    {
        #region Constants
#if !NUNITLITE
        private static readonly string CATEGORIES = "_CATEGORIES";
#endif
        #endregion

        #region Instance Fields
        /// <summary>
        /// The argument list to be provided to the test
        /// </summary>
        private object[] arguments;

#if !NUNITLITE
        /// <summary>
        /// The expected result to be returned
        /// </summary>
        private object result;

        /// <summary>
        ///  The expected exception Type
        /// </summary>
        private Type expectedExceptionType;

        /// <summary>
        /// The FullName of the expected exception
        /// </summary>
        private string expectedExceptionName;

        //private string expectedMessage;
        //private MessageMatch matchType;

        /// <summary>
        /// The name to be used for the test
        /// </summary>
        private string testName;

        /// <summary>
        /// The description of the test
        /// </summary>
        private string description;

        /// <summary>
        /// A dictionary of properties, used to add information
        /// to tests without requiring the class to change.
        /// </summary>
        private IDictionary properties;

        /// <summary>
        /// If true, indicates that the test case is to be ignored
        /// </summary>
        bool isIgnored;

        /// <summary>
        /// The reason for ignoring a test case
        /// </summary>
        string ignoreReason;
#endif
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public TestCaseData(params object[] args)
        {
            if (args == null)
                this.arguments = new object[] { null };
            else
                this.arguments = args;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
        /// </summary>
        /// <param name="arg">The argument.</param>
        public TestCaseData(object arg)
        {
            this.arguments = new object[] { arg };
        }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    public TestCaseData(object arg1, object arg2)
    {
        this.arguments = new object[] { arg1, arg2 };
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:TestCaseData"/> class.
    /// </summary>
    /// <param name="arg1">The first argument.</param>
    /// <param name="arg2">The second argument.</param>
    /// <param name="arg3">The third argument.</param>
    public TestCaseData(object arg1, object arg2, object arg3)
    {
        this.arguments = new object[] { arg1, arg2, arg3 };
    }
        #endregion

        #region ITestCaseData Members
        /// <summary>
        /// Gets the argument list to be provided to the test
        /// </summary>
        public object[] Arguments
        {
            get { return arguments; }
        }

#if !NUNITLITE
        /// <summary>
        /// Gets the expected result
        /// </summary>
        public object Result
        {
            get { return result; }
        }

        /// <summary>
        ///  Gets the expected exception Type
        /// </summary>
        public Type ExpectedException
        {
            get { return expectedExceptionType; }
        }

        /// <summary>
        /// Gets the FullName of the expected exception
        /// </summary>
        public string ExpectedExceptionName
        {
            get { return expectedExceptionName; }
        }

        /// <summary>
        /// Gets or sets the expected message of the expected exception
        /// </summary>
        /// <value>The expected message of the exception.</value>
        public string ExpectedMessage
        {
            get { return null; }
        }

        /// <summary>
        ///  Gets or sets the type of match to be performed on the expected message
        /// </summary>
        public MessageMatch MatchType
        {
            get { return 0; }
        }

        /// <summary>
        /// Gets the name to be used for the test
        /// </summary>
        public string TestName
        {
            get { return testName; }
        }

        /// <summary>
        /// Gets the description of the test
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="ITestCaseData"/> is ignored.
        /// </summary>
        /// <value><c>true</c> if ignored; otherwise, <c>false</c>.</value>
        public bool Ignored
        {
            get { return isIgnored; }
        }

        /// <summary>
        /// Gets the ignore reason.
        /// </summary>
        /// <value>The ignore reason.</value>
        public string IgnoreReason
        {
            get { return ignoreReason; }
        }
#endif
        #endregion

#if !NUNITLITE
        #region Additional Public Properties
        /// <summary>
        /// Gets a list of categories associated with this test.
        /// </summary>
        public IList Categories
        {
            get
            {
                if (Properties[CATEGORIES] == null)
                    Properties[CATEGORIES] = new StringCollection();

                return (IList)Properties[CATEGORIES];
            }
        }

        /// <summary>
        /// Gets the property dictionary for this test
        /// </summary>
        public IDictionary Properties
        {
            get
            {
                if (properties == null)
                    properties = new ListDictionary();

                return properties;
            }
        }
        #endregion

        #region Fluent Instance Modifiers
        /// <summary>
        /// Sets the expected result for the test
        /// </summary>
        /// <param name="result">The expected result</param>
        /// <returns>A modified TestCaseData</returns>
        public TestCaseData Returns(object result)
        {
            this.result = result;
            return this;
        }

        /// <summary>
        /// Sets the expected exception type for the test
        /// </summary>
        /// <param name="exceptionType">Type of the expected exception.</param>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData Throws(Type exceptionType)
        {
            this.expectedExceptionType = exceptionType;
            this.expectedExceptionName = exceptionType.FullName;
            return this;
        }

        /// <summary>
        /// Sets the expected exception type for the test
        /// </summary>
        /// <param name="exceptionName">FullName of the expected exception.</param>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData Throws(string exceptionName)
        {
            this.expectedExceptionName = exceptionName;
            return this;
        }

        /// <summary>
        /// Sets the name of the test case
        /// </summary>
        /// <returns>The modified TestCaseData instance</returns>
        public TestCaseData SetName(string name)
        {
            this.testName = name;
            return this;
        }

        /// <summary>
        /// Sets the description for the test case
        /// being constructed.
        /// </summary>
        /// <param name="description">The description.</param>
        /// <returns>The modified TestCaseData instance.</returns>
        public TestCaseData SetDescription(string description)
        {
            this.description = description;
            return this;
        }

        /// <summary>
        /// Applies a category to the test
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public TestCaseData SetCategory(string category)
        {
            this.Categories.Add(category);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, string propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, int propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }

        /// <summary>
        /// Applies a named property to the test
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <returns></returns>
        public TestCaseData SetProperty(string propName, double propValue)
        {
            this.Properties.Add(propName, propValue);
            return this;
        }

        /// <summary>
        /// Ignores this TestCase.
        /// </summary>
        /// <returns></returns>
        public TestCaseData Ignore()
        {
            isIgnored = true;
            return this;
        }

        /// <summary>
        /// Ignores this TestCase, specifying the reason.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <returns></returns>
        public TestCaseData Ignore(string reason)
        {
            isIgnored = true;
            ignoreReason = reason;
            return this;
        }
        #endregion
#endif
    }
}

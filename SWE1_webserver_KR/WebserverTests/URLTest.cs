using System;
using SWE1_webserver_KR;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebserverTests
{
    [TestClass]
    public class URLTest
    {
        [TestMethod]
        public void SimpleURL()
        {
            string test_Url = "https://myserver.com";

            Dictionary<string, string> expected_parameters = new Dictionary<string, string>();

            HttpUrl URL_Handler = new HttpUrl();
            URL_Handler.CWebURL(test_Url);

            string actual_Address = URL_Handler.WebAddress;
            Dictionary<string, string> actual_parameters = URL_Handler.WebParameters;

            Assert.AreEqual(actual_Address, test_Url, "Address was not parsed correctly");
            CollectionAssert.AreEquivalent(actual_parameters, expected_parameters, "The parameters were not parsed correctly");

        }

        [TestMethod]
        public void URL_with_single_Parameter()
        {
            string test_Url = "https://myserver.com?data=helloWorld";
            string expected_Url = "https://myserver.com";

            Dictionary<string, string> expected_parameters = new Dictionary<string, string>();
            expected_parameters.Add("data", "helloWorld");

            HttpUrl URL_Handler = new HttpUrl();
            URL_Handler.CWebURL(test_Url);

            string actual_Address = URL_Handler.WebAddress;
            Dictionary<string, string> actual_parameters = URL_Handler.WebParameters;

            Assert.AreEqual(actual_Address, expected_Url, "Address was not parsed correctly");
            CollectionAssert.AreEquivalent(actual_parameters, expected_parameters, "The parameters were not parsed correctly");

        }

        [TestMethod]
        public void URL_with_single_Parameter_with_Whitespace()
        {
            string test_Url = "https://myserver.com?data=hello%20World";
            string expected_Url = "https://myserver.com";

            Dictionary<string, string> expected_parameters = new Dictionary<string, string>();
            expected_parameters.Add("data", "hello World");

            HttpUrl URL_Handler = new HttpUrl();
            URL_Handler.CWebURL(test_Url);

            string actual_Address = URL_Handler.WebAddress;
            Dictionary<string, string> actual_parameters = URL_Handler.WebParameters;

            Assert.AreEqual(actual_Address, expected_Url, "Address was not parsed correctly");
            CollectionAssert.AreEquivalent(actual_parameters, expected_parameters, "The parameters were not parsed correctly");

        }

        [TestMethod]
        public void URL_with_multiple_Parameters()
        {
            string test_Url = "https://myserver.com?data=helloWorld&data2=stuff";
            string expected_Url = "https://myserver.com";

            Dictionary<string, string> expected_parameters = new Dictionary<string, string>();
            expected_parameters.Add("data", "helloWorld");
            expected_parameters.Add("data2", "stuff");

            HttpUrl URL_Handler = new HttpUrl();
            URL_Handler.CWebURL(test_Url);

            string actual_Address = URL_Handler.WebAddress;
            Dictionary<string, string> actual_parameters = URL_Handler.WebParameters;

            Assert.AreEqual(actual_Address, expected_Url, "Address was not parsed correctly");
            CollectionAssert.AreEquivalent(actual_parameters, expected_parameters, "The parameters were not parsed correctly");

        }

        [TestMethod]
        public void URL_with_unicode_chars()
        {
            string test_Url = "https://myserver.com?street=Wiener Straße";
            string expected_Url = "https://myserver.com";

            test_Url = System.Net.WebUtility.UrlEncode(test_Url);

            Dictionary<string, string> expected_parameters = new Dictionary<string, string>();
            expected_parameters.Add("street", "Wiener Straße");

            HttpUrl URL_Handler = new HttpUrl();
            URL_Handler.CWebURL(test_Url);

            string actual_Address = URL_Handler.WebAddress;
            Dictionary<string, string> actual_parameters = URL_Handler.WebParameters;

            Assert.AreEqual(actual_Address, expected_Url, "Address was not parsed correctly");
            CollectionAssert.AreEquivalent(actual_parameters, expected_parameters, "The parameters were not parsed correctly");

        }
    }
}

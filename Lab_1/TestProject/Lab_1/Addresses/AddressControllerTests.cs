using Lab_1.Addresses;
using NUnit.Framework;

namespace TestProject.Lab_1.Addresses
{
    public class AddressControllerTests
    {
        [TestCase("0.0.0.255", true)]
        [TestCase("0.0.0.256", false)]
        [TestCase("-1.0.0.255", false)]
        [TestCase("0.0.0", false)]
        [TestCase("0.0.0.fff", false)]
        [TestCase("", false)]
        public void IsIPv4Address_Works(string address, bool expected)
        {
            Assert.AreEqual(expected, AddressController.IsIPv4(address));
        }

        [TestCase("30ff:A044:a03f:fff:f:f", true)]
        [TestCase("0:0:0:256ff:a:0", false)]
        [TestCase("0:00:", false)]
        [TestCase("0.0.0", false)]
        [TestCase("0:00:ffg", false)]
        [TestCase("", false)]
        public void IsIPv6Address_Works(string address, bool expected)
        {
            Assert.AreEqual(expected, AddressController.IsIPv6(address));
        }

        [TestCase("30ff:A044:a033:ffa:f:f/127", true)]
        [TestCase("0.0.0.255/3", true)]
        [TestCase("30ff:A044:a033:ffa:f:f/129", false)]
        [TestCase("0.0.0.255/33", false)]
        [TestCase("30ff:A044:a033:ffa:f:f/1k", false)]
        [TestCase("0.0.0.255/3/4", false)]
        [TestCase("0.0.0.255/-1", false)]
        [TestCase("", false)]
        public void IsCidrAddress_Works(string address, bool expected)
        {
            Assert.AreEqual(expected, AddressController.IsCidr(address));
        }
    }
}
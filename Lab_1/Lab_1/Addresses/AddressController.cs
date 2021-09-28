using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;

namespace Lab_1.Addresses
{
    /// <summary>
    /// Controller for addresses
    /// </summary>
    public static class AddressController
    {
        /// <summary>
        /// Checks if address is CIDR type
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns><see langword="true"/> if address is CIDR type, otherwise <see langword="false"/></returns>
        public static bool IsCidr(string address)
        {
            var parts = address.Split('/');
            if (parts.Length != 2 || !int.TryParse(parts[1], out var bits))
            {
                return false;
            }

            if (IsIPv4(parts[0]))
            {
                return bits >= 0 && bits <= 32;
            }

            if (IsIPv6(parts[0]))
            {
                return bits >= 0 && bits <= 128;
            }

            return false;
        }

        /// <summary>
        /// Checks if address is IPv4 type
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns><see langword="true"/> if address is IPv4 type, otherwise <see langword="false"/></returns>
        public static bool IsIPv4(string address)
        {
            var bytes = address.Split('.');
            return bytes.Length == 4 && bytes.All(b => int.TryParse(b, out var res) && res >= 0 && res <= 255);
        }

        /// <summary>
        /// Checks if address is IPv6 type. Checks only default representation.
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns><see langword="true"/> if address is IPv6 type, otherwise <see langword="false"/></returns>
        public static bool IsIPv6(string address)
        {
            var bytes = address.Split(':');
            return bytes.Length == 6 && bytes.All(b => b.Length <= 4 && int.TryParse(b, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out _));
        }
    }
}
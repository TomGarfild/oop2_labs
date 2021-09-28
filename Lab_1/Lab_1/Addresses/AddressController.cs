using System;
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

        /// <summary>
        /// Gets ips range from CIDR address
        /// </summary>
        /// <param name="address">Address to check</param>
        /// <returns>Tuple of min and max ip addresses</returns>
        public static Tuple<string, string> CidrToIPRange(string address)
        {
            var parts = address.Split('/');
            if (parts.Length != 2 || !int.TryParse(parts[1], out var bits))
            {
                return default;
            }

            if (bits >= 0 && bits < 32 && IsIPv4(parts[0]))
            {
                var values = parts[0].Split('.').Select(int.Parse).ToArray();
                var num = 1L * values[0] * (1 << 24) + values[1] * (1 << 16) + values[2] * (1 << 8) + values[3] + 1 << (32 - bits - 1);
                
                if (num > uint.MaxValue)
                {
                    return default;
                }

                var maxAddress = string.Empty;

                for (var i = 24; i > 0; i -= 8)
                {
                    maxAddress += num / (2 ^ i) + ".";
                    num %= 2 ^ i;
                }

                return new Tuple<string, string>(parts[0], maxAddress + num);
            }

            if ( bits >= 0 && bits < 128 && IsIPv6(parts[0]))
            {
                var values = parts[0].Split(':').Select(int.Parse).ToArray();
                var num = 1L * values[0] * (1 << 24) + values[1] * (1 << 16) + values[2] * (1 << 8) + values[3] + 1 << (32 - bits - 1);

                if (num > uint.MaxValue)
                {
                    return default;
                }

                var maxAddress = string.Empty;

                for (var i = 24; i > 0; i -= 8)
                {
                    maxAddress += num / (2 ^ i) + ".";
                    num %= 2 ^ i;
                }

                return new Tuple<string, string>(parts[0], maxAddress + num);
            }

            return default;
        }
    }
}
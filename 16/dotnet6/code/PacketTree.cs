namespace AdventOfCode
{
    public class PacketTree
    {
        private readonly short _type; // Packet type (see PacketTypes)
        private List<short> _literalValues = new(); // List for literal values
        private readonly string _rawString; // The string used for construction
        private int _subPacketCount = 0; // Count of subpackets. Could be replaced with a Count
        private readonly short _version; // Version number
        private short _literalValueCount = 0; // Count of literals. Could be replaced with a Count
        private List<PacketTree> _subPackets = new(); // List containing the subpackets
        private bool _isLengthCount; // Flag for Operator length type. Used in ToBinaryString
        private short _subPacketBits = 0; // Subpacket bits for Operator types

        // Enum for packet types
        private enum PacketTypes : short
        {
            Sum,
            Product,
            Minimum,
            Maximum,
            Literal,
            GreaterThan,
            LessThan,
            Equal
        }

        public PacketTree(string binaryString)
        {
            _rawString = binaryString; // Save the constructor value
            _version = Convert.ToInt16(binaryString[0..3], 2); // Set the version
            _type = Convert.ToInt16(binaryString[3..6], 2); // Set the packet type
            if (_type == (int)PacketTypes.Literal)
            {
                parseLiteral(); // Parse Literal packets
            }
            else
            {
                ParseOperator(); // Parse Operator packets
            }
        }

        public long VersionTotal()
        {
            // Return the version plus the recursive total of all subpacket versions
            return _version + _subPackets.Sum(packet => packet.VersionTotal());
        }

        private void ParseOperator()
        {
            var tempString = _rawString[6..];
            if (tempString[0] == '0')
            {
                // Bit count type
                _isLengthCount = false;
                // Get the number of bits for the subpackets
                _subPacketBits = Convert.ToInt16(tempString[1..16], 2);
                // Get the bits for all the subpackets
                tempString = tempString[16..(16 + _subPacketBits)];
                var tally = 0; // Counter for the bits processed
                while (tally < _subPacketBits) // Loop through all subpackets bits
                {
                    // Create a new PacketTree object from the bits
                    var temp = new PacketTree(tempString);
                    _subPackets.Add(temp); // Add to the subpackets list
                                           // Get the length of the subpacket just added
                    var len = temp.ToBinaryString(false).Length;
                    // Remove the bits for the subpacket just added
                    tempString = tempString[len..];
                    tally += len; // Keep track of the bits processed so far
                }
            }
            else
            {
                _isLengthCount = true; // Set the bit count type
                                       // Get the number of subpackets
                var subPackets = Convert.ToInt16(tempString[1..12], 2);
                tempString = tempString[12..]; // Remove the subpacket header
                for (var subPacket = 0; subPacket < subPackets; subPacket++)
                {
                    // Create a new PacketTree
                    var temp = new PacketTree(tempString);
                    // Add the new PacketTree to the subpackets list
                    _subPackets.Add(temp);
                    // Remove the bits we just processed
                    tempString = tempString[temp.ToBinaryString(false).Length..];
                }
            }
            // Store the number of subpackets
            _subPacketCount = _subPackets.Count;
        }

        private void parseLiteral()
        {
            var tempString = _rawString[6..]; // Remove the header bits
                                              // Keep adding literal values while the "more to come" bit is set
            while (tempString.Length > 5 && tempString[0] == '1')
            {
                _literalValues.Add(Convert.ToInt16(tempString[1..5], 2));
                tempString = tempString[5..]; // Remove the current literal 
            }
            // Add the literal from the last group
            _literalValues.Add(Convert.ToInt16(tempString[1..5], 2));
            // Store the number of literals
            _literalValueCount = (short)_literalValues.Count;
        }

        public string ToBinaryString(bool Padding = true)
        {
            // Parse the version number to a 3 bit binary string
            var ret = Convert.ToString(_version, 2).PadLeft(3, '0')[^3..];
            // Append the type number as a 3 bit binary string
            ret += Convert.ToString(_type, 2).PadLeft(3, '0')[^3..];
            if (_type == (int)PacketTypes.Literal)
            {
                // Loop through each literal value
                for (var literal = 0; literal < _literalValueCount - 1; literal++)
                {
                    // If there is > 1 value then set the "more to come" bit
                    ret += "1";
                    // Add the value as a 4 bit binary string
                    ret += Convert.ToString(_literalValues[literal], 2).PadLeft(4, '0');
                }
                ret += "0"; // Set the group end flag
                            // Add the value as a 4 bit binary string
                ret += Convert.ToString(_literalValues[^1], 2).PadLeft(4, '0');
            }
            else
            {
                if (_isLengthCount) // Process bit count Operator type
                {
                    ret += "1"; // Append bit count flag
                                // Append the bit count as an 11 bit binary string
                    ret += Convert.ToString(_subPacketCount, 2).PadLeft(11, '0');
                    // Append the binary strings for each subpacket
                    ret = _subPackets.Aggregate(ret, (current, subPacket) => current + subPacket.ToBinaryString(false));
                }
                else
                {
                    ret += "0"; // Append the packet count flag
                                // Append the number of subpackets as a 15 bit binary string
                    ret += Convert.ToString(_subPacketBits, 2).PadLeft(15, '0');
                    // Append the binary strings for each subpacket
                    ret += _subPackets.Aggregate("", (current, sub) => current + sub.ToBinaryString(false)).PadRight(_subPacketBits, '0');
                }
            }

            var padding = ret.Length % 8;
            if (Padding && padding != 0)
            {
                // If the Padding flag was passed then pad to a valid HEX string
                ret = ret.PadRight(ret.Length + (8 - padding), '0');
            }
            return ret;
        }
        public void Process()
        {
            switch (_type)
            {
                case (int)PacketTypes.Sum:
                    Calculation = 0;
                    foreach (var subPacket in _subPackets)
                    {
                        subPacket.Process();
                        Calculation += subPacket.Calculation;
                    }

                    break;
                case (int)PacketTypes.Product:
                    Calculation = 1;
                    foreach (var subPacket in _subPackets)
                    {
                        subPacket.Process();
                        Calculation *= subPacket.Calculation;
                    }

                    break;
                case (int)PacketTypes.Minimum:
                    Calculation = 0;
                    var min = new List<long>();
                    foreach (var subPacket in _subPackets)
                    {
                        subPacket.Process();
                        min.Add(subPacket.Calculation);
                    }

                    Calculation = min.Min();
                    break;
                case (int)PacketTypes.Maximum:
                    Calculation = 0;
                    var max = new List<long>();
                    foreach (var subPacket in _subPackets)
                    {
                        subPacket.Process();
                        max.Add(subPacket.Calculation);
                    }

                    Calculation = max.Max();
                    break;
                case (int)PacketTypes.Literal:
                    Calculation = _literalValues[0];
                    break;
                case (int)PacketTypes.GreaterThan:
                    Calculation = 0;
                    foreach (var subPacket in _subPackets) subPacket.Process();
                    Calculation = _subPackets[0].Calculation > _subPackets[1].Calculation ? 1 : 0;
                    break;
                case (int)PacketTypes.LessThan:
                    Calculation = 0;
                    foreach (var subPacket in _subPackets) subPacket.Process();
                    Calculation = _subPackets[0].Calculation < _subPackets[1].Calculation ? 1 : 0;
                    break;
                case (int)PacketTypes.Equal:
                    Calculation = 0;
                    foreach (var subPacket in _subPackets) subPacket.Process();
                    Calculation = _subPackets[0].Calculation == _subPackets[1].Calculation ? 1 : 0;
                    break;

                default:
                    Console.WriteLine("This could bee error!!");
                    break;
            }
        }
        // Return a string representation of the packet type
        public string TypeStr => ((PacketTypes)_type).ToString();
        public short Version => _version; // Public GET for the version
        public short LiteralValueCount => _literalValueCount; // Public Literal count GET
        public int SubPacketCount => _subPacketCount; // Public subpacket count GET
        public long Calculation { get; private set; } = -1L;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketHeaders.Filters
{
    public enum IPTarget
    {
        DestinationIP = 0,
        SourceIP = 1
    }

    public enum PortTarget
    {
        DestinationPort = 0,
        SourcePort = 1
    }
    
    public enum FilterOperators
    {
        Equal = 0,
        NotEqual = 1,
        Like = 2
    }

    public enum FilterStatus
    {
        Confirmed = 0,
        NotConfirmed = 1
    }
}

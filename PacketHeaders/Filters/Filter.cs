using System.Collections.Generic;

namespace PacketHeaders.Filters
{
    public class Filter
    {
        private List<IPFilter> _ipFilters;

        public List<IPFilter> IPFilters
        {
            get { return this._ipFilters; }
        }

        public Filter()
        {
            this._ipFilters = new List<IPFilter>();
        }

        public bool FallsIntoFilter(IPHeader ipHeader, TCPHeader tcpHeader, UDPHeader udpHeader, DNSHeader dnsHeader)
        {
            return this.FallsIntoIpFilter(ipHeader);
        }

        private bool FallsIntoIpFilter(IPHeader ipHeader)
        {
            lock (this._ipFilters)
            {
                if (ipHeader != null && this.IPFilters.Count > 0)
                {
                    string incomingSourceIpAddress = ipHeader.SourceAddress.ToString();
                    string incomingDestinationIpAddress = ipHeader.DestinationAddress.ToString();

                    foreach (var ipFilter in this.IPFilters)
                    {
                        var filteredIpAddress = ipFilter.FilterByIP;

                        switch (ipFilter.IPTarget)
                        {
                            case IPTarget.DestinationIP:
                                switch (ipFilter.Operator)
                                {
                                    case FilterOperators.Equal:

                                        if (incomingDestinationIpAddress != filteredIpAddress)
                                            return false;

                                        break;
                                    case FilterOperators.NotEqual:

                                        if (incomingDestinationIpAddress == filteredIpAddress)
                                            return false;

                                        break;
                                    case FilterOperators.Like:

                                        if (!incomingDestinationIpAddress.StartsWith(filteredIpAddress))
                                            return false;

                                        break;
                                }
                                break;
                            case IPTarget.SourceIP:
                                switch (ipFilter.Operator)
                                {
                                    case FilterOperators.Equal:

                                        if (incomingSourceIpAddress != filteredIpAddress)
                                            return false;

                                        break;
                                    case FilterOperators.NotEqual:

                                        if (incomingSourceIpAddress == filteredIpAddress)
                                            return false;

                                        break;
                                    case FilterOperators.Like:

                                        if (!incomingSourceIpAddress.StartsWith(filteredIpAddress))
                                            return false;

                                        break;
                                }
                                break;
                        }
                    }
                }
            }
            return true;
        }
    }
}

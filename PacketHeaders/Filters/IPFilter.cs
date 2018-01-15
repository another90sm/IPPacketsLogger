namespace PacketHeaders.Filters
{
    public class IPFilter
    {
        private IPTarget _ipTarget;
        private FilterOperators _operator;
        private FilterStatus _filterStatus;
        private string _filterByIp;

        public IPTarget IPTarget
        {
            get { return this._ipTarget; }
        }

        public FilterOperators Operator
        {
            get { return this._operator; }
        }

        public string FilterByIP
        {
            get { return this._filterByIp; }
        }

        public FilterStatus FilterStatus
        {
            get { return this._filterStatus; }
            set { this._filterStatus = value; }
        }

        public IPFilter()
        {
            this._ipTarget = IPTarget.DestinationIP;
            this._operator = FilterOperators.Equal;
            this._filterByIp = string.Empty;
            this._filterStatus = FilterStatus.NotConfirmed;
        }

        public IPFilter(IPTarget ipTarget, FilterOperators fOperator, string filterByIp, FilterStatus status)
        {
            this._ipTarget = ipTarget;
            this._operator = fOperator;
            this._filterByIp = filterByIp;
            this._filterStatus = status;
        }

        public bool CheckFilter(out string msg)
        {
            msg = string.Empty;

            if (this.FilterByIP == string.Empty)
            {
                msg = "Please enter IP address.";
                return false;
            }
            return true;
        }
    }
}

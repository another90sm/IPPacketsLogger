using PacketHeaders.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PLogger.UserInterface
{
    public partial class FilterForm : Form
    {
        private Filter _filter;
        private BindingList<IPFilter> _resultIpFilters;

        public FilterForm(Filter filter)
        {
            InitializeComponent();
            this._filter = filter;
            this.targetIpComboBox.DataSource = Enum.GetValues(typeof(IPTarget));
            this.operationComboBox.DataSource = Enum.GetValues(typeof(FilterOperators));

            this.ipFilterdataGridView.AutoGenerateColumns = false;
            this._resultIpFilters = new BindingList<IPFilter>(this._filter.IPFilters.Where(x => x.FilterStatus == FilterStatus.Confirmed).ToList());
            this.ipFilterdataGridView.DataSource = _resultIpFilters;
        }
                
        private void AddIpFilterButton_Click(object sender, EventArgs e)
        {
            IPTarget ipTarget;
            Enum.TryParse<IPTarget>(this.targetIpComboBox.SelectedValue.ToString(), out ipTarget);

            FilterOperators filterOperator;
            Enum.TryParse<FilterOperators>(this.operationComboBox.SelectedValue.ToString(), out filterOperator);

            string ipAddress = this.ipAddressTextBox.Text;

            IPFilter filter = new IPFilter(ipTarget, filterOperator, ipAddress.Trim(), FilterStatus.NotConfirmed);

            string errorMessage = string.Empty;

            if (filter.CheckFilter(out errorMessage))
            {
                this._resultIpFilters.Add(filter);
                this.ipAddressTextBox.Text = string.Empty;
            }
            else
            {
                MessageBox.Show(errorMessage, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void IpFilterdataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == this.ipFilterdataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == this.ipFilterdataGridView.Columns["Delete"].Index)
            {
                this._resultIpFilters.RemoveAt(e.RowIndex);
            }
        }

        private void IpFilterdataGridView_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == this.ipFilterdataGridView.NewRowIndex || e.RowIndex < 0)
                return;

            if (e.ColumnIndex == this.ipFilterdataGridView.Columns["Delete"].Index)
            {
                this.ipFilterdataGridView.Cursor = Cursors.Hand;
            }
            else
            {
                this.ipFilterdataGridView.Cursor = Cursors.Arrow;
            }
        }
                
        private void SaveButton_Click(object sender, EventArgs e)
        {
            this._resultIpFilters
                .ToList()
                .ForEach(x => x.FilterStatus = FilterStatus.Confirmed);

            this._filter.IPFilters.Clear();
            this._filter.IPFilters.AddRange(this._resultIpFilters);
        }

        private void DeleteNotConfirmedFilters()
        {
            var notConfirmedFilters = this._resultIpFilters.Where(x => x.FilterStatus == FilterStatus.NotConfirmed);

            notConfirmedFilters
                .ToList()
                .ForEach(x => this._resultIpFilters.Remove(x));

            this._filter.IPFilters.Clear();
            this._filter.IPFilters.AddRange(this._resultIpFilters);
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DeleteNotConfirmedFilters();
        }

        private void FilterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DeleteNotConfirmedFilters();
        }
    }
}

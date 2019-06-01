using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Crane_Logs_Report_Creator_Controls
{
    public partial class CraneDelayBreakdown : Form
    {
        public CraneDelayBreakdown(DataTable Delays)
        {
            InitializeComponent();
            DelayView.AutoGenerateColumns = false;
            this.Delays = Delays;
            DelayView.DataSource = Delays;
        }

        private DataTable Delays;

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4kTiles_Frontend.MVVM.Models.Report
{
    public class ReportModel
    {
        public int ReportId { get; set; }
        public int SongId { get; set; }
        public int AccountId { get; set; }
        public string ReportTitle { get; set; } = null!;
        public string ReportReason { get; set; } = null!;
        public DateTime ReportDate { get; set; }
        public bool ReportStatus { get; set; }
    }
}

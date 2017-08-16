using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardMobileApp.Models
{

    public class Projects
    {
        public int projectId { get; set; }
        public string projectName { get; set; }
        public Projectmetric[] projectMetrics { get; set; }
    }

    public class Projectmetric
    {
        public string metricName { get; set; }
        public float[] ll { get; set; }
        public float[] ul { get; set; }
        public float[] actuals { get; set; }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashboardMobileApp.Models
{
    public class Rootobject
    {
        public Value[] value { get; set; }
        public object[] formatters { get; set; }
        public object[] contentTypes { get; set; }
        public object declaredType { get; set; }
        public object statusCode { get; set; }
    }

    public class Value
    {
        public int accountId { get; set; }
        public int serviceLineId { get; set; }
        public int projectTypeId { get; set; }
        public string projectName { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int id { get; set; }
        public int createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public int updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
        public bool isActive { get; set; }
    }
}

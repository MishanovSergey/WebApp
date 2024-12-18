using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBConnect.Models
{
    public class NewsInfoModel
    {
        public int Id { get; set; }
        public string? Header { get; set; }
        public string? PostTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandartsClient.Models
{
    public class Standart
    {
        public int Id { get; set; }

        public string Header { get; set; }

        public string Details { get; set; }

        public int StandartTypeId { get; set; }
    }
}

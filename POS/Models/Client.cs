using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Models
{
    class Client
    {
        public Client()
        {

        }

        public string cID { get; set; }
        public string cFName { get; set; }
        public string cLName { get; set; }
        public string cCNIC { get; set; }
        public string cEmail { get; set; }
        public string cDesc { get; set; }
        public bool cStatus { get; set; }
    }
}

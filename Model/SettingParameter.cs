using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEE_dotNET.Model
{
    public class SettingParameter
    {
        public string? Machine_Id { get; set; }
        public bool Remember_me { get; set; }
        public int DecrypLength { get; set; }
    }
}

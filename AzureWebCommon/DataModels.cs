using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureWebCommon
{
    public abstract class IdBaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class Customer : IdBaseModel
    {
    }
}

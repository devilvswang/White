using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Model
{
    public class RelationTree
    {
        public string name { get; set; }

        public string title { get; set; }

        public List<RelationTree> children { get; set; }

    }
}

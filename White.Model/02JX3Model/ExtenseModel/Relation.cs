using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace White.Model
{
    public partial class Relation
    {
        public List<Relation> ChildrenRelationList { get; set; }
    }
}

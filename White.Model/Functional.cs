//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace White.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class Functional
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ParentID { get; set; }
        public bool IsDealer { get; set; }
        public bool IsValid { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int CreateUserID { get; set; }
        public int CreateLoginID { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<int> UpdateUserID { get; set; }
        public Nullable<int> UpdateLoginID { get; set; }
    }
}

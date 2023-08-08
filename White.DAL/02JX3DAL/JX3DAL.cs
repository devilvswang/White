 
/*------------------------------------
 * 
 * T4模板自动生成数据操作类
 * 生成时间：2020-05-11 09:55:23
 * 
*--------------------by WN---------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White.Model;

namespace White.DAL
{
	public partial class RelationDAL : BaseDAL<Relation>
    {
		public RelationDAL():base("JX3Context")
        {
        }
    }
	public partial class ScreenShotDAL : BaseDAL<ScreenShot>
    {
		public ScreenShotDAL():base("JX3Context")
        {
        }
    }
	public partial class UserDAL : BaseDAL<User>
    {
		public UserDAL():base("JX3Context")
        {
        }
    }
}
 
/*------------------------------------
 * 
 * T4模板自动生成业务操作类
 * 生成时间：2020-05-11 09:55:44
 * 
*--------------------by WN---------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White.DAL;
using White.Model;

namespace White.BLL
{
	public partial class RelationBLL : BaseBLL<Relation>
    {
		public RelationBLL()
		{
			dal = new BaseDAL<Relation>("JX3Context");
		}
    }
	public partial class ScreenShotBLL : BaseBLL<ScreenShot>
    {
		public ScreenShotBLL()
		{
			dal = new BaseDAL<ScreenShot>("JX3Context");
		}
    }
	public partial class UserBLL : BaseBLL<User>
    {
		public UserBLL()
		{
			dal = new BaseDAL<User>("JX3Context");
		}
    }
}
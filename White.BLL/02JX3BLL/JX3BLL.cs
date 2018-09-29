 
/*------------------------------------
 * 
 * T4模板自动生成业务操作类
 * 生成时间：2018-09-13 11:36:06
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
}
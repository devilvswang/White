 
/*------------------------------------
 * 
 * T4模板自动生成业务操作类
 * 生成时间：2018-08-01 11:05:23
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
	public partial class Action_LogBLL : BaseBLL<Action_Log>
    {
		public Action_LogBLL()
		{
			dal = new BaseDAL<Action_Log>("AdminContext");
		}
    }
	public partial class Error_LogBLL : BaseBLL<Error_Log>
    {
		public Error_LogBLL()
		{
			dal = new BaseDAL<Error_Log>("AdminContext");
		}
    }
	public partial class FunctionalBLL : BaseBLL<Functional>
    {
		public FunctionalBLL()
		{
			dal = new BaseDAL<Functional>("AdminContext");
		}
    }
	public partial class ResourceBLL : BaseBLL<Resource>
    {
		public ResourceBLL()
		{
			dal = new BaseDAL<Resource>("AdminContext");
		}
    }
	public partial class RoleBLL : BaseBLL<Role>
    {
		public RoleBLL()
		{
			dal = new BaseDAL<Role>("AdminContext");
		}
    }
	public partial class User_InfoBLL : BaseBLL<User_Info>
    {
		public User_InfoBLL()
		{
			dal = new BaseDAL<User_Info>("AdminContext");
		}
    }
	public partial class V_User_InfoBLL : BaseBLL<V_User_Info>
    {
		public V_User_InfoBLL()
		{
			dal = new BaseDAL<V_User_Info>("AdminContext");
		}
    }
	public partial class WX_ConfigBLL : BaseBLL<WX_Config>
    {
		public WX_ConfigBLL()
		{
			dal = new BaseDAL<WX_Config>("AdminContext");
		}
    }
}
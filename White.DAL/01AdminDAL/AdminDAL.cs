 
/*------------------------------------
 * 
 * T4模板自动生成数据操作类
 * 生成时间：2018-08-01 11:05:09
 * 
*--------------------by WN---------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using White.Model;

namespace White.DAL
{
	public partial class Action_LogDAL : BaseDAL<Action_Log>
    {
		public Action_LogDAL():base("AdminContext")
        {
        }
    }
	public partial class Error_LogDAL : BaseDAL<Error_Log>
    {
		public Error_LogDAL():base("AdminContext")
        {
        }
    }
	public partial class FunctionalDAL : BaseDAL<Functional>
    {
		public FunctionalDAL():base("AdminContext")
        {
        }
    }
	public partial class ResourceDAL : BaseDAL<Resource>
    {
		public ResourceDAL():base("AdminContext")
        {
        }
    }
	public partial class RoleDAL : BaseDAL<Role>
    {
		public RoleDAL():base("AdminContext")
        {
        }
    }
	public partial class User_InfoDAL : BaseDAL<User_Info>
    {
		public User_InfoDAL():base("AdminContext")
        {
        }
    }
	public partial class V_User_InfoDAL : BaseDAL<V_User_Info>
    {
		public V_User_InfoDAL():base("AdminContext")
        {
        }
    }
	public partial class WX_ConfigDAL : BaseDAL<WX_Config>
    {
		public WX_ConfigDAL():base("AdminContext")
        {
        }
    }
}
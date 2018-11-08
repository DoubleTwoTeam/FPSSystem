using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;

namespace FPS.IServices
{
    public interface IApprove
    {
        /// <summary>
        /// 将案件添加到审批表
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        int InsertApprove(Approve approve);

        /// <summary>
        /// 修改审批表内容
        /// </summary>
        /// <param name="approve"></param>
        /// <returns></returns>
        int UpdateApprove(Approve approve);

        /// <summary>
        /// 审批显示
        /// </summary>
        /// <returns></returns>
        List<ApproveDataModel> GetApproveList();

        /// <summary>
        /// 点击审批页面的某个详情进入查看案情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Instance GetInstanceById(int id);


    }
}

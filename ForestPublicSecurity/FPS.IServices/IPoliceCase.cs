﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FPS.Models;
using FPS.Models.DTO;

namespace FPS.IServices
{
    public interface IPoliceCase
    {
        /// <summary>
        /// 添加案件
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        int InsertInstance(Instance instance);

        /// <summary>
        /// 案件申请与配置表作比较
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        Approve GetApprove(Instance instance);

        /// <summary>
        /// 案件显示页面
        /// </summary>
        /// <returns></returns>
        PageList<InstanceDataModel> GetInstanceList();

        /// <summary>
        /// 根据ID查询对应的案件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Instance GetInstanceById(int id);

        /// <summary>
        /// 对相应的案件进行更改
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        int UpdateinStance(Instance instance);

        /// <summary>
        /// 获取类型表(立案、结案)
        /// </summary>
        /// <returns></returns>
        List<Business> GetBusinessesList();

        /// <summary>
        /// 找到案件表的ID
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        int GetinStanceByClass(Instance instance);
    }
}

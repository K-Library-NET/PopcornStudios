using Pstudio.MVC.NTSvr.ApplyWcfCore.Exports;
using Pstudio.MVC.NTSvr.ApplyWcfCore.Imports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Pstudio.MVC.NTSvr.ApplyWcfCore
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IApplyService”。
    /// <summary>
    /// 申报服务
    /// </summary>
    [ServiceContract]
    public interface IApplyService
    {
        /// <summary>
        /// 用户请求登录，通过IC卡或者指纹
        /// </summary>
        /// <param name="machineName">机器名。暂时默认一个机器名对应一台机，
        /// 同时对应一个IC卡（Type I，记录了用户信息的卡，不能进出门禁)读卡设备和一个指纹读取设备</param>
        /// <returns></returns>
        [OperationContract]
        UserLoginResult UserLoginRequest(string machineName);

        /// <summary>
        /// 边民申报进口
        /// </summary>
        /// <param name="machineName">机器名。暂时默认一个机器名对应一台机，
        /// 同时对应一个IC卡（Type I，记录了用户信息的卡，不能进出门禁)读卡设备和一个指纹读取设备</param>
        /// <param name="securityToken">安全令牌。必须传入</param>
        /// <param name="importApply">申报的进口信息</param>
        /// <param name="vehicleInfo">申报的运输车辆信息</param>
        /// <returns></returns>
        [OperationContract]
        UserImportApplyResult UserImportApplyResult(string machineName,
            string securityToken, UserImportApply importApply, VehicleInfo vehicleInfo);

        /// <summary>
        /// 边民申报出口
        /// </summary>
        /// <param name="machineName">机器名。暂时默认一个机器名对应一台机，
        /// 同时对应一个IC卡（Type I，记录了用户信息的卡，不能进出门禁)读卡设备和一个指纹读取设备</param>
        /// <param name="securityToken">安全令牌。必须传入</param>
        /// <param name="exportApply">申报的出口信息</param>
        /// <param name="vehicleInfo">申报的运输车辆信息</param>
        /// <returns></returns>
        [OperationContract]
        UserExportApplyResult UserExportApplyResult(string machineName,
            string securityToken, UserExportApply exportApply, VehicleInfo vehicleInfo);

        /// <summary>
        /// 获取允许进出口的商品列表
        /// 通常用于界面展示
        /// </summary>
        /// <param name="machineName">机器名</param>
        [OperationContract]
        AuthorizedProductListResult GetAuthorizedProductList(string machineName);
    }
}

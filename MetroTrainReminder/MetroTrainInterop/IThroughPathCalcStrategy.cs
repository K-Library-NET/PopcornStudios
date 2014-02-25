using System;
namespace PopcornStudio.MetroTrainInterop
{
    /// <summary>
    /// 查找路线策略接口
    /// </summary>
    public interface IThroughPathCalcStrategy
    {
        System.Collections.Generic.List<ThroughPath> CalcThroughPaths();
    }
}

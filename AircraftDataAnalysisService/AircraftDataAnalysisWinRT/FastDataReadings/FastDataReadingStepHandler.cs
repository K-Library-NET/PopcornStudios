using FlightDataEntitiesRT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AircraftDataAnalysisWinRT.FastDataReadings
{
    abstract class FastDataReadingStepHandler
    {
        public FastDataReadingStepHandler(int startSecond, int endSecond, bool putIntoServer,
            AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
            AircraftDataAnalysisWinRT.Services.IDataReading dataReading)
        {
            this.m_startSecond = startSecond;
            this.m_endSecond = endSecond;
            this.m_putIntoServer = putIntoServer;
            this.m_previewModel = previewModel;
            this.m_dataReading = dataReading;
        }

        public FlightDataEntitiesRT.Flight CurrentFlight
        {
            get;
            set;
        }

        public FlightParameters Parameters
        {
            get;
            set;
        }

        protected int m_startSecond;
        protected int m_endSecond;
        protected bool m_putIntoServer;
        protected DataModel.RawDataPointViewModel m_previewModel;
        protected Services.IDataReading m_dataReading;

        protected Task m_handlerMainTask = null;

        public virtual string GetResult()
        {
            try
            {
                if (m_handlerMainTask != null)
                {
                    m_handlerMainTask.Wait();
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }

            return string.Empty;
        }

        public virtual void DoWorkAsync()
        {
            try
            {
                m_handlerMainTask = Task.Run<string>(
                    new Func<string>(delegate()
                {
                    return this.DoWorkCore();
                }));
            }
            catch (Exception e)
            {
                LogHelper.Error(e);
            }
        }

        protected abstract string DoWorkCore();
    }

    abstract class StepSecondsFastDataReadingStepHandler : FastDataReadingStepHandler
    {
        public StepSecondsFastDataReadingStepHandler(int startSecond, int endSecond, bool putIntoServer,
             AircraftDataAnalysisWinRT.DataModel.RawDataPointViewModel previewModel,
             AircraftDataAnalysisWinRT.Services.IDataReading dataReading)
            : base(startSecond, endSecond,
                putIntoServer, previewModel, dataReading)
        {

        }

        public FastDataReading.FastDataReadingResourceObject ResourceObject
        {
            get;
            set;
        }

        public virtual string OnStepData(int second, ParameterRawData[] datas)
        {
            string result = string.Empty;
            try
            {
                result = this.HandleOneStepData(second, datas);
            }
            catch (Exception e)
            {
                result = e.Message + "\r\n" + e.StackTrace;
            }
            finally
            {//控制DoWorkCore什么时候完成，无论如何都要释放一个
                this.m_lastResult = result;
                //LastResult用于返回最后一个错误，因为释放信号量之后可能比result返回更快完成
                try
                {
                    m_semaphore.Release();
                }
                catch
                {

                }
            }
            return result;
        }

        /// <summary>
        /// 真正的单步做法，只需要Override这个即可
        /// </summary>
        /// <param name="second"></param>
        /// <param name="datas"></param>
        /// <returns></returns>
        protected abstract string HandleOneStepData(int second, ParameterRawData[] datas);

        protected Semaphore m_semaphore = null;
        private string m_lastResult;

        public string LastResult
        {
            get { return m_lastResult; }
            //set { m_lastResult = value; }
        }

        protected override string DoWorkCore()
        {
            //初始化信号量，按照读取的总秒数
            m_semaphore = new Semaphore(0, this.m_endSecond - this.m_startSecond);

            try
            {
                int secondsCount = 0;

                while (m_semaphore.WaitOne())
                {
                    secondsCount++;
                    if (this.m_endSecond - this.m_startSecond <= secondsCount)
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                return e.Message + "\r\n" + e.StackTrace;
            }
            finally
            {
                try
                {
                    m_semaphore.Dispose();
                }
                catch
                {
                }
            }

            if (string.IsNullOrEmpty(this.m_lastResult))
                return string.Empty;
            return this.LastResult;
        }
    }
}

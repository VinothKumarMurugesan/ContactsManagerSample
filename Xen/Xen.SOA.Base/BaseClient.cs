using System;
using Xen.Common.Services.Logging;

namespace Xen.SOA.Base
{
    public interface IBaseContract
    {
    }

    public class BaseWCFClient  
    {
        #region GetWCFChannelManager

        public static WCFChannelManager<T> GetWCFChannelManager<T>()
            where T : IBaseContract
        {
            WCFChannelManager<T> channelManager = null;

            try
            {
                channelManager = new WCFChannelManager<T>();
            }
            catch (Exception ex)
            {
                XenLoggerService.HandleException(ex);
            }

            return channelManager;
        }

        #endregion GetWCFChannelManager

        #region GetWCFChannelManager

        //public static void HandleException(Exception ex)
        //{
        //    Logger.Log(ex);
        //}

        //public static void HandleException<TEntity>(Exception exception, ICRUDResult<TEntity> crudResult)
        //{
        //    crudResult.PopulateExceptionInfo(exception);
        //    Logger.Log(exception);
        //}

        //public static void HandleException<TEntity>(Exception exception, IOperationResult<TEntity> operationResult)
        //{
        //    operationResult.PopulateExceptionInfo(exception);
        //    Logger.Log(exception);
        //}

        #endregion GetWCFChannelManager
    }
}

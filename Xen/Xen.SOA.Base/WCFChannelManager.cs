using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Xen.Common.Services.Logging;

namespace Xen.SOA.Base
{
    public class WCFChannelManager<T> : IDisposable
    {
        #region Fields

        private T _channel = default(T);

        #endregion

        #region Constructor

        public WCFChannelManager()
        {

        }

        #endregion

        #region IWCFChannelManager<T> Members

        public T Channel
        {
            get { return this.OpenChannel(); }
        }

        public T OpenChannel()
        {
            string endpointName = typeof(T).FullName;
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(endpointName);

            channelFactory.Faulted += new EventHandler(channelFactory_Faulted);
            _channel = channelFactory.CreateChannel();

            return _channel;
        }

        void channelFactory_Faulted(object sender, EventArgs e)
        {
            try
            {
                if (sender != null)
                    ((ICommunicationObject)sender).Abort();
            }
            catch (Exception ex)
            {
                XenLoggerService.HandleException(ex);
            }
        }

        public void CloseChannel()
        {
            try
            {
                var iChannel = this._channel as IChannel;
                if (iChannel != null)
                {
                    iChannel.Close();
                    this._channel = default(T);
                }
            }
            catch (Exception ex)
            {
                XenLoggerService.HandleException(ex);
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.CloseChannel();
        }

        #endregion
    }
}
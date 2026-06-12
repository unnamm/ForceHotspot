using Common;
using Common.Message;
using Common.Config;
using CommunityToolkit.Mvvm.Messaging;
using Common.Model;

namespace Sequence
{
    /// <summary>
    /// flow program sequence
    /// </summary>
    public class Flow : IRecipient<MainWindowRenderedMessage>, IRecipient<MainViewCloseMessage>, IRecipient<ContentRunMessage>
    {
        private readonly Log _log;
        private readonly DataYaml _yamlData;
        private readonly Hotspot _hotspot = new();
        private readonly NetworkData _networkData;

        public Flow(Log log, DataYaml dataYaml, NetworkData networkData)
        {
            WeakReferenceMessenger.Default.RegisterAll(this);
            _log = log;
            _yamlData = dataYaml;
            _networkData = networkData;
        }

        public async void Receive(MainWindowRenderedMessage message)
        {
            try
            {
                //do init
                await _yamlData.LoadAsync();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("init error", ex.Message));
                _log.Write(ex.Message);
            }
            finally
            {
                WeakReferenceMessenger.Default.Send(new BusyMessage(false)); //close wait
            }
        }

        public async void Receive(MainViewCloseMessage message)
        {
            WeakReferenceMessenger.Default.Send(new BusyMessage(true, "exit..."));
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new DialogMessage("dispose error", ex.Message));
                _log.Write(ex.Message);
            }
        }

        public async void Receive(ContentRunMessage message)
        {
            try
            {
                WeakReferenceMessenger.Default.Send(new BusyMessage(true, "connecting..."));
                SettingData config = new();
                await config.LoadAsync();

                var wifi = await _hotspot.Run(_networkData.Id, _networkData.Password, config.IanaInterfaceType);
                _networkData.Id = wifi;
                _log.Write("connected");
            }
            catch (Exception ex)
            {
                _log.Write(ex.Message);
            }
            finally
            {
                WeakReferenceMessenger.Default.Send(new BusyMessage(false));
            }
        }
    }
}

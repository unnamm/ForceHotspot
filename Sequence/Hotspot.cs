using Windows.Networking.Connectivity;
using Windows.Networking.NetworkOperators;

namespace Sequence
{
    public class Hotspot
    {
        private NetworkOperatorTetheringManager? manager;

        public async Task<string> Run(string ssid, string password, uint ianaInterfaceType)
        {
            var profiles = NetworkInformation.GetConnectionProfiles();
            var wifiProfile = profiles.FirstOrDefault(p => p.NetworkAdapter?.IanaInterfaceType == ianaInterfaceType);
            var adapter = wifiProfile?.NetworkAdapter;

            manager = NetworkOperatorTetheringManager.CreateFromConnectionProfile(wifiProfile, adapter);

            if (manager.TetheringOperationalState == TetheringOperationalState.On)
            {
                throw new Exception($"already connected: {manager.GetCurrentAccessPointConfiguration().Ssid}");
            }

            var config = new NetworkOperatorTetheringAccessPointConfiguration
            {
                Ssid = ssid,
                Passphrase = password
            };

            await manager.ConfigureAccessPointAsync(config);
            var result = await manager.StartTetheringAsync();

            return manager.GetCurrentAccessPointConfiguration().Ssid;
        }

        public async Task DisposeAsync()
        {
            if (manager != null)
                await manager.StopTetheringAsync();
        }
    }
}

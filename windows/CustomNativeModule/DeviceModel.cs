using Microsoft.ReactNative.Managed;
using Windows.Security.ExchangeActiveSyncProvisioning;

namespace CustomNativeModule
{
    [ReactModule]
    class DeviceModel
    {
        [ReactMethod("getDeviceModel")]
        public string GetDeviceModel()
        {
            EasClientDeviceInformation info = new EasClientDeviceInformation();
            return info.SystemProductName;
        }
    }
}

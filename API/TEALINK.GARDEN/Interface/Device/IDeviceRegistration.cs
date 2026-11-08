using TEALINK.GARDEN.Model.Device;

namespace TEALINK.GARDEN.Interface.Device
{
    public interface IDeviceRegistration
    {
        public Tuple<string, string> Create(DeviceInfo device);
        public Tuple<string, string> Delete(string code, int unitid);
        public List<DeviceModel> GetDevicesByUnit(int unitid);
    }
}

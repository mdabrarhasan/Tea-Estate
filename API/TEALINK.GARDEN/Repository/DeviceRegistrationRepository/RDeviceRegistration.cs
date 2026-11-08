using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN.Interface.Device;
using TEALINK.GARDEN.Model.Device;

namespace TEALINK.GARDEN.Repository.DeviceRegistrationRepository
{
    public class RDeviceRegistration : IDeviceRegistration
    {
        private readonly Context _context;
        string strMsg = "";
        string strStatusMsg = "";

        public RDeviceRegistration(Context context)
        {
            _context = context;
        }

        // POST Method
        public Tuple<string, string> Create(DeviceInfo device)
        {
            try
            {
                var MSG = new SqlParameter
                {
                    ParameterName = "@MSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };


                var STATUSMSG = new SqlParameter
                {
                    ParameterName = "@STATUSMSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };

                SqlParameter CODE = new SqlParameter("@CODE", device.CODE);
                SqlParameter NAME = new SqlParameter("@NAME", device.NAME);
                SqlParameter ENROLL = new SqlParameter("@ENROLL", device.ENROLL);
                SqlParameter UNITID = new SqlParameter("@UNITID", device.UNITID);


                _context.Database.ExecuteSqlRaw(
                    "Exec SPINSERTDEVICEINFO @CODE, @NAME, @ENROLL, @UNITID, @MSG OUTPUT,@STATUSMSG OUTPUT",
                    CODE, NAME, ENROLL, UNITID, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                //catch
            }
            return null;

        }

        // DELETE Method
        public Tuple<string, string> Delete(string code, int unitid)
        {
            try
            {
                var MSG = new SqlParameter
                {
                    ParameterName = "@MSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };


                var STATUSMSG = new SqlParameter
                {
                    ParameterName = "@STATUSMSG",
                    Size = 100,
                    SqlDbType = System.Data.SqlDbType.Char,
                    Direction = System.Data.ParameterDirection.Output
                };

                SqlParameter CODE = new SqlParameter("@CODE", code);
                SqlParameter UNITID = new SqlParameter("@UNITID", unitid);


                _context.Database.ExecuteSqlRaw(
                    "Exec SPDELETEDEVICEINFO @CODE, @UNITID, @MSG OUTPUT,@STATUSMSG OUTPUT",
                    CODE, UNITID, MSG, STATUSMSG);

                strMsg = MSG.Value.ToString();
                strStatusMsg = STATUSMSG.Value.ToString();
                return Tuple.Create(strMsg, strStatusMsg);
            }
            catch (Exception ex)
            {
                //catch
            }
            return null;

        }
        // GET Method
        public List<DeviceModel> GetDevicesByUnit(int unitid)
        {
            try
            {
                var unitIdParam = new SqlParameter("@UNITID", unitid);

                var devices = _context.Devices
                    .FromSqlRaw("Exec SPGETDEVICE @UNITID", unitIdParam)
                    .ToList();

                return devices;
            }
            catch (Exception ex)
            {
                return new List<DeviceModel>();
            }
        }

    }
}

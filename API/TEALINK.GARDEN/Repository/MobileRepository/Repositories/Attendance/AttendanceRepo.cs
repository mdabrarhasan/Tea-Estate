using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TEALINK.GARDEN;
using TEALINK.GARDEN.Model.Mobile.Models.Attendance;



namespace TEALINK.Repositories.Attendance
{
    public class AttendanceRepo : IAttendanceRepo
    {
        public Context _context;
        SqlConnection _connection;
        IConfiguration _configuration;

        public AttendanceRepo(Context contextt, IConfiguration configuration)
        {
            _configuration = configuration;
            _context = contextt;
            _connection = (SqlConnection)_context.Database.GetDbConnection();
        }

        public AttendanceResponse SubmitAttendance(AttendanceRequest attReq)
        {
            AttendanceResponse attendanceResponse = new AttendanceResponse();
            try
            {
                
                _connection.Open();
                using (SqlCommand cmd = new SqlCommand("sprEmpAttendanceMobile", _connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    _ = cmd.Parameters.AddWithValue("@type", attReq.Type);
                    _ = cmd.Parameters.AddWithValue("@empcode", attReq.EmpCode);
                    _ = cmd.Parameters.AddWithValue("@shiftid", attReq.ShiftId);
                    _ = cmd.Parameters.AddWithValue("@operatorid", attReq.Operator);
                    _ = cmd.Parameters.AddWithValue("@sectionid", attReq.SectionId);
                    _ = cmd.Parameters.AddWithValue("@deviceid", attReq.DeviceId);
                    _ = cmd.Parameters.AddWithValue("@hazaricode", attReq.HazariCode);
                    _ = cmd.Parameters.AddWithValue("@KamjariId", attReq.KamjariId);
                    _ = cmd.Parameters.AddWithValue("@UnitId", attReq.UnitId);
                    _ = cmd.Parameters.AddWithValue("@weightmentid", attReq.WeightmentId);
                    _ = cmd.Parameters.AddWithValue("@pluckedqty", attReq.PluckedQty);
                    _ = cmd.Parameters.AddWithValue("@matching", attReq.PictureMatched);
                    _ = cmd.Parameters.AddWithValue("@AttendanceDate", attReq.AttendanceDATE);
                    _ = cmd.Parameters.AddWithValue("@QTY", attReq.Qty);




                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            attendanceResponse.Message = reader.GetString("Message");
                            attendanceResponse.Status = reader.GetString("Status");
                            attendanceResponse.Code = reader.GetString("Code");
                        }
                    }
                }
            }
            catch (Exception ex) { }
            _connection.Close();
            return attendanceResponse;
        }

    }
}

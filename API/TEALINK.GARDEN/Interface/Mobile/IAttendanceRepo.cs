using TEALINK.GARDEN.Model.Mobile.Models.Attendance;

namespace TEALINK.Repositories.Attendance
{
    public interface IAttendanceRepo
    {
        public AttendanceResponse SubmitAttendance(AttendanceRequest attReq);
    }
}

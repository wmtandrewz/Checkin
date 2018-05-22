using System;
namespace Checkin.Models.ModelClasses
{
    public static class CheckinAPIModel
    {
        public static string CheckinDate { get; set; }
        public static string HotelCode { get; set; }
        public static string ReservationNumber { get; set; }
        public static string RoomNo { get; set; }
        public static string StartTime { get; set; }
        public static string EndTime { get; set; }
        public static string CreatedBy { get; set; }
    }
}

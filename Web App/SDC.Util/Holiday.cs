using System;
namespace SystemSecurityUtil
{
    public class Holiday
    {
        public long Id { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string InsertUserId { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime EntryDate { get; set; }
        //public int Version{ get; set; }
    }
}

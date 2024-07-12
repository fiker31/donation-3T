using System;
namespace SystemSecurityUtil
{
    public class PasswordHistory
    {
        public long Id { get; set; }
        public long UserAccountId { get; set; }
        public string Password { get; set; }
        public string InsertUserId { get; set; }
        public DateTime EntryDate { get; set; }
        //public int Version{ get; set; }
    }
}

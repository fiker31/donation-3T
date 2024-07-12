using System;
namespace SystemSecurityUtil
{
    public class SecurityParameter
    {
        public byte LoginIdMinLength { get; set; }
        public byte PasswordMinLength { get; set; }
        public bool PasswordMustHaveDigit { get; set; }
        public bool PasswordMustHaveLowerCase { get; set; }
        public bool PasswordMustHaveUpperCase { get; set; }
        public bool PasswordMustHaveSpecialChar { get; set; }
        public byte NoOfUnsuccessfulAttempts { get; set; }
        public byte PasswordInterval { get; set; }
        public byte PasswordHistory { get; set; }
        public string WorkingHourFrom { get; set; }
        public string WorkingHourTo { get; set; }
        public string WorkingHourToSat { get; set; }
        public bool AllowWorkOnHoliday { get; set; }
        public bool AllowWorkOnWeekend { get; set; }
        public bool AllowWorkAfterWorkingHours { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}

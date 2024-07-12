using System;
using System.Collections.Generic;
namespace SystemSecurityUtil
{
    public class WorkingTime
    {
        string _workingTimeStart = "8:00:00 AM";
        string _workingTimeEnd = "4:30:00 PM";
        string _workingTimeEndSaterday = "12:00:00 PM";
        bool _allowWorkOnHoliday = true;
        bool _allowWorkOnWeekend = true;
        bool _allowWorkAfterWorkingHour = true;
        public WorkingTime()
        {
        }
        public WorkingTime(string workingTimeStart, string workingTimeEnd, string workingTimeEndSaterday,
            bool allowWorkOnHoliday, bool allowWorkOnWeekend, bool allowWorkAfterWorkingHour)
        {
            _workingTimeStart = workingTimeStart;
            _workingTimeEnd = workingTimeEnd;
            _workingTimeEndSaterday = workingTimeEndSaterday;
            _allowWorkOnHoliday = allowWorkOnHoliday;
            _allowWorkOnWeekend = allowWorkOnWeekend;
            _allowWorkAfterWorkingHour = allowWorkAfterWorkingHour;
        }
        #region Properties
        //Property for getting password strength validation 
        // errors
        private List<string> _validationErrors = new List<string>();
        public List<string> ValidationErrors
        {
            get { return _validationErrors; }
        }
        #endregion Properties
        public bool IsLogInAllowed()
        {
            if (_validationErrors != null)
                _validationErrors.Clear();
            if (!_allowWorkOnHoliday)
            {
                Holiday hl = SystemSecurityDAC.GetHolidayObject(DateTime.Now);
                if (hl != null)
                {
                    _validationErrors.Add("You are not allowed to login to the system on Holiday. ( Today is " + hl.HolidayName + ". )");
                }
            }
            if (!_allowWorkOnWeekend)
            {
                if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
                {
                    _validationErrors.Add("You are not allowed to login to the system on Sunday.");
                }
            }
            if (!_allowWorkAfterWorkingHour)
            {
                try
                {
                    DateTime workingtimestart = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + _workingTimeStart);
                    DateTime workingtimeend = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy") + " " + ((DateTime.Now.DayOfWeek == DayOfWeek.Saturday) ? _workingTimeEndSaterday : _workingTimeEnd));
                    if (DateTime.Compare(DateTime.Now, workingtimestart) < 0 ||
                        DateTime.Compare(DateTime.Now, workingtimeend) > 0)
                    {
                        _validationErrors.Add("You are not allowed to login to the system before " + _workingTimeStart +
                            " or after " + (DateTime.Now.DayOfWeek == DayOfWeek.Saturday ? _workingTimeEndSaterday : _workingTimeEnd) + ".");
                    }
                }
                catch (Exception)
                {
                    _validationErrors.Add("Contact Your Administrator.");
                }
            }
            if (_validationErrors.Count == 0)
                return true;
            else
                return false;
        }
    }
}

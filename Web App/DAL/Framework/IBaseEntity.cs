using System;
using System.Data.Linq;
namespace DAL.Framework
{
    public interface IBaseEntity
    {
        DateTime EntryDate { get; set; }
        string InsertUserId { get; set; }
        DateTime UpdateDate { get; set; }
        string UpdateUserId { get; set; }
        Binary Version { get; set; }
    }
}

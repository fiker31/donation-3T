﻿using DAL.Framework;
using System;
using System.Data.Linq;
namespace BLL.Framework
{
    /// <summary>
    /// The BaseBO class is the Base for any business object class that will retrieve data from the database.
    /// </summary>    
    [Serializable()]
    public abstract class BaseBO
    {
        #region Constructor
        /// <summary>
        /// Default constructor.
        /// </summary>
        public BaseBO() { }
        #endregion Constructor
        #region Properties
        //public int ID { get; set; }
        public DateTime EntryDate { get; set; }
        public String InsertUserId { get; set; }
        public DateTime UpdateDate { get; private set; }
        public string UpdateUserId { get; private set; }
        public Binary Version { get; set; }
        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// The property is used when binding to a control.
        /// </summary>
        public string DisplayText
        {
            get { return GetDisplayText(); }
        }
        #endregion Properties
        #region Abstract Methods
        /// <summary>
        /// Get the record from the database and load the object's properties
        /// </summary>
        /// <returns>Returns true if the record is found.</returns>
        public abstract bool Load(long id);
        /// <summary>
        /// Get the record from the database and load the object's properties
        /// </summary>
        /// <returns>Returns true if the record is found.</returns>
        public abstract bool Load(string id);
        /// <summary>
        /// This method will map the fields in the data reader to the member variables in the object.
        /// </summary>
        protected abstract void MapEntityToCustomProperties(IBaseEntity entity);
        /// <summary>
        /// This returns the text that should appear in a list box or drop down list for this object.
        /// </summary>
        protected abstract string GetDisplayText();
        #endregion Abstratct Methods
        #region Public Methods
        /// <summary>
        /// This method will load all the properties of the object from the entity.
        /// </summary>        
        public void MapEntityToProperties(IBaseEntity entity)
        {
            if (entity != null)
            {
                EntryDate = entity.EntryDate;
                InsertUserId = entity.InsertUserId;
                UpdateDate = entity.UpdateDate;
                UpdateUserId = entity.UpdateUserId;
                Version = entity.Version;
                this.MapEntityToCustomProperties(entity);
            }
        }
        #endregion Public Methods
    }
}

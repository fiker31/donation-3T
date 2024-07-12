using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Linq.Expressions;
using System.Data.Linq;
using FCRMSDAL;
using FCRMSDAL.Framework;

namespace FCRMSBLL.Framework
{
    #region User Status
    #region User Status BO
    public class UserStatusBO
    {

        #region Properties

        public int StatusID { get; set; }

        public string Description { get; set; }



        #endregion Properties

        #region Public Methods




        #endregion Public Methods

        #region Private Methods

        public void MapEntityToProperties(UserStatus entity)
        {
            StatusID = (entity.StatusID);
            Description = (entity.Description);
        }

        public string DisplayText
        {
            get { return GetDisplayText(); }
        }
        public string GetDisplayText()
        {
            return SectorName;
        }


        #endregion Private Methods
    }
    #endregion Economic Sector BO
    #region Economic Sector BOList

    [Serializable()]
    public class EconomicSectorBOList : List<EconomicSectorBO>
    {
        #region Overrides

        public void Load()
        {
            LoadFromList(new EconomicSectorData().Select());
        }

        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<EconomicSector> ess)
        {
            foreach (EconomicSector es in ess)
            {
               EconomicSectorBO newEcoSectBO = new EconomicSectorBO();
               newEcoSectBO.MapEntityToProperties(es);
               this.Add(newEcoSectBO);
            }
        }

        #endregion Private Mothod



    }

    #endregion Economic Sector BOList

    #endregion Economic Sector

    #region Customer Class

    #region Customer Class BO
    public class CustomerClassBO
    {

        #region Properties

        public byte ClassId { get; set; }

        public string Description { get; set; }



        #endregion Properties

        #region Public Methods




        #endregion Public Methods

        #region Private Methods

        public void MapEntityToProperties(CustomerClass  entity)
        {
            ClassId  = (entity.ClassId);
            Description = entity.Description;
        }

        public string DisplayText
        {
            get { return GetDisplayText(); }
        }
        public string GetDisplayText()
        {
            return Description;
        }


        #endregion Private Methods
    }
    #endregion Customer Class BO

    #region Customer Class BOList

    [Serializable()]
    public class CustomerClassBOList : List<CustomerClassBO>
    {
        #region Overrides

        public void Load()
        {
            LoadFromList(new CustomerClassData().Select());
        }

        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<CustomerClass> ccs)
        {
            foreach (CustomerClass cc in ccs)
            {
                CustomerClassBO newCustClassBO = new CustomerClassBO();
                newCustClassBO.MapEntityToProperties(cc);
                this.Add(newCustClassBO);
            }
        }

        #endregion Private Mothod



    }

    #endregion Customer Class BOList

    #endregion Customer Class

    #region Company Form

    #region Company Form BO
    public class CompanyFormBO
    {

        #region Properties

        public string CompFormID { get; set; }
        public string CompFormType { get; set; }
        public string Remark { get; set; }
        


        #endregion Properties

        #region Public Methods




        #endregion Public Methods

        #region Private Methods

        public void MapEntityToProperties(CompanyForm entity)
        {
            CompFormID = entity.CompFormID;
            CompFormType = entity.CompFormType;
            Remark = entity.Remark;
        }

        public string DisplayText
        {
            get { return GetDisplayText(); }
        }
        public string GetDisplayText()
        {
            return CompFormType;
        }


        #endregion Private Methods
    }
    #endregion Company Form BO
    #region Company Form BOList

    [Serializable()]
    public class CompanyFormBOList : List<CompanyFormBO>
    {
        #region Overrides

        public void Load()
        {
            LoadFromList(new CompanyFormData().Select());
        }

        #endregion Overrides

        #region Private Methods

        protected void LoadFromList(List<CompanyForm> cfs)
        {
            foreach (CompanyForm cf in cfs)
            {
                CompanyFormBO newCompanyFormBO = new CompanyFormBO();
                newCompanyFormBO.MapEntityToProperties(cf);
                this.Add(newCompanyFormBO);
            }
        }

        #endregion Private Mothod



    }

    #endregion Company Form BOList

    #endregion Company Form

using BLL.Framework;
using System;
using System.Collections.Specialized;
using System.Linq;
/// <summary>
/// Summary description for BaseEditPage
/// </summary>
public abstract class BaseEditPage<T> : BasePage
    where T : BaseEO, new()
{
    #region Constructor
    public BaseEditPage() { }
    #endregion Constructor
    #region Properties
    #endregion Properties
    #region Methods
    #region Virtual Methods
    /// <summary>
    /// Initializes a new edit object and then calls load object from screen.
    /// </summary>
    protected virtual void LoadNew()
    {
        T baseEO = new T();
        baseEO.Init();
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
        LoadScreenFromObject(baseEO);
    }
    //public virtual void DisableContols(ControlCollection controls)
    //{
    //    EnableDisableControls(controls, false);
    //}
    //public virtual void EnableContols(ControlCollection controls)
    //{
    //    EnableDisableControls(controls, true);
    //}
    #endregion Virtual Methods
    #region Abstract Methods
    /// <summary>
    /// Scrapes the screen and loads the edit object.
    /// </summary>
    /// <param name="baseEO"></param>
    protected abstract void LoadObjectFromScreen(T baseEO);
    /// <summary>
    /// Load the controls on the screen from the object's properties.
    /// </summary>
    /// <param name="baseEO"></param>
    protected abstract void LoadScreenFromObject(T baseEO);
    /// <summary>
    /// Preload the controls such as drop down lists and listboxes.
    /// </summary>
    protected abstract void LoadControls();
    /// <summary>
    /// Open the requested Search page.  The Find button call this.
    /// </summary>
    protected abstract void GoToSearchPage();
    /// <summary>
    /// Enable or Disable buttons to their initial state. The cancel button and a successful save should both call this.
    /// </summary>
    protected abstract void ResetButtons();
    #endregion Abstract Methods
    #region Overrides
    //All String Id must start with 's'
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        if (!IsPostBack)
        {
            //Load any list boxes, drop downs, etc.
            LoadControls();
            string id = GetId();
            if (id == "0" || id == "" || id == null || id == "s")
            {
                LoadNew();
            }
            else
            {
                T baseEO = new T();
                //id = id.StartsWith("s") ? id.Substring(2) : Convert.ToInt64(id);
                if (id.StartsWith("s"))
                {
                    baseEO.Load(id.Substring(1));
                }
                else
                {
                    try
                    {
                        baseEO.Load(Convert.ToInt64(id));
                    }
                    catch { }
                }
                LoadScreenFromObject(baseEO);
            }
        }
    }
    #endregion Overrides
    #region Private
    #endregion Private
    #region Public Methods
    public string GetId()
    {
        //Decrypt the query string
        NameValueCollection queryString = DecryptQueryString(Request.QueryString.ToString());
        if (queryString == null)
        {
            return "0";
        }
        else
        {
            //Check if the id was passed in.
            string id = queryString["id"];
            if ((id == null) || (id == "0"))
            {
                return "0";
            }
            else
            {
                return id; //Convert.ToInt64(id);
            }
        }
    }
    #endregion Public Methods
    #endregion Methods
}

﻿using BLL;
using BLL.Framework;
using System;
using System.Linq;
public partial class Basic_Student : BaseEditPage<StudentBO>
{
    private const string VS_IE = "Ticket_Category";
    private const string VS_MODE = "Entry Mode";
    private const string VS_CONTROL = "Control Collections";
    protected void Page_Load(object sender, EventArgs e)
    {
        Master.NewButton_Click += new CreditEditPage.ButtonClickedHandler(Master_NewButton_Click);
        Master.EditButton_Click += new CreditEditPage.ButtonClickedHandler(Master_EditButton_Click);
        Master.SaveButton_Click += new CreditEditPage.ButtonClickedHandler(Master_SaveButton_Click);
        Master.CancelButton_Click += new CreditEditPage.ButtonClickedHandler(Master_CancelButton_Click);
        Master.DeleteButton_Click += new CreditEditPage.ButtonClickedHandler(Master_DeleteButton_Click);
        Master.FindButton_Click += new CreditEditPage.ButtonClickedHandler(Master_FindButton_Click);
        if (!IsPostBack)
        {
            MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
        }
    }
    void Master_NewButton_Click(object sender, EventArgs e)
    {
        ViewState[VS_MODE] = 'N';
        Master.EnableButtons(false, false, true, true, false, false);
        //Enable & Clear controls
        MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
        ClearForm(this.Controls);
    }
    void Master_EditButton_Click(object sender, EventArgs e)
    {
        if (((StudentBO)ViewState[VS_IE]).Id > 0)
        {
            ViewState[VS_MODE] = 'U';
            Master.EnableButtons(false, false, true, true, false, false);
            //Enable Controls
            MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
        }
        else
        {
            Master.ValidationErrors.Add("Record is not selected.");
        }
    }
    void Master_SaveButton_Click(object sender, EventArgs e)
    {
        EntValidationErrors validationErrors = new EntValidationErrors();
        //Validate the Input Date and Number Data types
        StudentBO td;
        if ((char)ViewState[VS_MODE] == 'N')
        {
            td = new StudentBO();
            td.DBAction = BaseEO.DBActionEnum.Insert;
        }
        else
        {
            td = (StudentBO)ViewState[VS_IE];
            td.DBAction = BaseEO.DBActionEnum.Update;
        }
        LoadObjectFromScreen(td);
        if (!td.Save(ref validationErrors, Convert.ToString(Session["LoginId"])))
        {
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            ResetButtons();
            //Refresh the Viewstate with current record 
            td.Load(td.Id);
            LoadScreenFromObject(td);
            //DisableControls
        }
    }
    void Master_CancelButton_Click(object sender, EventArgs e)
    {
        LoadScreenFromObject((StudentBO)ViewState[VS_IE]);
        ResetButtons();
    }
    void Master_DeleteButton_Click(object sender, EventArgs e)
    {
        if (((StudentBO)ViewState[VS_IE]).Id.ToString().Trim().Length > 0)
        {
            EntValidationErrors validationErrors = new EntValidationErrors();
            StudentBO td = (StudentBO)ViewState[VS_IE];
            td.DBAction = BaseEO.DBActionEnum.Delete;
            if (!td.Delete(ref validationErrors, Convert.ToString(Session["LoginId"])))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                //Clear Controls
                ClearForm(this.Controls);
                ViewState[VS_IE] = new StudentBO();
            }
        }
        else
        {
            Master.ValidationErrors.Add("Record is not selected.");
        }
    }
    void Master_FindButton_Click(object sender, EventArgs e)
    {
        GoToSearchPage();
    }
    #region overrides
    protected override void LoadObjectFromScreen(StudentBO baseEO)
    {
        //Check the controls and ammend accordingly
        baseEO.Firstname = txtFirstName.Text;
        baseEO.Lastname = txtLastname.Text;
    }
    protected override void LoadScreenFromObject(StudentBO baseEO)
    {
        //Check the controls and ammend accordingly
        txtFirstName.Text = baseEO.Firstname;
        txtLastname.Text = baseEO.Lastname;
        //Put the object in the view state so it can be attached back to the data context.
        ViewState[VS_IE] = baseEO;
    }
    protected override void LoadControls()
    {
    }
    protected override void GoToSearchPage()
    {
        Session[GymConst.SS_ID] = ((StudentBO)ViewState[VS_IE]).Id.ToString();
        Response.Redirect("SetUpSearch.aspx" + EncryptQueryString("page=Student.aspx"));
    }
    protected override void ResetButtons()
    {
        Master.EnableButtons(true, true, false, false, true, true);
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    public override string MenuItemName()
    {
        return "Student";
    }
    public override string[] CapabilityNames()
    {
        return new string[] { "Student" };
    }
    #endregion overrides
}
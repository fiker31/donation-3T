using BLL.Framework;
using System;
using System.Web.UI.HtmlControls;

public partial class CreditEditPage : System.Web.UI.MasterPage
{
    public delegate void ButtonClickedHandler(object sender, EventArgs e);
    public event ButtonClickedHandler SaveButton_Click;
    public event ButtonClickedHandler CancelButton_Click;
    public event ButtonClickedHandler EditButton_Click;
    public event ButtonClickedHandler NewButton_Click;
    public event ButtonClickedHandler DeleteButton_Click;
    public event ButtonClickedHandler FindButton_Click;
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveButton_Click != null)
        {
            SaveButton_Click(sender, e);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (CancelButton_Click != null)
        {
            CancelButton_Click(sender, e);
        }
    }
    protected void btnFind_Click(object sender, EventArgs e)
    {
        if (FindButton_Click != null)
        {
            FindButton_Click(sender, e);
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        //EnableButtons(false, false, true, true, false, false);
        //if (IsPeriodActive())
        //{
        if (NewButton_Click != null)
        {
            NewButton_Click(sender, e);
        }
        //}
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        //EnableButtons(false, false, true, true, false, false);
        //if (IsPeriodActive())
        //{
        if (EditButton_Click != null)
        {
            EditButton_Click(sender, e);
            {
                //ReadOnly = true;
                //EnableDisableControls(controls,true);
                //CustomReadOnlyLogic(capabilityName);
            }
        }
        //}
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //if (IsPeriodActive())
        //{
        if (DeleteButton_Click != null)
        {
            DeleteButton_Click(sender, e);
        }
        //}
    }
    public EntValidationErrors ValidationErrors
    {
        get
        {
            return ValidationErrorMessages1.ValidationErrors;
        }
        set
        {
            ValidationErrorMessages1.ValidationErrors = value;
        }
    }
    public void EnableButtons(bool newButton, bool editButton, bool saveButton, bool cancelButton, bool deleteButton, bool findButton)
    {
        if (newButton == false)
        {
            btnNew.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnNew.CssClass = "btn btn-secondary";
        }
        if (editButton == false)
        {
            btnEdit.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnEdit.CssClass = "btn btn-secondary";
        }
        if (saveButton == false)
        {
            btnSave.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnSave.CssClass = "btn btn-secondary";
        }
        if (cancelButton == false)
        {
            btnCancel.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnCancel.CssClass = "btn btn-secondary";
        }
        if (deleteButton == false)
        {
            btnDelete.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnDelete.CssClass = "btn btn-secondary";
        }
        if (findButton == false)
        {
            btnFind.CssClass = "btn btn-secondary disabled";
        }
        else
        {
            btnFind.CssClass = "btn btn-secondary";
        }
        btnNew.Enabled = newButton;
        btnEdit.Enabled = editButton;
        btnSave.Enabled = saveButton;
        btnCancel.Enabled = cancelButton;
        btnDelete.Enabled = deleteButton;
        btnFind.Enabled = findButton;
    }
    public void ShowHideButtons(bool newButton, bool editButton, bool saveButton, bool cancelButton, bool deleteButton, bool findButton)
    {
        btnNew.Visible = newButton;
        btnEdit.Visible = editButton;
        btnSave.Visible = saveButton;
        btnCancel.Visible = cancelButton;
        btnDelete.Visible = deleteButton;
        btnFind.Visible = findButton;
    }
    public void ChangeButtonsText(string newButton, string editButton, string saveButton, string cancelButton, string deleteButton, string findButton)
    {


        HtmlGenericControl iconNew = new HtmlGenericControl("i");
        HtmlGenericControl textNew = new HtmlGenericControl("span");

        iconNew.Attributes["class"] = "fa fa-plus-circle";
        textNew.InnerText = newButton;
        btnNew.Controls.Add(iconNew);
        btnNew.Controls.Add(textNew);

        HtmlGenericControl iconEdit = new HtmlGenericControl("i");
        HtmlGenericControl textEdit = new HtmlGenericControl("span");
        iconEdit.Attributes["class"] = "fa fa-pencil-alt";
        textEdit.InnerText = editButton;
        btnEdit.Controls.Add(iconEdit);
        btnEdit.Controls.Add(textEdit);


        HtmlGenericControl iconSave = new HtmlGenericControl("i");
        HtmlGenericControl textSave = new HtmlGenericControl("span");
        iconSave.Attributes["class"] = "fa fa-check-circle";
        textSave.InnerText = saveButton;
        btnSave.Controls.Add(iconSave);
        btnSave.Controls.Add(textSave);


        HtmlGenericControl iconCancel = new HtmlGenericControl("i");
        HtmlGenericControl textCancel = new HtmlGenericControl("span");
        iconCancel.Attributes["class"] = "fa fa-times";
        textCancel.InnerText = cancelButton;
        btnCancel.Controls.Add(iconCancel);
        btnCancel.Controls.Add(textCancel);


        HtmlGenericControl iconDelete = new HtmlGenericControl("i");
        HtmlGenericControl textDelete = new HtmlGenericControl("span");
        iconDelete.Attributes["class"] = "fa fa-trash-alt";
        textDelete.InnerText = deleteButton;
        btnDelete.Controls.Add(iconDelete);
        btnDelete.Controls.Add(textDelete);


        HtmlGenericControl iconFind = new HtmlGenericControl("i");
        HtmlGenericControl textFind = new HtmlGenericControl("span");
        iconFind.Attributes["class"] = "fa fa-search";
        textFind.InnerText = findButton;
        btnFind.Controls.Add(iconFind);
        btnFind.Controls.Add(textFind);



    }
    //protected bool IsPeriodActive()
    //{
    //    //if (Application["Period"] != null)
    //    //{
    //    //    if (((BusinessPeriodBO)Application["Period"]).PeriodStatus == BusinessPeriodBO.PStatusEnum.CurrentAndActive)
    //    //    {
    //    //        return true;
    //    //    }
    //    //    else
    //    //    {
    //    //        ValidationErrors.Add( "Current Period is not Active");
    //    //        return false;
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    ValidationErrors.Add( "Unable to find current period");
    //    //    return false;
    //    //}
    //}
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        if (((BasePage)this.Page).ReadOnly)
        {
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnEdit.Visible = false;
            btnNew.Visible = false;
            btnDelete.Visible = false;
            btnNew.Visible = false;
            btnEdit.Visible = false;
            btnSave.Visible = false;
            btnCancel.Visible = false;
            btnDelete.Visible = false;
        }
        else if (((BasePage)this.Page).Edit)
        {
            //Hide the delete button
            btnDelete.Visible = false;
        }
    }
}

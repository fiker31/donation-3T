using BLL;
using BLL.Framework;
using System;
using System.Configuration;
using System.IO;
using System.Linq;
public partial class Basic_DonationRegList : BaseEditPage<DonationReg_ListBO>
{
    private const string VS_IE = "DonationRegList";
    private const string VS_MODE = "Entry Mode";
    private const string VS_CONTROL = "Control Collections";
	static string base64String = null;

	private static byte[] byteImageData;

	public static String poster = "";
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
			filePoster.Enabled = false;
			poster = "";
		}
    }

	

	private void byteArrayToImage(byte[] byteArrayIn)
	{
		System.Drawing.Image newImage;
		//string strFileName = GetTempFolderName() + “yourfilename.gif”;
		string strFileName = "";
		if (byteArrayIn != null)
		{
			using (MemoryStream stream = new MemoryStream(byteArrayIn))
			{
				newImage = System.Drawing.Image.FromStream(stream);
				newImage.Save(strFileName);
				// img.Attributes.Add(“src”, strFileName);
			}
			// lblMessage.Text = “The image conversion was successful.”;
		}
		else
		{
			//  Response.Write(“No image data found!”);
		}
	}

	void Master_NewButton_Click(object sender, EventArgs e)
    {
		filePoster.Enabled = true;
		ViewState[VS_MODE] = 'N';
        Master.EnableButtons(false, false, true, true, false, false);
        //Enable & Clear controls
        MakeFormEditable(CapabilityNames().FirstOrDefault(), this.Controls);
        ClearForm(this.Controls);
    }
    void Master_EditButton_Click(object sender, EventArgs e)
    {
        filePoster.Enabled = true;
        if (((DonationReg_ListBO)ViewState[VS_IE]).id > 0)
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
        if (poster == "")
        {
            validationErrors.Add("Please upload the poster!");
            Master.ValidationErrors = validationErrors;
        }
        else
        {
            String Path = poster;
            //Validate the Input Date and Number Data types
            DonationReg_ListBO td;
            if ((char)ViewState[VS_MODE] == 'N')
            {
                td = new DonationReg_ListBO();
                td.DBAction = BaseEO.DBActionEnum.Insert;
            }
            else
            {
                td = (DonationReg_ListBO)ViewState[VS_IE];
                td.DBAction = BaseEO.DBActionEnum.Update;
            }
            LoadObjectFromScreen(td);
            td.poster = File.ReadAllBytes(Path);
            if (!td.Save(ref validationErrors, Convert.ToString(Session["LoginId"])))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                ResetButtons();
                //Refresh the Viewstate with current record 
                td.Load(td.id);
                LoadScreenFromObject(td);
                //DisableControls
            }
        }
    }

	public string ImageToBase64(string path)
	{
		using (System.Drawing.Image image = System.Drawing.Image.FromFile(path))
		{
			using (MemoryStream m = new MemoryStream())
			{
				image.Save(m, image.RawFormat);
				byte[] imageBytes = m.ToArray();
				base64String = Convert.ToBase64String(imageBytes);
				return base64String;
			}
		}
	}

	void Master_CancelButton_Click(object sender, EventArgs e)
    {
        LoadScreenFromObject((DonationReg_ListBO)ViewState[VS_IE]);
        ResetButtons();
    }
    void Master_DeleteButton_Click(object sender, EventArgs e)
    {
        if (((DonationReg_ListBO)ViewState[VS_IE]).id.ToString().Trim().Length > 0)
        {
            EntValidationErrors validationErrors = new EntValidationErrors();
            DonationReg_ListBO td = (DonationReg_ListBO)ViewState[VS_IE];
            td.DBAction = BaseEO.DBActionEnum.Delete;
            if (!td.Delete(ref validationErrors, Convert.ToString(Session["LoginId"])))
            {
                Master.ValidationErrors = validationErrors;
            }
            else
            {
                //Clear Controls
                ClearForm(this.Controls);
                ViewState[VS_IE] = new DonationReg_ListBO();
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
	protected void btnUpload_Click(object sender, EventArgs e)
	{

		if (filePoster.HasFile)
		{

			filePoster.SaveAs(ConfigurationManager.AppSettings["poster"].ToString() + filePoster.FileName);
			txtImageStatus.InnerText = "Image Uploaded Successfully.";
			txtImageStatus.Style.Add("color", "ForestGreen;");

			poster = ConfigurationManager.AppSettings["poster"].ToString() + filePoster.FileName;
			ShowLogo(poster);


		}
		else
		{
			txtImageStatus.InnerText = "Please Select your file";
			txtImageStatus.Style.Add("color", "red;");
		}


	}
	public void ShowLogo(String Path)
	{
		byteImageData = ReadImage(Path, new string[] { ".gif", ".png", ".jpg", ".bmp" });
		byte[] image = File.ReadAllBytes(Path);
		string base64String = Convert.ToBase64String(image, 0, image.Length);
		imgposter.ImageUrl = "data:image/png;base64," + base64String;
	}
	private static byte[] ReadImage(string p_postedImageFileName, string[] p_fileType)
	{
		bool isValidFileType = false;
		try
		{
			FileInfo file = new FileInfo(p_postedImageFileName);
			foreach (string strExtensionType in p_fileType)
			{
				if (strExtensionType == file.Extension)
				{
					isValidFileType = true;
					break;
				}
			}
			if (isValidFileType)
			{
				FileStream fs = new FileStream(p_postedImageFileName, FileMode.Open, FileAccess.Read);
				BinaryReader br = new BinaryReader(fs);
				byte[] image = br.ReadBytes((int)fs.Length);
				br.Close();
				fs.Close();
				return image;
			}
			return null;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
	#region overrides
	protected override void LoadObjectFromScreen(DonationReg_ListBO baseEO)
    {
        //Check the controls and ammend accordingly
        baseEO.companyid = txtCompanyID.Text;
        baseEO.donationtitle = TextBox9.Text;
        baseEO.donationdescription = TextBox1.Text;
        baseEO.donationamountrequired = TextBox2.Text;
        baseEO.donationenddate =   TextBox3.Text;
        baseEO.specialshortcode =  TextBox4.Text;
        baseEO.donationrelatedlinks = TextBox5.Text;
        
    }
    protected override void LoadScreenFromObject(DonationReg_ListBO baseEO)
    {
        //Check the controls and ammend accordingly
        txtCompanyID.Text = baseEO.companyid;
        TextBox9.Text = baseEO.donationtitle;
        TextBox1.Text = baseEO.donationdescription;
        TextBox2.Text = baseEO.donationamountrequired;
        TextBox3.Text = baseEO.donationenddate;
        TextBox4.Text = baseEO.specialshortcode;
        TextBox5.Text = baseEO.donationrelatedlinks;
        //Put the object in the view state so it can be attached back to the data context.
        ViewState[VS_IE] = baseEO;
    }
    protected override void LoadControls()
    {
    }
    protected override void GoToSearchPage()
    {
        Session[GymConst.SS_ID] = ((DonationReg_ListBO)ViewState[VS_IE]).id.ToString();
        Response.Redirect("SetUpSearch.aspx" + EncryptQueryString("page=DonationRegList.aspx"));
    }
    protected override void ResetButtons()
    {
        Master.EnableButtons(true, true, false, false, true, true);
        MakeFormReadOnly(CapabilityNames().FirstOrDefault(), this.Controls);
    }
    public override string MenuItemName()
    {
        return "Donation Registration";
    }
    public override string[] CapabilityNames()
    {
        return new string[] { "Donation Registration" };
    }
    #endregion overrides
}

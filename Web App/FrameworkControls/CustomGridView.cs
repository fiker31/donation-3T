using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace FrameworkControls
{
    #region CustomGridView
    [ToolboxData("<{0}:CustomGridView runat=server></{0}:CustomGridView>")]
    public class CustomGridView : GridView
    {
        #region Members
        private ArrayList _methodParameters = new ArrayList();
        #endregion Members
        #region Constructor
        public CustomGridView()
        {
            AutoGenerateColumns = false;
            AllowPaging = true;
            AllowSorting = false;
            EmptyDataText = "Matching Record not found.";
            PageSize = 25;
            GridLines = GridLines.Both;
            HeaderStyle.CssClass = "gridViewHeader";
            RowStyle.CssClass = "gridViewRow";
            AlternatingRowStyle.CssClass = "gridViewAlternatingRow";
            PagerStyle.CssClass = "gridViewPager";
            CellPadding = 3;
        }
        #endregion Constructor
        private SortDirection SortDirectionLast
        {
            get
            {
                if (ViewState["SortDirectionLast"] == null)
                    //Default to ascending
                    return SortDirection.Ascending;
                else
                    return (SortDirection)ViewState["SortDirectionLast"];
            }
            set { ViewState["SortDirectionLast"] = value; }
        }
        public void AddBoundField(string dataField, string headerText, string sortExpression)
        {
            BoundField bf = new BoundField();
            if (dataField != "")
            {
                bf.DataField = dataField;
            }
            if (headerText != "")
            {
                bf.HeaderText = headerText;
            }
            if (sortExpression != "")
            {
                bf.SortExpression = sortExpression;
            }
            Columns.Add(bf);
        }
    }
    #endregion CustomGridView
}

using System;
using Criterion;

namespace ExampleCriterion.CriterionBase.CustomItems
{
    public partial class TestCustom : System.Web.UI.UserControl,ICriterionCustomControl
    {
        private string idd;

        protected void Page_Load(object sender, EventArgs e)
        {  
           

        }

        public void SetId(string id, object helper)
        {
            this.idd = id;
            Literal1.Text = string.Format("<input id=\"{0}-hiion\" data-custom=\"{0}\" type=\"hidden\" name=\"{0}\">", id);



            for (var i = 0; i < cb.Items.Count; i++)
            {
                cb.Items[i].Attributes.Add("onclick", "ActIOns('" + id + "-hiion')");
            }

        }



        public void GetData(string data)
        {

            var par = data.Split(',');
            foreach (var s in par)
            {
                var ee = cb.Items.FindByValue(s);
                if (ee != null)
                    ee.Selected = true;
            }
            Literal1.Text = string.Format("<input id=\"{0}-hiion\" data-custom=\"{0}\" type=\"hidden\" value=\"{1}\" name=\"{0}\">", idd, data);
        }
    }
}
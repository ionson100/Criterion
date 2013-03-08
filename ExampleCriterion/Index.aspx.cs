using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Criterion;
using ExampleCriterion.CriterionBase.BodyItems;

namespace ExampleCriterion
{
    public partial class Index : System.Web.UI.Page
    {
        private static bool _active;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!_active)//начальная закрузка при старте приложения, положил сюда, что бы далеко не лазить
            {
                CriterionForms.AddHelpWriter(new MyClass1());
                CriterionForms.ImageHelpUrl = "/CriterionBase/ContentCriterion/help.png";
                CriterionForms.PrefixTypeForKeyNameOueryString = "typename";
                _active = !_active;
            }

            GetValue();
        }







        protected void Button3Click(object sender, EventArgs e)
        {
            GetValue();
        }
        private void GetValue()
        {
            var sb = new StringBuilder();
            foreach (var s in CriterionForms1.GetValidate().ValidateExceptionList)
            {
                sb.AppendFormat("{0}<br/>", s.Message);
            }
            ErrorText.Text = sb.ToString();
            sb.Clear();

            foreach (var variable in CriterionForms1.GetValidate().SqlPartDescriptionList)
            {
                sb.Append(" and ");
                sb.Append(variable.TemplateSqlPart("'"));
            }
            SqlText.Text = sb.ToString();
        }

        protected void BtBodyClick(object sender, EventArgs e)
        {
            CriterionForms1.TypeCore = typeof(Body);
        }

        protected void BtTelephonesClick(object sender, EventArgs e)
        {
            CriterionForms1.TypeCore = typeof(Telephones);
        }

    }

    public class MyClass1 : ICriterionHelpWriter
    {
        public string GetHelpString(Type typemeny, int idHelpPrt)
        {
            return idHelpPrt.ToString(CultureInfo.InvariantCulture);
        }
    }
  
}
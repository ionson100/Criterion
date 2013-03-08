using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Criterion;
using ExampleCriterion.CriterionBase.Model;

namespace ExampleCriterion.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index(CriterionValidate valid)
        {
            var sb = new StringBuilder();
            var cri = ControllerContext.CriterionContext();
            foreach (var variable in cri.GetSqlPartDescriptionList)
            {
                sb.Append(" and ");
                sb.Append(variable.TemplateSqlPart("'"));
            }
            var error = new StringBuilder();
            if (!ControllerContext.CriterionContext().IsValid)
            {
                foreach (var variable in cri.GetAllErrorList)
                {
                    error.Append(variable.Message);
                    error.Append("<br/>");
                }
            }
            var t = new TestModel { Type = cri.TypeCore, Sql = sb.ToString(), Error = error.ToString() };
            return View(t);

        }
        [HttpPost]
        public ActionResult Index(CriterionValidate valid, string s)
        {
            var sb = new StringBuilder();
            foreach (var variable in valid.SqlPartDescriptionList)
            {
                sb.Append(" and ");
                sb.Append(variable.TemplateSqlPart("'"));
            }
            var error = new StringBuilder();
            if (!valid.IsValid)
            {
                foreach (var variable in valid.ValidateExceptionList)
                {
                    error.Append(variable.Message);
                    error.Append(variable.Source);
                    error.Append("<br/>");
                }
            }


            var t = new TestModel { Type = valid.TypeCore, Sql = sb.ToString(), Error = error.ToString() };

            return View(t);
        }
        public ActionResult About()
        {

            return View();
        }
    }
}

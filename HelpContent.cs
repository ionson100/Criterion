using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Criterion
{
   
    /// <summary>
    /// 
    /// </summary>
    public class HelpContent : IHttpHandler
    {
       
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var id = context.Request.QueryString["id"];
            var strtypeguid = context.Request.QueryString["type"];
            Type typecore = null;
            if (!string.IsNullOrEmpty(strtypeguid))
            {
                var t = CriterionValidate.Listtype.Value.Where(a => a.GUID == Guid.Parse(strtypeguid));
                var enumerable = t as Type[] ?? t.ToArray(); 
                if (enumerable.Any())
                 {
                     typecore = enumerable.First();
                 }
            }
            int idCore;
            int.TryParse(id, out idCore);
            if (ControlActivator.TypeHelpWriter == null)
            {
                throw new Exception("Not assigned to an object that is responsible for the transmission of information");
            }
            var sob = Activator.CreateInstance(ControlActivator.TypeHelpWriter);
            if (sob == null)
            {
                throw new Exception("I can not create an object that is responsible for the transmission of information may not have a default constructor .");
            }
            if (!(sob is ICriterionHelpWriter))
            {
                throw new Exception("object that does not implement the ICriterionHelpWriter");
            }
            context.Response.ContentType = "html";
            context.Response.Write(((ICriterionHelpWriter)sob).GetHelpString(typecore, idCore));

        }

        #endregion
    }
}

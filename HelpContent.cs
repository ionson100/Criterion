using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// Обработчик поставки контента справки помощи
    /// </summary>
    public class HelpContent : IHttpHandler
    {
        /// <summary>
        /// Вам потребуется настроить этот обработчик в файле Web.config вашего 
        /// веб-сайта и зарегистрировать его с помощью IIS, чтобы затем воспользоваться им.
        /// Дополнительные сведения см. по ссылке: http://go.microsoft.com/?linkid=8101007
        /// </summary>
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
                throw new Exception("Не назначен обьект, отвечающий за передачу информации");
            }
            var sob = Activator.CreateInstance(ControlActivator.TypeHelpWriter);
            if (sob == null)
            {
                throw new Exception("Не могу создать объект, отвечающий за передачу информации, возможно не имеет конструктора по умолчанию...");
            }
            if (!(sob is ICriterionHelpWriter))
            {
                throw new Exception("Переданый объект, не реализует интерфейс ICriterionHelpWriter ");
            }
            context.Response.ContentType = "html";
            context.Response.Write(((ICriterionHelpWriter)sob).GetHelpString(typecore, idCore));

        }

        #endregion
    }
}

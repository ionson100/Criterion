using System;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// Интерфейс котрый должен реализовать тип, обьект которого будет создавать  записи для хелпов панели
    /// </summary>
    public interface ICriterionHelpWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="typemeny"> </param>
        /// <param name="idHelpPrt"></param>
        /// <returns></returns>
        string GetHelpString(Type typemeny, int idHelpPrt);
    }
}
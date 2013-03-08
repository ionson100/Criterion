using System;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    ///Interface buyout should implement a type object which will create entries for HELP panel
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
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Criterion
{
   /// <summary>
    /// Used to load the slider and create  IEnumerable(ListItem)
   /// </summary>
   public  interface ICriterion
    {
       /// <summary>
        /// Loading slider,as select the type of price
       /// </summary>
       /// <param name="max"></param>
       /// <param name="min"></param>
       /// <param name="fieldName"></param>
       /// <param name="helper"></param>
       void GetMinMaxForSlider(out int max, out int min, string fieldName, object helper);
       /// <summary>
       /// Creating a List List Item, to load controls, it should be noted that the application, in MVC - helper=ViewContext, and in the application of it is passed as WebForms - HttpContext
       /// </summary>
       /// <param name="t"></param>
       /// <param name="helper"></param>
       /// <typeparam name="T"></typeparam>
       /// <returns></returns>
       IEnumerable<ListItem> GetListItems<T>(T t, object helper) where T : Type;

      

    }
}

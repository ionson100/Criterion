using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace Criterion
{
   /// <summary>
    /// Применяется для загрузки слайдера и для  IEnumerable(ListItem)
   /// </summary>
   public  interface ICriterion
    {
       /// <summary>
       /// Загрузка слайдера, по типу выбора цены
       /// </summary>
       /// <param name="max"></param>
       /// <param name="min"></param>
       /// <param name="fieldName"></param>
       /// <param name="helper"></param>
       void GetMinMaxForSlider(out int max, out int min, string fieldName, object helper);
       /// <summary>
       /// Создание списка ListItem, для загрузки в контролы, сдедует заметить что при применении в MVC - helper=ViewContext, а в применении WebForms он передается как HttpContext
       /// </summary>
       /// <param name="t"></param>
       /// <param name="helper"></param>
       /// <typeparam name="T"></typeparam>
       /// <returns></returns>
       IEnumerable<ListItem> GetListItems<T>(T t, object helper) where T : Type;

      

    }
}

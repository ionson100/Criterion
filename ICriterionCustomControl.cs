using System.Web.Mvc;

namespace Criterion
{
   /// <summary>
   /// 
   /// </summary>
   public  interface ICriterionCustomControl
   {
       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <param name="helper"></param>
       void SetId(string id, object helper);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        void GetData(string data);
       
    }
}

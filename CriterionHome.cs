using System;
using System.Drawing;
using System.Threading;
using System.Web.Mvc;

namespace Criterion
{
   /// <summary>
   /// 
   /// </summary>
   public   class CriterionHomeController : Controller
    {
       private static readonly Lazy<byte[]> Bute = new Lazy<byte[]>(GetBytesImage,LazyThreadSafetyMode.ExecutionAndPublication);

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <param name="type"> </param>
       /// <returns></returns>
       /// <exception cref="Exception"></exception>
       public string Index(string id,string type)
       {
           Type typecore = null; 
           if(!string.IsNullOrEmpty(type))
           {
               typecore = ControllerContext.CriterionContext().GetTypeForGuid(type);
           }
           int idCore;
           int.TryParse(id, out idCore);
           if(ControlActivator.TypeHelpWriter==null)
           {
               throw new Exception("Not assigned to an object that is responsible for the transmission of information.");
           }
           var sob = Activator.CreateInstance(ControlActivator.TypeHelpWriter);
           if(sob==null)
           {
               throw new Exception("I can not create an object that is responsible for the transmission of information may not have a default constructor ...");
           }
           if(!(sob is ICriterionHelpWriter))
           {
               throw new Exception("object that does not implement the ICriterionHelpWriter.");
           }
           return ((ICriterionHelpWriter) sob).GetHelpString(typecore, idCore);
       }
       /// <summary>
       /// 
       /// </summary>
       /// <returns></returns>
       public ActionResult ImageHelp()
       {

           return File(Bute.Value, "image/png");
          
       }

       static byte[] GetBytesImage()
       {
           return (byte[]) new ImageConverter().ConvertTo(Properties.Resources.help, typeof (byte[]));
       }
    }
   
}



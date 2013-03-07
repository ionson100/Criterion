using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    internal class CritetionValueProvideFactory : ValueProviderFactory
    {
        public override IValueProvider GetValueProvider(ControllerContext controllerContext)
        {
            return new CritetionValueProvider(controllerContext);
        }
    }
}
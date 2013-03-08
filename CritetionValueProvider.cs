using System.Globalization;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    internal class CritetionValueProvider : IValueProvider
    {

        public int OpenPage { get; set; }

        private readonly ControllerContext _context;

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context"></param>
        public CritetionValueProvider(ControllerContext context)
        {
            _context = context;
        }



        public bool ContainsPrefix(string prefix)
        {
            return true;
        }

        public ValueProviderResult GetValue(string key)
        {



            return new ValueProviderResult(_context.CriterionContext().CriterionValidate, "", CultureInfo.InvariantCulture);
        }


    }
}
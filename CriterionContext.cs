using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    public class  CriterionContext
    {

        /// <summary>
        /// Type menu
        /// </summary>
        /// <returns></returns>
        public Type TypeCore{get
        {
            return CriterionValidate.TypeCore;
        }}

        private const string Www = "312312";

        /// <summary>
        /// 
        /// </summary>
        public  CriterionValidate CriterionValidate 
        {
            get {
                return (CriterionValidate)(_context.Controller.ViewData[Www] ??
                                            (
                                                _context.Controller.ViewData[Www] = new CriterionValidate(_context)));
            }
        }

        

        readonly ControllerContext _context;
        

        /// <summary>
        /// ctor.
        /// </summary>
        /// <param name="context"></param>
        public CriterionContext(ControllerContext context)
        {
            _context = context;
        }
       
        /// <summary>
        /// IEnumerable  SqlPartDescription
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SqlPartDescription> GetSqlPartDescriptionList{get
        {
            return CriterionValidate.SqlPartDescriptionList;
        }}
       
        /// <summary>
        /// Validate
        /// </summary>
        /// <returns></returns>
        public bool IsValid{get
        {
            return CriterionValidate.IsValid;
        }}
     
        /// <summary>
        /// list of conversion errors
        /// </summary>
        /// <returns></returns>
        public List<CriterionValidateException> GetAllErrorList{get
        {
             return CriterionValidate.ValidateExceptionList;
        }}
      
       
        /// <summary>
        /// Dictionary of the - query as  sql-where
        /// </summary>
        /// <returns></returns>
        public Dictionary<string,string> GetWherePartList{get
        {
            return CriterionValidate.RequestParametrWhere;
        }}
    
        /// <summary>
        ///Type Search by Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public Type GetTypeForGuid(string guid)
        {
            
            
            return CriterionValidate.GetTypeForGuid(guid);
        }

        /// <summary>
        /// Type Search by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Type GetTypeForName(string name)
        {
            
           
            return  CriterionValidate.GetTypeForName(name);
        }
   
        /// <summary>
        /// Type Search by full name
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public Type GetTypeForFullName(string fullname)
        {


            return CriterionValidate.GetTypeForFullName(fullname);
        }
    }
}
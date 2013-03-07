using System;

namespace Criterion
{
    /// <summary>
    /// Ошибки преобразования типов
    /// </summary>
    [Serializable]
    public  class CriterionValidateException:Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        public CriterionValidateException(string errorMessage):base(errorMessage)
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="exception"></param>
        public CriterionValidateException(string errorMessage, Exception exception):base(errorMessage,exception)
        {
        }
    }
}
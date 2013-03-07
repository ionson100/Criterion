using System;
using System.Web.Mvc;

namespace Criterion
{
    /// <summary>
    /// ��������� ������ ������ ����������� ���, ������ �������� ����� ���������  ������ ��� ������ ������
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
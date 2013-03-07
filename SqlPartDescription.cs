using System;
using System.Collections;
using System.Text;

namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    public class SqlPartDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public string NameColumnFomBase { get; set; }
        /// <summary>
        /// 
       
        /// ��� ��������
        /// </summary>
        public Type TypePropery { get; set; }
        /// <summary>
        /// �������������� ��������
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// ��  ������ ��
        /// </summary>
        public bool IsArea { get; set; }
        /// <summary>
        /// �� ������ �� �����
        /// </summary>
        public bool IsBetween { get; set; }
        /// <summary>
        /// ��������� sql ���� ����� c ��������� ������
        /// </summary>
        public string TemplateSqlPart( string prefix)
        {
            var ee = prefix;
            

            if(!IsArea&&IsBetween&&!string.IsNullOrEmpty(DataCore.ToString()))
            {
                if(!(DataCore is string))
                {
                    ee = String.Empty;
                }
                return string.Format(" {0} = {2}{1}{2} ", NameColumnFomBase, DataCore,ee);
            }
            var list = DataCore as IList; 
            if (list != null && (IsBetween&&list.Count>0))
            {
                var s = DataCore as IList;

                if(s != null && !(s[0] is String))
                {
                    ee = string.Empty;
                }

                return string.Format(" {0} {2}{1}{2} between {2}{3}{2} ", NameColumnFomBase,s[0],ee,s[1]);
            }
            if (list != null&&IsArea && !IsBetween&&list.Count>0)
            {
                var datas = DataCore as IList;
                if (datas != null && !(datas[0] is String))
                {
                    ee = string.Empty;
                }
                var sb=new StringBuilder(string.Format(" {0} in (",NameColumnFomBase));

                for (var index = 0; index < datas.Count; index++) 
                {
                    var data = datas[index];
                    sb.AppendFormat("{2} {1}{0}{1}", data, ee,index==0?"":",");
                }
                sb.Append(") ");
                return sb.ToString();
            }
            if(!IsBetween && !IsBetween&&!string.IsNullOrEmpty(DataCore.ToString()))
            {
                if (!(DataCore is string))
                {
                    ee = String.Empty;
                }
                return string.Format(" {0} = {2}{1}{2} ", NameColumnFomBase, DataCore, ee);
            }

            return null;
        }
      
        /// <summary>
        /// ���� �� ���� ������ ������� sql , ������ ������ ������
        /// </summary>
        public object DataCore { get; set; }
    }
}
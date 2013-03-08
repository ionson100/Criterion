using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Web.Mvc;
using Criterion.Attributes;

namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    public class CriterionValidate
    {
       

        /// <summary>
        /// ctor.
        /// </summary>
        public CriterionValidate()
        {
        }

        /// <summary>
        /// List of errors in the validation Parts of  Request
        /// </summary>
        public readonly List<CriterionValidateException> ValidateExceptionList = new List<CriterionValidateException>();
       
        /// <summary>
        /// Parts of the list curent Request
        /// </summary>
        public readonly List<SqlPartDescription> SqlPartDescriptionList = new List<SqlPartDescription>();
     
        /// <summary>
        /// Getting current type menu the Guid
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static Type GetTypeForGuid(string guid)
        {
            var eee = Listtype.Value.Where(a => a.GUID.ToString() == guid);
            var enumerable = eee as Type[] ?? eee.ToArray();
            if(enumerable.Any()) return enumerable.First();
            return null;

        }

        /// <summary>
        /// Getting current type menu the  name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Type GetTypeForName(string name)
        {
            var eee = Listtype.Value.Where(a => a.Name.ToLower() == name.ToLower());
            var enumerable = eee as Type[] ?? eee.ToArray();
            if(enumerable.Any()) return enumerable.First();
            return null;
        }
        /// <summary>
        /// Getting current type menu the full name
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public static Type GetTypeForFullName(string fullname)
        {
            var eee = Listtype.Value.Where(a => a.FullName.ToLower() == fullname.ToLower());
            var enumerable = eee as Type[] ?? eee.ToArray();
            if(enumerable.Any()) return enumerable.First();
            return null;
        }

        /// <summary>
        /// The validity of the query
        /// </summary>
        public bool IsValid
        {
            get
            {
                return ValidateExceptionList.Count == 0;
            }
        }

        /// <summary>
        /// List of key values ​​query
        /// </summary>
        public readonly Dictionary<string,string> RequestParametrWhere=new Dictionary<string, string>(); 
        /// <summary>
        /// 
        /// </summary>
        private List<BaseAttribute> _baseAttribute=new List<BaseAttribute>();
        /// <summary>
        /// Type menu
        /// </summary>
        public Type TypeCore;
        internal static readonly Lazy<List<Type>> Listtype = new Lazy<List<Type>>(GetTypeList, LazyThreadSafetyMode.None);
        static readonly Lazy<Dictionary<Type, List<BaseAttribute>>> Dictionary =
            new Lazy<Dictionary<Type, List<BaseAttribute>>>(LazyThreadSafetyMode.ExecutionAndPublication);
        private ControllerContext _context;
        private static List<Type> GetTypeList()
        {
            var list = new List<Type>();
            var eee = AppDomain.CurrentDomain.GetAssemblies();
            var fl =
                eee.Where(a => a.IsDynamic == false && a.GlobalAssemblyCache == false)
                   .Select(assembly => assembly.GetExportedTypes()).ToList();
            foreach (var tupl in fl)
            {
                var ass =
                    tupl.Where(
                        a =>
                        a.GetProperties().Any(b => b.GetCustomAttributes(typeof (BaseAttribute), true).Any()));
              
                list.AddRange(ass);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="typecore"></param>
        /// <param name="b"></param>
       
        public CriterionValidate(ControllerContext context, Type typecore)
        {
           

            GetValue(context,typecore);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public CriterionValidate(ControllerContext context)
        {
            GetValue(context,null);
        }

        private void GetValue(ControllerContext context,Type type)
        {
            _context = context;


            if (_context.HttpContext.Request.RequestType == "GET")
            {
                if (type == null)
                {
                    var t = _context.HttpContext.Request.QueryString[ControlActivator.TypeNameKey];
                    if (string.IsNullOrEmpty(t)) return;
                    TypeCore = GetTypeCore(t);
                    if (TypeCore == null) return;
                }
                else
                {
                    TypeCore = type;
                }
            }
            if (_context.HttpContext.Request.RequestType == "POST")
            {
                var t = _context.HttpContext.Request.Form["typeguid"];
                if (string.IsNullOrEmpty(t)) return;
                TypeCore = GetTypeCoreGuid(t);
                if (TypeCore == null) return;
            }
            if (Dictionary.Value.ContainsKey(TypeCore))
            {
                _baseAttribute = new List<BaseAttribute>(Dictionary.Value[TypeCore]);
            }
            else
            {
                var rr = GetBaseAttribute(TypeCore);
                if (rr != null)
                {
                    Dictionary.Value.Add(TypeCore, rr);
                    _baseAttribute = rr;
                }
                else
                {
                    return;
                }
            }

            if (_context.HttpContext.Request.RequestType == "GET")
            {
                var t = _context.HttpContext.Request.QueryString["where"];
                if (string.IsNullOrEmpty(t)) return;
                var ee = t.Split(';');
                foreach (var s in ee)
                {
                    var ees = s.Split(':');
                    if (ees.Count() == 2)
                        if (!RequestParametrWhere.ContainsKey(ees[0].Trim()))
                        {
                            RequestParametrWhere.Add(ees[0].Trim(), ees[1].Trim());
                        }
                }
                if (!RequestParametrWhere.Any()) return;
            }
            if (_context.HttpContext.Request.RequestType == "POST")
            {
                AppleDictionaryReguesqWherePost(RequestParametrWhere, _context, _baseAttribute);
            }
////////////////////////////////////////////////////////////////////////////////////////////////////////////
            foreach (var item in RequestParametrWhere)
            {
                var key = item.Key;
                if (_baseAttribute.All(a => a.Id != key)) continue;
                var res = _baseAttribute.First(a => a.Id == key);
                if (res == null)
                {
                    ValidateExceptionList.Add(
                        new CriterionValidateException(string.Format("I can not find the attributes of the type corresponding to the key - {0}",
                                                                     key)));
                    continue;
                }

                var parse = res.TypeProperty.GetMethod("Parse", new[] {typeof (String)}) ??
                            res.TypeProperty.GetProperty("Value").PropertyType.GetMethod("Parse",
                                                                                         new[] {typeof (String)});


                if (res is CriterionBetweenDateTimeAttribute)
                {
                    var dd = res as CriterionBetweenDateTimeAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);

                    Between(par, item, parse, dd, ',');
                }
                if (res is CriterionBoolAsCheckBoxAttribute)
                {
                    var dd = res as CriterionBoolAsCheckBoxAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    OnePart(parse, par, item, dd);
                }
                if (res is CriterionBoolAsDropDouwnAttribute)
                {
                    var dd = res as CriterionBoolAsDropDouwnAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    OnePart(parse, par, item, dd);
                }
                if (res is CriterionCheckBoxListListAttribute)
                {
                    var dd = res as CriterionCheckBoxListListAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    MultiPart(item, parse, par, dd);
                }
                if (res is CriterionDropDouwnAttribute)
                {
                    var dd = res as CriterionDropDouwnAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    OnePart(parse, par, item, dd);
                }
                if (res is CriterionListBoxAttribute)
                {
                    var dd = res as CriterionListBoxAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    OnePart(parse, par, item, dd);
                }

                if (res is CriterionListBoxMultipleAttribute)
                {
                    var dd = res as CriterionListBoxMultipleAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    MultiPart(item, parse, par, dd);
                }
                if (res is CriterionRadioButtonListAttribute)
                {
                    var dd = res as CriterionRadioButtonListAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    OnePart(parse, par, item, dd);
                }
                if (res is CriterionSimpleBettwenAttribute)
                {
                    var dd = res as CriterionSimpleBettwenAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    Between(par, item, parse, dd, ',');
                }

                if (res is CriterionCustomAttribute)
                {
                    var dd = res as CriterionCustomAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    switch (dd.ValidationType)
                    {
                        case ValidationType.None:
                            {
                                continue;
                            }

                        case ValidationType.Between:
                            {
                                Between(par, item, parse, dd, '-');
                            }
                            break;
                        case ValidationType.Multi:
                            {
                                MultiPart(item, parse, par, dd);
                            }
                            break;
                        case ValidationType.Single:
                            {
                                OnePart(parse, par, item, dd);
                            }
                            break;
                    }
                }

                if (res is CriterionSliderAttribute)
                {
                    bool error = false;
                    var dd = res as CriterionSliderAttribute;
                    var par = Activator.CreateInstance(dd.TypeProperty);
                    var vls = item.Value.Split(',');
                    if (vls.Count() != 2) continue;
                    var listo = new List<object>();
                    foreach (var vl in vls)
                    {
                        try
                        {
                            listo.Add(parse.Invoke(par, new object[] {vl}));
                        }
                        catch (Exception ex)
                        {
                            ValidateExceptionList.Add(
                                new CriterionValidateException(
                                    string.Format("conversion error - [{0}] to {1} Id={2}", vl, dd.TypeProperty.Name, dd.Id),
                                    ex));
                            error = true;
                          
                        }
                    }
                    if(error==false)
                    SqlPartDescriptionList.Add(new SqlPartDescription
                                                   {
                                                       Id = item.Key,
                                                       IsArea = true,
                                                       IsBetween = true,
                                                       TypePropery = dd.TypeProperty,
                                                       DataCore = listo,
                                                       NameColumnFomBase =
                                                           string.IsNullOrEmpty(dd.NameColumnFomBase)
                                                               ? dd.Id
                                                               : dd.NameColumnFomBase
                                                   });
                }
            }
        }

        static List<BaseAttribute> GetBaseAttribute(Type type)
        {
            var ba = new List<BaseAttribute>();

            foreach (var propertyInfo in type.GetProperties())
            {
                var i = propertyInfo.GetCustomAttributes(typeof(BaseAttribute), true);
                if (!i.Any()) continue;
                ((BaseAttribute)i[0]).TypeProperty = propertyInfo.PropertyType;
                if (string.IsNullOrEmpty(((BaseAttribute)i[0]).DisplayName))
                {
                    ((BaseAttribute)i[0]).DisplayName = propertyInfo.Name;
                }
                if (string.IsNullOrEmpty(((BaseAttribute)i[0]).Id))
                {
                    ((BaseAttribute)i[0]).Id = propertyInfo.Name;
                }
                ba.Add((BaseAttribute)i[0]);
            }
            return ba.Any() ? ba : null;
        }
        static Type GetTypeCore(string typestr)
        {
            var ee = Listtype.Value.Where(a => a.Name.ToLower() == typestr.ToLower());
            var enumerable = ee as Type[] ?? ee.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }
        static Type GetTypeCoreGuid(string typestr)
        {
            var ee = Listtype.Value.Where(a => a.GUID.ToString() == typestr);
            var enumerable = ee as Type[] ?? ee.ToArray();
            return enumerable.Any() ? enumerable.First() : null;
        }



        private void MultiPart(KeyValuePair<string, string> item, MethodInfo parse, object par, BaseAttribute dd)
        {
            bool error = false;
            if(string.IsNullOrEmpty(item.Value)) return;
            var vls = item.Value.Split(',');
            var listO = new List<object>();
            foreach (var v in vls)
            {
                try
                {
                    listO.Add(parse.Invoke(par, new object[] { v }));
                }
                catch (Exception ex)
                {
                    ValidateExceptionList.Add(
                        new CriterionValidateException(string.Format("conversion error - [{0}] to {1} Id={2}", v, dd.TypeProperty.Name, dd.Id), ex));
                    error = true;
                }
            }
            if (error) return;
            var sb = new StringBuilder();
            var sbs = new StringBuilder();
            foreach (var value in listO)
            {

                sbs.AppendFormat(" and {0} = {1} ", item.Key, value);
            }
            sb.Append(sbs.ToString().TrimStart(" and".ToCharArray()));
            SqlPartDescriptionList.Add(new SqlPartDescription
                                           {
                                               Id = item.Key,
                                               IsArea = true,
                                               IsBetween = false,
                                               TypePropery = dd.TypeProperty,
                                               DataCore = listO,
                                               NameColumnFomBase =
                                                   string.IsNullOrEmpty(dd.NameColumnFomBase)
                                                       ? dd.Id
                                                       : dd.NameColumnFomBase
                                           });
        }

        private void OnePart(MethodInfo parse, object par, KeyValuePair<string, string> item, BaseAttribute dd)
        {
           
            object real;
            try
            {
                real = parse.Invoke(par, new object[] { item.Value });
            }
            catch (Exception ex)
            {
                ValidateExceptionList.Add(
                    new CriterionValidateException(string.Format("conversion error - [{0}] to {1} Id={2}", item.Value, dd.TypeProperty.Name, dd.Id), ex));
                return;
            }
            SqlPartDescriptionList.Add(new SqlPartDescription
                                           {
                                               Id = item.Key,
                                               IsArea = false,
                                               IsBetween = false,
                                               TypePropery = dd.TypeProperty,
                                               DataCore = real,
                                               NameColumnFomBase = string.IsNullOrEmpty(dd.NameColumnFomBase) ? dd.Id : dd.NameColumnFomBase
                                           });
        }

        private void Between(Object par, KeyValuePair<string, string> item, MethodInfo parse, BaseAttribute dd, char split)
        {
            bool error = false;
            var vls = item.Value.Split(split);
            if (vls.Count() == 1) return;
            string sas1 = "", sas2 = "";
            if (vls.Count() == 2)
            {
                sas1 = vls[1];
            }
            if (vls.Count() == 3)
            {
                sas1 = vls[1];
                sas2 = vls[2];
            }
            if (string.IsNullOrEmpty(sas1)) return;
            object real1 = null, real2 = null;
            if (!string.IsNullOrEmpty(sas1))
            {
                try
                {
                    real1 = parse.Invoke(par, new object[] { sas1 });
                }
                catch (Exception ex)
                {
                    ValidateExceptionList.Add(
                        new CriterionValidateException(string.Format("conversion error - [{0}] to {1} Id={2}", sas1, dd.TypeProperty.Name, dd.Id), ex));
                    error = true;
                }
            }
            else
            {
                ValidateExceptionList.Add(
                    new CriterionValidateException(string.Format("conversion error - [{0}] to {1} Id={2}", sas1, dd.TypeProperty.Name, dd.Id)));
                error = true;
            }
            if (!string.IsNullOrEmpty(sas2))
            {
                try
                {
                    real2 = parse.Invoke(par, new object[] { sas2 });
                }
                catch (Exception ex)
                {
                    ValidateExceptionList.Add(
                        new CriterionValidateException(string.Format("conversion error - [{0}] to {1} Id={2}", sas2, dd.TypeProperty.Name, dd.Id), ex));
                    error = true;
                }
            }
            if(error)return;
            var data = vls.Count() == 2 ? real1 : new[] { real1, real2 };

            SqlPartDescriptionList.Add(new SqlPartDescription
                                           {
                                               Id = item.Key,
                                               IsArea = vls.Count() != 2,
                                               IsBetween = vls.Count() != 2,
                                               TypePropery = dd.TypeProperty,
                                               DataCore = data,
                                               NameColumnFomBase = string.IsNullOrEmpty(dd.NameColumnFomBase) ? dd.Id : dd.NameColumnFomBase 
                                           });
        }




        static  void AppleDictionaryReguesqWherePost(Dictionary<string, string> dictionary,ControllerContext context,IEnumerable<BaseAttribute> list )
        {

            var forms = context.HttpContext.Request.Form;
            foreach (var ba in list)
            {
                if (ba is CriterionBetweenDateTimeAttribute)
                {
                    var manager = forms["manager-" + ba.Id];
                    var t1 = forms["ot-" + ba.Id];
                    var t2 = forms["do-" + ba.Id];

                    if (manager == "1")
                    {
                        if(!string.IsNullOrEmpty(t1)&&!string.IsNullOrEmpty(t2))
                            dictionary.Add(ba.Id, string.Format("{0},{1},{2}", manager, t1, t2));
                    }
                    if (manager == "2")
                    {
                        if(!string.IsNullOrEmpty(t2))
                            dictionary.Add(ba.Id, string.Format("{0},{1}", manager, t2));
                    }

                }
                if (ba is CriterionSimpleBettwenAttribute)
                {
                    var manager = forms["manager-" + ba.Id];
                    var t1 = forms["ot-" + ba.Id];
                    var t2 = forms["do-" + ba.Id];
                    if (!string.IsNullOrEmpty(manager) && !string.IsNullOrEmpty(t1) && !string.IsNullOrEmpty(t2))
                    {
                        if (manager == "1")
                        {
                            if (!string.IsNullOrEmpty(t1) && !string.IsNullOrEmpty(t2))
                                dictionary.Add(ba.Id, string.Format("{0},{1},{2}", manager, t1, t2));
                        }
                    }
                    if (!string.IsNullOrEmpty(manager) && !string.IsNullOrEmpty(t2))
                    {
                        if (manager == "2")
                        {
                            if (!string.IsNullOrEmpty(t2))
                                dictionary.Add(ba.Id, string.Format("{0},{1}", manager, t2));
                        }
                    }

                }
                if (ba is CriterionBoolAsCheckBoxAttribute)
                {
                    var value = forms[ba.Id];
                    if (!string.IsNullOrEmpty(value))
                        dictionary.Add(ba.Id, "True");

                }
                if (ba is CriterionSliderAttribute)
                {

                    var t1 = forms[ba.Id + "1"];
                    var t2 = forms[ba.Id + "2"];
                    if (!string.IsNullOrEmpty(t1) && !string.IsNullOrEmpty(t2))
                        dictionary.Add(ba.Id, string.Format("{0},{1}", t1, t2));
                }

                if (ba is CriterionCheckBoxListListAttribute)
                {
                    BaseAttribute ba1 = ba;
                    var valuekey = forms.AllKeys.Where(a => a.IndexOf(ba1.Id + "$" + ba1.Id, StringComparison.Ordinal) != -1);
                    int iii = 0;
                    var dv = new StringBuilder();
                    foreach (var ss in valuekey)
                    {
                        
                        dv.AppendFormat("{0}{1}", iii == 0 ? "" : ",", forms[ss]);
                        iii++;
                    }
                    if (!dictionary.ContainsKey(ba.Id))
                        dictionary.Add(ba.Id, dv.ToString());

                }

                var valuew = forms[ba.Id];
                if (!string.IsNullOrEmpty(valuew))
                {
                    if (!dictionary.ContainsKey(ba.Id))
                        dictionary.Add(ba.Id, valuew);
                }
               

            }
        }
    }
}
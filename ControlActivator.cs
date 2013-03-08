using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using Criterion.Attributes;

namespace Criterion
{
    /// <summary>
    /// Базовый класс для создания менб
    /// </summary>
    public static class ControlActivator
    {
        /// <summary>
        ///   key for querystrung  as typename ( default ="tabs")
        /// </summary>
        public static string TypeNameKey = "tabs";

        internal static Type TypeHelpWriter { get; private set; }

        internal static Boolean KeyAddUrl;
        /// <summary>
        /// Path url for image - help ( default usage image from resource)
        /// </summary>
        public static string ImageHeplUrl { get; set; }
        /// <summary>
        /// Core Method
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static MvcHtmlString CriterionFor<TModel, T>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, T>> expression)
        {
            var value = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
            return MvcHtmlString.Create(new RenderingCriterion<Type>(htmlHelper).RenderingCore((Type)value));

        }

        /// <summary>
        /// Contex menu
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static CriterionContext CriterionContext(this ControllerContext context)
        {
            return context == null ? null : new CriterionContext(context);
        }

        /// <summary>
        /// Core Method ( not uasge model)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="helper"></param>
        /// <param name="type">Type menu</param>
        /// <returns></returns>
        public static MvcHtmlString Criterion<T>(this HtmlHelper helper, T type) where T : Type
        {

            if (type == null)
                return MvcHtmlString.Create("");
            return MvcHtmlString.Create(new RenderingCriterion<T>(helper).RenderingCore(type));
        }
        
        public static void AddHelpWriter(ICriterionHelpWriter writer)
        {
            
                TypeHelpWriter = writer.GetType();
        }
    }


   
    internal  class RenderingCriterion<T> where T : Type
    {
        
        private const string UrlPouter = "Criterion/{controller}/{action}/{id}";
        private const string ProbelTire = "--------------";
        private const String EndDiv = "</div>";
        readonly HtmlHelper _helper;
        private const string HideForSlider = "<input type=\"hidden\" data-core=\"1\" value=\"{1}\" name=\"{0}\" id=\"{0}-hiion\"/>";
        private readonly bool _isContinue;

        public RenderingCriterion(HtmlHelper helper, WebControl control, bool isContinue)
        {
            _helper = helper;
            _isContinue = isContinue;
        }

       



        public RenderingCriterion(HtmlHelper helper)
        {
            _helper = helper;
            if (ControlActivator.KeyAddUrl) return;
            _helper.RouteCollection.MapRoute("CriterionHelpText", UrlPouter,
                                             namespaces: new[] { "Criterion" },
                                             defaults: new { controller = "CriterionHome", action = "Index", id = UrlParameter.Optional });
            ValueProviderFactories.Factories.Add(new CritetionValueProvideFactory());
            ControlActivator.KeyAddUrl = true;
        }


        public string RenderingCore(T type)
        {
            if (type == null) return string.Empty;

            if (ControlActivator.ImageHeplUrl == null)
            {
                ControlActivator.ImageHeplUrl = "/CriterionHome/ImageHelp/22";
            }



           

            Dictionary<string, string> dictionary = null;
            var typerequest =  _helper.ViewContext.HttpContext.Request.RequestType;
            string open = null;
            var list = new List<BaseAttribute>();
            foreach (var bas in type.GetProperties())
            {
                var i = bas.GetCustomAttributes(typeof(BaseAttribute), true);
                if (!i.Any()) continue;
                ((BaseAttribute)i[0]).TypeProperty = bas.PropertyType;
                if (string.IsNullOrEmpty(((BaseAttribute)i[0]).DisplayName))
                {
                    ((BaseAttribute)i[0]).DisplayName = bas.Name;
                }
                if (string.IsNullOrEmpty(((BaseAttribute)i[0]).Id))
                {
                    ((BaseAttribute)i[0]).Id = bas.Name;
                }
                list.Add((BaseAttribute)i[0]);
            }
            if (typerequest == "GET")
            {
                var where =_helper.ViewContext.HttpContext.Request.QueryString["where"];
                open = _helper.ViewContext.HttpContext.Request.QueryString["open"];
                if (!string.IsNullOrEmpty(where))
                {
                    var ee = where.Split(';');
                    dictionary = new Dictionary<string, string>();
                    foreach (var s in ee)
                    {
                        var ees = s.Split(':');
                        if (ees.Count() == 2)
                            if (!dictionary.ContainsKey(ees[0].Trim()))
                                dictionary.Add(ees[0].Trim(), ees[1].Trim());
                    }
                }
            }
           
            if (typerequest == "POST")
            {
              
                dictionary = new Dictionary<string, string>();
                open = _helper.ViewContext.HttpContext.Request.Form["open"];
                var forms = _helper.ViewContext.HttpContext.Request.Form;
                foreach (var ba in list)
                {
                    if (ba is CriterionBetweenDateTimeAttribute)
                    {
                        var manager = forms["manager-" + ba.Id];
                        var t1 = forms["ot-" + ba.Id];
                        var t2 = forms["do-" + ba.Id];

                        if (manager == "1")
                        {
                            dictionary.Add(ba.Id, string.Format("{0},{1},{2}", manager, t1, t2));
                        }
                        if (manager == "2")
                        {
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
                                dictionary.Add(ba.Id, string.Format("{0},{1},{2}", manager, t1, t2));
                            }
                        }
                        if (!string.IsNullOrEmpty(manager) && !string.IsNullOrEmpty(t2))
                        {
                            if (manager == "2")
                            {
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




            

            if (!list.Any()) return "ss";

            var sbJs = new StringBuilder();//"<noscript><style> #Criterion{display:none;}</style></noscript>"style=\"position: static;left: 0;top: 0;z-index: 0\"
            var sb = new StringBuilder();

            sb.Append("<div id='Criterion' >");



            sb.AppendFormat("<input type=\"hidden\"  value=\"{0}\" data-ssd=\"1\" name=\"typeguid\" id=\"typeguid\"/>", type.GUID);
            sb.AppendFormat("<input type=\"hidden\"  value=\"{0}\" data-ssd=\"1\" name=\"open\" id=\"open\"/>", open);
            var dispetcher = new StringBuilder();
            foreach (var baseAttribute in list)
            {
                if (baseAttribute is CriterionBetweenDateTimeAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "betweendate");
                if (baseAttribute is CriterionBoolAsCheckBoxAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "boolcheckbox");
                if (baseAttribute is CriterionBoolAsDropDouwnAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "booldropdown");
                if (baseAttribute is CriterionCheckBoxListListAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "checkboxlist");
                if (baseAttribute is CriterionCustomAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "custom");
                if (baseAttribute is CriterionDropDouwnAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "dropdown");
                if (baseAttribute is CriterionListBoxAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "listbox");
                if (baseAttribute is CriterionListBoxMultipleAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "listboxmultiple");
                if (baseAttribute is CriterionRadioButtonListAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "radiobuttonlist");
                if (baseAttribute is CriterionSimpleBettwenAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "between");
                if (baseAttribute is CriterionSliderAttribute)
                    dispetcher.AppendFormat("{0}-{1};", baseAttribute.Id, "slider");


            }

            sb.AppendFormat("<input type=\"hidden\"  value=\"{0}\" name=\"dictionarycriterion\" id=\"dictionarycriterion\"/>", dispetcher);
            var namegr = new List<string>();
            foreach (var baseAttribute in list.Where(a => !string.IsNullOrEmpty(a.NameGroup)).OrderBy(a => a.NameGroup))
            {
                if (!namegr.Contains(baseAttribute.NameGroup))
                {
                    namegr.Add(baseAttribute.NameGroup);
                }
            }




            #region Asssa


            var ll = list.Where(a => string.IsNullOrEmpty(a.NameGroup) && !(a is CriterionManagementAttribute)).ToList();
            GetValue(sbJs, sb, dictionary, ll);
            
            int ii = 0;
            foreach (var str in namegr)
            {
                ii++;
                var ss = " style='display: none' ";
                sb.AppendFormat(" <div class=\"spoiler-head open\"  data-name=\"{1}\" > {0} </div>", str, ii);
                var str1 = str;
                var ddd = list.Where(a => a.NameGroup == str1).ToList();
                if (!string.IsNullOrEmpty(open))
                {
                    if (open.IndexOf((ii).ToString(CultureInfo.InvariantCulture), StringComparison.Ordinal) != -1)
                    {
                        ss = string.Empty;
                    }
                }
                sb.AppendFormat("  <div class=\"spoiler-body\" {0} >", ss);
                GetValue(sbJs, sb, dictionary, ddd);
                sb.Append(EndDiv);
            }
            GetValue(sbJs, sb, dictionary, list.Where(a => a is CriterionManagementAttribute).ToList());

            #endregion
            sb.Append("<div id='criterionformmodal'></div>");
            sb.Append(EndDiv);

            return sbJs + sb.ToString();
        }

        private void GetValue(StringBuilder sbJs, StringBuilder sb, Dictionary<string, string> dictionary, IEnumerable<BaseAttribute> list)
        {
            foreach (var baseAttribute in list.OrderBy(a => a.IndexSortable))
            {
                if (baseAttribute is CriterionSliderAttribute)
                {
                    var ba = baseAttribute as CriterionSliderAttribute;
                    int max, min, max1, min1;
                    var value = "";
                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                   
                        ob.GetMinMaxForSlider(out max, out min, ba.Id, _helper.ViewContext);
                   
                   
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id];
                        var sss = dictionary[ba.Id].Split(',');
                        if (sss.Count() == 2)
                        {
                            if (!int.TryParse(sss[0], out min1))
                            {
                                min1 = min;
                            }
                            if (!int.TryParse(sss[1], out max1))
                            {
                                max1 = max;
                            }
                        }
                        else
                        {
                            max1 = max;
                            min1 = min;
                        }
                    }
                    else
                    {
                        max1 = max;
                        min1 = min;
                    }
                    //if (max1 != min1)
                    //{
                    
                    if (_isContinue)
                    {
                        max1 = max;
                        min1 = min;
                    }
                        var step = (max1 - min1) / 10;
                        sb.Append(string.Format(HideForSlider, ba.Id, value));
                        sb.Append(Properties.Resources.Slider1.Replace("#id#", ba.Id).
                                      Replace("#name#", ba.DisplayName).
                                      Replace("#min#", min1.ToString(CultureInfo.InvariantCulture)).
                                      Replace("#min1#", min.ToString(CultureInfo.InvariantCulture)).
                                      Replace("#max1#", max.ToString(CultureInfo.InvariantCulture)).
                                      Replace("#max#", max1.ToString(CultureInfo.InvariantCulture)));
                        sbJs.Append("<script type=\"text/javascript\">");
                        sbJs.Append(Properties.Resources.Slider.Replace("#id#", ba.Id).
                                        Replace("#min#", min.ToString(CultureInfo.InvariantCulture)).
                                        Replace("#max#", max.ToString(CultureInfo.InvariantCulture)).
                                        Replace("#max1#", max1.ToString(CultureInfo.InvariantCulture)).
                                        Replace("#min1#", min1.ToString(CultureInfo.InvariantCulture)).
                                        Replace("#step#", step.ToString(CultureInfo.InvariantCulture)));
                        sbJs.Append("</script>");
                    //}
                }
                
                if (baseAttribute is CriterionListBoxAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionListBoxAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                    if(ob==null)continue;
                    var listBox = ob.GetListItems(ba.TypeGeneratorListItem, _helper.ViewContext);
                    var listItems = listBox as ListItem[] ?? listBox.ToArray();
                    if (listBox == null || !listItems.Any()) continue;
                    using (var listBoxCore = new ListBox { ID = ba.Id, Width = new Unit("100%") })
                    {
                        listBoxCore.Items.AddRange(listItems.ToArray());
                        listBoxCore.Attributes.Add("data-core", "1");
                        listBoxCore.Items.Insert(0, new ListItem(ProbelTire, ""));
                        listBoxCore.SelectedIndex = 0;
                        var ee = listBoxCore.Items.FindByValue(value);
                        if (ee != null)
                        {
                            listBoxCore.ClearSelection();
                            ee.Selected = true;
                        }
                        else
                        {
                            value = "";
                        }
                        NewBase("listbox", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(listBoxCore), sb, value);
                    }
                }
            
                if (baseAttribute is CriterionDropDouwnAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionDropDouwnAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                    if (ob == null) continue;
                    var listBox = ob.GetListItems(ba.TypeGeneratorListItem, _helper.ViewContext);
                    using (var dropDownCore = new DropDownList { ID = ba.Id, Width = new Unit("100%") })
                    {
                        var listItems = listBox as ListItem[] ?? listBox.ToArray();
                        if (listBox == null || !listItems.Any()) continue;

                        dropDownCore.Items.AddRange(listItems.ToArray());
                        dropDownCore.Attributes.Add("data-core", "1");
                        dropDownCore.Items.Insert(0, new ListItem(ProbelTire, ""));
                        dropDownCore.SelectedIndex = 0;

                        var ee = dropDownCore.Items.FindByValue(value);
                        if (ee != null)
                        {
                            dropDownCore.ClearSelection();
                            ee.Selected = true;
                        }
                        else
                        {
                            value = "";
                        }
                        NewBase("dropdown", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(dropDownCore), sb, value);
                    }
                }

               
                if (baseAttribute is CriterionBoolAsDropDouwnAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionBoolAsDropDouwnAttribute;

                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    using (var dropDownCore = new DropDownList { ID = ba.Id, Width = new Unit("100%") })
                    {

                        dropDownCore.Items.Add(new ListItem(ProbelTire, ""));
                        dropDownCore.Attributes.Add("data-core", "1");
                        dropDownCore.Items.Add(new ListItem(ba.DisplayTextAsTrue, "true"));
                        dropDownCore.Items.Add(new ListItem(ba.DisplayTextAsFalse, "false"));
                        dropDownCore.SelectedIndex = 0;

                        var ee = dropDownCore.Items.FindByValue(value);
                        if (ee != null)
                        {
                            dropDownCore.ClearSelection();
                            ee.Selected = true;
                        }
                        else
                        {
                            value = "";
                        }

                        NewBase("booldropdown", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(dropDownCore), sb, value);
                    }
                }
              
                if (baseAttribute is CriterionBoolAsCheckBoxAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionBoolAsCheckBoxAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }
                    using (var chexBoxCore = new CheckBox { ID = ba.Id })
                    {
                        chexBoxCore.Attributes.Add("data-core", "1");
                        if (value.ToLower() == "true")
                            chexBoxCore.Checked = true;
                        NewBase("boolcheckbox", RenderControl(chexBoxCore), ba.DisplayName, ba.Id, ba.IdHelp, "", sb, value);
                    }
                }

                if (baseAttribute is CriterionRadioButtonListAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionRadioButtonListAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }
                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                    if (ob == null) continue;
                    var listBox = ob.GetListItems(ba.TypeGeneratorListItem, _helper.ViewContext);
                    var listItems = listBox as IList<ListItem> ?? listBox.ToList();
                    if (listBox == null || !listItems.Any()) continue;
                    using (var radioButtonListCore = new RadioButtonList { ID = ba.Id, Width = new Unit("100%") })
                    {
                        radioButtonListCore.ClientIDMode = ClientIDMode.Static;
                        radioButtonListCore.Items.AddRange(listItems.ToArray());
                        radioButtonListCore.Attributes.Add("data-core", "1");

                        var ee = radioButtonListCore.Items.FindByValue(value);
                        if (ee != null)
                        {
                            radioButtonListCore.ClearSelection();
                            ee.Selected = true;
                        }
                        else
                        {
                            value = "";
                        }

                        NewBase("radiobuttonlist", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(radioButtonListCore), sb, value);
                    }
                }

               
                if (baseAttribute is CriterionCheckBoxListListAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionCheckBoxListListAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }
                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                    if (ob == null) continue;
                    var listBox = ob.GetListItems(ba.TypeGeneratorListItem,_helper.ViewContext);
                    var listItems = listBox as ListItem[] ?? listBox.ToArray(); 
                    if (listBox == null || !listItems.Any()) continue;
                    using (var checkBoxListCore = new CheckBoxList { ID = ba.Id, Width = new Unit("100%") })
                    {
                        checkBoxListCore.ClientIDMode = ClientIDMode.Static;
                        checkBoxListCore.Items.AddRange(listItems.ToArray());
                        checkBoxListCore.Items.OfType<ListItem>().ToList().ForEach(a => a.Attributes.Add("data-core", "1"));
                        var values = value.Split(',');
                        foreach (var s in values)
                        {
                            var ee = checkBoxListCore.Items.FindByValue(s.Trim());
                            if (ee != null)
                            {
                                ee.Selected = true;
                            }
                        }

                        NewBase("checkboxlist", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(checkBoxListCore), sb, value);
                    }
                }

                if (baseAttribute is CriterionListBoxMultipleAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionListBoxMultipleAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    var ob = GetCriterionICriterion(ba.TypeGeneratorListItem);
                    if(ob==null)continue;
                    var listBox = ob.GetListItems(ba.TypeGeneratorListItem, _helper.ViewContext);
                    using (
                        var listBoxCore = new ListBox { ID = ba.Id, Width = new Unit("100%"), SelectionMode = ListSelectionMode.Multiple }
                        )
                    {
                        var listItems = listBox as ListItem[] ?? listBox.ToArray(); 
                        if (listBox == null || !listItems.Any()) continue;
                        listBoxCore.Items.AddRange(listItems.ToArray());
                        listBoxCore.Attributes.Add("data-core", "1");
                        listBoxCore.Items.OfType<ListItem>().ToList().ForEach(a => a.Attributes.Add("data-core", "1"));

                        if (!string.IsNullOrEmpty(ba.Height)) listBoxCore.Height = new Unit(ba.Height);
                        listBoxCore.Items.Insert(0, new ListItem(ProbelTire, ""));

                        var values = value.Split(',');
                        foreach (var s in values)
                        {
                            var ee = listBoxCore.Items.FindByValue(s.Trim());
                            if (ee != null)
                            {
                                ee.Selected = true;
                            }
                        }
                        NewBase("listboxmultiple", "", ba.DisplayName, ba.Id, ba.IdHelp, RenderControl(listBoxCore), sb, value);
                    }
                }

                if (baseAttribute is CriterionBetweenDateTimeAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionBetweenDateTimeAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    var values = value.Split(',');

                    var pos = "";
                    var val1 = "";
                    var val2 = "";
                    if (value.Count() != 1)
                    {
                        if (values[0] == "1" && values.Count() == 3)
                        {
                            pos = values[0];
                            val1 = values[1];
                            val2 = values[2];
                        }
                        if (values[0] == "2" && values.Count() == 2)
                        {
                            pos = values[0];
                            val1 = string.Empty;
                            val2 = values[1];
                        }
                    }

                    using (var dd = new DropDownList())
                    {
                        dd.ID = "manager-" + ba.Id;

                        dd.CssClass = "dd-between";
                        dd.Items.Add(new ListItem(ba.TextBetween, "1"));
                        dd.Items.Add(new ListItem(ba.TextOne, "2"));
                        if (!string.IsNullOrEmpty(pos))
                        {
                            var ee = dd.Items.FindByValue(pos);
                            if (ee != null)
                            {
                                ee.Selected = true;
                            }
                            else
                            {
                                val1 = "";
                                val2 = "";
                            }
                        }

                        string ddd = RenderControl(dd);

                        using (var textBox1 = new TextBox { ID = "ot-" + ba.Id, CssClass = "textbox-between" })
                        {

                            if (pos == "2")
                                textBox1.Attributes.Add("style", "display: none");
                            textBox1.Text = val1;
                            var ttt = RenderControl(textBox1);
                            using (var textBox2 = new TextBox { ID = "do-" + ba.Id, CssClass = "textbox-between" })
                            {
                                textBox2.Attributes.Add("data-core", "1");
                                if (pos == "2")
                                    textBox2.Width = new Unit("60px");
                                textBox2.Text = val2;
                                var ttt1 = RenderControl(textBox2);
                                NewBaseBetween("datapicker", ba.DisplayName, ba.Id, 1, ddd, ttt, ttt1, sb, value);
                            }
                        }
                    }
                }


                if (baseAttribute is CriterionSimpleBettwenAttribute)
                {
                    var value = "";
                    var ba = baseAttribute as CriterionSimpleBettwenAttribute;
                    if (dictionary != null && dictionary.ContainsKey(ba.Id))
                    {
                        value = dictionary[ba.Id].Trim();
                        if (_isContinue)
                        {
                            value = string.Empty;
                        }
                    }

                    var values = value.Split(',');

                    var pos = "";
                    var val1 = "";
                    var val2 = "";
                    if (value.Count() != 1)
                    {
                        if (values[0] == "1" && values.Count() == 3)
                        {
                            pos = values[0];
                            val1 = values[1];
                            val2 = values[2];
                        }
                        if (values[0] == "2" && values.Count() == 2)
                        {
                            pos = values[0];
                            val1 = string.Empty;
                            val2 = values[1];
                        }
                    }

                    using (var dd = new DropDownList { ID = "manager-" + ba.Id })
                    {

                        dd.CssClass = "dd-between";
                        dd.Items.Add(new ListItem(ba.TextBetween, "1"));
                        dd.Items.Add(new ListItem(ba.TextOne, "2"));
                        if (!string.IsNullOrEmpty(pos))
                        {
                            var ee = dd.Items.FindByValue(pos);
                            if (ee != null)
                            {
                                ee.Selected = true;
                            }
                            else
                            {
                                val1 = "";
                                val2 = "";
                            }
                        }

                        var ddd = RenderControl(dd);

                        using (var textBox1 = new TextBox { ID = "ot-" + ba.Id, CssClass = "textbox-between" })
                        {
                            // textBox1.Attributes.Add("onchange", string.Format("betweenAction('{0}-hiion','simplebetween{0}')", ba.Id));
                            textBox1.Attributes.Add("data-datas", ba.Id);
                            if (pos == "2")
                                textBox1.Attributes.Add("style", "display: none");
                            textBox1.Text = val1;
                            var ttt = RenderControl(textBox1);
                            using (var textBox2 = new TextBox { ID = "do-" + ba.Id, CssClass = "textbox-between" })
                            {
                                textBox2.Attributes.Add("data-core", "1");
                                //   textBox2.Attributes.Add("onchange", string.Format("betweenAction('{0}-hiion','simplebetween{0}')", ba.Id));
                                textBox2.Attributes.Add("data-datas", ba.Id);
                                if (pos == "2")
                                    textBox2.Width = new Unit("60px");
                                textBox2.Text = val2;
                                var ttt1 = RenderControl(textBox2);

                                NewBaseBetween("simplebetween", ba.DisplayName, ba.Id, 1, ddd, ttt, ttt1, sb, value);

                            }
                        }
                    }
                }
                if (baseAttribute is CriterionCustomAttribute)
                {
                    var ba = baseAttribute as CriterionCustomAttribute;

                    using (var page = new PageEditor())
                    {
                        var dd = page.LoadControl(ba.UrlCustomControl);
                        page.Controls.Add(dd);
                        ((ICriterionCustomControl)dd).SetId(ba.Id,_helper.ViewContext);
                        if (dictionary != null && dictionary.ContainsKey(ba.Id))
                        {
                            ((ICriterionCustomControl)dd).GetData(dictionary[ba.Id]);
                        }
                        using (TextWriter myTextWriter = new StringWriter())
                        {
                            var myWriter = new HtmlTextWriter(myTextWriter);
                            dd.RenderControl(myWriter);
                            NewBaseCustom(ba.DisplayName, ba.IdHelp, myTextWriter.ToString(), sb);
                        }
                    }
                }


                if (baseAttribute is CriterionManagementAttribute)
                {
                    var ba = baseAttribute as CriterionManagementAttribute;
                    using (var button = new Button { Text = ba.TextButton })
                    {
                        button.Attributes.Add("data-ssd", "1");
                        button.ID = "button-manager";

                        using (var link = new LinkButton { Text = ba.TextClearAll, ID = "link-manager" })
                        {
                            sb.Append(Properties.Resources.ButtonPart.Replace("#1#", RenderControl(link)).Replace("#2#", ba.UsingButton ?
                             RenderControl(button) : string.Empty));
                        }
                    }
                }
            }

        }

        private static ICriterion GetCriterionICriterion(Type typeGeneratorListItem)
        {
            try
            {
                var o = Activator.CreateInstance(typeGeneratorListItem);
                return o as ICriterion;
            }
            catch (Exception ex)
            {

                throw new Exception("Ошибка создания обьекта ICriterion" + ex.Message);
            }

        }

        private static string RenderControl(Control control)
        {
            using (var sw = new StringWriter())
            {
                var tw = new HtmlTextWriter(sw);

                control.RenderControl(tw);
                return sw.ToString();

            }
        }


        private static void NewBase(string nameclass, string s1, string s2, string id, int idHelp, string html, StringBuilder sb, string value)
        {
            var ss = string.Format("<img class=\"image-help\" alt=\"help\" onclick=\"imageHelp({0});\" src=\"{1}\" />", idHelp, ControlActivator.ImageHeplUrl);
            sb.Append(Properties.Resources.newBase.
                          Replace("#val#", value).
                          Replace("#1#", s1).
                          Replace("#2#", s2).
                          Replace("#3#", idHelp == 0 ? "" : ss).
                          Replace("#id#", id).
                          Replace("#name#", nameclass).
                          Replace("#4#", html));

        }

       

        private static void NewBaseCustom(string displayName, int idHelp, string html, StringBuilder sb)
        {
            var ss = string.Format("<img class=\"image-help\" alt=\"help\" onclick=\"imageHelp({0});\" src=\"{1}\" />", idHelp, ControlActivator.ImageHeplUrl);
            sb.Append(Properties.Resources.CustomPart.
                          Replace("#2#", displayName).
                          Replace("#3#", idHelp == 0 ? "" : ss).
                          Replace("#4#", html));

        }
        private static void NewBaseBetween(string nameclass, string displayName, string id, int idHelp, string htmlDd, string htmlTt1, string htmlTt2, StringBuilder sb, string value)
        {
            var ss = string.Format("<img class=\"image-help\" alt=\"help\" onclick=\"imageHelp({0});\" src=\"{1}\" />", idHelp, ControlActivator.ImageHeplUrl);
            sb.Append(Properties.Resources.BetweenPart.
                          Replace("#val#", value).
                          Replace("#1#", "").
                          Replace("#2#", displayName).
                          Replace("#3#", idHelp == 0 ? "" : ss).
                          Replace("#id#", id).
                          Replace("#name#", nameclass).
                          Replace("#dd#", htmlDd)).Replace("#tt1#", htmlTt1).Replace("#tt2#", htmlTt2);

        }



    }

    sealed class PageEditor : Page
    {

        public PageEditor()
        {

            ClientIDMode = ClientIDMode.Static;
            EnableEventValidation = false;
            ClientIDMode = ClientIDMode.Static;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {

        }
    }
}

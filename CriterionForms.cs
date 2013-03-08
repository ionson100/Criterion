using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;


namespace Criterion
{
    /// <summary>
    /// 
    /// </summary>
    [DefaultProperty("TypeCore")]

    [DesignerAttribute(typeof(CriterionControlDesigner))]
    [ ToolboxData("<{0}:TextControl runat=\"server\"></{0}:TextControl>")]

    public class CriterionForms : WebControl
    {
        private bool _isContinue;
        MyState _stateControl = new MyState();
        /// <summary>
        /// type for menu
        /// </summary>
        [Browsable(false)]
        public Type TypeCore
        {
            get
            {
                var type = CriterionValidate.Listtype.Value.Where(a => a.GUID == _stateControl.TypeGuid);
                var enumerable = type as Type[] ?? type.ToArray();
                return enumerable.Any() ? enumerable.First() : null;
               

            }

            set
            {
                if (value == null) return;
                if (Context.Request.RequestType == "POST"&& _stateControl.TypeGuid != value.GUID)
                    _isContinue = true;
                _stateControl.TypeGuid = value.GUID;
               
            }
        }

       
        /// <summary>
        /// url path for image-help
        /// </summary>
        public static string ImageHelpUrl
        {
            get { return ControlActivator.ImageHeplUrl; }
            set { ControlActivator.ImageHeplUrl = value; }
        }

        /// <summary>
        /// PrefixTypeForKeyNameOueryString
        /// </summary>
        public static string PrefixTypeForKeyNameOueryString
        {
            get { return ControlActivator.TypeNameKey; }
            set { ControlActivator.TypeNameKey = value; }
        }

          /// <summary>
        /// Function of the object that's just be responsible for the generation of the Help files, has a default constructor.
          /// </summary>
          /// <param name="writer"></param>
          public static void AddHelpWriter(ICriterionHelpWriter writer)
          {

              ControlActivator.AddHelpWriter(writer);
          }

        private CriterionValidate _criterionValidate;
       


        /// <summary>
        /// Validate in values
        /// </summary>
        public CriterionValidate GetValidate()
        {
            if (_criterionValidate == null)
            {
                ControllerBase f = new CriterionHomeController();
                var d = new ControllerContext(Context.Request.RequestContext, f);

                _criterionValidate= new CriterionValidate(d, TypeCore);
            }
            if (_criterionValidate.TypeCore != null)
                TypeCore = _criterionValidate.TypeCore;
            return _criterionValidate;

        }
        public override Unit Height
        {
            get
            {
                return 0;
            }
            set
            {
               
            }
        }
    

        protected override void RenderContents(HtmlTextWriter output)
        {
           
            if (HttpContext.Current == null)
            {
                output.Write("CriterionForms");
                return;
            }
            var context = new ViewContext{ HttpContext =  new HttpContextWrapper(HttpContext.Current)  };
            var dHelper = new HtmlHelper(context,new MyClass());
            var rcr = new RenderingCriterion<Type>(dHelper,this,_isContinue);
            var str= rcr.RenderingCore(TypeCore);
            output.Write(str);
        }
        ///////////////////////////////////////////
        protected override void OnInit(EventArgs e)
        {
            Page.RegisterRequiresControlState(this);
            base.OnInit(e);
        } 
        
        protected override object SaveControlState()
        {
            return _stateControl;
        }
        protected override void LoadControlState(object state)
        {
            if (state != null)
            {
                _stateControl = (MyState)state;
            }
            base.LoadControlState(state);
        }
    }

    class CriterionControlDesigner : ControlDesigner
    {


        
        public override string GetDesignTimeHtml()
        {
            return "CriterionForms";
        }


    }


    [Serializable]
    class MyState
    {
        public Guid TypeGuid { get; set; }
    }

    class MyClass:IViewDataContainer 
    {
        public MyClass()
        {
            ViewData = new ViewDataDictionary();
            
        }

        public ViewDataDictionary ViewData { get; set; }
    }
}

﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.17929
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Criterion.Properties {
    using System;
    
    
    /// <summary>
    ///   Класс ресурса со строгой типизацией для поиска локализованных строк и т.д.
    /// </summary>
    // Этот класс создан автоматически классом StronglyTypedResourceBuilder
    // с помощью такого средства, как ResGen или Visual Studio.
    // Чтобы добавить или удалить член, измените файл .ResX и снова запустите ResGen
    // с параметром /str или перестройте свой проект VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Возвращает кэшированный экземпляр ResourceManager, использованный этим классом.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Criterion.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Перезаписывает свойство CurrentUICulture текущего потока для всех
        ///   обращений к ресурсу с помощью этого класса ресурса со строгой типизацией.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;div  id=&quot;#name#--#id#&quot; class=&quot;item-criterion&quot;&gt;
        ///    &lt;div  class=&quot;body-head-left&quot;&gt;#1#&lt;/div&gt;
        ///    &lt;div  class=&quot;body-head-center&quot;&gt;#2#&lt;/div&gt;
        ///    &lt;div  class=&quot;body-head-right&quot;&gt;#3#&lt;/div&gt;
        ///    &lt;div  class=&quot;body-base&quot;&gt;&lt;/div&gt;
        ///    &lt;div  class=&quot;bettbeen-left&quot;&gt;#dd#&lt;/div&gt;
        ///    &lt;div  class=&quot;bettbeen-center&quot;&gt;#tt1#&lt;/div&gt;
        ///    &lt;div  class=&quot;bettbeen-right&quot;&gt;-#tt2#&lt;/div&gt;
        ///    &lt;div  class=&quot;body-base&quot;&gt;
        ///    
        ///    &lt;/div&gt;
        ///&lt;/div&gt;
        ///.
        /// </summary>
        internal static string BetweenPart {
            get {
                return ResourceManager.GetString("BetweenPart", resourceCulture);
            }
        }
        
       
        internal static string ButtonPart {
            get {
                return ResourceManager.GetString("ButtonPart", resourceCulture);
            }
        }
        
       
        internal static string CustomPart {
            get {
                return ResourceManager.GetString("CustomPart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Поиск локализованного ресурса типа System.Drawing.Bitmap.
        /// </summary>
        internal static System.Drawing.Bitmap help {
            get {
                object obj = ResourceManager.GetObject("help", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;div id=&quot;#name#&quot; class=&quot;item-criterion&quot;&gt;
        ///&lt;div class=&quot;body-head-left&quot;&gt;#1#&lt;/div&gt;
        ///    &lt;div class=&quot;body-head-center&quot;&gt;#2#&lt;/div&gt;
        ///    &lt;div class=&quot;body-head-right&quot;&gt;#3#&lt;/div&gt;
        ///    &lt;div id=&quot;#name#_#id#&quot; class=&quot;body-base&quot;&gt;#4#
        ///    &lt;input id=&quot;#id#-hiion&quot; core=&quot;1&quot; name=&quot;#id#-res&quot; type=&quot;hidden&quot; value=&quot;#val#&quot; /&gt;
        ///    &lt;/div&gt;
        ///&lt;/div&gt;&gt;.
        /// </summary>
        internal static string MultiPart {
            get {
                return ResourceManager.GetString("MultiPart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;div id=&quot;#name#_#id#&quot; class=&quot;item-criterion&quot;&gt;
        ///&lt;div class=&quot;body-head-left&quot;&gt;#1#&lt;/div&gt;
        ///    &lt;div class=&quot;body-head-center&quot;&gt;#2#&lt;/div&gt;
        ///    &lt;div class=&quot;body-head-right&quot;&gt;#3#&lt;/div&gt;
        ///    &lt;div id=&quot;#name#-#id#&quot; class=&quot;body-base&quot;&gt;#4#
        ///  &lt;!--  &lt;input id=&quot;#id#-hiion&quot; name=&quot;#id#&quot; type=&quot;hidden&quot; value=&quot;#val#&quot; /&gt;--&gt;
        ///    &lt;/div&gt;
        ///&lt;/div&gt;.
        /// </summary>
        internal static string newBase {
            get {
                return ResourceManager.GetString("newBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на $(function () {
        ///    $(&quot;##id#_criterion&quot;).slider({ min: #min#, max: #max#, step: #step#, values: [#min1#, #max1#], slide:function (event, ui) {
        ///       // alert(ui.values.toString().split(&apos;,&apos;)[0]);
        ///        $(&apos;#ot-#id#&apos;).val(ui.values.toString().split(&apos;,&apos;)[0]);
        ///        $(&apos;#do-#id#&apos;).val(ui.values.toString().split(&apos;,&apos;)[1]);
        ///    }, change:function (event, ui) {
        ///       // $(&apos;##id#-hiion&apos;).val(ui.values.toString());
        ///        pizdaticus2();
        ///    } });
        ///}).
        /// </summary>
        internal static string Slider {
            get {
                return ResourceManager.GetString("Slider", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Ищет локализованную строку, похожую на &lt;div id=&quot;slider-#id#&quot; class=&quot;item-criterion&quot;&gt;
        ///&lt;table style=&quot;width: 100%;&quot; &gt;
        ///    &lt;tr&gt;
        ///        &lt;td colspan=&quot;2&quot; &gt;&lt;div class=&quot;i-ion&quot;&gt;#name#&lt;/div&gt;&lt;/td&gt;
        ///    &lt;/tr&gt;
        ///    &lt;tr&gt;
        ///        &lt;td style=&quot;width: 50%;&quot; &gt;&lt;label for=&quot;ot-#id#&quot;&gt;От&lt;/label&gt;
        ///            &lt;input type=&quot;text&quot; ssd=&quot;1&quot; name=&quot;#id#1&quot; id=&quot;ot-#id#&quot; value=&quot;#min#&quot;  style=&quot;width: 50px&quot;/&gt;&lt;/td&gt;
        ///        &lt;td style=&quot;width: 50%;&quot;  align=&quot;right&quot;&gt;&lt;label for=&quot;do-#id#&quot;&gt;До&lt;/label&gt;
        ///            &lt;input type=&quot;text&quot; ssd=&quot;1&quot;  name=&quot;#id#2&quot; id=&quot;do-#id#&quot; value=&quot;#max#&quot; style= [остаток строки не уместился]&quot;;.
        /// </summary>
        internal static string Slider1 {
            get {
                return ResourceManager.GetString("Slider1", resourceCulture);
            }
        }
    }
}
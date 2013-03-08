using System;

namespace Criterion.Attributes
{
    /// <summary>
    /// Base attribut
    /// </summary>
     [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class BaseAttribute:Attribute
    {
         /// <summary>
        /// Name of the group
         /// </summary>
        public string NameGroup { get; set; }
         /// <summary>
        /// Index for sorting group
         /// </summary>
        public int IndexSortable { get; set; }
         /// <summary>
        /// Title block
         /// </summary>
        public string DisplayName { get; set; }
         /// <summary>
        /// Identifier (unique) panel if it will not put the name of the PROPER
         /// </summary>
        public string Id { get; set; }
         /// <summary>
        /// Reference identifier help record, if you put 0 (zero) means pictures will not be visible
         /// </summary>
        public int IdHelp { get; set; }

        internal Type TypeProperty { get; set; }
        /// <summary>
        /// Name field in the database for query autobuilding
        /// </summary>
        public string NameColumnFomBase { get; set; }
      
    }
    /// <summary>
     ///Attribute to create a slider, select the type of the as price range
    /// </summary>
    public class CriterionSliderAttribute:BaseAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }

    /// <summary>
    ///Creating a sheet list
    /// </summary>
    public class CriterionListBoxAttribute : BaseAttribute
    {
        /// <summary>
        /// Type that will create a list, should inherit interface ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
      
    }
    /// <summary>
    /// ListBox Attribute
    /// </summary>
    public class CriterionListBoxMultipleAttribute : BaseAttribute
    {
        /// <summary>
        /// Type that will create a list, should inherit interface ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
        /// <summary>
        /// Высота листа пример: 100px
        /// </summary>
        public string Height { get; set; }

    }
    /// <summary>
    /// DropDouwnList Attribute
    /// </summary>
    public class CriterionDropDouwnAttribute : BaseAttribute
    {
        /// <summary>
        /// Type that will create a list, should inherit interface ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }
    /// <summary>
    /// DropDouwn as bool 
    /// </summary>
    public class CriterionBoolAsDropDouwnAttribute : BaseAttribute
    {
        /// <summary>
        /// Display as true
        /// </summary>
        public string DisplayTextAsTrue { get; set; }
        /// <summary>
        /// Display as false
        /// </summary>
        public string DisplayTextAsFalse { get; set; }
    }
    /// <summary>
    /// RadioButtonList
    /// </summary>
    public class CriterionRadioButtonListAttribute : BaseAttribute
    {
        /// <summary>
        /// Type that will create a list, should inherit interface ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }

    /// <summary>
    /// CheckBoxListList
    /// </summary>
    public class CriterionCheckBoxListListAttribute : BaseAttribute
    {
        /// <summary>
        /// Type that will create a list, should inherit interface ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }
    /// <summary>
    /// CheckBox One
    /// </summary>
    public class CriterionBoolAsCheckBoxAttribute : BaseAttribute
    {
        
    }
    /// <summary>
    /// BetweenDateTime
    /// </summary>
    public class CriterionBetweenDateTimeAttribute : BaseAttribute
    {  /// <summary>
        /// Text for a range of
        /// </summary>
        public string TextBetween { get; set; }
        /// <summary>
        /// Text for one of
        /// </summary>
        public string TextOne { get; set; }
    }
    /// <summary>
    /// SimpleBettwen
    /// </summary>
    public class CriterionSimpleBettwenAttribute : BaseAttribute
    {
        /// <summary>
        /// Text for a range of
        /// </summary>
        public string TextBetween { get; set; }
        /// <summary>
        /// Text for one of
        /// </summary>
        public string TextOne { get; set; }
    }
    /// <summary>
    /// ManagerMeny
    /// </summary>
    public class CriterionManagementAttribute : BaseAttribute
    {
        /// <summary>
        /// is usage button
        /// </summary>
        public bool UsingButton { get; set; }
        /// <summary>
        /// Text as ClearAll
        /// </summary>
        public string TextClearAll { get; private set; }
        /// <summary>
        /// Текст кнопки
        /// </summary>
        public string TextButton { get; set; }
     
        /// <summary>
        /// ctor.
        /// </summary>
        public CriterionManagementAttribute()
        {
            TextClearAll = "clear all.";
            TextButton = "Go.";
        }
    }
    /// <summary>
    /// Castom item
    /// </summary>
    public class CriterionCustomAttribute : BaseAttribute
    {
        /// <summary>
        /// type validate
        /// </summary>
        public ValidationType ValidationType { get; set; }
        /// <summary>
        /// url path for castom control
        /// </summary>
        public  string UrlCustomControl { get; set; }
    }

    

  
   
   
}
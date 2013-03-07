using System;

namespace Criterion.Attributes
{
    /// <summary>
    /// Базовый атрибут абстрактный
    /// </summary>
     [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class BaseAttribute:Attribute
    {
         /// <summary>
         /// Имя для группировки
         /// </summary>
        public string NameGroup { get; set; }
         /// <summary>
         /// Индекс для сортировки в группе
         /// </summary>
        public int IndexSortable { get; set; }
         /// <summary>
         /// Название блока
         /// </summary>
        public string DisplayName { get; set; }
         /// <summary>
         /// Индентификатор ( уникальный) панели, если его не ставить возьмет название пропери
         /// </summary>
        public string Id { get; set; }
         /// <summary>
         /// Индентификатор справочной записи, если вы поставите 0( ноль) картинки  помощи видно не будет
         /// </summary>
        public int IdHelp { get; set; }

        internal Type TypeProperty { get; set; }
        /// <summary>
        /// Название поля в база для автопостроения запроса
        /// </summary>
        public string NameColumnFomBase { get; set; }
      
    }
    /// <summary>
    /// Атрибут для создания слайдера, по типу выбора диапазона цен
    /// </summary>
    public class CriterionSliderAttribute:BaseAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }

    /// <summary>
    /// Создание листа списка
    /// </summary>
    public class CriterionListBoxAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип который будет создавать писок, должен наследовать интерфейс ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
      
    }
    /// <summary>
    /// Создание листа списка со множественным выбором
    /// </summary>
    public class CriterionListBoxMultipleAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип который будет создавать cписок, должен наследовать интерфейс ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
        /// <summary>
        /// Высота листа пример: 100px
        /// </summary>
        public string Height { get; set; }

    }
    /// <summary>
    /// Тип который будет создавать выпадающий cписок
    /// </summary>
    public class CriterionDropDouwnAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип который будет создавать писок, должен наследовать интерфейс ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }
    /// <summary>
    /// Тип который будет создавать выпадающий cписок( по типу да нет.)
    /// </summary>
    public class CriterionBoolAsDropDouwnAttribute : BaseAttribute
    {
        /// <summary>
        /// Что показывать при верно
        /// </summary>
        public string DisplayTextAsTrue { get; set; }
        /// <summary>
        /// Что показывать при не верно
        /// </summary>
        public string DisplayTextAsFalse { get; set; }
    }
    /// <summary>
    /// Тип который будет создавать радиокнопок ( с возможностью выбора одной)
    /// </summary>
    public class CriterionRadioButtonListAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип который будет создавать писок, должен наследовать интерфейс ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }

    /// <summary>
    /// Тип который будет создавать cписок чеков ( с множественным выбором, должен наследовать интерфейс ICriterion
    /// </summary>
    public class CriterionCheckBoxListListAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип который будет создавать писок, должен наследовать интерфейс ICriterion
        /// </summary>
        public Type TypeGeneratorListItem { get; set; }
    }
    /// <summary>
    /// Создание листа списка с одним чеком
    /// </summary>
    public class CriterionBoolAsCheckBoxAttribute : BaseAttribute
    {
        
    }
    /// <summary>
    /// Создание композиции между, или равно для дат
    /// </summary>
    public class CriterionBetweenDateTimeAttribute : BaseAttribute
    {  /// <summary>
        /// Текст для диапазона
        /// </summary>
        public string TextBetween { get; set; }
        /// <summary>
        /// Текст для  одиночного сравнения
        /// </summary>
        public string TextOne { get; set; }
    }
    /// <summary>
    /// Создание композиции между, или равно для простых типов
    /// </summary>
    public class CriterionSimpleBettwenAttribute : BaseAttribute
    {
        /// <summary>
        /// Текст для диапазона
        /// </summary>
        public string TextBetween { get; set; }
        /// <summary>
        /// Текст для  одиночного сравнения
        /// </summary>
        public string TextOne { get; set; }
    }
    /// <summary>
    /// Создания панели управления меню
    /// </summary>
    public class CriterionManagementAttribute : BaseAttribute
    {
        /// <summary>
        /// Использовать ли кнопу
        /// </summary>
        public bool UsingButton { get; set; }
        /// <summary>
        /// Текст ссылк -Очистить все
        /// </summary>
        public string TextClearAll { get; private set; }
        /// <summary>
        /// Текст кнопки
        /// </summary>
        public string TextButton { get; set; }
        /// <summary>
        /// Менеджер меню
        /// </summary>
        public CriterionManagementAttribute()
        {
            TextClearAll = "clear all.";
            TextButton = "Go.";
        }
    }
    /// <summary>
    /// Создание самодельной пользовательской панели
    /// </summary>
    public class CriterionCustomAttribute : BaseAttribute
    {
        /// <summary>
        /// Тип валидации
        /// </summary>
        public ValidationType ValidationType { get; set; }
        /// <summary>
        /// Путь к контролу ( пользовательскому)
        /// </summary>
        public  string UrlCustomControl { get; set; }
    }

    //public class CriterionLinkAttribute : BaseAttribute
    //{

    //}

  
   
   
}
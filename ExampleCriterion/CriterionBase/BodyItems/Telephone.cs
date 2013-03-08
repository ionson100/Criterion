using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Criterion;
using Criterion.Attributes;

namespace ExampleCriterion.CriterionBase.BodyItems
{
    
    
    public class Telephones : Body, ICriterion
    {
        const string Res = "Basic parameters";

        public int IdTelephones { get; set; }

         public int IdBody { get; set; }

        [CriterionDropDouwn(NameGroup = Res,
            DisplayName = "Manufacturer",
            Id = "producer", 
            IndexSortable = 3,
            TypeGeneratorListItem = typeof(Producer))]
         public int IdProducer { get; set; }



        [CriterionRadioButtonList(IdHelp = 34, 
            NameGroup = Res,
            DisplayName = "Manufacturer1",
            Id = "producer33",
            IndexSortable = 6,
            TypeGeneratorListItem = typeof(Producer))]
        public int Ptodacshen { get { return IdProducer; } set { IdProducer = value; } }

        [CriterionCheckBoxListList(NameGroup = Res,
            DisplayName = "ManufacturerList",
            Id = "producerwwww", 
            IndexSortable = 7, 
            TypeGeneratorListItem = typeof(Telephones ))]
        public int Ptodacshen22 { get { return IdProducer; } set { IdProducer = value; } }

         [CriterionListBox(IdHelp = 2333,
             DisplayName = "Manufacturer4",
             Id = "producer2",
             IndexSortable = 2,
             TypeGeneratorListItem = typeof(Producer))]
        public int IdProducer2 { get { return IdProducer; }set { IdProducer = value; }}

        [CriterionBoolAsDropDouwn(
            IdHelp = 23,
            NameGroup = Res,
            DisplayName = "Availability Wi-Fi",
            IndexSortable = 4, Id = "iswifi",
            DisplayTextAsFalse = "Yes",
            DisplayTextAsTrue = "No")]
         public bool IsWiFi { get; set; }

        [CriterionBoolAsCheckBox(NameGroup = Res,
            DisplayName = "Is Wi-Fi",
            IndexSortable = 5,
            Id = "qiswifi2")]
        public bool IsWiFi2 { get { return IsWiFi; } set { IsWiFi = value; } }
      

        [CriterionListBoxMultiple(
            DisplayName = "Manufacturer34",
            Id = "producerwwww2",
            IndexSortable = 8, 
            TypeGeneratorListItem = typeof(Producer),
            Height = "150px")]
        public int Ptodacshen22Www { get { return IdProducer; } set { IdProducer = value; } }













        [CriterionBetweenDateTime(TextBetween = "between", TextOne = "equally", IndexSortable = 10)]
          public DateTime DateTime { get; set; }

        [CriterionSimpleBettwen(NameGroup = "Additionally", TextBetween = "between", TextOne = "equally", IndexSortable = 10)]
        public int DateInt { get; set; }
        [CriterionSimpleBettwen(TextBetween = "between", TextOne = "equally", IndexSortable = 10)]
        public int DateInt22 { get; set; }





         
          public void GetMinMaxForSlider(out int max, out int min, string fieldName, object helper)
          {
              min = 100;
              max = 300;
          }

          public IEnumerable<ListItem> GetListItems<T>(T t, object helper) where T : Type
          {
              return new List<ListItem>
                            {
                                new ListItem("Has sound", "1"),
                                new ListItem("It does not sound", "2"),
                                new ListItem("Has video", "3"),
                                new ListItem("It does not video", "4")
                            };
          }
    }














    public class Producer :ICriterion
    {
      

        public void GetMinMaxForSlider(out int max, out int min, string fieldName, object helper)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ListItem> GetListItems<T>(T t, object helper) where T : Type
        {
            return new List<ListItem>
                          {
                              new ListItem("Sony", "1"),
                              new ListItem("Samsung", "2"),
                              new ListItem("Toshiba", "3"),
                              new ListItem("Ford", "4")
                          };
        }
    }
}
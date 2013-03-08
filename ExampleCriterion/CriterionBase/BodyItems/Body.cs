using System;
using Criterion;
using Criterion.Attributes;
using ExampleCriterion.CriterionBase.Model;

namespace ExampleCriterion.CriterionBase.BodyItems
{
    public class Body 
    {
        public int Id { get; set; }

         public string Description { get; set; }

         public string Name { get; set; }

         public string BigName { get; set; }

         [CriterionSlider(DisplayName = "Price",Id = "price",IndexSortable = 1,TypeGeneratorListItem = typeof(Telephones))]///////////////
         public decimal Price { get; set; }

         public float   DeltaPrice { get; set; }

         public int GroupMeny { get; set; }

         public UInt64 IsShow { get; set; }

          public bool IsShowShop
          {
              get
              {
                  return IsShow != 0;
              }
          }

         public int Rating { get; set; }

         [CriterionListBox(Id = "sdasd",IndexSortable = 100,TypeGeneratorListItem = typeof(Producer))]
          public Test Test { get; set; }

         [CriterionCustom(Id = "ion312312",IdHelp = 22,IndexSortable = 222,UrlCustomControl = "~/CriterionBase/CustomItems/TestCustom.ascx",ValidationType = ValidationType.Multi)]/////////////////
     public int Colorww { get; set; }


          [CriterionManagement(IndexSortable = 100, UsingButton = true, TextButton = "Find All.")]
          public string Dd { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace ExampleCriterion.CriterionBase.BodyItems
{
    public class ListItems 
    {
        public IEnumerable<ListItem> GetItems()
        {
            var listItems=new List<ListItem>
                              {
                                  new ListItem {Text = "2", Value = "2"},
                                  new ListItem {Text = "5", Value = "5"},
                                  new ListItem {Text = "10", Value = "10"},
                                  new ListItem {Text = "All", Value = "10000000"}
                              };

            return listItems.ToList();
        }
    }
}
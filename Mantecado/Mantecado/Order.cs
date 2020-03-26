using System;
using System.Collections.Generic;
using System.Text;

namespace Mantecado
{
    class Item
    {
        public string itemName;
        public double itemPrice;
        public List<AddOns> ItemAddons = new List<AddOns>();
        public System.Windows.Controls.Border B;

    }

    class AddOns
    {
        public string addonName;
        public double addonPrice;
    }

    class Order
    {
        public List<Item> OrderItems = new List<Item>();
       
        double TotalPrice = 0;

        public void AddItem(Item item)
        {

            OrderItems.Add(item);
            TotalPrice += item.itemPrice;

        }

        public void RemoveItem(Item item)
        {
            OrderItems.Remove(item);
            TotalPrice -= item.itemPrice;
        }

        public void AddAddon(Item item, AddOns add)
        {
            item.ItemAddons.Add(add);
            TotalPrice += add.addonPrice;
        }

        public double GetTotalPrice()
        {
            return Math.Round(TotalPrice, 2);
        }

     

        public override string ToString()
        {
            string fullOrder = "";
            foreach(Item i in OrderItems)
            {
                fullOrder += i.itemName + "\t\t$" + i.itemPrice + '\n';
                foreach (AddOns a in i.ItemAddons)
                {
                    fullOrder += "\t" + a.addonName + "\t\t$" + a.addonPrice + '\n';
                }
                fullOrder += '\n';
            }
            fullOrder += "Total Price: " + "$" + Math.Round(this.TotalPrice, 2);
            return fullOrder;
        }
    }
}

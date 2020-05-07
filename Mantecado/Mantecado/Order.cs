using System;
using System.Collections.Generic;
using System.Text;

namespace Mantecado
{
    class Item
    {
        
        public string itemName;
        public double itemPrice;
        public string itemCategory;
        public int itemID;
        public List<AddOns> ItemAddons = new List<AddOns>();
        public System.Windows.Controls.Border B;

    }

    class AddOns
    {
        public string addonName;
        public double addonPrice;
        public string addonCategory;
    }

    class Order
    {
        const double TAX_RATE = 0.089;
        double AddedTax = 0;
        double TotalPrice = 0;
        public List<Item> OrderItems = new List<Item>();
        int item_amount = 0;
        double SubTotal = 0;

        public void AddItem(Item item)
        {

            OrderItems.Add(item);
            SubTotal += item.itemPrice;
            AddedTax += item.itemPrice * TAX_RATE;
            TotalPrice = SubTotal + AddedTax;
            item_amount++;
        }

        public void RemoveItem(Item item)
        {
            OrderItems.Remove(item);
            SubTotal -= item.itemPrice;
            AddedTax -= item.itemPrice * TAX_RATE;
            TotalPrice = SubTotal + AddedTax;
            item_amount--;
        }

        public void AddAddon(Item item, AddOns add)
        {
            item.ItemAddons.Add(add);
            SubTotal += add.addonPrice;
            AddedTax += add.addonPrice * TAX_RATE;
            TotalPrice = SubTotal + AddedTax;

        }

        public void RemoveAddon(Item item, string text)
        {   
            
            foreach(AddOns a in item.ItemAddons)
            {
                if (a.addonName == text)
                {
                    
                    SubTotal -= a.addonPrice;
                    AddedTax -= a.addonPrice * TAX_RATE;
                    TotalPrice = SubTotal + AddedTax;
                    item.ItemAddons.Remove(a);
                    break;
                }
            }
    
        }


        public string GetTotalPrice()
        {
            return (Math.Round(TotalPrice, 2)).ToString("0.00");
        }
        public int GetItemAmount()
        {
            return item_amount;
        }
        public string GetTax()
        {
            return (Math.Round(AddedTax, 2)).ToString("0.00");
        }

        public string GetSubtotal()
        {
            return (Math.Round(SubTotal, 2)).ToString("0.00");
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
            fullOrder += "Total Price: " + "$" + ((Math.Round(this.SubTotal, 2))).ToString("0.00");

            return String.Format("{0:0.00}", fullOrder);
        }
    }
}

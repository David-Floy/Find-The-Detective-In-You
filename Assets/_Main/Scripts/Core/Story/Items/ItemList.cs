
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
   private static Item Item1 = new Item("Lupe", false);
   private static Item Item2 = new Item("TaschenMesser", true);
   private static Item Item3 = new Item("TaschenLampe", false);
   private static Item Item4 = new Item("Hut", true);
   private static Item Item5 = new Item("Handschuhe", false);
   private static Item Item6 = new Item("Kamera", false);
   private static Item Item7 = new Item("Fingerabdruckset", true);

   private static List<Item> Items = new List<Item>()
   {
      Item1,
      Item2,
      Item3,
      Item4,
      Item5,
      Item6,
      Item7
   };

   private static Item IterateItemList(string name)
   {
      foreach (Item item in Items)
      {
         if (name == item.name)
         {
            return item;
         }
      }
      return null;
   }

   public static void SetItemState(string name, bool state) => IterateItemList(name).status = state;
   

   public static bool GetItemState(string name) =>  IterateItemList(name).status;

   public static bool AllItemsTrue()
   {
      if (GetItemState("Lupe")&& GetItemState("TaschenMesser")&& GetItemState("TaschenLampe")&& GetItemState("Hut")&& GetItemState("Handschuhe")&& GetItemState("Kamera")&& GetItemState("Fingerabdruckset"))
      {
         return true;
      }
      return false;
   }
}

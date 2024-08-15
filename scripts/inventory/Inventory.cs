using Godot;
using System;
using System.Collections.Generic;

namespace inventory;
public class Inventory
{
    public List<InventorySlot> Slots { get; private set; }

    public Inventory(int slotCount)
    {
        Slots = new List<InventorySlot>(slotCount);
        for (int i = 0; i < slotCount; i++)
        {
            Slots.Add(new InventorySlot(null, 0));
        }
    }

    public bool AddItem(Item item, int quantity)
    {
        // Сначала пытаемся добавить предметы в существующие стеки
        foreach (var slot in Slots)
        {
            if (slot.CanStack(item))
            {
                int remainingSpace = item.MaxStack - slot.Quantity;
                int amountToAdd = Math.Min(remainingSpace, quantity);
                slot.AddItem(item, amountToAdd);
                quantity -= amountToAdd;
                if (quantity <= 0)
                {
                    return true;
                }
            }
        }

        // Если остались предметы, пытаемся найти пустые слоты
        foreach (var slot in Slots)
        {
            if (slot.IsEmpty)
            {
                slot.AddItem(item, quantity);
                return true;
            }
        }

        // Если не смогли добавить все предметы, возвращаем false
        return false;
    }

    public void RemoveItem(Item item, int quantity)
    {
        foreach (var slot in Slots)
        {
            if (slot.Item != null && slot.Item.Name == item.Name)
            {
                int amountToRemove = Math.Min(slot.Quantity, quantity);
                slot.RemoveItem(amountToRemove);
                quantity -= amountToRemove;
                if (quantity <= 0)
                {
                    return;
                }
            }
        }
    }
}



public class Item
{
    public string Name { get; set; }
    public int MaxStack { get; set; }
    public Texture2D Icon { get; set; } // Иконка предмета

    public Item(string name, int maxStack, Texture2D icon)
    {
        Name = name;
        MaxStack = maxStack;
        Icon = icon;
    }
}



public class InventorySlot
{
    public Item Item { get; private set; }
    public int Quantity { get; private set; }

    public InventorySlot(Item item, int quantity)
    {
        Item = item;
        Quantity = quantity;
    }

    public bool IsEmpty => Item == null;

    public bool CanStack(Item item)
    {
        return Item != null && Item.Name == item.Name && Quantity < Item.MaxStack;
    }

    public void AddItem(Item item, int quantity)
    {
        if (IsEmpty)
        {
            Item = item;
            Quantity = quantity;
        }
        else if (CanStack(item))
        {
            Quantity += quantity;
        }
    }

    public void RemoveItem(int quantity)
    {
        Quantity -= quantity;
        if (Quantity <= 0)
        {
            Item = null;
            Quantity = 0;
        }
    }
}
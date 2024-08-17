using Godot;
using System;
using System.Collections.Generic;

namespace inventory;
/// <summary>
/// Тут осуществляется хранение всех вещей в инвентаре игрока на постоянке
/// Также обработка событий, таких как удаление вещи из инвентаря/добавление в инвентарь
/// </summary>



public partial class InventoryManager: Node
{
    private GameProfileManager profileManager;
    private List<Control> inventorySlots = new List<Control>();

    private Item item;

    public override void _Ready()
    {
        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");
        // Подписываемся на событие
        DropItem.ItemPickedUp += AddItemToInventory;

        //указываем ссылку на слоты в инвентаре
        var gridContainer = GetNode<GridContainer>($"{profileManager.inventoryGUIPath}/NinePatchRect/GridContainer");

        foreach (Control slot in gridContainer.GetChildren()) // добавляем в массив слотов слоты из сцены инвентаря
        {
            inventorySlots.Add(slot);
        }
    }

    public void AddItemToInventory(string ItemName, int MaxStack, Texture ItemTexture, int ItemValue, int ItemID)
    {
        /// функция вызываемая при добавлении предмета в инвентарь
        /// 
        //Texture2D itemTextureIcon2D = ItemTexture as Texture2D;

        // Создаем новый объект Item, передавая необходимые аргументы в конструктор
        item = new Item(ItemName, MaxStack, ItemTexture as Texture2D, ItemValue, ItemID);

        //item = new Item
        //{
        //    Name = ItemName,
        //    MaxStack = MaxStack,
        //    Icon = ItemTexture as Texture2D

        //};

        int availableSlot = FindAvailableSlot(item); // нахождение одинакового или свободного слота
        //int availableSlot = 1;

        if (availableSlot >= 0)
        {
            // Обновляем слот с новым предметом
            UpdateSlot(availableSlot, item.Icon, item.Value);
        }

        GD.Print($"Текстура{ItemTexture} в инвентаре");
        GD.Print($"Предмет {ItemName} в инвентаре");
        
    }





    // Метод для обновления конкретного слота инвентаря
    public void UpdateSlot(int slotIndex, Texture itemTexture, int itemCount)
    {
        Texture2D itemTextureIcon2D = itemTexture as Texture2D; // необходимое преобразование типов для текстуры
        // Обновляем текстуру предмета в слоте
        var itemNode = inventorySlots[slotIndex].GetNode<Sprite2D>("CenterContainer/Panel/Item");
        itemNode.Texture = itemTextureIcon2D;

        // Обновляем количество предметов
        var itemCountNode = inventorySlots[slotIndex].GetNode<Label>("CenterContainer/Panel/ItemCount");

        itemCount += itemCountNode.Text.ToInt(); // добавление предыдущего значения

        itemCountNode.Text = itemCount.ToString();

        UpdateVisualLabel(slotIndex);
    }

    //public void RemoveItemFromInventory(Item item, int quantity)
    //{
    //    PlayerInventory.RemoveItem(item, quantity);
    //}



    // Пример поиска свободного слота
    private int FindAvailableSlot(Item item)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            
            // Логика поиска свободного слота или слота для стакающегося предмета
            var itemNode = inventorySlots[i].GetNode<Sprite2D>("CenterContainer/Panel/Item");
            var itemCountNode = inventorySlots[i].GetNode<Label>("CenterContainer/Panel/ItemCount");
            var itemCount = Convert.ToInt32(itemCountNode.Text);

            if (itemNode.Texture == null || itemNode.Texture == item.Icon &&  (itemCount) != item.MaxStack) // Свободный слот или одинаковый предмет и не максимум предмета в стаке
            {
                return i;
            }

            
        }
        return -1; // Если нет доступных слотов
    }




    public void UpdateVisualLabel(int slotIndex)
    {
        /// в зависимости от присутствия или отсутствия предмета в ячейке, скрывает ее Label
        var itemCountNode = inventorySlots[slotIndex].GetNode<Label>("CenterContainer/Panel/ItemCount");
        var slotFrame = inventorySlots[slotIndex].GetNode<Sprite2D>("FrameSelector"); // переключение фрейма в ячейке

        if (itemCountNode.Text != "0") // если слот не пустой
        {

            itemCountNode.Visible = true;
            slotFrame.Frame = 0;

        }

        else
        {
            itemCountNode.Visible = false;
            slotFrame.Frame = 1;
        }


                
    }
}

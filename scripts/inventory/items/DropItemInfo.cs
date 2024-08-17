using Godot;
using System;
using System.Collections.Generic;

namespace inventory;

/// <summary>
/// базовый класс, в котором расписаны все свойства предметов, которые дропаются с объектов
/// например, сцена предмета дропа, шанс дропа, количество предметов
/// </summary>
public class DropItemInfo
{
    public PackedScene DropScene { get; set; }
    public float DropChance { get; set; }
    public int MaxCount { get; set; }
   

    public DropItemInfo(PackedScene dropScene, float dropChance, int maxCount)
    {
        DropScene = dropScene;
        DropChance = dropChance;
        MaxCount = maxCount;
       
    }
}

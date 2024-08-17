using Godot;
using inventory;
using System;
using System.Collections.Generic;

public partial class Tree : Node2D
{
    /// <summary>
    /// класс объекта дерево, сцена объекта дропа итема, шанс дропа предметов
    /// </summary>

    [Export]
    public PackedScene WoodScene;
    

    [Export]
    public PackedScene AppleScene;
    

    private bool isChopped = false;
    private DropSystem dropSystem;

    private List<DropItemInfo> dropsItems;

    // массив со сценами дропа
    public PackedScene[] ArrDropItemScenes;
    // массив с шансами на дроп предметов
    public float[] ArrDropItemChances;

    private AnimatedSprite2D _animatedSprite_1;

    public override void _Ready()
    {
        _animatedSprite_1 = GetNode<AnimatedSprite2D>("TreeAnimation");
        var area = GetNode<Area2D>("ChopZone");
        area.BodyEntered += OnTreeBodyEntered;
        _animatedSprite_1.AnimationFinished += OnAnimationFinished;


        dropsItems = new List<DropItemInfo>
        {
            /// хар-ки дропа предметов, сцена предмета, шанс дропа, макс кол-во
            new DropItemInfo(WoodScene, 1f,  3), // 50% шанс дропа,  3 штуки дерева
            new DropItemInfo(AppleScene, 1f,  2), 
           
        };



        dropSystem = new DropSystem();
        AddChild(dropSystem);
    }

    private void OnTreeBodyEntered(Node body)
    {
        if (body.Name == "Player" && !isChopped)
        {
            GD.Print("OnTreeBodyEntered");
            ChopTree();
        }
    }

    private void ChopTree()
    {
        GD.Print("Срубил дерево");
        _animatedSprite_1.Animation = "falling";
        isChopped = true;
        //dropSystem.DropItems(DropItemScene, GlobalPosition, ItemCount, DropChance);
        //QueueFree();
    }


    private void OnAnimationFinished()
    {
        if (_animatedSprite_1.Animation == "falling" && isChopped)
        {
            dropSystem.DropItems(dropsItems, Position);

            CallDeferred("queue_free");
        }
    }
}

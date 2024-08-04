using Godot;
using System;


public partial class ParentInventoryGUI : Node2D, IBaseUI
{
    

    public void ShowWindow(Control UIScene, GameProfileManager profileManager)
    {
        GD.Print(profileManager.playerPos);


        Vector2 playerPosition = profileManager.playerPos;
        Vector2 inventorySize = UIScene.Size;
        Vector2 inventoryPosition = playerPosition - (inventorySize / 2);
        UIScene.Position = inventoryPosition;


        UIScene.Visible = true;
        profileManager.isInventoryOpen = true; // юишка активна


    }

    public void HideWindow(Control UIScene, GameProfileManager profileManager)
    {



        UIScene.Visible = false;
        profileManager.isInventoryOpen = false;

    }

    public void SendParamsToGameProfileManager()
    {


    }

}







public partial class InventoryGUI : Node
{

    private ParentInventoryGUI _inventoryGUI;
    private Control UIScene;
    
    private bool _uiIsVisible;

    private GameProfileManager profileManager;



    public override void _Ready()
	{
        UIScene = GetNode<Control>("InventoryGUI");
        UIScene.Visible = false;

        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");

        _inventoryGUI = new ParentInventoryGUI();
    }
















    public override void _Input(InputEvent @event)
    {
        // Проверка, что событие - это нажатие клавиши, и если кнопка взаимодействия была нажата
        if (@event.IsActionPressed("inventory"))
        {
            if (_uiIsVisible)
            {
                // Скрываем UI
                _inventoryGUI.HideWindow(UIScene, profileManager);
                _uiIsVisible = false;
            }
            else
            {
                if (profileManager.isUIActive == false)
                {
                    // Показываем UI
                    _inventoryGUI.ShowWindow(UIScene, profileManager);
                    _uiIsVisible = true;
                }
                    
                

            }
        }
    }






    public override void _Process(double delta)
    {

        

    }
}

using Godot;
using System;


public partial class ParentplayerHouseUI: Node2D,  IBaseUI
{
  


    public void ShowWindow(PanelContainer houseUIScene, GameProfileManager profileManager)
	{

        

        Vector2 offset = new Vector2(50, -100);
        

        
        var playerpos = profileManager.playerPos;

        

        GD.Print($"Позиция игрока: {playerpos}");
        houseUIScene.Position = playerpos + offset; ;
        houseUIScene.Visible = true;
        profileManager.isUIActive = true; // юишка активна
        //GD.Print($"Позиция интерфейса: {houseUIScene.Position}");

    }

    public void HideWindow(PanelContainer houseUIScene, GameProfileManager profileManager)
    {
        

        
        houseUIScene.Visible = false;
        profileManager.isUIActive = false;

    }

    public void SendParamsToGameProfileManager()
    {
        

    }
}




public partial class PlayerHouseUI : Node2D
{
    private ParentplayerHouseUI _playerHouseUIManager;
    private PanelContainer houseUIScene;
    private Vector2 playerpos; // неверное решение, нужна позиция игрока в момент активации интерфейса
    private bool _uiIsVisible;

    private GameProfileManager profileManager;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
        //objplayerHouseUI = GetNode("/root/Main/World/InteractableObjects/PlayerHouse/PlayerHouseUI") as playerHouseUI;
        houseUIScene = GetNode<PanelContainer>("PlayerHouseUI");
        houseUIScene.Visible = false;

        //PlayerMovement _playerMovement;


        //_playerMovement = GetNode<PlayerMovement>("/root/Main/World/Player");
        //playerpos = _playerMovement.Position;



        // Создаем и инициализируем PlayerHouseUIManager
        _playerHouseUIManager = new ParentplayerHouseUI();
        //objplayerHouseUI.houseUIScene.Visible = false;



    }


    public override void _Input(InputEvent @event)
    {
        // Проверка, что событие - это нажатие клавиши, и если кнопка взаимодействия была нажата
        if (@event.IsActionPressed("interaction") )
        {
            if (_uiIsVisible)
            {
                // Скрываем UI
                _playerHouseUIManager.HideWindow(houseUIScene, profileManager);
                _uiIsVisible = false;
            }
            else
            {
                if (profileManager.whatUIInArea == "PlayerHouse" & profileManager.isInventoryOpen == false)
                {
                    // Показываем UI
                    _playerHouseUIManager.ShowWindow(houseUIScene, profileManager);
                    _uiIsVisible = true;
                }
                
            }
        }
    }







    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{

        profileManager = GetNode<GameProfileManager>("/root/GameProfileManager");

    }
}




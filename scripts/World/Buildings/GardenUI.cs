using Godot;
using System;


public partial class ParentGardenUI : Node2D, IBaseUI
{

    public void ShowWindow(Control UIScene, GameProfileManager profileManager)
    {

        //Vector2 offset = new Vector2(50, -100);



        var playerpos = profileManager.playerPos;



        GD.Print($"Позиция игрока: {playerpos}");
        //UIScene.Position = playerpos;
        UIScene.Visible = true;
        profileManager.isUIActive = true; // юишка активна
        

    }

    public void HideWindow(Control UIScene, GameProfileManager profileManager)
    {



        UIScene.Visible = false;
        profileManager.isUIActive = false;

    }

    public void SendParamsToGameProfileManager()
    {


    }

}


public partial class GardenUI : Node2D
{
    private ParentGardenUI _gardenUI;
    private Control UIScene;
    private Vector2 playerpos; // неверное решение, нужна позиция игрока в момент активации интерфейса
    private bool _uiIsVisible;

    private GameProfileManager profileManager;
    

    public override void _Ready()
    {

        
        UIScene = GetNode<Control>("PlantUI");
        UIScene.Visible = false;

        



        
        _gardenUI = new ParentGardenUI();
        



    }


    public override void _Input(InputEvent @event)
    {
        // Проверка, что событие - это нажатие клавиши, и если кнопка взаимодействия была нажата
        if (@event.IsActionPressed("interaction"))
        {
            if (_uiIsVisible)
            {
                // Скрываем UI
                _gardenUI.HideWindow(UIScene, profileManager);
                _uiIsVisible = false;
            }
            else
            {
                if (profileManager.whatUIInArea == "Garden" & profileManager.isInventoryOpen == false)
                {
                    // Показываем UI
                    _gardenUI.ShowWindow(UIScene, profileManager);
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

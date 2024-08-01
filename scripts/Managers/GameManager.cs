using Godot;
using System;


public interface IGameManager
{
	void Player();
}


public class PlayerInst : IGameManager
{
	public void Player()
	{

		GD.Print("ABOBA");
	}

}



public partial class GameManager : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        
        
        PlayerInst player = new PlayerInst();
		player.Player();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}



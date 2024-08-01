using Godot;
using System;

public partial class InteractableObjects : TileMap
{
	// Called when the node enters the scene tree for the first time.

	public int layers = 4;
	
	public override void _Ready()
	{
		YSortEnabled = true;
		

		for (int i = 0; i <= layers; i++)
		{
			
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}

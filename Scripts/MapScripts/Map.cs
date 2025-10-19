using Godot;
using System;

public partial class Map : TileMapLayer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public int GetTileDomain(Vector2I mapCoordinates)
	{
		if(GetCellSourceId(mapCoordinates) == -1)
		{
			return -1;
		}
		else
		{
			switch(GetCellAtlasCoords(mapCoordinates).Y % 129)
			{
				case 0:
                    return 0;
				case 1:
					return 1;
				case 2:
                    return 1;
			}
        }
		return -1;
	}
}

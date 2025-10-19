using Godot;
using System;

public partial class Unit : Sprite2D
{
	// Statistics that all units have.
	protected int m_movementPoints = 0;
	protected int m_movementPointsRemaining = 0;
	protected int m_ValidDomain;

	protected Map r_Map;
	protected Vector2I m_CurrentMapPosition;

	// Statistics that a transport might have.
	protected int m_CargoSlots = 0;
	protected int m_CargoSlotsRemaining = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		r_Map = (Map)GetParent<UnitManager>().GetParent<TileMapLayer>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public void Order(Order order)
	{
		switch (order.m_Type)
		{
			case "MOVE":
				if(IsValidMove(order.m_Map_Coordinates))
				{
                    setMapPosition(order.m_Map_Coordinates);
					m_movementPointsRemaining--;
                }
					break;
		}
	}

	private void setMapPosition(Vector2I targetMapPosition)
	{
		Vector2 offset = GetParent<UnitManager>().GetParent<TileMapLayer>().MapToLocal(targetMapPosition) - Position;

		Translate(offset);

        m_CurrentMapPosition = targetMapPosition;
	}

	private bool IsValidMove(Vector2I targetMapPosition) 
	{
		if(	targetMapPosition == m_CurrentMapPosition ||
			m_movementPointsRemaining == 0 ||
			IsTileNeighbor(targetMapPosition) == false ||
            m_ValidDomain != r_Map.GetTileDomain(targetMapPosition))
		{
			return false;
		}
		return true;
	}

	public void ProcessTurn()
	{
		m_movementPointsRemaining += m_movementPoints;
	}

	private bool IsTileNeighbor(Vector2I targetMapPosition)
	{
		Godot.Collections.Array<Vector2I> neighbors = r_Map.GetSurroundingCells(targetMapPosition);

		for (int i = 0; i < neighbors.Count; i++)
		{
			if(neighbors[i] == m_CurrentMapPosition)
			{
				return true;
			}
		}

		return false;
	}
}

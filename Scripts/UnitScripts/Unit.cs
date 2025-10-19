using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Unit : Sprite2D
{
	// Statistics that all units have.
	protected int m_movementPoints = 0;
	protected int m_movementPointsRemaining = 0;
	protected int m_ValidDomain;
	protected string m_UnitType;

	protected Map r_Map;
	protected Vector2I m_CurrentMapPosition;

	// Who is transporting me?
	protected Unit m_UnitTransportingMe = null;

	// Statistics that a transport might have.
	protected int m_CargoSlots = 0;
	protected int m_CargoSlotsRemaining = 0;
	protected List<Unit> m_Manifest;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		r_Map = (Map)GetParent<UnitManager>().GetParent<TileMapLayer>();
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}

	public bool Order(Order order)
	{
		switch (order.m_Type)
		{
			case "MOVE":
				if(IsValidMove(order.m_Map_Coordinates))
				{
                    setMapPosition(order.m_Map_Coordinates);
					m_movementPointsRemaining--;
					return true;
                }
				return false;

			case "LOAD":
				return LoadInto(order.m_TargetUnit);

			case "UNLOAD":
                Unload();
				return true;

            case "UNLOAD_AND_MOVE":
                if (IsValidMove(order.m_Map_Coordinates))
                {
                    setMapPosition(order.m_Map_Coordinates);
                    m_movementPointsRemaining = 0; // Coming ashore spends all movement.
					Unload();
                    return true;
                }
                return false;

            default:
				return false;
		}
	}

	private void setMapPosition(Vector2I targetMapPosition)
	{
		// Translate the map coordinate into a local game coordinate.
		Vector2 offset = GetParent<UnitManager>().GetParent<TileMapLayer>().MapToLocal(targetMapPosition) - Position;

		Translate(offset);

        m_CurrentMapPosition = targetMapPosition;

		// Ensure any cargo moves with the unit.
		for(int i = 0; i < m_Manifest.Count; i++)
		{
			m_Manifest.ElementAt(i).m_CurrentMapPosition = m_CurrentMapPosition;
            m_Manifest.ElementAt(i).Translate(offset);
        }
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

	private bool LoadInto(Unit targetUnit)
	{
		if(targetUnit.m_CargoSlotsRemaining == 0)
		{
			return false;
		}

		// Load onto the transport and go to its current position such as boarding a ship on the coast.
		m_UnitTransportingMe = targetUnit;
        m_CurrentMapPosition = targetUnit.m_CurrentMapPosition;
        targetUnit.AddToManifest(this);

		// Hide the unit from game view now that it's loaded, unless specifically selected later.
		base.Visible = false;

		return true;
	}

	private void Unload()
	{
		m_UnitTransportingMe.RemoveFromManifest(this);
        m_UnitTransportingMe = null;
    }

	public bool AddToManifest(Unit unitToLoad)
	{
		if(m_CargoSlotsRemaining == 0)
		{
			return false;
		}
		if(unitToLoad.m_UnitType != "Treasure")
		{
            m_CargoSlotsRemaining--;
        }
		else if(m_CargoSlots >= 6 && m_CargoSlotsRemaining >= 3)
		{
			m_CargoSlotsRemaining -= 3;
        }
		else
		{
			// Not enough room for a treasure.
			return false;
		}
		m_Manifest.Add(unitToLoad);
		return true;
	}

	public bool RemoveFromManifest(Unit unitToLoad)
	{
		if(m_Manifest.Remove(unitToLoad))
		{
			if(unitToLoad.m_UnitType == "Treasure")
			{
				m_CargoSlotsRemaining += 3;
			}
			else
			{
				m_CargoSlotsRemaining++;
			}
			return true;
		}
		return false;
	}
}

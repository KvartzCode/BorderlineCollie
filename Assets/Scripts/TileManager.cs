using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public enum TileDirection
	{
        Up,
        Down,
        Left,
        Right
	}

	[SerializeField] float moveDistanceHorizontal = 100;
	[SerializeField] float moveDistanceVertical = 100;

    public void MoveTile(TileDirection direction)
	{
		switch (direction)
		{
			case TileDirection.Up:
				transform.Translate(0, moveDistanceVertical, 0);
				break;
			case TileDirection.Down:
				transform.Translate(0, -moveDistanceVertical, 0);
				break;
			case TileDirection.Left:
				transform.Translate(-moveDistanceHorizontal, 0, 0);
				break;
			case TileDirection.Right:
				transform.Translate(moveDistanceHorizontal, 0, 0);
				break;
			default:
				break;
		}
	}
}

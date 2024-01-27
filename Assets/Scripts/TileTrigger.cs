using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TileTrigger : MonoBehaviour
{
	[SerializeField] TileManager.TileDirection direction;
    [SerializeField] TileManager tileManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.CompareTag("Player"))
			tileManager.MoveTile(direction);
	}
}

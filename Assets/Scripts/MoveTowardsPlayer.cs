using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour
{
	private Transform _player;
	public float Speed = 2.0f;

	private void Start ()
	{
		_player = GameObject.Find("PlayerShip").transform;
	}

	private void Update ()
	{
		Vector3 delta = _player.position - transform.position;
		delta.Normalize();
		float moveSpeed = Speed * Time.deltaTime;
		transform.position = transform.position + delta * moveSpeed;
	}
}

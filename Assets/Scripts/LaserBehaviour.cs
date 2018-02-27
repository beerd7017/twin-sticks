using UnityEngine;

public class LaserBehaviour : MonoBehaviour
{

	public float Lifetime = 2.0f;
	public float Speed = 5.0f;
	public int Damage = 1;

	private void Start () {
		Destroy(gameObject, Lifetime);
	}

	private void Update () {
		transform.Translate(Vector3.up * Time.deltaTime * Speed);
	}
}

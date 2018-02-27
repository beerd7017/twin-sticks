using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
	public int Health = 2;
	public Transform Explosion;
	public AudioClip HitSound;

	private void OnCollisionEnter2D(Collision2D theCollision)
	{
		if (theCollision.gameObject.name.Contains("Laser"))
		{
			LaserBehaviour laser = theCollision.gameObject.GetComponent("LaserBehaviour") as LaserBehaviour;
			Health -= laser.Damage;
			Destroy(theCollision.gameObject);
		}

		if (Health > 0) return;
		Destroy(theCollision.gameObject);
		GetComponent<AudioSource>().PlayOneShot(HitSound);
		GameController controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
		controller.KilledEnemy();

		controller.IncreaseScore(10);

		if (!Explosion) return;
		GameObject exploder = Instantiate(Explosion, transform.position, transform.rotation).gameObject;
		Destroy(exploder, 2.0f);
	}
}

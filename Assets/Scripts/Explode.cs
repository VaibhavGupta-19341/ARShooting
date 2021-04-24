using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public GameObject explosion;
    public GameObject scoreToSpawn;
    public GameObject enemyToSpawn;
    Vector3 killPos;
    Quaternion killRot;
    public float waitTime = 3.0f;
    bool bulletCollision = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Zombie" && bulletCollision == false)
        {
            Destroy(collision.transform.gameObject);
            Scoring.score += 5;
            bulletCollision = true;
            killPos = collision.transform.position;
            killRot = collision.transform.rotation;
            StartCoroutine(SpawnEnemyAgain());
            Destroy(Instantiate(explosion, collision.transform.position, collision.transform.rotation),waitTime);
            Destroy(Instantiate(scoreToSpawn, collision.transform.position, collision.transform.rotation  * Quaternion.Euler(90f,0f,0f) * Quaternion.Euler(0f,180f,0f)),waitTime);
        }
    }

    IEnumerator SpawnEnemyAgain()
    {
        yield return new WaitForSeconds(waitTime);
        Instantiate(enemyToSpawn, killPos, killRot * Quaternion.Euler(90f,0f,0f));
        bulletCollision = false;
        Destroy(gameObject);
    }
}

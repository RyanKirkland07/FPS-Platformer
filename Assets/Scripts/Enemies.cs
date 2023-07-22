
using UnityEngine;

public class Enemies : MonoBehaviour
{
    public float health;

    public PlayerInfo InfoScript;
    private void Start()
    {
        GameObject player = GameObject.Find("Capsule");
        PlayerInfo InfoScript = player.GetComponent<PlayerInfo>();
        health = 100f;
    }

    public void TakeDamage (float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    } 
    void Die ()
    {
        Destroy(gameObject);
        InfoScript.PointGain();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float health;
    public List<AttackContent> AttackList;
    public GameObject healthbar;
    Transform healthbartrans;
    float width;
    // Start is called before the first frame update

    
    public double enemyTurn()
    {

        int size = AttackList.Count;
        System.Random rnd = new System.Random();
        int index = rnd.Next(0, size);
        AttackContent chosen = AttackList[index];
        return chosen.getDamage();

    }

    void Start()
    {

        healthbartrans = healthbar.transform;
        width = healthbartrans.localScale.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0)
        {
            Vector3 scalevector = new Vector3(width * (health / 100.0f), healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
        }
        else
        {
            Vector3 scalevector = new Vector3(0, healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
        }


    }
}

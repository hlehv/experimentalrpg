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
    float originalx;
    double attackDamage; //shitty way :(
                         // Start is called before the first frame update
    bool canAttack;


    public double enemyTurn()
    {
        //First time it's called it needs to satart corotuine waiting for  x seconds
        //And does what is below
        //Must return attackadamage
        StartCoroutine(enemywait());
        return attackDamage;



    }

    IEnumerator enemywait()
    {
        if (canAttack)
        {
            canAttack = false;
            yield return new WaitForSeconds(2);
            int size = AttackList.Count;
            System.Random rnd = new System.Random();
            int index = rnd.Next(0, size);
            AttackContent chosen = AttackList[index];
            attackDamage = chosen.getDamage();
        }
    }

    void Start()
    {
        attackDamage = -1;
        canAttack = true;
        healthbartrans = healthbar.transform;
        width = healthbartrans.localScale.x; 
        originalx = healthbartrans.localPosition.x;


    }

    // Update is called once per frame
    void Update()
    {
        if (health >= 0)
        {
            Vector3 scalevector = new Vector3(width * (health / 100.0f), healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
            float diff = Mathf.Abs(width - (width * (health / 100.0f)));
            Vector3 positionVector = new Vector3((originalx - 3.7f * diff), healthbartrans.localPosition.y, healthbartrans.localPosition.z);
            healthbartrans.localPosition = positionVector;
        }
        else
        {
            Vector3 scalevector = new Vector3(0, healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
        }


    }
    public void Reset()
    {
        attackDamage = -1;
        canAttack = true;
    }
}

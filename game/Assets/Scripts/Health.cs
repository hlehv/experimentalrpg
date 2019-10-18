using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health;
    public List<GameObject> AttackList;
    public GameObject healthbar;
    Transform healthbartrans;
    float width;
    // Start is called before the first frame update
    void Start()
    {

        healthbartrans = healthbar.transform;
        width = healthbartrans.localScale.x;


    }

    // Update is called once per frame
    void Update()
    {
        Vector3 scalevector = new Vector3(width * (health / 100.0f), healthbartrans.localScale.y, healthbartrans.localScale.z);
        healthbartrans.localScale = scalevector;

    }
}

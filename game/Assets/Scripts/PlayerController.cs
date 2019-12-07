using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public float health;
    public List<AttackContent> AttackList;
    public List<AttackContent> totalAttackList;
    public GameObject healthbar;
    Transform healthbartrans;
    float width;
    float originalx;
    public int index;
    public double attackDamage;
    bool beginningOfTurn;
    bool canAttack;
    public int anam;
    public int MAXANAM;
    public ManaBall manaBallPrefab;
    List<ManaBall> manaBalls;
    public bool isTurn;
    public int ATTACKLISTSIZE;

    // Start is called before the first frame update

    public double playerTurn()
    {
        int size = AttackList.Count;
        if (beginningOfTurn)
        {
            for (int i = 0; i < AttackList.Count; i++)
            {
                if (AttackList[i] != null && AttackList[i].gameObject.scene.name != null)
                {
                    Debug.Log("destroying" + AttackList[i]);
                    Destroy(AttackList[i].gameObject);
                }
            }
            AttackList.Clear();
            for (int i = 0; i < ATTACKLISTSIZE; i++)
            {
                int j = Random.Range(0, totalAttackList.Count);
                AttackList.Add(totalAttackList[j]);
            }
            float middle = (ATTACKLISTSIZE + 1) / 2.0f;
            for (int i = 0; i < ATTACKLISTSIZE; i++)
            {

                AttackContent tempAttack = Instantiate(AttackList[i], new Vector3((i + 1 - middle) * 6.0f, -6.5f, 0), Quaternion.identity);
                Debug.Log("scene name" + gameObject.scene.name);
                if (AttackList[i].gameObject.scene.name != null)
                {
                    Debug.Log("destroying" + AttackList[i]);
                    Destroy(AttackList[i].gameObject);
                }
                AttackList[i] = tempAttack;
            }
            beginningOfTurn = false;
        }

        return attackDamage;

    }


    public void Reset()
    {
        beginningOfTurn = true;
        attackDamage = -1;
        canAttack = true;
        anam = MAXANAM;
    }

    public void ResetDamage()
    {
        attackDamage = -1;
        canAttack = true;
    }

    void Start()
    {
        manaBalls = new List<ManaBall>();
        beginningOfTurn = true;
        canAttack = true;
        healthbartrans = healthbar.transform;
        width = healthbartrans.localScale.x;
        originalx = healthbartrans.localPosition.x;
        attackDamage = -1;
        //testing
        playerTurn();
        anam = MAXANAM;

        //setup mana balls
        for (int i = 0; i < MAXANAM; i++)
        {
            if (i > anam)
            {
                manaBallPrefab.isActive = false;
            }
            else
            {
                manaBallPrefab.isActive = true;
            }

            ManaBall newmanaBall = Instantiate(manaBallPrefab, new Vector3(-16.4f, 6.55f - 2.1f*i), Quaternion.identity);
            manaBalls.Add(newmanaBall);
        }
    }

    // Update is called once per frame
    void Update()
    {

        //setup mana balls
        for (int i = 0; i < manaBalls.Count; i++)
        {
            if (i >= anam)
            {
                manaBalls[i].isActive = false;
            }
            else
            {
                manaBalls[i].isActive = true;
            }

        }


        if (health >= 0)
        {
            Vector3 scalevector = new Vector3(width * (health / 100.0f), healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
            float diff = Mathf.Abs(width - (width * (health / 100.0f)));
            Vector3 positionVector = new Vector3((originalx - 3.7f*diff), healthbartrans.localPosition.y, healthbartrans.localPosition.z);
            healthbartrans.localPosition = positionVector;
        }
        else
        {
            Vector3 scalevector = new Vector3(0, healthbartrans.localScale.y, healthbartrans.localScale.z);
            healthbartrans.localScale = scalevector;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(mouseClickCoroutine());
        }
    }

    IEnumerator mouseClickCoroutine() {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if ((hit.collider != null && hit.collider.gameObject.GetComponent<AttackContent>() != null) && canAttack)
        {
            if (anam >= hit.collider.gameObject.GetComponent<AttackContent>().anamCost && isTurn)
            {
                canAttack = false;
                yield return new WaitForSeconds(.0000001f);
                Debug.Log(hit.collider.gameObject.name);
                AudioSource audioSource = this.GetComponent<AudioSource>();
                audioSource.Play();
                attackDamage = hit.collider.gameObject.GetComponent<AttackContent>().damage;
                Debug.Log("reducing anam");
                anam = anam - hit.collider.gameObject.GetComponent<AttackContent>().anamCost;
                if (hit.collider.gameObject.scene.name != null)
                {
                    Debug.Log("destroying" + hit.collider.gameObject);
                    Destroy(hit.collider.gameObject);
                }
            }
            else
            {
                Debug.Log("Can't make attack, anam too low");
            }
        }
    }

}


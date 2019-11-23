using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public float health;
    public List<AttackContent> AttackList;
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

    // Start is called before the first frame update

    public double playerTurn()
    {
        /*
     * Instantiate - this is how you take a unity object thing and put it into the scene
     * Unity UI - button
     * something involving instantiating a unity UI button...
     * 
     * 
     * 
     * 
     * 
     * 
     */
        int size = AttackList.Count;
        float middle = (size + 1)/ 2.0f;
        if (beginningOfTurn)
        {
            for (int i = 0; i < AttackList.Count; i++)
            {

                AttackList[i] = Instantiate(AttackList[i], new Vector3((i + 1 - middle) * 6.0f, -6.5f, 0), Quaternion.identity);
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
        beginningOfTurn = true;
        canAttack = true;
        healthbartrans = healthbar.transform;
        width = healthbartrans.localScale.x;
        originalx = healthbartrans.localPosition.x;
        attackDamage = -1;
        //testing
        playerTurn();
        anam = MAXANAM;
    }

    // Update is called once per frame
    void Update()
    {
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
            if (anam >= hit.collider.gameObject.GetComponent<AttackContent>().anamCost)
            {
                Debug.Log("anam" + anam);
                Debug.Log("Cost anam cost" + hit.collider.gameObject.GetComponent<AttackContent>().anamCost);
                canAttack = false;
                yield return new WaitForSeconds(.0000001f);
                Debug.Log(hit.collider.gameObject.name);
                AudioSource audioSource = this.GetComponent<AudioSource>();
                audioSource.Play();
                attackDamage = hit.collider.gameObject.GetComponent<AttackContent>().damage;
                anam = anam - hit.collider.gameObject.GetComponent<AttackContent>().anamCost;
            }
            else
            {
                Debug.Log("Can't make attack, anam too low");
            }
        }
    }

}


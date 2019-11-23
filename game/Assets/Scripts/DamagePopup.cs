using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DamagePopup : MonoBehaviour
{

    private enum DpState
    {
        None = 0,
        Ascend = 1, 
        Fade = 2
    } ;

    // Duration of Ascend state, set to stateTimeout.
    private static float dpAscendTime = 0.25f;
    // Duration of Fade state, set to stateTimeout.
    private static float dpFadeTime = 0.15f;
    // X-coordinate movement of text in the Ascend state, per frame.
    private static float dpMoveDeltaX = 0f;
    // Y-coordinate movement of text in the Ascend state, per frame.
    private static float dpMoveDeltaY = 10f;
    // State timeout counter.
    private float stateTimeout;
    // The update state of this object.
    private DpState state;
    // The current text color this object, used to fade the text.
    private Color textColor;

    // The object's TextMeshPro reference.
    private TextMeshPro textMesh;


    public static DamagePopup Create(Transform instance, Vector3 position, int damageAmount)
    {
        Transform damagePopupTransform = Instantiate(instance, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);
        return damagePopup;
    }
    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    // Start is called before the first frame update
    void Setup(int damageAmout)
    {
        textMesh.SetText(damageAmout.ToString());
        state = DpState.Ascend;
        stateTimeout = dpAscendTime;
        textColor = textMesh.color;
    }

    // Update is called once per frame
    // There are 3 states: None, Ascend and Fade.
    // States are transitioned when the stateTimeout is less than 0.
    // The state transitions is: Ascend -> Fade -> (Destroyed)
    // The state None is only applied to the "mother" object as we want
    // to preserve the object while letting it do nothing.
    // Created objects are instantiated into the Ascend state.
    // Ascend: In this state the text moves (ascends) for
    // (dpMoveDeltaX, dpMoveDeltaY) each frame.
    // Fade: In this state the text fades (alpha decreases) so that when
    // the timeout hits 0, the text is faded completely.
    // After that, the object is destroyed.
    void Update()
    {
        stateTimeout -= Time.deltaTime;
        switch (state)
        {
            case DpState.Ascend:
                transform.position += new Vector3(dpMoveDeltaX, dpMoveDeltaY) * Time.deltaTime;
                if (stateTimeout < 0)
                {
                    state = DpState.Fade;
                    stateTimeout = dpFadeTime;
                }
                break;
            case DpState.Fade:
                transform.position += new Vector3(dpMoveDeltaX, dpMoveDeltaY) * Time.deltaTime;
                textColor.a -= Time.deltaTime / dpFadeTime;
                textMesh.color = textColor;
                if (stateTimeout < 0)
                {
                    Destroy(gameObject);
                }
                break;
            case DpState.None:
                break;
        }
    }


}


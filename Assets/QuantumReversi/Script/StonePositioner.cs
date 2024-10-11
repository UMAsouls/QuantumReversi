using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class StonePositioner : MonoBehaviour, IStonePositioner
{

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private GameObject PlayerStone;

    private bool isClick = false;
    private bool putable = false;

    private bool isStay = true;

    private StoneSettable setMass;

    private Vector2 mousePos;

    private StoneType mode;

    private bool settable;

    public void OnClick(InputValue value)
    {
        isClick = true;
    }

    public void OnMove(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    public void ChangeMode()
    {
        if (settable)
        {
            PlayerStone.SetActive(false);
            settable = false;
        }else
        {
            PlayerStone.SetActive(true);
            settable=true;
        }
    }

    public async UniTask<StoneType> PutStone()
    {
        PlayerStone.SetActive(true);
        input.SwitchCurrentActionMap("Main");
        Debug.Log(mousePos);
        while (true)
        {
            isClick = false;
            await UniTask.WaitUntil(() => isClick);

            if (putable && settable && setMass != null)  break;
        }

        var type = mode;
        setMass.StoneSet(type);

        if(mode == StoneType.NINETY) mode = StoneType.SEVENTY;
        else mode = StoneType.NINETY;

        transform.position = Vector3.zero;
        input.SwitchCurrentActionMap("EnemyTurn");
        PlayerStone.SetActive(false);
        putable = false;
        setMass.UnFocus();
        setMass = null;
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        putable = false;
        if(setMass != null)
        {
            setMass.UnFocus();
            setMass = null;
        }
        setMass = collision.gameObject.GetComponent<StoneSettable>();
        
        if (setMass == null) return;

        putable = setMass.IsSettable;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Mass")
        {
            putable = setMass.IsSettable;
            setMass.Focus();
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Board" && !collision.IsTouching(GetComponent<Collider2D>()))
        {
            setMass.UnFocus();
            setMass = null;
            putable = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        mode = StoneType.NINETY;
        settable = true;
        isStay = false;
    }

    // Update is called once per frame
    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(mousePos);
        position.z = 0;

        transform.position = position;
    }
}
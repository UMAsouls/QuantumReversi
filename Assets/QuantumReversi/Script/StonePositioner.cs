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

    private StoneSettable setMass;

    private Vector2 mousePos;

    private StoneType mode;

    private void OnClick(InputValue value)
    {
        isClick = true;
    }

    private void OnMove(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    public async UniTask<StoneType> PutStone()
    {
        input.SwitchCurrentActionMap("Main");
        while (true)
        {
            isClick = false;
            await UniTask.WaitUntil(() => isClick);

            if (putable)  break;
        }

        var type = mode;
        setMass.StoneSet(type);

        if(mode == StoneType.NINETY) mode = StoneType.SEVENTY;
        else mode = StoneType.NINETY;

        transform.position = Vector3.zero;
        input.SwitchCurrentActionMap("EnemyTurn");
        return type;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        putable = false;
        setMass = collision.gameObject.GetComponent<StoneSettable>();
        if (setMass == null) return;

        PlayerStone.SetActive(true);
        putable = setMass.IsSettable;
        setMass.Focus();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStone.SetActive(false);
        setMass.Focus();
        putable = false;
        setMass = null;
    }

    // Use this for initialization
    void Start()
    {
        mode = StoneType.NINETY;
    }

    // Update is called once per frame
    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(mousePos);
        position.z = 0;

        transform.position = position; 
    }
}
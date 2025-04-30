using Cysharp.Threading.Tasks;
using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

//小田原担当
//石を置く位置を決めるクラス
//プレイヤーの操作を受け付ける
public class StonePositioner : MonoBehaviour, IStonePositioner
{

    [SerializeField]
    private PlayerInput input;

    [SerializeField]
    private GameObject PlayerStone;

    private bool isClick = false;
    private bool putable = false;

    private bool isStay = true;

    private StoneSettable setMass = null;

    private Vector2 mousePos;

    private StoneType mode;

    private bool settable;

    private CancellationToken cts;

    public void OnClick(InputValue value)
    {
        isClick = true;
    }

    public void OnMove(InputValue value)
    {
        mousePos = value.Get<Vector2>();
    }

    public void OnESC(InputValue value)
    {
        SceneManager.LoadScene("Title");
    }

    public async UniTask<StoneType> PutStone()
    {
        PlayerStone.SetActive(true);
        input.SwitchCurrentActionMap("Main");
        settable = true;
        while (true)
        {
            isClick = false;
            await UniTask.WaitUntil(() => isClick, PlayerLoopTiming.Update, cts);

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
  

    // Use this for initialization
    void Start()
    {
        mode = StoneType.NINETY;
        settable = true;
        isStay = false;
        cts = this.GetCancellationTokenOnDestroy();
    }

    private void RayHit(Collider2D hit)
    {
        
        var s = hit.gameObject.GetComponent<StoneSettable>();

        Debug.Log(hit.gameObject.name);

        if (s == null || s == setMass) return;
        else if (setMass != null)
        {
            setMass.UnFocus();
            setMass = null;
        }

        setMass = s;
        setMass.Focus();

        putable = setMass.IsSettable;
    }

    // Update is called once per frame
    void Update()
    {
        var position = Camera.main.ScreenToWorldPoint(mousePos);
        position.z = 0;

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0f);

        if (hit.collider != null) RayHit(hit.collider);
        else if(setMass != null)
        {
            setMass.UnFocus();
            setMass = null;
        }

        transform.position = position;
    }
}
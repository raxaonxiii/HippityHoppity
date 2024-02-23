using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameObject playerPrefab;
    public GameObject modelParent;

    public enum SwipingDirection { LEFT, RIGHT, UP, DOWN, NONE }
    public SwipingDirection SwipeDirection => _swipingDirection;
    
    public Animator playerAnim;
    public TrailRenderer trailRenderer;
    public KeyCode inputShortHop;
    public KeyCode inputLongHop;

    public bool isShortHop;
    public bool isLongHop;
    public bool isFalling;
    public bool isGrounded;
    public bool isWaitingToStartGame = true;

    [Header("Mobile Settings")]
    public bool firstInput;
    public float minSwipingDistance = 50f;
    public float maxTouchDuration = 0.2f;

    private SwipingDirection _swipingDirection = SwipingDirection.NONE;
    private Touch _initialTouch;
    private float _touchDuration = 0;

    public void SetPlayerPrefab(ShopItem item)
    {
        playerPrefab = item.prefabs[0];
    }

    public void LoadPlayerPrefab()
    {
        if (modelParent.transform.childCount > 0)
            Destroy(modelParent.transform.GetChild(0).gameObject);
        GameObject model = Instantiate(playerPrefab, modelParent.transform.position, Quaternion.identity);
        model.transform.parent = modelParent.transform;
        model.GetComponent<PlayerCollider>().SetUp(this);
    }

    public void SetUp()
    {
        trailRenderer.enabled = false;
        firstInput = false;
        LoadPlayerPrefab();
    }

    void OnEnable( )
    {
        isGrounded = true;
    }

    void Update( )
    {
        if( isWaitingToStartGame ) return;
        if (GameManager.Instance == null) return;
        trailRenderer.enabled = true;
        if (GameManager.Instance.paused) return;

        if (isGrounded)
        {

            isShortHop = Input.GetKeyDown(inputShortHop);
            isLongHop = Input.GetKeyDown(inputLongHop);

            if(isShortHop || isLongHop)
            {
                Debug.Log("First input Detected");
                firstInput = true;
                GameManager.Instance.SetControlsOn(!firstInput);
            }

            if (Application.isEditor == false)
            {
                if (Input.touchCount > 0)
                {
                    _touchDuration += Time.deltaTime;
                    Touch touch = Input.GetTouch(0);
                    Debug.Log("First input Detected");
                    firstInput = true;
                    GameManager.Instance.SetControlsOn(!firstInput);

                    if (touch.phase == TouchPhase.Began)
                    {
                        _initialTouch = touch;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        Vector2 touchDirection = new Vector2(
                            touch.position.x - _initialTouch.position.x,
                            touch.position.y - _initialTouch.position.y);

                        _swipingDirection = GetSwipeDirection(touchDirection);
                    }
                    else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        if (_swipingDirection == SwipingDirection.NONE && _touchDuration <= maxTouchDuration)   // just tapped the screen
                        {
                            isShortHop = true;
                        }
                        else
                        {
                            if (_swipingDirection == SwipingDirection.UP)
                                isLongHop = true;
                        }

                        _touchDuration = 0;
                        _initialTouch = new Touch();
                        _swipingDirection = SwipingDirection.NONE;
                    }
                }
                else
                {
                    isShortHop = false;
                    isLongHop = false;
                }
            }
        }

        PlayAnims();
        if (transform.position.y < -1) isFalling = true;
    }

    void PlayAnims( )
    {
        playerAnim.SetBool( "isShortHop", isShortHop );
        playerAnim.SetBool( "isLongHop", isLongHop );
        playerAnim.SetBool( "isGrounded", isGrounded );
    }

    public void PlayJumpSound()
    {
        SFXManager.Instance.PlaySound("DM-CGS-21", Random.Range(0.9f, 1.5f));
    }
    
    private SwipingDirection GetSwipeDirection(Vector2 direction)
    {

        SwipingDirection swipeDirection = SwipingDirection.NONE;

        if (direction.magnitude < minSwipingDistance)
        {
            return SwipingDirection.NONE;
        }

        bool isHorizontalSwipe = Mathf.Abs(direction.x) > Mathf.Abs(direction.y);

        if (isHorizontalSwipe)
        {
            if (direction.x > 0)
            {
                swipeDirection = SwipingDirection.RIGHT;
            }
            else if (direction.x < 0)
            {
                swipeDirection = SwipingDirection.LEFT;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                swipeDirection = SwipingDirection.UP;
            }
            else if (direction.y < 0)
            {
                swipeDirection = SwipingDirection.DOWN;
            }
        }

        return swipeDirection;
    }

    // public void Reset( )
    // {
    //     transform.position = new Vector3( 0, 0.5f, 0 );
    //     playerAnim.Play( "Idle" );
    //     isWaitingToStartGame = true;
    //     isGrounded = true;
    //     isShortHop = false;
    //     isLongHop = false;
    //     platformCount = 0;
    // }
}

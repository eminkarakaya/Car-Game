using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CarController : MonoBehaviour
{
    public static event System.Action OnFall;
    [SerializeField] private bool _isGrounded;
    public bool isGrounded { get => _isGrounded ; set 
        {
            var old = _isGrounded;
            _isGrounded = value;
            if(_isGrounded != old && _isGrounded && TryGetComponent(out PlayerInput playerInput))
            {
                OnFall?.Invoke();
            }
        }
    }
    [SerializeField] private BoxCollider boxCollider;
    private RaycastHit hit;
    [SerializeField] private float rayLenght;
    [SerializeField] private Transform _backOfCar;
    MeshRenderer [] meshRenderers;
    public bool isFinish;
    public static CarController instance;
    public static event System.Action OnFinish;
    public LevelController levelController;
    bool kazaYaptiMi;
    bool dokunulmazMi;
    public bool specialFinish;
    [SerializeField] private int unTouchableLayerIndex, defaultLayerIndex;
    [SerializeField] private LayerMask layer;
    [SerializeField] CarData data;
    [SerializeField] private float maxSpeed,rotationMultiplier, speed,rayLength,reviveDistance,reviveDuration
        ,unTouchableDuration,horizontalSpeed;
    Rigidbody rb;
    public float horizontalData;
    [SerializeField] private TextMeshProUGUI numberOfText;
    public int numberOf;
    Vector3 oldColliderSize;
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        maxSpeed = data.maxSpeed;
        oldColliderSize = boxCollider.size;
        levelController = FindObjectOfType<LevelController>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        TextParse();
        if(Physics.Raycast(transform.position,Vector3.down,out hit,rayLength,layer))
        {
            if(hit.collider != null)
                isGrounded = true;
        }
        else
            isGrounded = false;
    }
    private void FixedUpdate()
    {
        if (!levelController.isStart || isFinish)
            return;
        if (!isGrounded)
        {
            rb.AddForce(Physics.gravity*2,ForceMode.Acceleration);
        }
        HorizontalMove();        
        if (rb.velocity.z < maxSpeed)
        {
            rb.velocity += new Vector3(rb.velocity.x, 0, Time.deltaTime * data.accelerationSpeed*2);
        }
        else
        {
            if (rb.velocity.z > 0)
            {
                rb.velocity -= new Vector3(rb.velocity.x, 0, Time.deltaTime * data.accelerationSpeed);
            }
        }
        Vector3 rby = rb.angularVelocity;
        rby.y = 0;
        Vector3 qwe = transform.rotation.eulerAngles;
        qwe.y = 0;
        transform.rotation = Quaternion.Euler(qwe);

        rb.angularVelocity = rby;
        ClampRotation();
    }
    public void HorizontalMove()
    {
        Vector3 rot = Vector3.forward * rotationMultiplier * horizontalData;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = rot.z;
        transform.rotation = Quaternion.Euler(temp);
        rb.velocity = new Vector3(horizontalSpeed * horizontalData, rb.velocity.y,rb.velocity.z);
    }
    private IEnumerator Revive()
    {
        if(!kazaYaptiMi)
        {
            kazaYaptiMi = true;
            //rb.velocity = Vector3.zero;
            Vector3 revivePos = transform.position + Vector3.back * reviveDistance;
            yield return new WaitForSeconds(reviveDuration);
            transform.rotation = Quaternion.Euler(Vector3.zero);
            kazaYaptiMi = false;
            transform.position = revivePos;
            StartCoroutine(UnTouchable());
        }
    }
    private IEnumerator UnTouchable()
    {
        if(!dokunulmazMi)
        {            
            StartCoroutine(MaterialToggle());
            dokunulmazMi = true;
            gameObject.layer = unTouchableLayerIndex;
            yield return new WaitForSeconds(unTouchableDuration);
            dokunulmazMi = false;
            gameObject.layer = default;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wall")
        {
            Collider[] colliders = other.gameObject.GetComponentsInChildren<Collider>();
            foreach (var item in colliders)
            {
                Debug.Log(item);
                item.enabled = true;
                if(item.TryGetComponent(out Rigidbody rb))
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                }
            }
        }
        if(other.gameObject.tag == "Obstacle")
        {
            StartCoroutine(Revive());
        }
        if(other.gameObject.tag == "Speed")
        {
            rb.velocity = new Vector3(0, rb.velocity.y, data.boostedMaxSpeed);
        }
        if(other.gameObject.tag =="Finish")
        {
            if(TryGetComponent(out PlayerInput playerInput))
            {
                OnFinish?.Invoke();
                isFinish = true;
            }
            Finish(other);
        }
        if(other.gameObject.tag == "Object")
        {
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * rb.velocity.z * 3);
        }
        if(other.gameObject.tag == "Trap")
        {
            WheelBurst();
        }
        if(other.gameObject.tag == "Wheel")
        {
            FixWheel();
        }
    }
    public void SpecialFinish()
    {
        if(specialFinish)
        {
            specialFinish = false;
        }
    }
    IEnumerator MaterialToggle()
    {
        var tempToplam = 0f;
        var temp = 0f;

        bool open = true;

        while(tempToplam < unTouchableDuration)
        {
            yield return null;
            tempToplam += Time.deltaTime;
            temp += Time.deltaTime;
            if(temp > .3f)
            {
                temp = 0f;
                if (open)
                {
                    foreach (var item in meshRenderers)
                    {
                        foreach (var material in item.materials)
                        {
                            material.color = new Color(material.color.r, material.color.g, material.color.b, 0);

                        }
                    }
                    open = false;
                }
                else
                {
                    foreach (var item in meshRenderers)
                    {
                        foreach (var material in item.materials)
                        {
                            material.color = new Color(material.color.r, material.color.g, material.color.b, 255);

                        }
                    }
                    open = true;
                }
            }
        }
        foreach (var item in meshRenderers)
        {
            foreach (var material in item.materials)
            {
                material.color = new Color(material.color.r, material.color.g, material.color.b, 255);

            }
        }

    }

    void ClampRotation()
    {
        if(transform.rotation.eulerAngles.x < 340 && transform.rotation.eulerAngles.x > 180)
        {
            transform.rotation = Quaternion.Euler(-20, transform.rotation.y, transform.rotation.z);
            Vector3 angX = rb.angularVelocity;
            angX.x = 0;
            rb.angularVelocity = angX;
        }
        else if (transform.rotation.eulerAngles.x > 30 && transform.rotation.eulerAngles.x < 180)
        {
            transform.rotation = Quaternion.Euler(25, transform.rotation.y, transform.rotation.z);
            Vector3 angX = rb.angularVelocity;
            angX.x = 0;
            rb.angularVelocity = angX;
        }
    }
    void WheelBurst()
    {
        maxSpeed = maxSpeed / 2;
        Vector3 colliderSize = new Vector3(boxCollider.bounds.size.x, boxCollider.bounds.size.y - .5f, boxCollider.bounds.size.z);
        boxCollider.size = colliderSize;
    }
    void FixWheel()
    {
        maxSpeed = data.maxSpeed;
        boxCollider.size = oldColliderSize;
    }
    void Finish(Collider other)
    {
        other.GetComponent<MeshRenderer>().enabled = false;
        other.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
        other.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
        other.transform.GetChild(0).gameObject.SetActive(true);
        
        other.transform.GetChild(0).GetComponent<Rigidbody>().AddExplosionForce(100, other.transform.GetChild(0).transform.position, 10f);

        //Collider[] colliders = rb.transform.GetChild(0).GetComponentsInChildren<Collider>();
        //foreach (var item in colliders)
        //{

        //}
    }
<<<<<<< Updated upstream
    private void OnDrawGizmos()
    {
        //Gizmos.DrawSphere()
    }
=======
>>>>>>> Stashed changes
    void TextParse()
    {
        if(numberOf+1 == 1)
        {
            numberOfText.text = (numberOf + 1) + "st";
        }
        else if(numberOf + 1 == 2)
        {
            numberOfText.text = (numberOf + 1) + "nd";

        }
        else if (numberOf + 1 == 3)
        {
            numberOfText.text = (numberOf + 1) + "rd";

        }
        else if (numberOf + 1 > 3)
        {
            numberOfText.text = (numberOf + 1) + "th";

        }
    }
}

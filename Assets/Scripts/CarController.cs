using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CarController : MonoBehaviour
{
    public static event System.Action OnFall;
    private bool _isGrounded;
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
    private void Awake()
    {
        instance = this;
    }
    
    private void Start()
    {
        levelController = FindObjectOfType<LevelController>();
        meshRenderers = GetComponentsInChildren<MeshRenderer>();
        rb = GetComponent<Rigidbody>();
        //maxSpeed = data.maxSpeed;
    }
    private void Update()
    {
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
       
        //rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z);
        
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

        rb.angularVelocity = rby;
    }
    public void HorizontalMove()
    {
        Vector3 rot = Vector3.forward * rotationMultiplier * (rb.velocity.z / maxSpeed) * horizontalData;
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = rot.z;
        transform.rotation = Quaternion.Euler(temp);

        rb.velocity = new Vector3(horizontalSpeed * horizontalData, rb.velocity.y,rb.velocity.z);
        

        //transform.Translate(Vector3.right * Time.deltaTime * data.horizontalSpeed * (rb.velocity.z / maxSpeed) * horizontalData * 1.3f);
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
            //rb.constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
            
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
        if(other.gameObject.tag == "Obstacle")
        {
            //CrashEffect2(other.transform);
            //CrashEffect();
            StartCoroutine(Revive());
        }
        if(other.gameObject.tag == "Speed")
        {
            rb.velocity = new Vector3(0, rb.velocity.y, data.boostedMaxSpeed);
        }
        if(other.gameObject.tag =="Finish")
        {
            OnFinish?.Invoke();
            isFinish = true;
        }
        if(other.gameObject.tag == "Object")
        {
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * rb.velocity.z * 3);
        }
    }
    public void SpecialFinish()
    {
        if(specialFinish)
        {
            specialFinish = false;
        }
    }
    void CrashEffect2(Transform target)
    {
        rb.AddForce(transform.position - target.position * 100 * rb.velocity.z);
    }
    void CrashEffect()
    {
        //rb.constraints = RigidbodyConstraints.None;
        rb.AddExplosionForce(1000 * rb.velocity.z,_backOfCar.position,.3f);
        
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
        Debug.Log(transform.rotation + " " + transform.rotation.eulerAngles);
    }
}

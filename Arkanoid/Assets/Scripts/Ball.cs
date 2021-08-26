using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//простейший вариант с багом застревания мяча отталкивающегося от стен перпендикулярно - 40 мин
//добавила OnCollisionEnter, определение на какой край падает мяч в ту сторону направление - 40 мин это я гуглила
//добавила в Upd штуки, по-моему, решающую вопрос с горизонтальным застреванием, но кривовато - минут 5
public class Ball : MonoBehaviour
{
    //игрок-платформа
    [SerializeField]
    private Transform Player;
    //скорость мяча
    [SerializeField]
    private float _ballSpeed = 600f;
    public float ballSpeed  
    { get{ return _ballSpeed; } }

    //начальная скорость (рандомится в start)
    private Vector3 startForce;
    //определяет активен ли мяч
    private bool activeBall = false;
    //начальная позиция мяча
    private Vector3 ballPos;
    private Rigidbody rb;

    //события попадания в ту или иную платформу
    public event System.Action Score;
    public event System.Action Lose;
    // Start is called before the first frame update
    void Start()
    {
        ballPos = transform.position;
        float rand = Random.Range(-100f, 100f);
        startForce = new Vector3(-ballSpeed, 0, rand);
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeBall)
        {
            //ДОЛОЙ горизонтальное застревание
            if(Mathf.Abs(rb.velocity.z)> Mathf.Abs(rb.velocity.x))
            {
                rb.velocity = new Vector3(rb.velocity.z, rb.velocity.y, rb.velocity.x);
            }
            //если мяч активен, то больше ничего не надо делать
            return;
        }
        //работает когда мяч не активен
        //мяч двигается вместе с платформой
        ballPos.z = Player.position.z;
        transform.position = ballPos;
        //если нажали ЛКМ добавляем линейную и угловую силу
        if(Input.GetMouseButtonDown(0))
        {
            rb.AddForce(startForce);
            rb.AddTorque(0, ballSpeed, 0);
            activeBall = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //если мяч неактивен, проверять нечего
        if (!activeBall) return;
        //если коснулись платформы включается событие
        if (collision.gameObject.tag == "bricks")
        {
            Score?.Invoke();
        }
        if (collision.gameObject.tag == "lose")
        {
            Lose?.Invoke();
        }
        //если касается игрока платформы
        if (collision.transform == Player)
        {
            //проверям смещение относительно центра платформы и 
            //в соответсвующую сторону направляем
            //это я гуглила
            Vector3 hitPoint = collision.contacts[0].point;
            Vector3 platformCenter = Player.transform.position;

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddTorque(0, ballSpeed, 0);
            float diff = platformCenter.z - hitPoint.z;
            if (hitPoint.z < platformCenter.z)
            {
                rb.AddForce(new Vector3(-ballSpeed, 0, -(Mathf.Abs(diff * 200))));
            }
            else
            {
                rb.AddForce(new Vector3(-ballSpeed, 0, (Mathf.Abs(diff * 200))));

            }
        }
    }
}

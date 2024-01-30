using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class valorvidaP : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public bool isDead;
    bool isCountingDown;
    public Slider slider;
    Animator anim;
    public GameObject pantallaReaparecer;
    public float tiempoInicial; // Tiempo inicial en segundos
    private float tiempoRestante;
    public Text textoCuentaRegresiva; // Referencia al texto en pantalla
    public GameObject boton;
    public Transform Respawn;
    public GameObject zombieagarrar;
    private zombieagarrador lengua; 



    void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        pantallaReaparecer.SetActive(false);
        anim = GetComponent<Animator>();
        tiempoRestante = tiempoInicial;
        isDead = false;
        lengua = zombieagarrar.GetComponent<zombieagarrador>();
    }

    void Update()
    {
        if (isCountingDown)
        {
            MostrarTiempo();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        slider.value = currentHealth;

        if (currentHealth <= 0)
        {
            Dead();
        }
    }

    private void MostrarTiempo()
    {
        textoCuentaRegresiva.text = tiempoRestante.ToString();

        if (tiempoRestante <= 0)
        {
            textoCuentaRegresiva.gameObject.SetActive(false);
            boton.SetActive(true);
            isCountingDown = false;
        }
    }

    public void Dead()
    {
        isDead = true;
        anim.SetTrigger("death");
        pantallaReaparecer.SetActive(true);
        GetComponent<ThirdPersonControllerMovement>().DisableMovement();
        StartCoroutine(IniciarCuentaRegresiva());
        boton.SetActive(false);
    }

    public IEnumerator IniciarCuentaRegresiva()
    {
        isCountingDown = true;

        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1);
            tiempoRestante--;
        }

        boton.SetActive(true);
    }

    public void Reaparecer()
    {
        transform.position = Respawn.position;
        lengua.ArrastraDeshabilitado();
        boton.SetActive(false);
        pantallaReaparecer.SetActive(false);
        Start();
        GetComponent<ThirdPersonControllerMovement>().EnableMovement();

    }
}

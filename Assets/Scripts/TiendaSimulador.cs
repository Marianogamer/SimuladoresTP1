using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TiendaSimulador : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text textoDinero;
    public TMP_Text textoVidas;
    public TMP_Text textoPedido;
    public TMP_Text textoEstado;
    public Slider barraPaciencia;

    [Header("Configuración")]
    public float tiempoPorCliente = 5f;
    public int dineroParaGanar = 100;

    private string[] productos = { "Manzana", "Pan", "Leche" };
    private string productoDeseado;
    private int dinero = 0;
    private int vidas = 3;
    private float tiempoRestante;
    private bool juegoActivo = true;

    void Start()
    {
        NuevoCliente();
        ActualizarUI();
    }

    void Update()
    {
        if (!juegoActivo) return;

        tiempoRestante -= Time.deltaTime;
        barraPaciencia.value = tiempoRestante / tiempoPorCliente;

        if (tiempoRestante <= 0)
        {
            PerderVida("¡El cliente se fue por esperar demasiado!");
        }
    }

    void NuevoCliente()
    {
        productoDeseado = productos[Random.Range(0, productos.Length)];
        textoPedido.text = "Cliente: ¡Quiero " + productoDeseado + "!";
        tiempoRestante = tiempoPorCliente;
    }

    public void VenderProducto(string nombreProducto)
    {
        if (!juegoActivo) return;

        if (nombreProducto == productoDeseado)
        {
            dinero += 20;
            if (dinero >= dineroParaGanar)
            {
                GanarJuego();
                return;
            }
        }
        else
        {
            PerderVida("¡Producto equivocado!");
            return;
        }

        NuevoCliente();
        ActualizarUI();
    }

    void PerderVida(string motivo)
    {
        vidas--;
        ActualizarUI();

        if (vidas <= 0)
        {
            juegoActivo = false;
            textoEstado.text = "GAME OVER\n" + motivo;
        }
        else
        {
            NuevoCliente();
        }
    }

    void GanarJuego()
    {
        juegoActivo = false;
        ActualizarUI();
        textoEstado.text = "¡VICTORIA!\nHas alcanzado la meta de ventas.";
    }

    void ActualizarUI()
    {
        textoDinero.text = "Dinero: $" + dinero;
        textoVidas.text = "Vidas: " + vidas;
    }
}
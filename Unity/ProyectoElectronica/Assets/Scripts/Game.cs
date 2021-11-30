using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    private float nextActionTime = 0.0f;
    public float period = 2f;
    public float[] ingresos = new float[4];
    public float[] sueldos = new float[4];
    public float[] deuda = new float[4];
    public float[] intereses = new float[4];
    public float ingresosTotales;
    public float deudasTotales;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ingresos.Length; i++)
        {
            ingresos[i] = Random.Range(100f, 150f);
            sueldos[i] = Random.Range(5f, 10f);
            deuda[i] = Random.Range(-200f, -100f);
            intereses[i] = Random.Range(1.03f, 1.05f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextActionTime)
        {
            nextActionTime += period;
            for (int i = 0; i < ingresos.Length; i++)
            {
                ingresos[i] += sueldos[i];
                if (deuda[i] < 0)
                    deuda[i] = deuda[i] * intereses[i];
            }
            ingresosTotales = ingresos[0] + ingresos[1] + ingresos[2] + ingresos[3];
            deudasTotales = deuda[0] + deuda[1] + deuda[2] + deuda[3];
        }
    }
}

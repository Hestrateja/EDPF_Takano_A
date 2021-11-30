using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{
    public Game game;
    public Text ganancias;
    public Text deudas;
    public Text gananciasTotal;
    public Text deudasTotal;
    public Image bancoLogo;
    public Image edificio;
    public int currentBancoIndex = 0;
    public int currentEdificioIndex = 0;
    public Sprite[] bancos = new Sprite[4];
    public Sprite[] edificios = new Sprite[3];

    public string tarjeta = "00000000", boton = "0", ejeX = "500", ejeY = "500";
    private bool ejeXflag=false, ejeYflag=false, buttonFlag=false;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 0f;
    }
    void Start()
    {
        bancoLogo.sprite = bancos[currentBancoIndex];
        edificio.sprite = edificios[currentEdificioIndex];
        ganancias.text = game.ingresos[currentBancoIndex].ToString("F2");
        deudas.text = game.deuda[currentBancoIndex].ToString("F2");
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
    }
    private void MoverDerecha()
    {
        if(currentEdificioIndex == 0)
        {
            if (currentBancoIndex < bancos.Length - 1)
            {
                currentBancoIndex++;
                bancoLogo.sprite = bancos[currentBancoIndex];
            }
            else
            {
                currentBancoIndex = 0;
                bancoLogo.sprite = bancos[currentBancoIndex];
            }
        }
        
    }
    private void MoverIzquierda()
    {
        if (currentEdificioIndex == 0)
        {
            if (currentBancoIndex > 0)
            {
                currentBancoIndex--;
                bancoLogo.sprite = bancos[currentBancoIndex];
            }
            else
            {
                currentBancoIndex = bancos.Length - 1;
                bancoLogo.sprite = bancos[currentBancoIndex];
            }
        }

    }
    
    private void MoverAdelante()
    {
        if (currentEdificioIndex < edificios.Length - 1)
        {
            currentEdificioIndex++;
            edificio.sprite = edificios[currentEdificioIndex];
        }

    }
    private void MoverAtras()
    {
        if (currentEdificioIndex > 0)
        {
            currentEdificioIndex--;
            edificio.sprite = edificios[currentEdificioIndex];
        }

    }
    public void Pagar(int indexTarjeta)
    {
        if(currentEdificioIndex==2)
        {
            if(game.ingresos[indexTarjeta]>Mathf.Abs(game.deuda[currentBancoIndex]))
            {
                game.ingresos[indexTarjeta] = game.ingresos[indexTarjeta] + game.deuda[currentBancoIndex];
                game.deuda[currentBancoIndex] = 0;
            }
            else
            {
                game.deuda[currentBancoIndex] = game.deuda[currentBancoIndex] + game.ingresos[indexTarjeta];
                game.ingresos[indexTarjeta] = 0;
            }
            
        }
        
    }
    public void HandleTarjetas(string tarjetas)
    {
        if (tarjetas.Contains("902"))
        {
            //BCP
            Pagar(0);
            Debug.Log("BCP");
        }
        if (tarjetas.Contains("1051"))
        {
            //Nacion
            Pagar(1);
            Debug.Log("Nación");
        }
        if (tarjetas.Contains("2173"))
        {
            //Scotia
            Pagar(2);
            Debug.Log("Scotia");
        }
        if (tarjetas.Contains("1451"))
        {
            //BBVA
            Pagar(3);
            Debug.Log("BBVA");
        }
    }
    public void HandleMovementX(string valEjeX)
    {
        int locEjeX = int.Parse(valEjeX);
        if (locEjeX > 400 && locEjeX < 600)
        {
            ejeXflag = false;
        }
        else
        {
            if (locEjeX < 400 && ejeXflag == false)
            {
                MoverIzquierda();
                ejeXflag = true;
            }
            if (locEjeX > 600 && ejeXflag == false)
            {
                MoverDerecha();
                ejeXflag = true;
            }
        }
    }
    public void HandleMovementY(string valEjeY)
    {
        int locEjeY = int.Parse(valEjeY);
        if (locEjeY > 400 && locEjeY < 600)
        {
            ejeYflag = false;
        }
        else
        {
            if (locEjeY < 400 && ejeYflag == false)
            {
                MoverAdelante();
                ejeYflag = true;
            }
            if (locEjeY > 600 && ejeYflag == false)
            {
                MoverAtras();
                ejeYflag = true;
            }
        }
    }
    public void HandleButton(string button)
    {
        int buttonVal = int.Parse(button);
        if(buttonVal==0&&buttonFlag==true)
        {
            buttonFlag = false;
        }
        if(buttonFlag==false && buttonVal==1)
        {
            if(currentEdificioIndex==0)
            game.ingresos[currentBancoIndex] += 1;
            buttonFlag = true;
        }

    }
    // Update is called once per frame
    void Update()
    {
        ganancias.text = game.ingresos[currentBancoIndex].ToString("F2");
        deudas.text = game.deuda[currentBancoIndex].ToString("F2");
        gananciasTotal.text = game.ingresosTotales.ToString("F2");
        deudasTotal.text = game.deudasTotales.ToString("F2");
        HandleTarjetas(tarjeta);
        HandleMovementX(ejeY);
        HandleMovementY(ejeX);
        HandleButton(boton);
    }
}

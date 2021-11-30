using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;

public class SerialCom : MonoBehaviour
{
    string[] ports;
    bool isConnected = false;
    SerialPort port;
    public Dropdown lista;
    private string texto_entrada;
    string portname;
    public Text text;
    public Mapa map;

    private void Awake() {
        lista.options.Clear();
        ports = SerialPort.GetPortNames();

        foreach (string port in ports)
        {
            lista.options.Add(new Dropdown.OptionData() { text = port });
        }

        DropdownItemSelected(lista);
        lista.onValueChanged.AddListener(delegate { DropdownItemSelected(lista); });
    }

    void DropdownItemSelected(Dropdown lista) {
        int indice = lista.value;
        portname = lista.options[indice].text;
    }
    
    public void conectar() {
        if (!isConnected)
        {
            connect_to_Arduino();
            map.StartGame();
        }
    }

    public void desconectar() {
        if(isConnected){ 
            disconnect_from_Arduino();
        }
    }

    void connect_to_Arduino() {
        isConnected = true;
        port = new SerialPort(portname, 115200, Parity.None, 8, StopBits.One);
        
        port.Open();
        port.Write("#STAR\n");
    }

    void disconnect_from_Arduino() {
        isConnected = false;
        port.Write("#STOP\n");
        port.Close();
    }

    public void readStrintInput(string s) {
        //TO DO: Revisar input text de unity para recuperar data
        texto_entrada = s;
        Debug.Log(texto_entrada);
    }

    public void sendText() {
        if (isConnected) {
            port.Write("#TEXT" + "probando uno dos tres" + "#\n");
            
        }
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
        
    }*/

    // Update is called once per frame
    void Update()
    {
        if (isConnected)
        {
            string tarjeta = port.ReadLine();
            string boton = port.ReadLine();
            string ejeX = port.ReadLine();
            string ejeY = port.ReadLine();
            
                try
                {
                    text.text = tarjeta;
                    map.tarjeta = tarjeta;
                    map.boton = boton;
                    map.ejeX = ejeX;
                    map.ejeY = ejeY;
                }
                catch (System.Exception)
                {
                }
        }
        
        
    }
}

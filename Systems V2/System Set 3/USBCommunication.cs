using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class USBCommunication : MonoBehaviour
{
    public int port = 8888; // Define port for communication
    private TcpListener listener;
    private TcpClient client;
    private bool isServer = false;

    public float connectionRadius = 10f; // Connection radius
    public Transform otherDevice; // Reference to the other USB device
    private bool isConnected;

    async void Start()
    {
        if (isServer)
        {
            StartServer();
        }
        else
        {
            await ConnectToServer("127.0.0.1"); // Replace with other USB device's IP
        }
    }

    void StartServer()
    {
        listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Debug.Log("Server started...");
        AcceptClients();
    }

    async Task ConnectToServer(string serverIp)
    {
        client = new TcpClient();
        await client.ConnectAsync(IPAddress.Parse(serverIp), port);
        Debug.Log("Connected to server.");
        StartReceiving(client);
    }

    async void AcceptClients()
    {
        while (true)
        {
            var incomingClient = await listener.AcceptTcpClientAsync();
            Debug.Log("Client connected.");
            StartReceiving(incomingClient);
        }
    }

    async void StartReceiving(TcpClient connectedClient)
    {
        var stream = connectedClient.GetStream();
        byte[] buffer = new byte[1024];

        while (true)
        {
            int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
            if (bytesRead > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Debug.Log("Received: " + message);
                await SendMessage(connectedClient, "Acknowledged: " + message);
            }
        }
    }

    async Task SendMessage(TcpClient connectedClient, string message)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        await connectedClient.GetStream().WriteAsync(data, 0, data.Length);
        Debug.Log("Sent: " + message);
    }

    void Update()
    {
        CheckConnection();

        if (isConnected)
        {
            // Trigger data transfer
            if (isServer)
            {
                SendMessage(client, "Data from Server");
            }
        }
    }

    void CheckConnection()
    {
        if (otherDevice == null)
        {
            isConnected = false;
            return;
        }

        float distance = Vector3.Distance(transform.position, otherDevice.position);

        if (distance <= connectionRadius)
        {
            isConnected = true;
            float signalStrength = Mathf.Clamp01(1 - (distance / connectionRadius));
            Debug.Log($"Connected to device. Signal Strength: {signalStrength * 100}%");
        }
        else
        {
            isConnected = false;
            Debug.Log("Disconnected from device.");
        }
    }
}

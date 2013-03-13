using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Xml.Serialization;
using UnityEngine;
using System.Linq;

public static class StrifeMasterServer
{
    private const string masterServerPreferenceName = "masterServerAddress";
    private static int masterServerPort = 11417;

    /// <summary>
    /// Returns the IPAddress of the Master Server currently being used.
    /// </summary>
    public static IPAddress MasterServerAddress
    {
        get
        {
            return IPAddress.Parse(PlayerPrefs.GetString(masterServerPreferenceName, "66.61.116.111"));
        }

    }

    /// <summary>
    /// Resets the master server's address to its default.
    /// </summary>
    public static void ResetMasterServerAddress() { PlayerPrefs.SetString(masterServerPreferenceName, "66.61.116.111"); }

    /// <summary>
    /// Sets the master server's address to the given address, if it is a valid IPAddress.
    /// </summary>
    /// <param name="address"></param>
    public static void SetMasterServerAddress(string address)
    {
        IPAddress ip;
        if (IPAddress.TryParse(address, out ip))
        {
            PlayerPrefs.SetString(masterServerPreferenceName, address);
        }
        else
        {
            Debug.LogWarning("Unable to parse IPAddress given for the Master Server. Check the formatting of the IP Address.");
        }
    }

    public static List<ServerInfo> GetMasterServerList()
    {
        TcpClient client = new TcpClient();
        client.Connect(MasterServerAddress, masterServerPort);
        // Translate the passed message into ASCII and store it as a Byte array.
        byte[] data = System.Text.Encoding.ASCII.GetBytes("getserverlist");
        var stream = client.GetStream();
        // Send the message to the connected TcpServer. 
        stream.Write(data, 0, data.Length);
        stream.Flush();

        var bytes = new byte[4096];
        stream.Read(bytes, 0, 4096);
        stream.Flush();
        var response = System.Text.Encoding.ASCII.GetString(bytes);
        XmlSerializer xs = new XmlSerializer(typeof(ServerInfo[]));
        var s = (ServerInfo[])xs.Deserialize(new MemoryStream(bytes));
        return s.ToList();
    }

    public static void RegisterWithMasterServer(int port, string gameName, string gameDescription, string gameType)
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect(MasterServerAddress, masterServerPort);
            var request = "registerserver " + port + " " + gameName + " " + gameDescription + " " + gameType;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(request);
            using (Stream stream = client.GetStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error registering with the Master Server:\n" + e.ToString());
        }
    }

    public static void DeRegisterWithMasterServer(int port)
    {
        try
        {
            TcpClient client = new TcpClient();
            client.Connect(MasterServerAddress, masterServerPort);
            var request = "deregisterserver " + port;
            byte[] data = System.Text.Encoding.ASCII.GetBytes(request);
            using (Stream stream = client.GetStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error deregistering with the Master Server:\n" + e.ToString());
        }
    }
}

public class ServerInfo
{
    [XmlElement("ipAddress")]
    public string ipAddress;
    [XmlElement("port")]
    public int port;
    [XmlElement("gameName")]
    public string gameName;
    [XmlElement("gameType")]
    public string gametype;
    [XmlElement("gameDescription")]
    public string gameDescription;
    [XmlElement("numConnectedPlayers")]
    public int numConnectedPlayers;
    [XmlElement("maxPlayers")]
    public int maxPlayers;

}
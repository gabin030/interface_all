using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace interface_all
{
    internal class scoker_pyserver_Cclient
    {
        public TcpClient client { get; set; }   
        public scoker_pyserver_Cclient()
        {
            client = new TcpClient("localhost", 12345);
        }
        public void SendMessage(string message)
        {
            byte[] data = Encoding.UTF8.GetBytes(message);
            NetworkStream stream = client.GetStream();
            stream.Write(data, 0, data.Length);
        }

        public string ReceiveMessage()
        {
            byte[] responseData = new byte[1024];
            NetworkStream stream = client.GetStream();
            int bytesRead = stream.Read(responseData, 0, responseData.Length);
            return Encoding.UTF8.GetString(responseData, 0, bytesRead);
        }
    }
}

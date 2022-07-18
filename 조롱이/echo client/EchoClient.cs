using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace echo_client
{
    public class EchoClient
    {
        public string IPStr
        {
            get;
            private set;
        }
        public int Port
        {
            get;
            private set;
        }

        public EchoClient(string ipstr, int port)
        {
            IPStr = ipstr;
            Port = port;
        }
        public void Start()
        {
            //소켓 생성
            Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //연결 요청
            //연결할 서버의 소켓 주소를 전달해야 함
            IPAddress ipaddr = IPAddress.Parse(IPStr);
            IPEndPoint servpt = new IPEndPoint(ipaddr, Port);
            sock.Connect(servpt);

            //서버와 메시지 주고 받기
            byte[] packet = new byte[2048];
            while (true)
            {
                Console.Write("☞:");
                string msg = Console.ReadLine();
                MemoryStream ms = new MemoryStream(packet);
                BinaryWriter bw = new BinaryWriter(ms);
                bw.Write(msg);
               // bw.Close();
                //ms.Close();

                sock.Send(packet);

                if (sock.Receive(packet) <= 0)
                {
                    break;
                }
                ms = new MemoryStream(packet);
                BinaryReader br = new BinaryReader(ms);
                string rstr = br.ReadString();
                Console.WriteLine("§:{0}", rstr);
                ms.Close();
                br.Close();
            }
            sock.Close();//연결 닫기
        }
    }
}

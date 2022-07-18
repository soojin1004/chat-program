using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace echo_server
{
    public class EchoServer
    {
        public string IPStr  // IP 주소 입력인자로 전달
        {
            get;
            private set;
        }
        public int Port // 포트 입력인자로 전달
        {
            get;
            private set;
        }
        public EchoServer(string ipstr, int port) // 비대칭 자동속성으로 정의한 멤버에 설정
        {
            IPStr = ipstr;
            Port = port;
        }
        public void Start() // 에코 서버를 가동하는 Start 메서드 추가
        {
            //소켓 생성 - 다른호스트와 통신에 사용할 소켓 생성
             Socket lisock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); // 네트워크 주소 체계, 소켓 타입 결정(전송 방식 선택), 프로토콜 선택

            //소켓과 주소 바인딩
            //주소: IP 주소와 포트번호로 구성
            IPAddress ipaddr = IPAddress.Parse(IPStr);
            IPEndPoint serport = new IPEndPoint(ipaddr, Port);
            lisock.Bind(serport);

            //백로그 큐 크기 결정
            //백로그 큐 - 연결 요청한 클라이언트의 요청을 수락하는 과정동안 기억해야 하는 정보를 보관하는 임시 큐
            lisock.Listen(5);
            while (true)//반복
            {
                //연결 대기 및 수락
                //반환한 소켓으로 실제 클라이언트와 통신을 주고 받음
                Socket dosock = lisock.Accept();
                //현재 연결한 클라이언트와 통신
                DoIt(dosock);
            }
        }

        private void DoIt(Socket dosock) // 실제 연결한 클라이언트와 에코 서비스를 수행하는 DoIt 메서드를 구현
        {

            Console.WriteLine("{0}의 연결 요청 수락", dosock.RemoteEndPoint);
            byte[] pack = new byte[2048]; // 수신할 버퍼 생성
            while (true)
            {
                //클라이언트로부터 데이터를 받음
                dosock.Receive(pack);
                MemoryStream ms = new MemoryStream(pack); // 백업 저장소가 메모리인 스트림을 만듦
                BinaryReader br = new BinaryReader(ms); // 문자열로 변환
                String msg = br.ReadString();
                Console.WriteLine("{0}▶:{1}", dosock.RemoteEndPoint, msg);
                if (msg == "Exit") // Exit가 입력되면 종료
                {
                    break;
                }
                //받은 데이터 다시 클라이언트한테 전송
                dosock.Send(pack);
            }
            //현재 연결한 클라이언트와 연결 종료
            dosock.Close();
        }
    }
}
using System;

namespace echo_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("에코 서버 IPv4 주소:");
            string ipstr = Console.ReadLine();
            int port = 13000;
            Console.Write("포트 번호:");
            int.TryParse(Console.ReadLine(), out port);
            Console.WriteLine("... IPv4:{0} 포트 번호:{1} ...", ipstr, port);
            try
            {
                EchoServer es = new EchoServer(ipstr, port);
                es.Start();
            }
            catch
            {
                Console.WriteLine("정상적으로 에코 서버를 가동하지 못하였습니다.");
            }
        }
    }
}
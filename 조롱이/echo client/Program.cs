using System;

namespace echo_client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("연결할 에코 서버 IPv4 주소:");
            string ipstr = Console.ReadLine();
            int port = 13000;
            Console.Write("포트 번호:");
            int.TryParse(Console.ReadLine(), out port);
            Console.WriteLine("... IPv4:{0} 포트 번호:{1} ...", ipstr, port);

            try
            {
                EchoClient ec = new EchoClient(ipstr, port);
                ec.Start();
            }
            catch
            {
                Console.WriteLine("연결 실패하였습니다.");
            }
        }
    }
}
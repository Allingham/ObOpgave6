using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OO1Classlibrary;

namespace ObOpgave6
{
    class Program
    {
        private static int _sessionId = 1;
        private static int _fanId = 1;
        private static readonly Random rng = new Random();

        public static int SessionID
        {
            get => _sessionId++;
            set => _sessionId = value;
        }

        public static int FanID
        {
            get => _fanId++;
            set => _fanId = value;
        }

        public static readonly List<FanOutput> fanOutputs = new List<FanOutput>()
        {
            new FanOutput(FanID,"Exhaust",rng.Next(15,25),rng.Next(30,80)),
            new FanOutput(FanID,"Exhaust",rng.Next(15,25),rng.Next(30,80)),
            new FanOutput(FanID,"Intake",rng.Next(15,25),rng.Next(30,80)),
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            TcpListener listen = new TcpListener(IPAddress.Loopback, 4646);
            listen.Start();

            while (true)
            {
                Console.WriteLine("Waiting for connection");

                TcpClient newClient = listen.AcceptTcpClient();

                Task.Run(() =>
                {
                    ClientHandler.HandleClient(newClient, SessionID);
                });

                Task.Delay(50);
            }

            //Console.WriteLine(IPAddress.Loopback);
        }

        

    }
}

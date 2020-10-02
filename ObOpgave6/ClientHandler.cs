using System;
using System.IO;
using System.Net.Sockets;
using Newtonsoft.Json;
using OO1Classlibrary;

namespace ObOpgave6
{
    public static class ClientHandler
    {
        public static void HandleClient(TcpClient client, int sessionID)
        {
            Console.WriteLine($"New client with Session ID {sessionID} connected!");

            using (client)
            {
                int currentSession = sessionID;
                NetworkStream stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream);
                StreamReader sr = new StreamReader(stream);
                sw.AutoFlush = true;
                string msgin = "";

                while (msgin != "Stop")
                {
                    sw.WriteLine("Svar venligst med en af foelgende: HentAlle, Hent, Gem eller Stop");

                    msgin = sr.ReadLine();

                    Console.WriteLine($"Session ID: {currentSession}, Message: {msgin}");

                    HandleMessages(msgin, sr, sw);
                }
            }
        }

        private static void HandleMessages(string msg, StreamReader sr, StreamWriter sw)
        {
            switch (msg)
            {
                case "HentAlle":
                    sw.WriteLine("Specifikationen vil have du skriver en tom linje saa det faar du lov til!");
                    sr.ReadLine();
                    sw.WriteLine(JsonConvert.SerializeObject(Program.fanOutputs));
                    break;
                case "Hent":
                    sw.WriteLine($"Hvilket objekt vil du have? (ID)");
                    string reponse = sr.ReadLine();

                    int getById;

                    if (!Int32.TryParse(reponse, out getById))
                    {
                        sw.WriteLine("forkert svar!");
                        break;
                    }
                    

                    FanOutput fanOutputToSend = Program.fanOutputs.Find(fo => fo.ID == getById);

                    sw.WriteLine(JsonConvert.SerializeObject(fanOutputToSend));
                    break;
                case "Gem":
                    sw.WriteLine("Send som json:");
                    string response = sr.ReadLine();
                    FanOutput newFanOutput = JsonConvert.DeserializeObject<FanOutput>(response);
                    newFanOutput.ID = Program.FanID;
                    Program.fanOutputs.Add(newFanOutput);
                    Console.WriteLine($"Saved new FanOutput: {newFanOutput}");

                    break;
                case "Stop":
                    sw.WriteLine("Afslutter forbindelse");
                    break;
                default:
                    break;
            }
        }
    }
}
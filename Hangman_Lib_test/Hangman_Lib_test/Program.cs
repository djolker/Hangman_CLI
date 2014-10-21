using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

//using System.Web;

//using IDM.SkPublish.API;
//using System.Web.Script.Serialization;

namespace Hangman_Lib_test
{
    class Program
    {
        static void Main(string[] args)
        {
            int wins = new int();
            int loss = new int();

            while (true) //Game
            {
                Console.Clear();

                Console.WriteLine("Wins: " + wins.ToString());
                Console.WriteLine(@"Loss's: " + loss.ToString());
                Console.WriteLine("");
                Console.WriteLine("Choose a difficulty");
                Console.WriteLine("1. Easy           1-4 Letters");
                Console.WriteLine("2. Medium         5-6 Letters");
                Console.WriteLine("3. Hard           7-8 Letters");
                Console.WriteLine("4. Expert         9+  Letters");

                int diffchoice = Convert.ToInt32((Console.ReadKey().KeyChar).ToString());

                Hangman game = new Hangman(diffchoice);

                while (true) //Round
                {
                    if (game.getGameOverStatus())
                    {
                        loss++;
                        break;
                    }
                    if (game.checkForCompletion())
                    {
                        wins++;
                        break;
                    }

                    game.newRound();
                }

                Console.Clear();
                Console.WriteLine("Play again? Y/N");

                char choice = Console.ReadKey().KeyChar;
                if (choice == 'y')
                {
                    continue;
                }
                else if (choice == 'n')
                {
                    break;
                }
            }
        }

        #region Test Code
        /// <summary>
        /// Pulls dictionary of words from Cambridge API
        /// </summary>
        //static void getDictionary()
        //{
        //    string baseUrl = @"https://dictionary.cambridge.org/";
        //    string accessKey = "AB0uPWh2AzwtaQptKSdRYAGOgoXXZsPHc8VzSo1tnCeKTcHXPSfqBgWN18T6vcoe";
                
        //    SkPublishAPI api = new SkPublishAPI(baseUrl + "/api/v1/", accessKey);
        //    IDictionary<string, object> results = JsonToObject(api.GetNearbyEntries("american-english", "circa", 5));

        //    int x = 0;
        //}

        ///// <summary>
        ///// Written by Cambridge Software Developer
        ///// </summary>
        ///// <param name="json"></param>
        ///// <returns></returns>
        //private static IDictionary<string, object> JsonToObject(string json)
        //{
        //    JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
        //    IDictionary<string, object> obj = jsonSerializer.Deserialize<IDictionary<string, object>>(json);
        //    return obj;
        //}
#endregion
    }
}
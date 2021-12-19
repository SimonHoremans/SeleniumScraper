using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace scrape
{
    class CommandlineInterface
    {
        public SimpleScraper SharedScraper;
        public DBManager DbManager;
        public static Tuple<string, string> ListMenu(List<string> first, string extraText, Dictionary<string, string> menuOptions)
        {
            string firstStringTemplate = "{0}- {1}\n";
            string firstString = "";
            string menuString = "";
            string returnString;
            string chooseOption = "Choose an option:";
            string wrongOption = "Option not valid, choose again:";
            int numberOfOptionsFirst = first.Count;
            for(int i = 0; i < numberOfOptionsFirst; i++)
            {
                firstString += string.Format(firstStringTemplate, i, first[i]);

            }

            foreach(KeyValuePair<string, string> option in menuOptions)
            {
                menuString += string.Format("{0}:{1} ", option.Value, option.Key);
            }

            firstString += "\n" + extraText;
            firstString += "\n" + menuString + "\n";


            returnString = firstString + chooseOption;
            //Console.Write(returnString);
            //var choice = Console.ReadLine();
            //int choiceNumber;
            bool badOption = true;
            string choice;
            string optionType;

            do
            {
                Console.Write(returnString);
                choice = Console.ReadLine();
                int choiceNumber;

                if (int.TryParse(choice, out choiceNumber))
                {
                    badOption = choiceNumber < 0 || choiceNumber >= numberOfOptionsFirst;
                    optionType = "number";
                }
                else
                {
                    badOption = !menuOptions.ContainsKey(choice);
                    optionType = "menu";
                }
                returnString = firstString + wrongOption;
                Console.Clear();
            } while (badOption);

            return Tuple.Create(optionType, choice);

        }

        public static string ListMenu(List<string> first, string extraText)
        {
            string firstStringTemplate = "{0}- {1}\n";
            string firstString = "";
            string returnString;
            string chooseOption = "Choose an option:";
            string wrongOption = "Option not valid, choose again:";
            int numberOfOptionsFirst = first.Count;
            for (int i = 0; i < numberOfOptionsFirst; i++)
            {
                firstString += string.Format(firstStringTemplate, i, first[i]);

            }

            firstString += "\n" + extraText;


            returnString = firstString + chooseOption;
            //Console.Write(returnString);
            //var choice = Console.ReadLine();
            //int choiceNumber;
            bool badOption = true;
            string choice;

            do
            {
                Console.Write(returnString);
                choice = Console.ReadLine();
                int choiceNumber;

                if (int.TryParse(choice, out choiceNumber))
                {
                    badOption = choiceNumber < 0 || choiceNumber >= numberOfOptionsFirst;
                }
                returnString = firstString + wrongOption;
                Console.Clear();
            } while (badOption);

            return choice;

        }

        public static string TextInput(string inputName)
        {
            Console.Write(inputName);
            var answer = Console.ReadLine();
            Console.Clear();
            return answer;
        }

        public static string ConvertInstanceToString<T>(T instance)
        {
            var returnString = "";
            var insertType = typeof(T);

            foreach (PropertyInfo info in insertType.GetProperties())
            {
                string name = info.Name;
                var value = info.GetValue(instance);
                returnString += string.Format("{0}: {1}\n", name, value);
            }

            return returnString;
        }

        public static string ConvertResultToString<T, D>(Tuple<T, List<D>> result)
        {
            var searchResult = result.Item1;
            var itemsResult = result.Item2;

            string returnString = "";

            returnString += "INFORMATION ABOUT SEARCH\n\n";
            returnString += ConvertInstanceToString<T>(searchResult) + "\n\n\n";

            returnString += "RESULTS OF SEARCH:\n\n";

            foreach(D item in itemsResult)
            {
                returnString += ConvertInstanceToString<D>(item) + "\n";
            }

            return returnString;

        }

        public static Tuple<string, string> ChooseSearch<T>(List<T> instances)
        {
            var searches = new List<string>();
            for(int i = 0; i < instances.Count; i++)
            {
                var instance = instances[i];
                string instanceString = "";
                instanceString += "\n" + ConvertInstanceToString(instance) + "\n\n";
                searches.Add(instanceString);
            }
            var menuOptions = new Dictionary<string, string>
            {
                {"b", "back" }
            };

            var choice = ListMenu(searches, "", menuOptions);

            return choice;
        }

        public static string ShowSearchResults<T, D>(Tuple<T, List<D>> result, Dictionary<string, string> options)
        {
            var resultString = ConvertResultToString(result);
            string optionsString = "";
            string returnString = "";
            string chooseOption = "Choose an option:";
            string wrongOption = "Option not valid, choose again:";

            foreach (KeyValuePair<string, string> option in options)
            {
                optionsString += string.Format("{0}:{1} ", option.Value, option.Key);
            }

            returnString += resultString + "\n" + optionsString + "\n";

            var printString = returnString + chooseOption;
            bool validOption = false;
            string choice = "";

            while(!validOption)
            {
                Console.Write(printString);
                choice = Console.ReadLine();
                validOption = options.ContainsKey(choice);
                printString = returnString + wrongOption;
            }
            Console.Clear();
            return choice;
            
            
        }

        public CommandlineInterface()
        {
            string absolutePath = "C:\\Users\\simeo\\source\\repos\\sqlite_tutorial\\sqlite_tutorial\\Results.db";
            string relativePath = "Results.db";

            string connectionStringTemplate = "Data Source=";
            
            //DbManager = new DBManager(connectionString);
            //SharedScraper = new SimpleScraper();
            //var youtubeScraper = new YoutubeScraper(SharedScraper);
            //var indeedScraper = new IndeedScraper(SharedScraper);
            //var steamScraper = new SteamScraper(SharedScraper);

            while (true)
            {
                DbManager = new DBManager(connectionStringTemplate, relativePath);
                SharedScraper = new SimpleScraper();
                var youtubeScraper = new YoutubeScraper(SharedScraper);
                var indeedScraper = new IndeedScraper(SharedScraper);
                var steamScraper = new SteamScraper(SharedScraper);

                var scraperList = new List<string> { "youtube", "indeed", "steam", "exit" };
                var scraperChoice = ListMenu(scraperList, "");
                if (scraperChoice == "3")
                {
                    SharedScraper.Stop();
                    TextInput("bye");
                    break;
                }
                var taskTypes = new List<string> { "new search", "view searches" };
                var taskTypeChoice = ListMenu(taskTypes, "");

                if (scraperChoice == "0")
                {
                    
                    Youtube(taskTypeChoice, youtubeScraper);
                } else if (scraperChoice == "1"){
                    
                    Indeed(taskTypeChoice, indeedScraper);
                } else if (scraperChoice == "2")
                {
                    
                    Steam(taskTypeChoice, steamScraper);
                }
                
                SharedScraper.Stop();
            }

        }

        public void Youtube(string taskType, YoutubeScraper youtubeScraper)
        {
            if(taskType == "0")
            {
               
                var searchTerm = TextInput("SearchTerm: ");
                var result = youtubeScraper.GetVideos(searchTerm);
                var options = new Dictionary<string, string>
                {
                    {"s", "save" },
                    {"b", "back" }
                };
                var resultsOption = ShowSearchResults(result, options);

                if(resultsOption == "s")
                {
                    DbManager.InsertYoutube(result.Item1, result.Item2);
                    TextInput("Search saved!(press enter to continue)");
                }


            } else if (taskType == "1")
            {
                var searches = DbManager.GetYoutubeSearches();
               
                var options = ChooseSearch(searches);
                var choiceType = options.Item1;
                var choice = options.Item2;
                

                if(choiceType == "number")
                {
                    var choiceNumber = int.Parse(choice);
                    var search = searches[choiceNumber];
                    var videos = DbManager.GetYoutubeVideos(search);
                    var searchTuple = Tuple.Create(search, videos);
                    var menuOptions = new Dictionary<string, string>
                    {
                       
                        {"b", "back" }
                    };
                    ShowSearchResults(searchTuple, menuOptions);
                };
              

                
            }
        }

        public void Indeed(string taskType, IndeedScraper indeedScraper)
        {
            if (taskType == "0")
            {

                var searchTermWhat = TextInput("SearchTerm what: ");
                var searchTermWhere = TextInput("SearchTerm where: ");
                var result = indeedScraper.GetJobs(searchTermWhat, searchTermWhere);
                var options = new Dictionary<string, string>
                {
                    {"s", "save" },
                    {"b", "back" }
                };
                var resultsOption = ShowSearchResults(result, options);

                if (resultsOption == "s")
                {
                    DbManager.InsertIndeed(result.Item1, result.Item2);
                    TextInput("Search saved!(press enter to continue)");
                }


            }
            else if (taskType == "1")
            {
                var searches = DbManager.GetIndeedSearches();

                var options = ChooseSearch(searches);
                var choiceType = options.Item1;
                var choice = options.Item2;


                if (choiceType == "number")
                {
                    var choiceNumber = int.Parse(choice);
                    var search = searches[choiceNumber];
                    var videos = DbManager.GetIndeedJobs(search);
                    var searchTuple = Tuple.Create(search, videos);
                    var menuOptions = new Dictionary<string, string>
                    {

                        {"b", "back" }
                    };
                    ShowSearchResults(searchTuple, menuOptions);
                };



            }
        }

        public void Steam(string taskType, SteamScraper steamScraper)
        {
            if (taskType == "0")
            {

                var posPlayers = new List<string>(SteamScraper.XPathsPlayers.Keys);
                var posTags = new List<string>(SteamScraper.XPathsTags.Keys);
                var posSort = new List<string>(SteamScraper.XPathsSort.Keys);
                var posOffers = new List<string> { "don't show special offers", "show special offers"  };

                var searchTerm = TextInput("searchTerm: ");
                var choicePlayers = new List<string> { posPlayers[int.Parse(ListMenu(posPlayers, ""))] };
                var choiceTags = new List<string> { posTags[int.Parse(ListMenu(posTags, ""))] };
                var choiceSort = posSort[int.Parse(ListMenu(posOffers, ""))];
                var choiceOffers = Convert.ToBoolean(int.Parse(ListMenu(posSort, "")));

                var result = steamScraper.GetGames(searchTerm, choiceOffers, choiceSort, choiceTags, choicePlayers);
                var options = new Dictionary<string, string>
                {
                    {"s", "save" },
                    {"b", "back" }
                };
                var resultsOption = ShowSearchResults(result, options);

                if (resultsOption == "s")
                {
                    DbManager.InsertSteam(result.Item1, result.Item2);
                    TextInput("Search saved!(press enter to continue)");
                }


            }
            else if (taskType == "1")
            {
                var searches = DbManager.GetSteamSearches();

                var options = ChooseSearch(searches);
                var choiceType = options.Item1;
                var choice = options.Item2;


                if (choiceType == "number")
                {
                    var choiceNumber = int.Parse(choice);
                    var search = searches[choiceNumber];
                    var videos = DbManager.GetSteamGames(search);
                    var searchTuple = Tuple.Create(search, videos);
                    var menuOptions = new Dictionary<string, string>
                    {

                        {"b", "back" }
                    };
                    ShowSearchResults(searchTuple, menuOptions);
                };



            }
        }

        private static bool IsNumber(string number)
        {
            var regex = @"^[0-9]+$";
            string testString = "10";
            return Regex.Match(testString, regex).Success;
        }
    }
}

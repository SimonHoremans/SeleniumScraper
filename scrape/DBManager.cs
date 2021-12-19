using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;

namespace scrape
{
    class DBManager
    {
        public static string CreateYoutube = @"
        DROP TABLE IF EXISTS YoutubeSearch;
        DROP TABLE IF EXISTS YoutubeVideo;

        CREATE TABLE YoutubeSearch(
          youtubeSearchID INTEGER PRIMARY KEY,
          searchTerm TEXT
        );

        CREATE TABLE YoutubeVideo(
            youtubeVideoID INTEGER PRIMARY KEY,
            title TEXT,
            link TEXT,
            views TEXT,
            channel TEXT,
            channelLink TEXT,
            youtubeSearchID INTEGER,
            FOREIGN KEY (youtubeSearchID) REFERENCES YoutubeSearch(youtubeSearchID)
        );
        ";

        public static string CreateIndeed = @"
        DROP TABLE IF EXISTS IndeedSearch;
        DROP TABLE IF EXISTS IndeedJob;

        CREATE TABLE IndeedSearch(
          indeedSearchID INTEGER PRIMARY KEY,
          searchTermWhat  TEXT,
          searchTermWhere TEXT
        );

        CREATE TABLE IndeedJob(
	        indeedJobID INTEGER PRIMARY KEY,
	        jobTitle TEXT,
	        company TEXT,
	        companyLink TEXT,
	        location TEXT,
	        joblink TEXT,
	        indeedSearchID INTEGER,
	        FOREIGN KEY (indeedSearchID) REFERENCES IndeedSearch(indeedSearchID)
        );
        ";

        public static string CreateSteam = @"
        DROP TABLE IF EXISTS SteamSearch;
        DROP TABLE IF EXISTS SteamGame;

        CREATE TABLE SteamSearch(
          steamSearchID INTEGER PRIMARY KEY,
          searchTerm  TEXT,
          specialOffers NUMERIC,
          sortBy TEXT,
          tags  TEXT,
          players TEXT
        );

        CREATE TABLE SteamGame(
	        steamGameID INTEGER PRIMARY KEY,
	        link TEXT,
            name TEXT,
            price TEXT,
            steamSearchID INTEGER,
	        FOREIGN KEY (steamSearchID) REFERENCES SteamSearch(steamSearchID)
        );
        ";
        public string ConnectionString;
        public string FilePathDB;
        public DBManager(string connectionStringTemplate, string filePathDB)
        {
            FilePathDB = filePathDB;
            ConnectionString = connectionStringTemplate + filePathDB;
            MakeDB();
        }

        public void MakeDB()
        {
            if(!File.Exists(FilePathDB))
            {
                using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
                {
                    cnn.Open();

                    cnn.Execute(CreateYoutube + CreateIndeed + CreateSteam);
                }
            }
        }

        public YoutubeSearch InsertYoutube(YoutubeSearch youtubeSearch, List<YoutubeVideo> youtubeVideos)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();

                cnn.Execute("insert into YoutubeSearch (searchTerm) values (@SearchTerm)", youtubeSearch);
                var youtubeSearchID = (int)cnn.LastInsertRowId;
                youtubeSearch.YoutubeSearchID = youtubeSearchID;

                foreach (YoutubeVideo youtubeVideo in youtubeVideos)
                {
                    youtubeVideo.YoutubeSearchID = youtubeSearchID;
                    cnn.Execute("insert into YoutubeVideo (title, link, views," +
                        "channel, channelLink, youtubeSearchID) values (@Title, @Link, @Views, @Channel, @ChannelLink," +
                        "@YoutubeSearchID)", youtubeVideo);
                }
            }

            return youtubeSearch;
        }

        public List<YoutubeVideo> GetYoutubeVideos(YoutubeSearch youtubeSearch)
        {
            var youtubeSearchID = youtubeSearch.YoutubeSearchID;

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from YoutubeVideo where youtubeSearchID = {0};", youtubeSearchID);
                
                var output = cnn.Query<YoutubeVideo>(queryString);

                return output.ToList();
            }
        }

        public List<YoutubeSearch> GetYoutubeSearches()
        {

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from YoutubeSearch;");
                var output = cnn.Query<YoutubeSearch>(queryString);

                return output.ToList();
            }
        }

        public IndeedSearch InsertIndeed(IndeedSearch indeedSearch, List<IndeedJob> indeedJobs)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();

                cnn.Execute("insert into IndeedSearch(searchTermWhat, searchTermWhere) values (@SearchTermWhat, @SearchTermWhere)", indeedSearch);
                var indeedSearchID = (int)cnn.LastInsertRowId;
                indeedSearch.IndeedSearchID = indeedSearchID;

                foreach (IndeedJob indeedJob in indeedJobs)
                {
                    indeedJob.IndeedSearchID = indeedSearchID;
                    cnn.Execute("insert into IndeedJob(indeedSearchID, jobTitle, company, companyLink, location, jobLink) values (@IndeedSearchID, @JobTitle, @Company, @CompanyLink, @Location, @JobLink)", indeedJob);
                }
            }

            return indeedSearch;
        }
        public List<IndeedJob> GetIndeedJobs(IndeedSearch indeedSearch)
        {
            var indeedSearchID = indeedSearch.IndeedSearchID;

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from IndeedJob where indeedSearchID = {0};", indeedSearchID);
                
                var output = cnn.Query<IndeedJob>(queryString);

                return output.ToList();
            }
        }

        public List<IndeedSearch> GetIndeedSearches()
        {

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from IndeedSearch;");
                var output = cnn.Query<IndeedSearch>(queryString);

                return output.ToList();
            }
        }

        public SteamSearch InsertSteam(SteamSearch steamSearch, List<SteamGame> steamGames)
        {
            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                cnn.Open();

                cnn.Execute("insert into SteamSearch(searchTerm, specialOffers, sortBy, tags, players) values (@SearchTerm, @SpecialOffers, @SortBy, @Tags, @Players)", steamSearch);
                var steamSearchID = (int)cnn.LastInsertRowId;
                steamSearch.SteamSearchID = steamSearchID;

                foreach (SteamGame steamGame in steamGames)
                {
                    steamGame.SteamSearchID = steamSearchID;
                    cnn.Execute("insert into SteamGame(link, name, price, steamSearchID) values (@Link, @Name, @Price, @SteamSearchID)", steamGame);
                }
            }

            return steamSearch;
        }

        public List<SteamGame> GetSteamGames(SteamSearch steamSearch)
        {
            var steamSearchID = steamSearch.SteamSearchID;

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from SteamGame where steamSearchID = {0};", steamSearchID);
                
                var output = cnn.Query<SteamGame>(queryString);

                return output.ToList();
            }
        }

        public List<SteamSearch> GetSteamSearches()
        {

            using (SQLiteConnection cnn = new SQLiteConnection(ConnectionString))
            {
                string queryString = String.Format("select * from SteamSearch;");
                var output = cnn.Query<SteamSearch>(queryString);

                return output.ToList();
            }
        }

    }
}

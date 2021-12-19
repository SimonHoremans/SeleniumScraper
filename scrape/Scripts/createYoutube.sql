DROP TABLE IF EXISTS YoutubeSearch;
DROP TABLE IF EXISTS YoutubeVideo;

CREATE TABLE YoutubeSearch(
  youtubeSearchID INTEGER PRIMARY KEY,
  searchTerm  TEXT
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
DROP TABLE IF EXISTS youtubeSearch;
DROP TABLE IF EXISTS youtubeResult;

CREATE TABLE youtubeSearch(
  youtubeSearchID INTEGER PRIMARY KEY,
  searchTerm  TEXT
);

CREATE TABLE youtubeResult(
	title TEXT,
	link TEXT,
	views INTEGER,
	channel TEXT,
	channelLinks TEXT,
	youtubeSearchID INTEGER,
	FOREIGN KEY (youtubeSearchID) REFERENCES youtubeSearch(youtubeSearchID)
);
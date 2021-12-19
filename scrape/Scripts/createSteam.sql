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
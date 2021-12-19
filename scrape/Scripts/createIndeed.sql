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
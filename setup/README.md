## Setup

Here we just use a simple local postgresql db to seed our datasets. This is just a quick way of getting something running and working end to end locally and accepts the datasets as is.
Realistically we'd want to preprocess the datasets before they should become available/searchable by our API. But that is it's own distinct architecture & piece of work.

At this point [install PostgreSQL](https://www.postgresql.org/download/) if you do not have it already locally.

Note the following commands should be run inside the psql CLI tool. (\copy doesn't behave well with some tools)

Connect to your local postgres via CLI:
`psql postgres://<username>:<password>@<hostname>:<port>/<dbName>`
Example 
`psql postgres://postgres:pass@localhost:5432/DB_NAME`

``` 
CREATE DATABASE COVID_19;
\c COVID_19
```

```sql
CREATE TABLE IF NOT EXISTS records(
	FIPS text,
	Admin2 text,
	Province_State text, 
	Country_Region text,
	Last_Update timestamp,
	Lat float8,
	Long_ float8,
	Confirmed bigint,
	Deaths bigint,
	Recovered bigint,
	Active bigint,
	Combined_Key text,
	Incident_Rate float8,
	Case_Fatality_Ratio float8);

```

The following can be run with more datasets, just the two included with the setup instructions have been used for example and some local testing.
Example dataset insertions:

```
\copy records(FIPS,Admin2,Province_State,Country_Region,Last_Update,Lat,Long_,Confirmed,Deaths,Recovered,Active,Combined_Key,Incident_Rate,Case_Fatality_Ratio
)
FROM '<YOUR_PATH_HERE>/charp-api/setup/datasets/04-20-2022.csv'
DELIMITER ','
CSV HEADER;
```

```
\copy records(FIPS,Admin2,Province_State,Country_Region,Last_Update,Lat,Long_,Confirmed,Deaths,Recovered,Active,Combined_Key,Incident_Rate,Case_Fatality_Ratio
)
FROM '<YOUR_PATH_HERE>/csharp-api/setup/datasets/04-20-2022.csv'
DELIMITER ','
CSV HEADER;
```

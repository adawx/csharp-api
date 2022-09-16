## Server

Before running, ensure that in `appsettings.json`/`appsettings.Development.json` has the correct relevant data for you local postgres installation. Sensible defaults have been put there, but if there is in issue in connecting this could be it.

The server can be run through either Visual Studio, or the CLI with `dotnet run`.

The API has 2 available endpoints:
`/api/covid/agg` - for aggregated data
`/api/covid/raw` - for raw data


Both endpoints expect the same type of request body, e.g.: 

```json
{
    "region": "Albania",
    "startDate": "2020-01-01",
    "endDate": "2023-01-01"
}
```

Example cURL:
```
curl --location --request GET 'https://localhost:7198/api/covid/agg' \
--header 'Content-Type: application/json' \
--data-raw '{
    "region": "Albania",
    "startDate": "2020-01-01",
    "endDate": "2023-01-01"
}'
```

Aggregated will yield a response showing the total number of cases for the region over the specified dates (inclusive of the end date).

Raw will output an array of the database objects.

### Documentation

Swagger documentation for the API can be found at `<host>:7198/swagger`

### Further notes:

Given this is my first exposure to .NET along with broad requirements I feel there a lot of things to be improved on given better knowledge of the ecosystem and more time.

- Data preprocessing

As stated in the setup readme, the data is just seeded into a DB as is for the purpose of this example. Preprocessing the data and and aligning any null values, etc would want to be done in a pipeline before it makes it into the database powering the API. Also a DB better suited to querying over time ranges might be an improvement but I feel postgres is good enough for this example.
A good example would be dependent on API design. Given the current example a smart preprocessing strategy would be to aggregate regions with multiple provinces/states into a single aggregated record, so then at request time only n days worth of records will need to be aggregated for the response.

- SQL string formatting

Something where I know this could be easily solved with a library to validate inputs to the API aren't going to expose some sort of SQL injection attack, but my knowledge of the ecosystem just isn't there.

- Better error handling 
A bunch of unhandled usecases currently exist:
  - Incorrect date string will just respond a full stacktrace. This should be handled ina  try/catch & an appropriate error response should be returned to the user with some helpful text
  - Same as above, returns a badrequest but doesn't provide helpful output as to why
  - Repository layer has no error handling on whether it can open a connection or not and presumes a healthy postgres connection. This should all ideally be wrapped and log sensible errors inside the postgres repo abstraction
  - Responses should vary depending on outcome and responsed with correct error codes, etc.

### Testing

Below is the thought process for the unit testing strategy. Implementation is TODO

Controller
  - Instantiate HTTP Controller
  - Mock Repository & method responses

  Test Cases:
  - Valid Input (sunny day)
  - Invalid region
    - Empty string, wrong type, etc.
  - Badly formatted date
  - SQL Injection & formatting
  - Validate response object to types

Repository
  - Instantiate Repository
  - Mock postgres (npgsql) connection & responses
  - Test error handling, etc.

Obviously both classes only have one method at the moment so ther wouldn't be an extensive amount of tests, but if we made specific api arguments optional etc we'd need to test those to ensure the API works as expected.

sequenceDiagram
    participant User
    participant UI
    participant API
    participant DB

    CSV->>DataObject: Ingest CSV and parse to data object
    DataObject->>DB: Store Data object in DB

    User->>UI: Enter parameters and search
    UI->>+API: Json API request with Parameters
    API->>API: Build DB query with parameters
    API->>+DB: Query DB
    DB-->>-API: Output records
    API->>API: Build response object
    API-->>-UI: JSON Response
    UI->>UI: Display JSON Response 


            

# Asset Tracker

Example microservices application that uses the domain of an Asset Tracker to show examples

## Asset Service

Single assembly service

- Maintain assets for a customer
  - Create Asset
  - Delete Asset
  - Update Asset

### CRUD Operations and Endpoints

| Method | Request URI                        | Use Case           |
|--------|------------------------------------|--------------------|
| GET    | api/v1/asset                       | Listing assets     |
| GET    | api/v1/asset/{id}                  | Get asset with id  |
| GET    | api/v1/asset/GetAssetByType/{type} | Get assets by type |
| POST   | api/v1/asset                       | Create new asset   |
| PUT    | api/v1/asset                       | Update asset       |
| DELETE | api/v1/asset/{id}                  | Delete asset       |

```powershell

docker pull mongo             # Pull mongo image
docker run -d -p 27017:27017 --name assets-mongo mongo
docker exec -it assets-mongo /bin/bash

mongo                         # Go into mongo commands
show dbs                      # Show the current databases

use AssetsDb                  # Create AssetsDb
db.createCollection('Assets') # Create Assets collections

db.Assets.insert
db.Assets.inserMany([{}])

```

## Scheduler Service

- Create schedule for an Asset
- Create maintenance jobs for an asset on a schedule

| Method | Request URI                        | Use Case                                |
|--------|------------------------------------|-----------------------------------------|
| GET    | api/v1/schedule                    | Get schedule with username              |
| POST   | api/v1/schedule                    | Update schedule and items (add -remove) |
| DELETE | api/v1/schedule/{id}               | Delete a schedule                       |
| DELETE | api/v1/schedule/{id}               | Delete a schedule                       |
| POST   | api/v1/schedule/publish            | Publish the schedule                    |

### Create Schedule

An asset can have a schedule, much like you service a car each year

| Type         | Frequency | Interval   | Duration |
|--------------|-----------|------------|----------|
| Test         | 1         | Yearly     | 60       |
| Calibration  | 6         | Monthly    | 60       |
| Service      | 1         | Yearly     | 180      |

This service will monitor this schedule and create a job record for when the test is due.

### Events

NewJobRequiredEvent

```json
{
    "AssetId",
    "JobType"
    ...
}
```

## Maintenance Service

- Handles asset maintenance jobs
- Keeps records of an assets maintenance history
  - View asset history

### Job

| Date       | Type    | State     |
|------------|---------|-----------|
| 01/01/2022 | Service | Scheduled |

## Customers Service

- Maintain customer records
  - Customers have contacts
  - Customers have addresses

### Customer Data

| Name        | Email                 | Phone      |
|-------------|-----------------------|------------|
| Tony Joanes | tonyjoanes@joanes.com | 1366868343 |

#### Address Data

| PostCode    | Town      | County      |
|-------------|-----------|-------------|
| GU52 12P    | Somewhere | Some County |

## Web Application

- Create customers
- Create customer addresses
- Create assets
- Create asset schedules

## Running things

We're using Docker Compose to run all the services and dependencies

```powershell
docker compose -f.\docker-compose.yml -f .\docker-compose.override.yml up -d
```

Now access the website on `http://localhost:8001/swagger`

Take it down with:

```powershell
docker compose -f.\docker-compose.yml -f .\docker-compose.override.yml down
```

### Mongo Client

You can use the following for a client GUI into Mongodb, run this

```powershell
docker run -d -p 3000:3000 mongoclient/mongoclient
```

Then access the client via `http://localhost:3000`

### Redis

```powershell
docker run -d -p 6379:6379 --name assettracker-redis redis
```

### Postgres

```sql
CREATE TABLE Schedule(
	ID SERIAL PRIMARY KEY NOT NULL,
	Name VARCHAR(24) NOT NULL,
	AssetName VARCHAR(50) NOT NULL,
	Description TEXT,
	Interval INT
)
```

Data:

```sql
INSERT INTO Schedule (name, assetname, description, interval) VALUES ('Basic', 'sink', 'A basic sink clean', 1);

INSERT INTO Schedule (name, assetname, description, interval) VALUES ('Advanced', 'car', 'A car service', 1);
```
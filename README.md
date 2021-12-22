# Asset Tracker

Example microservices application that uses the domain of an Asset Tracker to show examples

## Asset Service

- Maintain assets for a customer
  - Create Asset
  - Delete Asset
  - Update Asset

## Scheduler Service

- Create schedule for an Asset
- Create maintenance jobs for an asset on a schedule

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

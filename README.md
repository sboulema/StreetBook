# StreetBook

A website to gather and show information about all neighbours living in a single street.

[![StreetBook](https://github.com/sboulema/StreetBook/actions/workflows/workflow.yml/badge.svg)](https://github.com/sboulema/StreetBook/actions/workflows/workflow.yml)
[![Sponsor](https://img.shields.io/badge/-Sponsor-fafbfc?logo=GitHub%20Sponsors)](https://github.com/sponsors/sboulema)

## Screenshots
TODO

## Running
TODO

## Data model
All data is loaded from a `people.json` file stored in the designated volume mount.

```
[
  {
    "firstName": "Samir",
    "lastName": "Boulema",
    "houseNumber": 34,
    "mobilePhoneNumber": "+31612345678",
    "licensePlates": ["SH-LS-29"],
    "status": "",
    "hasPicture": true,
    "isDisabled": false,
    "isHidden": false
  },
  {
    ...
  }
]
```

### Person
| Name              | Required | Description |
| ----------------- | -------- | ----------- |
| firstName         | Yes      | First name of the person |
| lastName          | No       | Last name of the person |
| houseNumber       | Yes      | House number where person lives |
| mobilePhoneNumber | No       | Mobile phone number of the person |
| licensePlates     | No       | List of license plates of cars owned by person |
| status            | No       | Status to clarify why a person is disabled or hidden |
| hasPicture        | No       | Boolean indicating if person has a matching image file, used to determine if person takes part in the game |
| isDisabled        | No       | Person will be shown grayed out |
| isHidden          | No       | Person will not be shown |

### Images
A person can have a personal image. The image is linked by the filename following a predefined pattern:
```
<houseNumber>_<firstName>.jpg
```

## Play
TODO

## Volumes / Bind mounts
| Path on container | Description                                    |
| ----------------- | ---------------------------------------------- |
| /data             | people.json and images are read from this path |

## Caching
Data loaded will be cached in memory, if changes are made you can clear the cache by navigating to `<host>/cache/clear`

## Building

### Dependencies
- DotNet 8
- NodeJS

### Configuration
Create an appsettings.Development.json file, fill in a password.

```
{
  "Password": "SECRET"
}
```

### Debugging
Start debugging the project (Visual Studio, Visual Studio Code, DotNet CLI) and navigate to https://localhost:5001
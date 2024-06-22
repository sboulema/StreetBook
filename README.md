# StreetBook

A website to gather and show information about all neighbours living in a single street.

## Screenshots
TODO

## Data model

### Person
```
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
}
```

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
# Time tracking App for android and ios

![alt text](https://github.com/viktorseidl/Xamarin-TimeTracking-App/blob/main/IMAGES/mokupsdark.png "Mockup")

## Description

This version is a prototype of a time tracking application. This app was developed using Xamarin .NET/C#. A REST-API interface was implemented in the backend for communication with a MySQL database. The software was developed as an extension to the company's existing web application.

## Requirements

The requirements of the prototype were:

- [x] Login with chip-nr and password
- [x] Login with personalized QR-code
- [x] Come and go function
- [x] QR-code creation
- [x] Formatting of the 20 year old main database must be preserved

## REST-API

The REST API interface was developed in PHP 8.1 and uses the PDO class for database queries. All database queries of the application are controlled via the employee class. The content type of the REST API is set to "application/JSON" and is queried using http requests (POST,GET).

As an additional safeguard against SQL injections, all queries are controlled using checksums. For example:

1. When querying the database, the employee ID + password was hashed using the SHA256 hash function.
2. The REST API would use the MD5 hash function and re-encrypt the string.
3. The database query would then look like this, for example:

```SQL
"SELECT * FROM TABLE WHERE md5(SHA2(CONCAT(employeeId,password),256)) = :Str LIMIT 1"
```

## NuGet Packages

The following NuGet packages were used within the application:

- [x] newtonsoft.json.13.0.1 (The MIT License (MIT))
- [x] xamarin.essentials.1.7.0 (The MIT License (MIT))
- [x] zxing.net.mobile.2.4.1 (The MIT License (MIT))
- [x] zxing.net.mobile.forms.2.4.1 (The MIT License (MIT))

## Diagrams

### Use Case Diagram

![alt text](https://github.com/viktorseidl/Xamarin-TimeTracking-App/blob/main/IMAGES/ablauf.png "Use Case Diagram")

### Component Diagram

![alt text](https://github.com/viktorseidl/Xamarin-TimeTracking-App/blob/main/IMAGES/komponentendiagram.png "Component Diagram")

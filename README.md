# Apartment Management System

.Net 6 and MVC

## Features

- Admin can add apartments and users.
- Admin can manage users and apartments.
- Admin can assign apartments to users.
- Admin can add costs for apartments.
- Admin can see all mesages from user.
- Admin can export reports paid costs as excel/pdf.
- Admin needs to forward users password after registered them.
- Users can login system with the password that admin gave him/her.
- System automatically sends email daily for unpaid costs.
- Users can send messages to Admin and can see messages read or not.
- Users can pay their apartment costs.

## Tech Stack
Project uses below libraries and tech stack
- [.Net 6] - .net library
- [.Net Identity] - for user authentication and authorization
- [MsSQL] - Relational databse for apartment-management-system
- [MongoDB] - Document based database for credit-card-service
- [XUnit] - Testing tool
- [Moq] - Mocking framework for unit testing
- [Hangfire] - Recurring job to send email
- [MailKit] - To send email to users periodically
- [Syncfusion.PDF] - For pdf report
- [Fingers10.Excel] - For excel report (It works only windows platforms)
- [PasswordGenerator] - Standart password generator library
- [Bootstrap] - FE framework for css 

## Admin Password 
email:    admin@aps.com
password: admin

## Example User Passwords
email:    luffytaro@aps.com
password: test
email:    yokotoro@aps.com
password: test

## Library Tables [AspNetUser] - [AspNetRoles] - [AspNetUserRoles]
## Custom Tables

| Table Name | Columns |
| ------ | ------ |
| Apartment | [ID][ApartmentNumber][BlockNumber][Floor][Type][Status][UserId] |
| ApartmentCost | [ID][CostType][Amount][IsPaid][Month][ApartmentId] |
| Message | [ID][Description][Status][UserId] |


## Installation Steps

Aparment Management System requires .net 6.0.x and docker


docker mongo installation:

```sh
docker run -it --rm --name mongo-express --link web_db_1:mongo -p 8081:8081 -e ME_CONFIG_MONGODB_URL="mongodb://mongo:27017" -e ME_CONFIG_OPTIONS_EDITORTHEME="ambiance" -e ME_CONFIG_BASICAUTH_USERNAME="user" -e ME_CONFIG_BASICAUTH_PASSWORD="password" mongo-express
```

docker mssql installation:

```sh
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=MyPass@word" -e "MSSQL_PID=Express" -p 1433:1433 -d --name=sql mcr.microsoft.com/mssql/server:latest
```
First Run: 
credit-card-service

```sh
https://localhost:44379/swagger/index.html
```
Second run:
apartment-management-service
```sh
add-migration initial
```
```sh
update-database
```
```sh
https://localhost:44396/
```


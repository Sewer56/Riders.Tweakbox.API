# About

Riders.Tweakbox.API is a work in progress Web API service that provides various services to [Riders.Tweakbox](https://github.com/Sewer56/Riders.Tweakbox), an all in one Netplay mod for Sonic Riders.

It was created mostly for the purpose of a goal of creating something while trying out ASP.NET for the first time; but over time, turned to be a more of a fully fledged project.

# Functionality

✔ User Registration & Password Reset\
✔ Mail Service (Password Reset, Registration Confirmation)\
✔ Geolocation & Self-updating GeoIP Database\
✔ Transparent to end-users & (hopefully) GDPR Compliant.\
✔ Live Server Browser\
✔ Ranked Matchmaking
  - Solo, 1v1, 2v2, 2v2v2, 2v2v2v2 etc.

✔ Match Result Tracking
  - Including stats such as gear, character, fastest lap etc.
  
✔ Player Rating System (TrueSkill)\
✔ Built-in Swagger API Testing Site\
✔ Built-in .NET SDK/Library

As the main project (Riders.Tweakbox) is still not fully mature in terms of Netplay stability, it will take some time for ranked play to be implemented; however, it will happen eventually.

# Configuring the Project

## Edit The Configuration

Before deploying your own server, you should first make a few changes to the default config file, `appsettings.json`.

### Configure Let's Encrypt SSL

This project uses [LettuceEncrypt](https://github.com/natemcmaster/LettuceEncrypt) in order to create and renew SSL certificates.

Change the following in the configuration as needed:

```json
"UseLettuceEncrypt": true,
"LettuceEncrypt": {
  "AcceptTermsOfService": true,
  "DomainNames": [ "tweakbox.sewer56.moe" ],
  "EmailAddress": "admin@sewer56.dev"
},
```

Otherwise if you intend on obtaining a SSL cert through other means, set `UseLettuceEncrypt` to `false` and uncomment the `Certificates` section in the same config file. 

Use [Certificate Sources on MSDN](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel/endpoints?view=aspnetcore-5.0#certificate-sources) as reference.

### Set Default Admin User

When the backing database is first created, a default admin user is created which has additional permissions (such as deleting matches).

You should change the default settings here.c

```json
"AdminUser": {
  "AdminEmail": "admin@sewer56.dev",
  "Username": "Sewer56",
  "Password": "PasswordHere1"
},
```

### Set Secret Key

You will need to set an arbitrary secret key which will be used for JSON Web Token verification; as this API uses JSON Web Tokens for authorization.

```json
"JwtSettings": {
  "Secret": "YOUR SECRET KEY HERE",
  "TokenLifetime": "00:59:59"
},
```

**This key is a SECRET**, do not share this key with anyone.

### Add Mail Provider Settings
In order to send registration confirmation and password reset tokens, you will need to add a mail service. Personally I use Namecheap's private email.

Riders.Tweakbox.API uses SMTP in order to send address from using a mail server. You should check the SMTP settings of your email provider.

Sample config:

```json
"MailSettings": 
{
  "Host": "mail.privateemail.com",
  "Port": 587,
  "EnableSSL": true,
  "Username": "admin@totallyarealdomain.com",
  "Password": "totallyARealTokenOrPassword"
}
```

### Geo IP Location Service

Tweakbox API uses MaxMind's `GeoLite2-City` database for automatically assigning country information when a user registers.

```json
"GeoIpSettings": {
  "LicenseKey": "",
  "CronUpdateScheduleUtc": "0 6 ? * WED"
},
```

The API is set to automatically update this database weekly.
You should create an account on the MaxMind site and create a new license key from the account menu.

# Linux Setup Instructions

This is documentation for personal use.<br/>
Based on Ubuntu Server 20.04.

## Install Snap

In case it's not already pre-installed.

```
sudo apt update
sudo apt install snapd
```

## Installing .NET Core SDK

```
sudo snap install dotnet-sdk --channel=5.0/stable --classic
snap alias dotnet-sdk.dotnet dotnet
```

### Setting up a Snap Alias [Optional]
If you are hosting other servers using .NET on the same machine, you will have to use aliases to run multiple versions side by side.

```
snap set system experimental.parallel-instances=true

snap install --unaliased dotnet-sdk_21 --channel=2.1/stable --classic
snap install --unaliased dotnet-sdk_50 --channel=5.0/stable --classic

// Alias to dotnet_21 and dotnet_50
snap alias dotnet-sdk_21.dotnet dotnet_21
snap alias dotnet-sdk_50.dotnet dotnet_50
```

**Make sure to use those aliases in future scripts.**

## Launching Server at Startup

### First create the bootup script.

`> nano /opt/start-tweakbox-on-boot.sh`

```sh
#!/bin/sh

DATE=`date '+%Y-%m-%d %H:%M:%S'`
echo "BaGet Service Started at ${DATE}" | systemd-cat -p info

cd /opt/tweakbox-api/
dotnet /opt/tweakbox-api/Riders.Tweakbox.API.dll
```

### Then create a systemd service.

`> sudo nano /lib/systemd/system/tweakbox.service`

```ini
[Unit]
Description=Tweakbox Server.

[Service]
Type=simple
ExecStart=/bin/sh /opt/start-tweakbox-on-boot.sh

[Install]
WantedBy=multi-user.target
```

### Then enable and start the service.

```
sudo systemctl enable tweakbox.service
sudo systemctl start tweakbox.service
```

## Maintenance

### Updating BaGet-Reloaded

First stop the active server process:
```
sudo systemctl stop tweakbox.service
```

Then, using SFTP (or other method), overwrite published version in `/opt/tweakbox-api`.

Lastly, restart the service.

```
sudo systemctl start tweakbox.service
```

### Freeing Up Disk
To free up disk space, might be a good idea (on Ubuntu Server) to clean up old Snaps after updates.

```
chmod +x remove-old-snaps
sudo ./remove-old-snaps
```

```sh
#!/bin/sh
# Removes old revisions of snaps
# CLOSE ALL SNAPS BEFORE RUNNING THIS
set -eu
LANG=en_US.UTF-8

snap list --all | awk '/disabled/{print $1, $3}' |
while read snapname revision; do
    snap remove "$snapname" --revision="$revision"
done
```

# Development Guidelines

## Project Layout: Key Components
This project loosely follows the [Clean Architecture Layout by Jason Taylor.](https://github.com/jasontaylordev/CleanArchitecture)

**Riders.Tweakbox.API**: The top layer housing the main ASP.NET Application. This is the presentation layer that exposes the functionality (API) to the outside world.

**Riders.Tweakbox.Application**: Contains all application logic. Depends on the domain layer only. This layer defines interfaces that are implemented by outside layers. For example, if the application needed to have a new service, a new interface would be added to Application and an implementation would be created within Infrastructure.

**Riders.Tweakbox.Infrastructure**: Contains the implementations of the interfaces defined in the Application project; enforcing separation of interface and implementation. This is where the platform lives (databases, I/O, web services) and accessing of external resources occurs.

**Riders.Tweakbox.Domain**: This contains all entities, enums, exceptions, interfaces, types and logic specific to them.

## Project Layout: Misc Components

**Riders.Tweakbox.API.Application.Commands:** Provides models for all requests and responses that can be made through the API.

**Riders.Tweakbox.API.Application.SDK:** First party library to integrate/use the API.

## Running Migrations
dotnet ef migrations add InitialCreate --project Riders.Tweakbox.API.Infrastructure --startup-project Riders.Tweakbox.API
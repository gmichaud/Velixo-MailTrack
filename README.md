Velixo MailTrack for Acumatica
======================================
A customization that integrates Acumatica with SendGrid to get detailed delivery, open and click-through information on your Acumatica e-mails. Also leverages Chrome push notifications to notify users instantly when one of the supported event occurs.

### Prerequisites
* Acumatica 6.1 or later (tested with 6.10.1219, 17.205.0015)
* (Optional) For push notifications, your site must use HTTPS and Google Chrome for Windows/Mac/Android is required

Quick Start
-----------

### Installation
The latest version of the customization package is available on the [releases](https://github.com/gmichaud/Velixo-MailTrack/releases) page.

### Configuring SendGrid
A SendGrid account is required to use this customization. Your account should be configured like any other SMTP mail server in the System Email Accounts (SM204002) page. For more information, review the instructions in the [Acumatica help file](https://help.acumatica.com/?ScreenId=ShowWiki&pageid=77f0cf69-a363-4b12-9241-2ff4dd54d8ae). The SendGrid SMTP server is smtp.sendgrid.net. Use the username apikey and your [SendGrid API key](https://app.sendgrid.com/settings/api_keys) as password to authenticate with SendGrid.

Event notification needs to be enabled in the [mail settings page](https://app.sendgrid.com/settings/mail_settings) of SendGrid:

![SendGrid Event Notification Configuration](http://gmichaud.github.com/images/mailtrack/sendgridmailsettings.png)

The HTTP POST URL should correspond to your Acumatica web site URL with /mailtrack/event at the end. HTTP Basic Authentication is used to authenticate with your Acumatica web site and a limited-access user account should be created to receive the events from SendGrid. Here is an example HTTP POST URL:

https://myacumaticauser:myacumaticapassword@myerp.acumatica.com/mailtrack/event

The following actions/events are handled by MailTrack:
* Dropped
* Delivered
* Bounced
* Opened
* Clicked

### Using MailTrack
MailTrack adds a new Tracking tab to the e-mail activity page. Events that are received from SendGrid will be automatically added to the relevant e-mail, provided that this e-mail was originally sent through SendGrid:

![Tracking Tab](http://gmichaud.github.com/images/mailtrack/trackingtab.png)

### Configuring Chrome Push Notifications
Push Notifications are only available on Google Chrome for Windows/Mac/Android. Other browsers do not supported the required features. Please also note that your website must be accessed using HTTPS

![User Profile](http://gmichaud.github.com/images/mailtrack/userprofile.png)

![Enabling Push Notifications](http://gmichaud.github.com/images/mailtrack/enablepush.png)

![Sample Notification](http://gmichaud.github.com/images/mailtrack/samplenotification.png)

![Notification Center](http://gmichaud.github.com/images/mailtrack/notificationcenter.png)

Support
-----------
Corporate users of Velixo MailTrack that rely on the customization for their operations may want to enter into a formal support agreement. I offer a technical support contract for an annual subscription fee, which ensures:
* Continuity of the project, with ongoing development and maintenance.
* Access to direct assistance and support in using the add-in.
* Priority for bug-fixes and feature requests that you submit.

For more details, please contact me directly by email at gabriel@velixo.com.

Known Issues
------------

## Copyright and License

Copyright © `2018` `Velixo`

This component is licensed under the GPLv3 License, a copy of which is available online at https://github.com/gmichaud/Velixo-Reports/blob/master/LICENSE.md

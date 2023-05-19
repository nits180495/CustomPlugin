# AdsFramework
Ads Framework is used for implemeting Google Ads.
There are three type of ads that can be implemted by using this framework.
a. Rewards Ads
b Intertitial Ads
c. Banner Ads

## How to use this
You can use this framework by implemeting scripts based on your requirements.
The Initialisation is important for any ads to implement.
"AdsInitializer.cs" can be attached to gameobject and initialise the mobile ads sdk.
Then call the methods to show ads.
You can config App ID's, adsUnit ID's and Test Device ID's.

### Requirements
Install Google Mobile Ads Unity plugin from site or github.
Resovle all dependency and import this framework.

#### Setup
Create a "Resources" folder in Assets.
Create Scriptable object by right clicking inside of "Resources" folder, then click on "Creaete" option on top.
You will see two option on top "AdsData" and "DeviceIdsData".
Click on both to create scriptable objects and set config data.
Set test and live Ids according to requirements.
Add "AdsInitializer.cs" script for initialisation og Mobile ads sdk.
Now you can load any ads by calling appropiate methods.
For live/production Ads, pelase check the Scriptable object "AdsData".
By default it will be Test Ads.

##### Notice
Please properly install Google AsdMob sdk and resovle all dependency. 
If you face any issue regarding jdk or gradle, then try to open prefrences and copy the path for jdk. 
Then uncheck jdk option and past url in jdk path feild. 
Then try to force resolve dependency.
Also make sure you have added the Ads App ID in googl ads settings. 
For this go Assets > Google Mobile Ads > Settings.


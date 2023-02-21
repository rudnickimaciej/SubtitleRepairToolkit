
# SubtitleRepairToolkit
Program for fixing subtitles time (SRT). 

This program allows you to update SRT file so that it matches the movie timeline. 

## How to use

1. Update App.config
    1. SourcePath - path to the SRT file that needs to be fixed
    2. TargetPath - path to the new SRT file that will be created
    3. Time - number of seconds the SRT file will be moved. It can be negative or positive and accurate to 3 numbers after the decimal point
2. Run the program


## Example
Below is a fragment of the sample SRT file.  

```srt
23

00:03:39,364 --> 00:03:40,782

A może bezpieczniej?

24

00:03:44,119 --> 00:03:45,704

Jak czujecie się teraz?

25

00:03:50,542 --> 00:03:53,337

Na szczęście dla was nie jestem z Fedry.
```

It turns out the time does not match the movie timeline. After detailed investigation it turns out that in order for this to work we need to move  the subtitles timeline for -2.55 seconds. It means that each of the time in the example needs to have 2.55 second substracted from it.

All we have to do is to update parameters values in App.config

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
	<appSettings>
		<add key="SourcePath" value="C:\subtitlesWithWrongTime.srt"/>
		<add key="TargetPath" value="C:\subtitlesWithCorrectTime.srt"/>
		<add key="Time" value="-2.55"/>

	</appSettings>
</configuration>
```
```srt

23

00:03:36,814 --> 00:03:38,232

A może bezpieczniej?

24

00:03:41,569 --> 00:03:43,154

Jak czujecie się teraz?

25

00:03:47,992 --> 00:03:50,787

Na szczęście dla was nie jestem z Fedry.
```

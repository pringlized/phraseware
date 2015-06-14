# phraseware
A Diceware inspired command-line password generator in python and C#

Diceware home page: <a href="http://world.std.com/~reinhold/diceware.html">http://world.std.com/~reinhold/diceware.html</a>

Wikipedia: <a href="http://en.wikipedia.org/wiki/Diceware">http://en.wikipedia.org/wiki/Diceware</a>

Had a couple hours to burn and wanted to work on something random (pun intended).  Read something interesting about a password generation algorithm call Diceware. Thought about it and, it's not really difficult nowadays to create tough passwords but they are rarely if ever rememberable.

So I thought I'd whip one together for fun.  I'm not a cryptologist or a mathematician for that matter. It's really simple but works.  Haven't tested the true entropy of the random numbers generated, but it does create some interesting and unique pass phrases.  I'll have to calculate the "maths" another day.


### C-Sharp

Clone the repo, and build.  I used Xamarin to write this, so I just ran it from bin/Debug under the solution folder. I had no need at this time to set up a full build process.  To run from where ever you saved it, open a terminal and from that directory:

```
$ mono Phraseware.exe
```


### Python
```
$ python phraseware.py
```


#### Example output
```
Let's make a Diceware inspired password...
------------------------------------------
How many words do you want to use [5 - 12]: 7

Your phrase:  buck lava libido await waxen levi tour
Phrase length: 39
------------------------------------------
```

Possible ways to improve:
- Abilty to change language desired & ability to mix and match portions of languages
- Allow for caps at individual or certain locations
- Insert characters at chosen or random locations
- Set minimum & maximum word lengths

Future plans:
- Whip together a cross platform GUI

# phraseware
A Diceware inspired C# password generator

Passphrase home page: <a href="http://world.std.com/~reinhold/diceware.html">http://world.std.com/~reinhold/diceware.html</a>

Wikipedia: <a href="http://en.wikipedia.org/wiki/Diceware">http://en.wikipedia.org/wiki/Diceware</a>

Had a couple hours to burn and wanted to work on something random (pun intended).  Read an interesting forum post last night about a password generation algorithm call Diceware. It not difficult nowadays to create tough passwords but they are rarely if ever rememberable.

So thought I'd whip one together for fun.  I'm not a cryptologist or a mathematician for that matter. It's really simple but  works.  The randomness of the numbers feels like it could be better.  Will have to calculate the maths another day.

Example output:
```
Let's make a Diceware inspired password...
How many words do you want to generate (between 5 & 12):
7
Your phrase:  olav ninth earn nancy occur rear radon
```

Interesting ways I might improve it:
- Set minimum & maximum word lengths
- Expand the individual seed dictionary size
- Mix and match portions of languages other than english
- Allow for Caps at individual or certain locations
- Insert characters at chosen or random locations

Future plans:
- Whip together a GUI in Mono for cross platform usage

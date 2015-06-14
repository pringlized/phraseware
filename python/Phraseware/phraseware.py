import sys
import csv
import random
import constants

# init vars
numWords = 0
wordDict = {}
wordKeys = []
thePhrase = ""

print "\nLet's make a Diceware inspired password..."
print "------------------------------------------"

# ask for how many words to use and validate
while True:
    try:
        numWords = int(raw_input("How many words do you want to use [5 - 12]: "))
    except ValueError:
        print constants.INPUT_ERR_MSG
        continue
    else:
        if (constants.MIN_NUM_WORDS <= numWords) and (numWords <= constants.MAX_NUM_WORDS):
            break
        else:
            print constants.INPUT_ERR_MSG
            continue

# read in the dictionary from csv file
try:
    #TODO: Validate proper csv format in a function
    with open(constants.DICT_FILE, mode='r') as infile:
        reader = csv.reader(infile)
        wordDict = {rows[0]:rows[1] for rows in reader}
except IOError as e:
    sys.exit(e)

# roll the dice 5 times for each word and create keys
for i in range(0, int(numWords)):
    theWord = ""
    for r in range(constants.NUM_ROLLS):
        # get random number
        ranNum = str(random.randint(1, constants.NUM_SIDES))
        theWord = theWord + ranNum

    wordKeys.append(theWord)

# get words from list based on key
for key in wordKeys:
    thePhrase = thePhrase + " " + wordDict[key]

# echo the phase
print "\nYour phrase: " + thePhrase
print "Phrase length: " + str(len(thePhrase))
print "------------------------------------------"
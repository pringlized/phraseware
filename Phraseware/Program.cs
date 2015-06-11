using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace Phraseware
{
	class MainClass
	{
		private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

		Dictionary<string, string> wordListTemp = new Dictionary<string, string> ();

		private static byte numSides = 6;
		private static byte minNumWords = 5;
		private static byte maxNumWords = 12;

		public static void Main (string[] args)
		{
			Console.WriteLine ("Let's make a Diceware inspired password...");

			// read wordList CSV file into array
			Dictionary<string, string> wordList = readDictionaryFile(new Dictionary<string, string> (), "word_list.csv");

			// ask how many words
			Console.WriteLine("How many words do you want to generate (between 5 & 12):");
			string consoleInput = Console.ReadLine ();

			int numWords;
			int.TryParse (consoleInput, out numWords);
			while ( !(numWords >= minNumWords) || !(numWords <= maxNumWords) ) 
			{
				Console.WriteLine("Please enter a number between " + minNumWords + " and " + maxNumWords + ":");
				consoleInput = Console.ReadLine();
				int.TryParse (consoleInput, out numWords);
			}

			// generate keys to find word in word list
			string[] wordKeys = generateKeys(wordList, numWords);

			// Get Words
			string thePhrase = getPhrase(wordList, wordKeys);

			// echo out to console
			Console.WriteLine ("Your phrase: " + thePhrase);
			Console.WriteLine ("Phrase length: " + thePhrase.Length + " characters");
		}

		private static Dictionary<string, string> readDictionaryFile(Dictionary<string, string> wordList, string fileName)
		{
			string line;
			var filestream = new FileStream(fileName,
				FileMode.Open,
				FileAccess.Read,
				FileShare.ReadWrite);
			var file = new StreamReader(filestream, Encoding.UTF8, true, 128);

			while ((line = file.ReadLine()) != null)
			{
				string[] values = line.Split (',');
				wordList.Add (values[0], values[1]);
			}

			return wordList;
		}

		private static string[] generateKeys(Dictionary<string, string> wordList, int numWords)
		{
			string[] wordKeys = new string[numWords];

			for (var i = 0; i < numWords; ++i) 
			{
				string theWord = "";

				// roll the dice 5 times
				for (var r = 1; r < numSides; ++r) 
				{
					byte rolledNum = RollDice(numSides);
					theWord += System.Convert.ToString(rolledNum);
				}

				// add new number to array of wordKeys
				wordKeys[i] = theWord;
			}

			return wordKeys;
		}

		// get random number from cryptology class
		public static byte RollDice(byte numSides) 
		{
			// array holding random value
			byte[] randomNumber = new byte[1];

			do 
			{
				// get a random value
				rngCsp.GetBytes(randomNumber);
			} 
			while(!isFairRoll(randomNumber[0], numSides));

			return (byte)((randomNumber [0] % numSides) + 1);
		}

		//TODO: write explantion for how this works
		public static bool isFairRoll(byte roll, byte numSides)
		{
			int fullSetOfValues = Byte.MaxValue / numSides;
			return roll < numSides * fullSetOfValues;
		}

		// match up each word dice roll to word in dictionary
		private static string getPhrase(Dictionary<string, string> wordList, string[] wordKeys)
		{
			string thePhrase = "";
			foreach (string key in wordKeys) 
			{
				thePhrase += " " + wordList [key];
			}
			return thePhrase;
		}
	}
}

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
		private static Dictionary<string, string> dictList = new Dictionary<string, string> ();

		private static byte numSides = 6;
		private static byte minNumWords = 5;
		private static byte maxNumWords = 12;

		private static string dictionaryFileName = "dictionary.eng.csv";

		public static void Main (string[] args)
		{
			Console.WriteLine ("Let's make a Diceware inspired password...");
			Console.WriteLine ("------------------------------------------");

			// read dictionary CSV file into array
			LoadDictionaryFile();

			// ask how many words within a rango
			Console.WriteLine("How many words do you want to generate (" + minNumWords + " and " + maxNumWords + "):");
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
			string[] wordKeys = GenerateKeys(numWords);

			// Get Words
			string thePhrase = GetPhrase(wordKeys);

			// echo out to console
			Console.WriteLine ("\nYour phrase: " + thePhrase);
			Console.WriteLine ("Phrase length: " + thePhrase.Length + " characters");
		}

		private static void LoadDictionaryFile()
		{
			string line;
			var filestream = new FileStream(dictionaryFileName,
				FileMode.Open,
				FileAccess.Read,
				FileShare.ReadWrite);
			var file = new StreamReader(filestream, Encoding.UTF8, true, 128);

			while ((line = file.ReadLine()) != null)
			{
				string[] values = line.Split (',');
				dictList.Add (values[0], values[1]);
			}
		}

		private static string[] GenerateKeys(int numWords)
		{
			string[] wordKeys = new string[numWords];

			for (int i = 0; i < numWords; ++i) 
			{
				string theWord = "";

				// roll the dice
				for (int r = 1; r < numSides; ++r) 
				{
					byte rolledNum = RollDice(numSides);
					theWord += System.Convert.ToString(rolledNum);
				}

				// add new number to array of wordKeys
				wordKeys[i] = theWord;
			}

			return wordKeys;
		}

		// <summary>
		// get random number from cryptology class
		// </summary>

		public static byte RollDice(byte numSides) 
		{
			// array holding random value
			byte[] randomNumber = new byte[1];

			do 
			{
				// get a random value
				rngCsp.GetBytes(randomNumber);
			} 
			while(!IsFairRoll(randomNumber[0], numSides));

			return (byte)((randomNumber [0] % numSides) + 1);
		}
			
		public static bool IsFairRoll(byte roll, byte numSides)
		{
			int fullSetOfValues = Byte.MaxValue / numSides;
			return roll < numSides * fullSetOfValues;
		}

		// <summary>
		// match up each word dice roll to word in dictionary
		// </summary>

		private static string GetPhrase(string[] wordKeys)
		{
			string thePhrase = "";
			foreach (string key in wordKeys) 
			{
				thePhrase += " " + dictList [key];
			}
			return thePhrase;
		}
	}
}

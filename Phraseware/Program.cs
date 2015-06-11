using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Phraseware
{
	class MainClass
	{
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
			while ( !(numWords >= 5) || !(numWords <= 12) ) {
				Console.WriteLine("Please enter a number between 5 and 12:");
				consoleInput = Console.ReadLine();
				int.TryParse (consoleInput, out numWords);
			}

			// generateWordKeys
			string[] wordKeys = generateKeys(wordList, numWords);

			// Get Words
			string thePhrase = getPhrase(wordList, wordKeys);

			// echo out to console
			Console.WriteLine("Your phrase: " + thePhrase);
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

			for (var i = 0; i < numWords; ++i) {
				string theWord = "";

				// roll the dice 5 times
				for (var r = 1; r < 6; ++r) {
					// Random should not be placed here, but using GUID currently to seed a decent randomness)
					Random rnd = new System.Random (Guid.NewGuid().GetHashCode());
					int roll = rnd.Next(1, 6);
					theWord += roll;
				}

				// add new number to array of wordKeys
				wordKeys[i] = theWord;
			}

			return wordKeys;
		}

		private static string getPhrase(Dictionary<string, string> wordList, string[] wordKeys)
		{
			string thePhrase = "";
			foreach (string key in wordKeys) {
				thePhrase += " " + wordList [key];
			}
			return thePhrase;
		}
	}
}

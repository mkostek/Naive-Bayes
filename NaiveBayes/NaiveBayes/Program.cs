/*
 * SharpDevelop tarafından düzenlendi.
 * Kullanıcı: Asus
 * Tarih: 19.05.2019
 * Zaman: 11:29
 * 
 * Bu şablonu değiştirmek için Araçlar | Seçenekler | Kodlama | Standart Başlıkları Düzenle 'yi kullanın.
 */
using System;
using System.Collections.Generic;

namespace NaiveBayes
{
	class Program
	{
		
		public static void Main(string[] args)
		{
			Console.WriteLine("\nBegin Naive Bayes classification demo");
			Console.WriteLine("Goal is to predict (liberal/conservative) from job, " + "sex and income\n");
			string[][] rawData = new string[30][];
			rawData[0] = new string[] { "analyst", "male", "high", "conservative" };
			rawData[1] = new string[] { "barista", "female", "low", "liberal" };
			rawData[2] = new string[] { "cook", "male", "medium", "conservative" };
			rawData[3] = new string[] { "doctor", "female", "medium", "conservative" };
			rawData[4] = new string[] { "analyst", "female", "low", "liberal" };
			rawData[5] = new string[] { "doctor", "male", "medium", "conservative" };
			rawData[6] = new string[] { "analyst", "male", "medium", "conservative" };
			rawData[7] = new string[] { "cook", "female", "low", "liberal" };
			rawData[8] = new string[] { "doctor", "female", "medium", "liberal" };
			rawData[9] = new string[] { "cook", "female", "low", "liberal" };
			rawData[10] = new string[] { "doctor", "male", "medium", "conservative" };
			rawData[11] = new string[] { "cook", "female", "high", "liberal" };
			rawData[12] = new string[] { "barista", "female", "medium", "liberal" };
			rawData[13] = new string[] { "analyst", "male", "low", "liberal" };
			rawData[14] = new string[] { "doctor", "female", "high", "conservative" };
			rawData[15] = new string[] { "barista", "female", "medium", "conservative" };
			rawData[16] = new string[] { "doctor", "male", "medium", "conservative" };
			rawData[17] = new string[] { "barista", "male", "high", "conservative" };
			rawData[18] = new string[] { "doctor", "female", "medium", "liberal" };
			rawData[19] = new string[] { "analyst", "male", "low", "liberal" };
			rawData[20] = new string[] { "doctor", "male", "medium", "conservative" };
			rawData[21] = new string[] { "cook", "male", "medium", "conservative" };
			rawData[22] = new string[] { "doctor", "female", "high", "conservative" };
			rawData[23] = new string[] { "analyst", "male", "high", "conservative" };
			rawData[24] = new string[] { "barista", "female", "medium", "liberal" };
			rawData[25] = new string[] { "doctor", "male", "medium", "conservative" };
			rawData[26] = new string[] { "analyst", "female", "medium", "conservative" };
			rawData[27] = new string[] { "analyst", "male", "medium", "conservative" };
			rawData[28] = new string[] { "doctor", "female", "medium", "liberal" };
			rawData[29] = new string[] { "barista", "male", "medium", "conservative" };
			Console.WriteLine("The raw data is: \n");
			ShowData(rawData, 5, true);
			Console.WriteLine("Splitting data into 80%-20% train and test sets");
			string[][] trainData; string[][] testData;
			MakeTrainTest(rawData, 15, out trainData, out testData);
			Console.WriteLine("Done \n");
			Console.WriteLine("Training data: \n");
			ShowData(trainData, 5, true);
			Console.WriteLine("Test data: \n");
			ShowData(testData, 5, true);
			Console.WriteLine("Creating Naive Bayes classifier object");
			Console.WriteLine("Training classifier using training data");
			BayesClassifier bc = new BayesClassifier();
			bc.Train(trainData);
			Console.WriteLine("Done \n");
			double trainAccuracy = bc.Accuracy(trainData);
			Console.WriteLine("Accuracy of model on train data = " + trainAccuracy.ToString("F4"));
			double testAccuracy = bc.Accuracy(testData);
			Console.WriteLine("Accuracy of model on test data = " + testAccuracy.ToString("F4"));
			Console.WriteLine("\nPredicting politics for job = barista, sex = female, " + "income = medium \n");
			string[] features = new string[] { "barista", "female", "medium" };
			string liberal = "liberal";
			double pLiberal = bc.Probability(liberal, features);
			Console.WriteLine("Probability of liberal = " + pLiberal.ToString("F4"));
			string conservative = "conservative";
			double pConservative = bc.Probability(conservative, features);
			Console.WriteLine("Probability of conservative = " + pConservative.ToString("F4"));
			Console.WriteLine("\nEnd Naive Bayes classification demo\n");
			Console.ReadLine();
			// TODO: Implement Functionality Here
			
		}
		static void MakeTrainTest(string[][] allData, int seed, out string[][] trainData, out string[][] testData) {
			Random rnd = new Random(seed);
			int totRows = allData.Length;
			int numTrainRows = (int)(totRows * 0.80);
			int numTestRows = totRows - numTrainRows;
			trainData = new string[numTrainRows][];
			testData = new string[numTestRows][];
			string[][] copy = new string[allData.Length][];
			for (int i = 0; i < copy.Length; ++i)
				copy[i] = allData[i];
			for (int i = 0; i < copy.Length; ++i)
			{
				int r = rnd.Next(i, copy.Length);
				string[] tmp = copy[r];
				copy[r] = copy[i]; copy[i] = tmp;
			}
			for (int i = 0; i < numTrainRows; ++i)
				trainData[i] = copy[i];
			for (int i = 0; i < numTestRows; ++i) testData[i] = copy[i + numTrainRows];
		}
		static void ShowData(string[][] rawData, int numRows, bool indices) {
			for (int i = 0; i < numRows; ++i) {
				if (indices == true) Console.Write("[" + i.ToString().PadLeft(2) + "] ");
				for (int j = 0; j < rawData[i].Length; ++j) {
					string s = rawData[i][j]; Console.Write(s.PadLeft(14) + " ");
				}
				Console.WriteLine("");
			}
			if (numRows != rawData.Length-1)
				Console.WriteLine(". . .");
			int lastRow = rawData.Length - 1;
			if (indices == true) Console.Write("[" + lastRow.ToString().PadLeft(2) + "] ");
			for (int j = 0; j < rawData[lastRow].Length; ++j) {
				string s = rawData[lastRow][j]; Console.Write(s.PadLeft(14) + " ");
			}
			Console.WriteLine("\n");
		}
		static double[] MakeIntervals(double[] data, int numBins)
		{
			double max = data[0];
			double min = data[0];
			for (int i = 0; i < data.Length; ++i) {
				if (data[i] < min) min = data[i];
				if (data[i] > max) max = data[i];
			}
			double width = (max - min) / numBins;
			double[] intervals = new double[numBins * 2];
			intervals[0] = min;
			intervals[1] = min + width;
			for (int i = 2; i < intervals.Length - 1; i += 2) {
				intervals[i] = intervals[i - 1];
				intervals[i + 1] = intervals[i] + width;
			}
			intervals[0] = double.MinValue;
			intervals[intervals.Length - 1] = double.MaxValue;
			return intervals;
	
		}
		static int Bin(double x, double[] intervals) {
			for (int i = 0; i < intervals.Length - 1; i += 2) {
				if (x >= intervals[i] && x < intervals[i + 1])
					return i / 2;
			}
			return -1;
		}
	}
	public class BayesClassifier {
		private Dictionary<string, int>[] stringToInt;
		private int[][][] jointCounts;
		private int[] dependentCounts;
		public BayesClassifier() {
			this.stringToInt = null;
			this.jointCounts = null;this.dependentCounts = null;
		}
		public void Train(string[][] trainData) {
			int numRows = trainData.Length;
			int numCols = trainData[0].Length;
			this.stringToInt = new Dictionary<string, int>[numCols];
			for (int col = 0; col < numCols; ++col){
				stringToInt[col] = new Dictionary<string, int>();
				int idx = 0;
				for (int row = 0; row < numRows; ++row){
					string s = trainData[row][col];
					if (stringToInt[col].ContainsKey(s) == false){
						stringToInt[col].Add(s, idx);
						++idx;
					}
				}
			}
			this.jointCounts = new int[numCols - 1][][];
			for (int c = 0; c < numCols - 1; ++c)
			{
				int count = this.stringToInt[c].Count;
				jointCounts[c] = new int[count][];
			}
			
			for (int i = 0; i < jointCounts.Length; ++i)
				for (int j = 0; j < jointCounts[i].Length; ++j)	{
				jointCounts[i][j] = new int[2];
			}
			
			for (int i = 0; i < jointCounts.Length; ++i)
				for (int j = 0; j < jointCounts[i].Length; ++j)
					for (int k = 0; k < jointCounts[i][j].Length; ++k)
						jointCounts[i][j][k] = 1;
			
			for (int i = 0; i < numRows; ++i) {
				string yString = trainData[i][numCols - 1];
				int depIndex = stringToInt[numCols - 1][yString];
				for (int j = 0; j < numCols - 1; ++j) {
					int attIndex = j;
					string xString = trainData[i][j];
					int valIndex = stringToInt[j][xString];
					++jointCounts[attIndex][valIndex][depIndex];
				}
			}
			
			this.dependentCounts = new int[2];
			for (int i = 0; i < dependentCounts.Length;
			     ++i)dependentCounts[i] = numCols - 1;
			for (int i = 0; i < trainData.Length; ++i) {
				string yString = trainData[i][numCols - 1];
				int yIndex = stringToInt[numCols - 1][yString];
				++dependentCounts[yIndex];
			}
			return;
		}
		public double Probability(string yValue, string[] xValues) {
			int numFeatures = xValues.Length;
			double[][] conditionals = new double[2][];
			for (int i = 0; i < 2; ++i)
				conditionals[i] = new double[numFeatures];
			double[] unconditionals = new double[2];
			int y = this.stringToInt[numFeatures][yValue];
			int[] x = new int[numFeatures];
			for (int i = 0; i < numFeatures; ++i) {
				string s = xValues[i];
				x[i] = this.stringToInt[i][s];
			}
			
			for (int k = 0; k < 2; ++k){
				for (int i = 0; i < numFeatures; ++i) {
					int attIndex = i;
					int valIndex = x[i];
					int depIndex = k;
					conditionals[k][i] = (jointCounts[attIndex][valIndex][depIndex] * 1.0) / dependentCounts[depIndex];
				}
			}
			
			int totalDependent = 0;
			for (int k = 0; k < 2; ++k)
				totalDependent += this.dependentCounts[k];
			for (int k = 0; k < 2; ++k)
				unconditionals[k] = (dependentCounts[k] * 1.0) / totalDependent;
			double[] partials = new double[2];
			for (int k = 0; k < 2; ++k) {
				partials[k] = 1.0;
				for (int i = 0; i < numFeatures; ++i)
					partials[k] *= conditionals[k][i];
				partials[k] *= unconditionals[k];
			}
			
			double evidence = 0.0;
			for (int k = 0; k < 2; ++k)
				evidence += partials[k];
			
			return partials[y] / evidence;
		}
		public double Accuracy(string[][] data) {
			int numCorrect = 0;
			int numWrong = 0;
			int numRows = data.Length;
			int numCols = data[0].Length;
			for (int i = 0; i < numRows; ++i){
				string yValue = data[i][numCols - 1];
				string[] xValues = new string[numCols - 1];
				Array.Copy(data[i], xValues, numCols - 1);
				double p = this.Probability(yValue, xValues);
				if (p > 0.50) ++numCorrect; else ++numWrong;
			} 
			return (numCorrect * 1.0) / (numCorrect + numWrong);
		}
	}
	
}
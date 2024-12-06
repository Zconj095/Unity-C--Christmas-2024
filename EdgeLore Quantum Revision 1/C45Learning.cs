using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   C4.5 Learning algorithm for Decision Trees in Unity.
    /// </summary>
    [Serializable]
    public class C45Learning
    {
        private DecisionTreeV3 tree;
        private double[][] thresholds;
        private int splitStep = 1;

        /// <summary>
        ///   Gets or sets the step at which continuous attributes are split. Default is 1.
        /// </summary>
        public int SplitStep
        {
            get => splitStep;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "SplitStep must be greater than zero.");
                splitStep = value;
            }
        }

        /// <summary>
        ///   Initializes a new instance of the C4.5 learning algorithm.
        /// </summary>
        /// <param name="tree">The decision tree to be learned.</param>
        public C45Learning(DecisionTreeV3 tree)
        {
            this.tree = tree ?? throw new ArgumentNullException(nameof(tree));
        }

        /// <summary>
        ///   Learns a model that maps inputs to outputs using the C4.5 algorithm.
        /// </summary>
        /// <param name="inputs">Input data.</param>
        /// <param name="outputs">Output labels.</param>
        public DecisionTreeV3 Learn(double[][] inputs, int[] outputs)
        {
            if (tree == null)
                throw new InvalidOperationException("The decision tree must be initialized before learning.");

            thresholds = CalculateThresholds(inputs, outputs);
            Split(tree.Root, inputs, outputs, 0);
            return tree;
        }

        /// <summary>
        ///   Calculates split thresholds for continuous attributes.
        /// </summary>
        private double[][] CalculateThresholds(double[][] inputs, int[] outputs)
        {
            int attributes = inputs[0].Length;
            double[][] thresholds = new double[attributes][];

            for (int i = 0; i < attributes; i++)
            {
                List<double> candidates = new List<double>();
                var sorted = inputs.Select((row, idx) => (Value: row[i], Output: outputs[idx]))
                                   .OrderBy(x => x.Value)
                                   .ToArray();

                for (int j = 0; j < sorted.Length - 1; j++)
                {
                    if (sorted[j].Output != sorted[j + 1].Output)
                    {
                        double threshold = (sorted[j].Value + sorted[j + 1].Value) / 2.0;
                        candidates.Add(threshold);
                    }
                }

                thresholds[i] = candidates.ToArray();
            }

            return thresholds;
        }

        /// <summary>
        ///   Recursively splits nodes based on attribute selection.
        /// </summary>
        private void Split(DecisionNodeV3 node, double[][] inputs, int[] outputs, int depth)
        {
            if (inputs.Length == 0)
                return;

            // If all outputs are the same, this is a leaf node
            if (outputs.Distinct().Count() == 1)
            {
                node.Output = outputs[0];
                return;
            }

            // If we reached the maximum depth, assign majority class
            if (depth == tree.MaxDepth)
            {
                node.Output = outputs.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
                return;
            }

            // Find the best attribute and split
            int bestAttribute = -1;
            double bestThreshold = double.NaN;
            double bestGain = double.NegativeInfinity;
            List<int>[] bestPartitions = null;

            for (int i = 0; i < inputs[0].Length; i++)
            {
                foreach (double threshold in thresholds[i])
                {
                    var partitions = Partition(inputs, outputs, i, threshold);
                    double gain = CalculateInformationGain(outputs, partitions);

                    if (gain > bestGain)
                    {
                        bestGain = gain;
                        bestAttribute = i;
                        bestThreshold = threshold;
                        bestPartitions = partitions;
                    }
                }
            }

            if (bestAttribute == -1)
            {
                node.Output = outputs.GroupBy(x => x).OrderByDescending(g => g.Count()).First().Key;
                return;
            }

            node.SplitAttribute = bestAttribute;
            node.Threshold = bestThreshold;

            // Create child nodes and recurse
            node.Left = new DecisionNodeV3(tree);
            node.Right = new DecisionNodeV3(tree);

            Split(node.Left, inputs.GetSubset(bestPartitions[0]), outputs.GetSubset(bestPartitions[0]), depth + 1);
            Split(node.Right, inputs.GetSubset(bestPartitions[1]), outputs.GetSubset(bestPartitions[1]), depth + 1);
        }

        /// <summary>
        ///   Partitions data based on a given attribute and threshold.
        /// </summary>
        private List<int>[] Partition(double[][] inputs, int[] outputs, int attribute, double threshold)
        {
            var left = new List<int>();
            var right = new List<int>();

            for (int i = 0; i < inputs.Length; i++)
            {
                if (inputs[i][attribute] <= threshold)
                    left.Add(i);
                else
                    right.Add(i);
            }

            return new List<int>[] { left, right };
        }

        /// <summary>
        ///   Calculates information gain for a split.
        /// </summary>
        private double CalculateInformationGain(int[] outputs, List<int>[] partitions)
        {
            double entropyBefore = CalculateEntropy(outputs);
            double entropyAfter = 0;

            foreach (var partition in partitions)
            {
                if (partition.Count == 0)
                    continue;

                double proportion = (double)partition.Count / outputs.Length;
                int[] subset = outputs.GetSubset(partition);
                entropyAfter += proportion * CalculateEntropy(subset);
            }

            return entropyBefore - entropyAfter;
        }

        /// <summary>
        ///   Calculates the entropy of a set of outputs.
        /// </summary>
        private double CalculateEntropy(int[] outputs)
        {
            var probabilities = outputs.GroupBy(x => x)
                                       .Select(g => (double)g.Count() / outputs.Length);
            return -probabilities.Sum(p => p * Math.Log(p, 2));
        }
    }

    /// <summary>
    ///   Decision Tree Node.
    /// </summary>
    public class DecisionNodeV3
    {
        public int? SplitAttribute { get; set; }
        public double? Threshold { get; set; }
        public int? Output { get; set; }
        public DecisionNodeV3 Left { get; set; }
        public DecisionNodeV3 Right { get; set; }

        public DecisionNodeV3(DecisionTreeV3 tree)
        {
        }
    }

    /// <summary>
    ///   Decision Tree class.
    /// </summary>
    public class DecisionTreeV3
    {
        public DecisionNodeV3 Root { get; set; }
        public int MaxDepth { get; set; }

        public DecisionTreeV3(int maxDepth)
        {
            MaxDepth = maxDepth;
            Root = new DecisionNodeV3(this);
        }
    }

    /// <summary>
    ///   Extension methods for array manipulations.
    /// </summary>
    public static class ArrayExtensions
    {
        public static T[] GetSubset<T>(this T[] array, List<int> indices)
        {
            return indices.Select(i => array[i]).ToArray();
        }

        public static T[][] GetSubset<T>(this T[][] array, List<int> indices)
        {
            return indices.Select(i => array[i]).ToArray();
        }
    }
}

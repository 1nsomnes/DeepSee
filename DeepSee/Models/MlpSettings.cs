using System;

namespace DeepSee.Models
{
    public struct MlpSettings
    {
        //the amount of iterations before optimization is calculated
        public int Batches;
        //number of times the training data is cycled over
        public int Epochs;
        //percentage of the gradient (how big of a step it should take)
        public double LearningRate;
        
        public Func<double> WeightInitializationFunction;
    }
}
using System;

namespace DeepSee
{
    public class Neuron
    {
        private double value = 0f;
        private double bias = 0f;
        private double[] weights;

        public void SetBias(double biasValue)
        {
            bias = biasValue;
        }

        public double GetBias()
        {
            return bias;
        }
        
        //number of weights should always equal the number of neurons on the next layer!
        public void InitializeWeightZero(int numberOfWeights)
        {
            weights = new double[numberOfWeights];
        }

        public void SetWeights(double[] weightsValue)
        {
            weights = weightsValue;
        }

        public double[] GetAllWeights()
        {
            return weights;
        }

        public double GetWeight(int index)
        {
            return weights[index];
        }
        public void SetValue(double value)
        {
            this.value = value;
        }

        public double GetValue()
        {
            return value;
        }
    }
}
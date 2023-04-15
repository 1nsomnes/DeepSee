using System;

namespace DeepSee
{
    public class Neuron
    {
        public double Value = 0f;
        public double Bias = 0f;
        //number of weights should always equal the number of neurons on the next layer!
        public double[] Weights;
    }
}
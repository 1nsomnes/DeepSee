using System;
using Math;

namespace DeepSee.Layers
{
    public class Layer
    {
        //neurons must always be in the same order as there respective weights in
        //the neuron class
        private Neuron[] neurons;
        public Func<double, double> activation { get; }

        public Layer(int numberOfNeurons, Func<double, double> activation)
        {
            neurons = new Neuron[numberOfNeurons];
            this.activation = activation;
        }
        
        public void InitializeNeuronWeights(int numberOfWeights)
        {
            foreach (Neuron n in neurons)
            {
                n.SetWeights(new double[numberOfWeights]);
            }
        }

        public int NeuronCount()
        {
            return neurons.Length;
        }

        public Matrix CalculateNextLayerValues(Layer nextLayer)
        {
            var newMatrix = nextLayer.GetWeightMatrix(neurons.Length) * GetNeuronValuesMatrix();
            newMatrix = newMatrix + nextLayer.GetBiasesMatrix();
            
            newMatrix.ApplyOperation(nextLayer.activation);

            return newMatrix;
        }

        public Matrix GetWeightMatrix(int previousLayerNeuronCount)
        {
            var weightMatrix = new Matrix(previousLayerNeuronCount, neurons.Length);

            for (int row = 0; row < weightMatrix.Rows; row++)
            {
                for (int column = 0; column < weightMatrix.Columns; column++)
                {
                    weightMatrix.SetElement(row, column, neurons[column].GetWeight(row));
                }
            }

            return weightMatrix;
        }

        public Matrix GetBiasesMatrix()
        {
            var biasMatrix = new Matrix(neurons.Length, 1);

            for (int index = 0; index < neurons.Length; index++)
            {
                biasMatrix.SetElement(index,0, neurons[index].GetBias());
            }

            return biasMatrix;
        }

        private Matrix GetNeuronValuesMatrix()
        {
            double[,] values = new double[neurons.Length, 1];

            for (int index = 0; index < neurons.Length; index++)
            {
                values[index, 0] = neurons[index].GetValue();
            }

            return new Matrix(values);
        }
        
    }
}
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
        
        // rows x columns / # of neurons x 1
        public void InputValues(Matrix matrix)
        {
            for (int i = 0; i < matrix.Rows; i++)
            {
                neurons[i].Value = matrix.GetElement(i, 0);
            }
        }
        
        public void InitializeNeuronWeights(int numberOfWeights, Func<double> initializationFunction)
        {
            foreach (Neuron neuron in neurons)
            {
                neuron.Weights = new double[numberOfWeights];
                for(int i = 0; i < numberOfWeights; i++)
                {
                    neuron.Weights[i] = initializationFunction();
                }
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
                    weightMatrix.SetElement(row, column, neurons[column].Weights[row]);
                }
            }

            return weightMatrix;
        }

        public Matrix GetBiasesMatrix()
        {
            var biasMatrix = new Matrix(neurons.Length, 1);

            for (int index = 0; index < neurons.Length; index++)
            {
                biasMatrix.SetElement(index,0, neurons[index].Bias);
            }

            return biasMatrix;
        }

        private Matrix GetNeuronValuesMatrix()
        {
            double[,] values = new double[neurons.Length, 1];

            for (int index = 0; index < neurons.Length; index++)
            {
                values[index, 0] = neurons[index].Value;
            }

            return new Matrix(values);
        }
        
    }
}
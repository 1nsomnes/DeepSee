using System;
using System.Collections.Generic;
using System.Linq;
using DeepSee.Layers;
using Math;

//todo: PLEASE WRITE TESTS I BEG YOU!!! 
namespace DeepSee.Models
{
    public class MultilayerPerceptron : Model
    {
        private MlpSettings settings;
        private Layer[] layers;
        
        public MultilayerPerceptron(MlpSettings mlpSettings, int numberOfLayers)
        {
            HasAtLeastTwoLayers(ref numberOfLayers);
            settings = mlpSettings;
            layers = new Layer[numberOfLayers];
        }

        private void HasAtLeastTwoLayers(ref int numberOfLayers)
        {
            if (numberOfLayers < 2)
            {
                throw new ArgumentException("Neural network must at least have two layers.");
            }
        }

        public void SetLayer(int index, Layer layer)
        {
            layers[index] = layer;
        }

        public override void Train(Matrix[] inputs, Matrix[] expected)
        {
            AreInputsAndExpectedSameSize(inputs, expected);
            AreAllLayersInitialized();
            
            InitializeValues();

            Random rng = new Random();
            int[] indices = Enumerable.Range(0, inputs.Length).ToArray();
            rng.Shuffle(indices);

            Matrix avgGradient = NetworkIteration(null, null);

            int epochs = 0;
            int dataIndex = 1;
            while (epochs < settings.Epochs)
            {
                for (int i = 1; i < settings.Batches; i++)
                {
                    if (dataIndex >= inputs.Length)
                    {
                        rng.Shuffle(indices);
                        dataIndex = 0;
                        epochs++;
                    }
                    
                    var gradientIteration = NetworkIteration(null, null);
                    avgGradient = avgGradient + gradientIteration;
                    avgGradient = avgGradient * 0.5f;

                    dataIndex++;
                }
                
                //step
                UpdateWeightsAndBiases(null);
                
                // we don't want to average the first gradient, so we must step the dataIndex.
                if (dataIndex >= inputs.Length)
                {
                    rng.Shuffle(indices);
                    dataIndex = 0;
                    epochs++;
                }
                
                avgGradient = NetworkIteration(null, null);
                dataIndex++;
            }

            throw new NotImplementedException();
        }

        private void AreAllLayersInitialized()
        {
            foreach (Layer l in layers)
            {
                if (l == null)
                {
                    throw new NullReferenceException("One or more of your layers are not initialized!");
                }
            }
        }

        private void AreInputsAndExpectedSameSize(Matrix[] inputs, Matrix[] expected)
        {
            if (inputs.Length != expected.Length)
            {
                throw new ArgumentException(
                    "Training samples and expected results should have the same number of items.");
            }
        }
        
        public void InitializeValues()
        {
            for (int i = 1; i < layers.Length; i++)
            {
                layers[i].InitializeNeuronWeights(layers[i-1].NeuronCount(), settings.WeightInitializationFunction);
            }
        }

        //step
        public void UpdateWeightsAndBiases(Matrix gradient)
        {
            throw new NotImplementedException();
        }
        
        public Matrix NetworkIteration(Matrix input, Matrix expected)
        {
            layers[0].InputValues(input);

            for (int i = 0; i <= layers.Length; i++)
            {
                Matrix newValues = layers[i].CalculateNextLayerValues(layers[i + 1]);
                layers[i + 1].InputValues(newValues);
            }
            
            //todo: calculate back propagation
            
            throw new NotImplementedException();
        }

        public override Matrix[] Predict(Matrix[] input)
        {
            throw new System.NotImplementedException();
        }
    }
}
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
                    
                    NetworkIteration(inputs[dataIndex], expected[dataIndex]);

                    dataIndex++;
                }
                
                //step
                UpdateWeightsAndBiases();
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
        
        private void InitializeValues()
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].InitializeNeuronWeights(layers[i].NeuronCount(), settings.WeightInitializationFunction);
            }
        }

        //step
        private void UpdateWeightsAndBiases()
        {   
            //todo: make sure you average the gradients by dividing the # of batches 
            throw new NotImplementedException();
        }
        
        private void NetworkIteration(Matrix input, Matrix expected)
        {
            layers[0].InputValues(input);

            ForwardPass();
            BackPropagation(expected);
            
        }

        private void ForwardPass()
        {
            for (int i = 0; i < layers.Length - 1; i++)
            {
                layers[i].CalculateNextLayerValues(layers[i + 1]);
            }
        }

        private void BackPropagation(Matrix expected)
        {
            //calculate derivative for last layer
            //specifically designed for square difference error
            //not customizable lol
            var lastLayer = layers[^1];
            var lastNeurons = lastLayer.neurons;
            for (int i = 0; i < lastNeurons.Length; i++)
            {
                //advanced mathematics 
                var dzds = lastLayer.dActivation(lastNeurons[i].Z); //derivative of z w/ respect to sigmoid
                var dBias = -2 * (expected.GetElement(i, 0) - lastNeurons[i].Value) * dzds;

                lastNeurons[i].dBias += dBias;
                
                var weights = lastNeurons[i].Weights;
                
                //index for weights
                for (int j = 0; j < weights.Length; j++)
                {
                    layers[^2].neurons[j].dWeights[i] = dBias * layers[^2].neurons[j].Value;
                    //lastNeurons[i].dWeights[j] += dBias * layers[^2].neurons[j].Value; 
                }
            }
            
            //if it's a two layer network we're done calculating derivatives 
            if (layers.Length == 2) return;

            /*for (int layerIndex = layers.Length - 1; layerIndex > 0; layerIndex--)
            {
                var currentLayer = layers[layerIndex];
            }*/
            
            //calculate derivative for all other layers 
            for(int layerIndex = layers.Length - 2; layerIndex > 0; layerIndex--)
            {
                 
            }

        }

        public override Matrix[] Predict(Matrix[] input)
        {
            throw new System.NotImplementedException();
        }
    }
}
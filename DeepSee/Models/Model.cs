using Math;

namespace DeepSee.Models
{
    public abstract class Model
    {
        public abstract void Train(Matrix[] inputs, Matrix[] expected);
        public abstract Matrix[] Predict(Matrix[] input);
    }
}
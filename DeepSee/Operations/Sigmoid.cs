using System;

namespace DeepSee.Operations
{
    public static class Sigmoid
    {
        public static float Operation(float input)
        {
            float denominator = 1f + MathF.Exp(-input);
            return 1f / denominator;
        }

        public static float dOperation(float input)
        {
            float ex = MathF.Exp(-input);
            float denominator = MathF.Pow(1 + ex, 2);

            return ex / denominator;
        }
    }
}
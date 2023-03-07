class Program
{
    static void Main(string[] args)
    {
        // Set up the neural network architecture
        int numInputs = 3;
        int numHidden = 4;
        int numOutputs = 1;

        // Set up the training data
        double[][] inputs =
        {
            new double[] { 0.256, 0.420, 0.160 },
            new double[] { 0.420, 0.160, 0.429 },
            new double[] { 0.160, 0.429, 0.117 },
            new double[] { 0.429, 0.117, 0.440 },
            new double[] { 0.117, 0.440, 0.088 },
            new double[] { 0.440, 0.088, 0.414 },
            new double[] { 0.088, 0.414, 0.007 },
            new double[] { 0.414, 0.007, 0.477 },
            new double[] { 0.007, 0.477, 0.195 },
            new double[] { 0.477, 0.195, 0.418 }
        };
        double[][] outputs =
        {
            new double[] { 0.429 },
            new double[] { 0.117 },
            new double[] { 0.440 },
            new double[] { 0.088 },
            new double[] { 0.414 },
            new double[] { 0.007 },
            new double[] { 0.477 },
            new double[] { 0.195 },
            new double[] { 0.418 },
            new double[] { 0.004 }
        };

        // Create the neural network object
        NeuralNetwork net = new NeuralNetwork(numInputs, numHidden, numOutputs);

        // Train the neural network
        for (int i = 0; i < 100000; i++)
        {
            for (int j = 0; j < inputs.Length; j++)
            {
                double[] input = inputs[j];
                double[] output = outputs[j];
                net.Train(input, output);
            }
        }

        // Test the neural network
        double[] testInput = { 0.195, 0.418, 0.004 };
        double[] predictedOutput = net.Compute(testInput);
        Console.WriteLine($"Predicted output: {predictedOutput[0]} expexted : {0.505}");
        double[] testInput1 = { 0.418, 0.004, 0.505 };
        double[] predictedOutput1 = net.Compute(testInput1);
        Console.WriteLine($"Predicted output: {predictedOutput1[0]} expexted : {0.140}");
    }
}

class NeuralNetwork
{
    private int numInputs;
    private int numHidden;
    private int numOutputs;

    private double[,] hiddenWeights;
    private double[] hiddenBiases;
    private double[,] outputWeights;
    private double[] outputBiases;

    public NeuralNetwork(int numInputs, int numHidden, int numOutputs)
    {
        this.numInputs = numInputs;
        this.numHidden = numHidden;
        this.numOutputs = numOutputs;

        // Initialize weights and biases randomly
        hiddenWeights = new double[numInputs, numHidden];
        hiddenBiases = new double[numHidden];
        outputWeights = new double[numHidden, numOutputs];
        outputBiases = new double[numOutputs];
        Random rand = new Random();
        for (int i = 0; i < numInputs; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                hiddenWeights[i, j] = rand.NextDouble() - 0.5;
            }
        }
        for (int i = 0; i < numHidden; i++)
        {
            for (int j = 0; j < numOutputs; j++)
            {
                outputWeights[i, j] = rand.NextDouble() - 0.5;
            }
        }
        for (int i = 0; i < numHidden; i++)
        {
            hiddenBiases[i] = rand.NextDouble() - 0.5;
        }
        for (int i = 0; i < numOutputs; i++)
        {
            outputBiases[i] = rand.NextDouble() - 0.5;
        }
    }

    public void Train(double[] input, double[] targetOutput)
    {
        // Forward pass
        double[] hiddenOutput = new double[numHidden];
        for (int j = 0; j < numHidden; j++)
        {
            double sum = 0;
            for (int i = 0; i < numInputs; i++)
            {
                sum += input[i] * hiddenWeights[i, j];
            }
            sum += hiddenBiases[j];
            hiddenOutput[j] = Sigmoid(sum);
        }

        double[] output = new double[numOutputs];
        for (int j = 0; j < numOutputs; j++)
        {
            double sum = 0;
            for (int i = 0; i < numHidden; i++)
            {
                sum += hiddenOutput[i] * outputWeights[i, j];
            }
            sum += outputBiases[j];
            output[j] = Sigmoid(sum);
        }

        // Backward pass
        double[] outputError = new double[numOutputs];
        for (int j = 0; j < numOutputs; j++)
        {
            outputError[j] = (targetOutput[j] - output[j]) * SigmoidDerivative(output[j]);
        }

        double[] hiddenError = new double[numHidden];
        for (int j = 0; j < numHidden; j++)
        {
            double sum = 0;
            for (int i = 0; i < numOutputs; i++)
            {
                sum += outputError[i] * outputWeights[j, i];
            }
            hiddenError[j] = sum * SigmoidDerivative(hiddenOutput[j]);
        }

        // Update weights and biases
        for (int i = 0; i < numInputs; i++)
        {
            for (int j = 0; j < numHidden; j++)
            {
                hiddenWeights[i, j] += hiddenOutput[j] * hiddenError[j] * 0.1;
            }
        }
        for (int i = 0; i < numHidden; i++)
        {
            for (int j = 0; j < numOutputs; j++)
            {
                outputWeights[i, j] += output[j] * outputError[j] * 0.1;
            }
        }
        for (int i = 0; i < numHidden; i++)
        {
            hiddenBiases[i] += hiddenError[i] * 0.1;
        }
        for (int i = 0; i < numOutputs; i++)
        {
            outputBiases[i] += outputError[i] * 0.1;
        }
    }

    public double[] Compute(double[] input)
    {
        double[] hiddenOutput = new double[numHidden];
        for (int j = 0; j < numHidden; j++)
        {
            double sum = 0;
            for (int i = 0; i < numInputs; i++)
            {
                sum += input[i] * hiddenWeights[i, j];
            }
            sum += hiddenBiases[j];
            hiddenOutput[j] = Sigmoid(sum);
        }

        double[] output = new double[numOutputs];
        for (int j = 0; j < numOutputs; j++)
        {
            double sum = 0;
            for (int i = 0; i < numHidden; i++)
            {
                sum += hiddenOutput[i] * outputWeights[i, j];
            }
            sum += outputBiases[j];
            output[j] = Sigmoid(sum);
        }

        return output;
    }

    private double Sigmoid(double x)
    {
        return 1 / (1 + Math.Exp(-x));
    }

    private double SigmoidDerivative(double x)
    {
        return x * (1 - x);
    }
}
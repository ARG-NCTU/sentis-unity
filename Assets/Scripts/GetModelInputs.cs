using UnityEngine;
using System.Collections.Generic;
using Unity.Sentis;

public class GetModelInputs : MonoBehaviour
{
    public ModelAsset modelAsset;

    void Start()
    {
        Model runtimeModel = ModelLoader.Load(modelAsset);

        List<Model.Input> inputs = runtimeModel.inputs;

        // Loop through each input
        foreach (var input in inputs)
        {
            // Log the name of the input, for example Input3
            Debug.Log(input.name);

            // Log the tensor shape of the input, for example (1, 1, 28, 28)
            Debug.Log(input.shape);
        }

        // List<Model.Output> outputs = runtimeModel.outputs;

        // // Loop through each output
        // foreach (var output in outputs)
        // {
        //     // Log the name of the output
        //     Debug.Log(output.name);
        // }
    }
}
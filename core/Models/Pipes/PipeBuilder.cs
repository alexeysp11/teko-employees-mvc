using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeBuilder
{
    private System.Action<PipeResult> _mainFunction; 
    private List<System.Type> _pipeTypes; 

    public PipeBuilder(System.Action<PipeResult> mainFunction)
    {
        _mainFunction = mainFunction; 
        _pipeTypes = new List<System.Type>(); 
    }

    public PipeBuilder AddPipe(System.Type pipeType)
    {
        // if (!pipeType.IsInstanceOfType(typeof(AbstractPipe))) 
        //     throw new System.Exception("Incorrect pipe type"); 
        _pipeTypes.Add(pipeType); 
        return this; 
    }
    public PipeBuilder AddGenerating(System.Type pipeType)
    {
        return AddPipe(pipeType); 
    }

    public System.Action<PipeResult> Build()
    {
        return CreatePipe(0); 
    }

    private System.Action<PipeResult> CreatePipe(int index)
    {
        if (index < _pipeTypes.Count - 1)
        {
            var childPipeHandle = CreatePipe(index + 1); 
            var pipe = (AbstractPipe) System.Activator.CreateInstance(_pipeTypes[index], childPipeHandle); 
            return pipe.Handle; 
        }
        else 
        {
            var finalPipe = (AbstractPipe) System.Activator.CreateInstance(_pipeTypes[index], _mainFunction); 
            return finalPipe.Handle; 
        }
    }
}
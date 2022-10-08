using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueVarDictionary //this is a call object
{
    public Dictionary<string, Ink.Runtime.Object> variables; //this used to be private but changed to add extra scripting

    public DialogueVarDictionary(TextAsset loadGlobalsJSON)
    {
        // create the story
        Story globalVariablesStory = new Story(loadGlobalsJSON.text);
        {
            // initialize the dictionary
            variables = new Dictionary<string, Ink.Runtime.Object>();
            foreach (string name in globalVariablesStory.variablesState)
            {
                Ink.Runtime.Object value = globalVariablesStory.variablesState.GetVariableWithName(name);
                variables.Add(name, value);
                Debug.Log("Initialized global dialogue variable: " + name + " = " + value);
            }
        }
    }

    public void StartListening(Story story)
    {
        VariablesToStory(story); //needs to happen before the observer for some unexplained reason
        story.variablesState.variableChangedEvent += VariableChange;
    }

    public void StopListening(Story story)
    {
        story.variablesState.variableChangedEvent -= VariableChange;
    }

    private void VariableChange(string name, Ink.Runtime.Object value)
    {
        if (variables.ContainsKey(name))
        {
            variables.Remove(name);
            variables.Add(name, value);
        }
        Debug.Log("Variable changed: " + name + " = " + value);
    }

    private void VariablesToStory(Story story)
    {
        foreach(KeyValuePair<string, Ink.Runtime.Object> variable in variables)
        {
            story.variablesState.SetGlobal(variable.Key, variable.Value);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Calculator : MonoBehaviour
{
    //TODO: Create a TextMeshProUGUI variable here and assign it in the inspector
    public TextMeshProUGUI value;

    //TODO: Create a temporary float variable here called prevInput so we can store the previous input value
    //      when performaing calculations
    public float prevInput = 0;

    //TODO: Create a bool variable called clearPrevInput here so we can flip it to true/false if we should clear the prior input
    //      when typing in values. Example, if we type in the value 402 and then press the + button, the next value I enter
    //      should replace the 402 I previously entered
    public bool clearPrevInput = false;

    //TODO: Leave this alone
    private EquationType equationType;

    private void Start()
    {
        Clear();
    }

    public void AddInput(string input)
    {
        //TODO: Check the clearPrevInput variable you created
        //      and if true then set the current value of the text label to be string.Empty
        //      and set the clearPrevInput value to false
        if (clearPrevInput || value.text == "0" || value.text == "Infinity")
        {
            value.text = string.Empty;
            clearPrevInput = false;
        }

        //TODO: Add the input passed into the AddInput function to the current value of the label
        //      Hint. You can perform the + operations on string data to combine them
        value.text = value.text + input;
    }

    public void SetEquationAsAdd()
    {
        prevInput = float.Parse(value.text);
        clearPrevInput = true;
        equationType = EquationType.ADD;
    }

    //TODO: Create a SetEquationAsSubtract function similar to SetEquationAsAdd.
    //      Make sure you set equationType to EquationType.SUBTRACT
    public void SetEquationAsSubtract()
    {
        //TODO: Store the current input value on the text label into the float variable you created.
        prevInput = float.Parse(value.text);
        clearPrevInput = true;
        equationType = EquationType.SUBTRACT;
    }

    //TODO: Create a SetEquationAsMultiply function similar to SetEquationAsAdd.
    //      Make sure you set equationType to EquationType.Multiply
    public void SetEquationAsMultiply()
    {
        prevInput = float.Parse(value.text);
        clearPrevInput = true;
        equationType = EquationType.MULTIPLY;
    }

    //TODO: Create a SetEquationAsDivide function similar to SetEquationAsAdd.
    //      Make sure you set equationType to EquationType.DIVIDE
    public void SetEquationAsDivide()
    {
        prevInput = float.Parse(value.text);
        clearPrevInput = true;
        equationType = EquationType.DIVIDE;
    }

    public void SetEquationAsMod()
    {
        prevInput = float.Parse(value.text);
        clearPrevInput = true;
        equationType = EquationType.MOD;
    }

    public void Add()
    {
        //TODO: Calculate the sum of the float variable that stores the previous input value and the current input value
        //      Set the text label to display that sum
        float currentInput = float.Parse(value.text);
        value.text = (prevInput + currentInput).ToString();
    }

    //TODO: Implement Subtract function
    public void Subtract()
    {
        //TODO: Calculate the sum of the float variable that stores the previous input value and the current input value
        //      Set the text label to display that sum
        float currentInput = float.Parse(value.text);
        value.text = (prevInput - currentInput).ToString();
    }

    //TODO: Implement Multiply function
    public void Multiply()
    {
        //TODO: Calculate the sum of the float variable that stores the previous input value and the current input value
        //      Set the text label to display that sum
        float currentInput = float.Parse(value.text);
        value.text = (prevInput * currentInput).ToString();
    }

    //TODO: Implement Divide function
    public void Divide()
    {
        //TODO: Calculate the sum of the float variable that stores the previous input value and the current input value
        //      Set the text label to display that sum
        float currentInput = float.Parse(value.text);
        value.text = (prevInput / currentInput).ToString();
    }

    public void Mod()
    {
        //TODO: Calculate the sum of the float variable that stores the previous input value and the current input value
        //      Set the text label to display that sum
        float currentInput = float.Parse(value.text);
        value.text = (prevInput % currentInput).ToString();
    }

    public void Clear()
    {
        //TODO: Reset the state of your calculator... reset the display value to a 0, reset the bool variable
        //      that represents if the display should be cleared to true, reset the temporary float variable as well to 0
        value.text = "0";
        prevInput = 0;
        clearPrevInput = true;

        //TODO: Leave this alone
        equationType = EquationType.None;
    }

    public void Calculate()
    {
        //TODO: Check if equationTypep is Add/Subtract/Multiply/Divide and call the correct function
        if (equationType == EquationType.ADD) Add();
        if (equationType == EquationType.SUBTRACT) Subtract();
        if (equationType == EquationType.MULTIPLY) Multiply();
        if (equationType == EquationType.DIVIDE) Divide();
        if (equationType == EquationType.MOD) Mod();
    }

    public void Backspace()
    {
        if(value.text != "0")
        {
            value.text = value.text.Substring(0, value.text.Length - 1);

            if(value.text == "")
            {
                value.text = "0";
            }
        }
    }

    //TODO: Leave this alone
    public enum EquationType
    {
        None = 0,
        ADD = 1,
        SUBTRACT = 2,
        MULTIPLY = 3,
        DIVIDE = 4,
        MOD = 5
    }
}

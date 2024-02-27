using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;
using UnityEngine.Rendering;
using System;

namespace Dialogue.LogicalLines
{
    public static class LogicalLineUtilities
    {
        #region Encapsulated Data
        public static class Encapsulation
        {
            public struct EncapsulatedData
            {
                public List<string> lines;
                public int startingIndex;
                public int endingIndex;
            }

            private const char encapsulation_start = '{';
            private const char encapsulation_end = '}';

            public static bool IsEncapsulationStart(string line) => line.Trim().StartsWith(encapsulation_start);
            public static bool IsEncapsulationEnd(string line) => line.Trim().StartsWith(encapsulation_end);

            public static EncapsulatedData RipEncapsulationData(Conversation conversation, int startingIndex, bool ripHeaderandEncapsulators = false)
            { 
                int encapsulateDepth = 0;
                EncapsulatedData data = new EncapsulatedData { lines = new List<string>(), startingIndex = startingIndex, endingIndex = 0 };
                for (int i = startingIndex; i < conversation.Count; i++)
                {
                    string line = conversation.Getlines()[i];

                    if (ripHeaderandEncapsulators || encapsulateDepth > 0 && !IsEncapsulationEnd(line))
                        data.lines.Add(line);

                    if (IsEncapsulationStart(line))
                    {
                        encapsulateDepth++;
                        continue;
                    }

                    if (IsEncapsulationEnd(line))
                    {
                        encapsulateDepth--;
                        if (encapsulateDepth == 0)
                        {
                            data.endingIndex = i;
                            break;
                        }
                    }
                }

                return data;
            }

        }
        #endregion

        public static class Expressions
        {
            public static HashSet<string> operators = new HashSet<string>() { "-", "-=", "+", "+=", "*", "*=", "/", "/=", "=" };
            public static readonly string regex_arithmatic = @"([-+*/=]=?)";
            public static readonly string regex_operator_line = @"^\$\w+\s*(=|\+=|-=|\*=|/=|)\s*";

            public static object CalculateValue(string[] expressionParts)
            {
                List<string> operandStrings = new List<string>();
                List<string> operatorStrings = new List<string>();
                List<object> operands = new List<object>();

                for (int i = 0; i < expressionParts.Length; i++)
                {
                    string part = expressionParts[i].Trim();

                    if (part == string.Empty)
                        continue;

                    if (operators.Contains(part))
                        operatorStrings.Add(part);
                    else
                        operandStrings.Add(part);
                }
                foreach (string operandString in operandStrings)
                {
                    operands.Add(ExtractValue(operandString));
                }

                CalculateValue_DivisionAndMultiplication(operatorStrings, operands);

                CalculateValue_AdditionAndSubtraction(operatorStrings, operands);

                return operands[0];
            }

            #region Multiplication / Division
            private static void CalculateValue_DivisionAndMultiplication(List<string> operatorStrings, List<object> operands)
            {
                for (int i = 0; i < operatorStrings.Count;i++)
                {
                    string operatorString = operatorStrings[i];

                    if (operatorString == "*" || operatorString == "/")
                    {
                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if (operatorString == "*")
                            operands[i] = leftOperand * rightOperand;
                        else
                        {
                            if(rightOperand == 0)
                            {
                                if (rightOperand == 0)      // Check for division by zero
                                {
                                    Debug.LogError("Cannot divide by zero!");
                                    return;
                                }
                                operands[i] = leftOperand / rightOperand;
                            }
                        }

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;
                    }
                }
            }
            #endregion

            #region Addition / Subtraction
            private static void CalculateValue_AdditionAndSubtraction(List<string> operatorStrings, List<object> operands)
            {
                for (int i = 0; i < operatorStrings.Count; i++)
                {
                    string operatorString = operatorStrings[i];

                    if (operatorString == "+" || operatorString == "-")
                    {
                        double leftOperand = Convert.ToDouble(operands[i]);
                        double rightOperand = Convert.ToDouble(operands[i + 1]);

                        if (operatorString == "+")
                            operands[i] = leftOperand + rightOperand;

                        else
                            operands[i] = leftOperand - rightOperand;

                        operands.RemoveAt(i + 1);
                        operatorStrings.RemoveAt(i);
                        i--;
                    }
                }
            }
            #endregion

            private static object ExtractValue(string value)
            {
                bool negate = false;

                if (value.StartsWith('!'))
                {
                    negate = true;
                    value = value.Substring(1);
                }

                if (value.StartsWith(VariableStore.variable_id))
                {
                    string variableName = value.TrimStart(VariableStore.variable_id);
                    if (!VariableStore.HasVariable(variableName))
                    {
                        Debug.LogError($"Variable '{variableName}' does not exist.");
                        return null;
                    }

                    VariableStore.TryGetValue(variableName, out object val);

                    if (val is bool boolVal && negate)
                        return !boolVal;

                    return val;
                }

                else if (value.StartsWith('\"') && value.EndsWith('\"'))
                {
                    value = TagManager.Inject(value, injectTags: true, injectVariables: true);
                    return value.Trim('"');
                }

                else
                {
                    if (int.TryParse(value, out int intValue))
                        return intValue;
                    else if (float.TryParse(value, out float floatValue))
                        return floatValue;
                    else if (bool.TryParse(value, out bool boolValue))
                        return negate ? !boolValue : boolValue;
                    else
                        return value;
                }
            }
        }
    }
}

using BlazorCalculatorComponent.Classes;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorCalculatorComponent
{
    public class CompBlazorCalculator_Logic : BlazorComponent
    {

      

        public string CurrInput;
        public string CurrExpression;
        public string CurrAnswer ="Current answer 0";

        public List<MyCalculatorOperation> MyCalculatorOperationsList = new List<MyCalculatorOperation>();

        public event EventHandler CalculatorViewer_Closed;

        string Previous_Operator = string.Empty;
        double Global_Result = 0;
        bool reset = true;


        public CompBlazorCalculator_Logic()
        {



        }



        public void AddString(string s)
        {
            if (reset == true)
            {
                reset = false;
                CurrInput = string.Empty;
            }

            CurrInput += s;


        }


        public void buttonComp_Click()
        {
            double k = Convert.ToDouble(CurrInput.Replace(".", ","));
            k = -k;
            CurrInput = k.ToString();
        }

        public void buttonDot_Click()
        {
            Cmd_Add_Dot();
        }

        public void Cmd_Add_Dot()
        {

            if (!CurrInput.Contains("."))
            {
                AddString(".");
            }
        }


        public void Cmd_Add_Operator(string Par_Operator)
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                CurrInput = CurrInput.Replace(".", ",");
                if (!string.IsNullOrEmpty(Previous_Operator))
                {
                    
                    switch (Previous_Operator)
                    {
                        case "+":
                            Global_Result += Convert.ToDouble(CurrInput);
                            break;
                        case "-":
                            Global_Result -= Convert.ToDouble(CurrInput);
                            break;
                        case "*":
                            Global_Result *= Convert.ToDouble(CurrInput);
                            break;
                        case "/":
                            Global_Result /= Convert.ToDouble(CurrInput);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Global_Result = Convert.ToDouble(CurrInput);
                }

                Previous_Operator = Par_Operator;

                CurrExpression += CurrInput.Trim();
                CurrExpression += " " + Par_Operator + " ";
                CurrInput = string.Empty;

                CurrAnswer = "Current answer - " + Global_Result.ToString();
            }
        }

        public void Cmd_EQ()
        {

            if (!string.IsNullOrEmpty(CurrInput) && !string.IsNullOrEmpty(Previous_Operator))
            {
                Cmd_Add_Operator("=");

                Cmd_Add_Operation_In_History(CurrExpression.Trim() + " " + CurrInput.ToString().Trim());

                Cmd_Off();
            }

        }


        public void buttonEQ_Click()
        {

            Cmd_EQ();

        }

        public void Cmd_Add_Operation_In_History(string Par_Operation)
        {
            MyCalculatorOperation Tmp_MyCalculatorOperation = new MyCalculatorOperation();
            Tmp_MyCalculatorOperation.ID = MyCalculatorOperationsList.Count + 1;
            Tmp_MyCalculatorOperation.AddDate = DateTime.Now;
            Tmp_MyCalculatorOperation.Answer = Global_Result.ToString();
            Tmp_MyCalculatorOperation.Operation = Par_Operation + Global_Result.ToString().Replace(",", ".");
            MyCalculatorOperationsList.Add(Tmp_MyCalculatorOperation);
            StateHasChanged();
        }



        public void buttonclear_Click()
        {
            Cmd_Off();
        }

        public void buttonoff_Click()
        {
            Cmd_Off();
            MyCalculatorOperationsList = new List<MyCalculatorOperation>();
            StateHasChanged();
        }

        public void Cmd_Off()
        {
            reset = true;
            Global_Result = 0;
            CurrInput = string.Empty;
            CurrExpression = string.Empty;
            Previous_Operator = string.Empty;
            CurrAnswer = "Current answer 0";
        }



        public void SLWindow_Unloaded()
        {
            CalculatorViewer_Closed(this, new EventArgs());
        }

        public void buttonBackSpace_Click()
        {
            Cmd_BackSpace();

        }

        public void Cmd_BackSpace()
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                CurrInput = CurrInput.Substring(0, CurrInput.Length - 1);
            }

        }

        public void Cmd_KeyUp(UIKeyboardEventArgs e)
        {
          
            Console.WriteLine(e.Key);
            

            switch (e.Key)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                    AddString(e.Key);
                    break;
                case ".":
                    Cmd_Add_Dot();
                    break;
                case "Escape":
                    Cmd_Off();
                    break;
                case "Backspace":
                    Cmd_BackSpace();
                    break;
                case "Enter":
                    Cmd_EQ();
                    break;
                case "+":   
                case "-":   
                case "*":
                case "/":
                    Cmd_Add_Operator(e.Key);
                    break;
                default:
                    break;
            }


            
        }


        public void CurrAnswer_MouseLeftButtonUp()
        {
            CurrInput = Global_Result.ToString();
        }

        public void Button_Power_2_Click()
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                Global_Result = Convert.ToDouble(CurrInput.Replace(".", ","));
                Global_Result = Math.Pow(Global_Result, 2);
                Cmd_Add_Operation_In_History(CurrInput.Trim() + " ^ 2 = ");
                Cmd_Off();
            }
        }

        public void Button_Power_3_Click()
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                Global_Result = Convert.ToDouble(CurrInput.Replace(".", ","));
                Global_Result = Math.Pow(Global_Result, 3);
                Cmd_Add_Operation_In_History(CurrInput.Trim() + " ^ 3 = ");
                Cmd_Off();
            }
        }

        public void Button_SQRT_Click()
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                Global_Result = Convert.ToDouble(CurrInput.Replace(".", ","));
                Global_Result = Math.Sqrt(Global_Result);
                Cmd_Add_Operation_In_History(CurrInput.Trim() + " √ 2 = ");
                Cmd_Off();
            }
        }

        public void Button_CRT_Click()
        {
            if (!string.IsNullOrEmpty(CurrInput))
            {
                Global_Result = Convert.ToDouble(CurrInput.Replace(".", ","));
                Global_Result = Math.Pow(Global_Result, 0.3333333333333333333333);
                Cmd_Add_Operation_In_History(CurrInput.Trim() + " √ 3 = ");
                Cmd_Off();
            }
        }

        public void Dispose()
        {

        }
    }
}

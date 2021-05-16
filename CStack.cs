using System;

namespace Inlamning_3_ra_kod
{
    /* CLASS: CStack
     * PURPOSE: Is essentially a RPN-calculator with four registers X, Y, Z, T
     *   like the HP RPN calculators. Numeric values are entered in the entry
     *   string by adding digits and one comma. For test purposes the method
     *   RollSetX can be used instead. Operations can be performed on the
     *   calculator preferrably by using one of the methods
     *     1. BinOp - merges X and Y into X via an operation and rolls down
     *        the stack
     *     2. Unop - operates X and puts the result in X with overwrite
     *     3. Nilop - adds a known constant on the stack and rolls up the stack
     */
    public class CStack
    {
        double[] lettersValuesArr = new double[8];
        public double X, Y, Z, T;
        public string entry;
        /* CONSTRUCTOR: CStack
         * PURPOSE: create a new stack and init X, Y, Z, T and the text entry
         * PARAMETERS: --
         */
        public CStack()
        {
            X = Y = Z = T = 0;
            entry = "";
        }
        /* METHOD: Exit
         * PURPOSE: called on exit, prepared for saving
         * PARAMETERS: --
         * RETURNS: --
         */
        public void Exit()
        {

        }
        /* METHOD: StackString
         * PURPOSE: construct a string to write out in a stack view
         * PARAMETERS: --
         * RETURNS: the string containing the values T, Z, Y, X with newlines 
         *   between them
         */
        public string StackString()
        {
            return $"{T}\n{Z}\n{Y}\n{X}\n{entry}";
        }
        /* METHOD: VarString
         * PURPOSE: construct a string to write out in a variable list
         * PARAMETERS: --
         * RETURNS: NOT YET IMPLEMENTED
         */
        public string VarString()
        {
            // return the value of all letters 
            return $"{lettersValuesArr[0]}\n{lettersValuesArr[1]}\n{lettersValuesArr[2]}\n{lettersValuesArr[3]}\n{lettersValuesArr[4]}\n" +
                   $"{lettersValuesArr[5]}\n{lettersValuesArr[6]}\n{lettersValuesArr[7]}\n";
        }
        /* METHOD: SetX
         * PURPOSE: set X with overwrite
         * PARAMETERS: double newX - the new value to put in X
         * RETURNS: --
         */
        public void SetX(double newX)
        {
            X = newX;
        }
        /* METHOD: EntryAddNum
         * PURPOSE: add a digit to the entry string
         * PARAMETERS: string digit - the candidate digit to add at the end of the
         *   string
         * RETURNS: --
         * FAILS: if the string digit does not contain a parseable integer, nothing
         *   is added to the entry
         */
        public void EntryAddNum(string digit)
        {
            int val;
            if (int.TryParse(digit, out val))
            {
                entry = entry + val;
            }
        }
        /* METHOD: EntryAddComma
         * PURPOSE: adds a comma to the entry string
         * PARAMETERS: --
         * RETURNS: --
         * FAILS: if the entry string already contains a comma, nothing is added
         *   to the entry
         */
        public void EntryAddComma()
        {
            if (entry.IndexOf(",") == -1)
                entry = entry + ",";
        }
        /* METHOD: EntryChangeSign
         * PURPOSE: changes the sign of the entry string
         * PARAMETERS: --
         * RETURNS: --
         * FEATURES: if the first char is already a '-' it is exchanged for a '+',
         *   if it is a '+' it is changed to a '-', otherwise a '-' is just added
         *   first
         */
        public void EntryChangeSign()
        {
            char[] cval = entry.ToCharArray();
            if (cval.Length > 0)
            {
                switch (cval[0])
                {
                    case '+': cval[0] = '-'; entry = new string(cval); break;
                    case '-': cval[0] = '+'; entry = new string(cval); break;
                    default: entry = '-' + entry; break;
                }
            }
            else
            {
                entry = '-' + entry;
            }
        }
        /* METHOD: Enter
         * PURPOSE: converts the entry to a double and puts it into X
         * PARAMETERS: --
         * RETURNS: --
         * FEATURES: the entry is cleared after a successful operation
         */
        public void Enter()
        {
            if (entry != "")
            {
                RollSetX(double.Parse(entry));
                entry = "";
            }
        }
        /* METHOD: Drop
         * PURPOSE: drops the value of X, and rolls down
         * PARAMETERS: --
         * RETURNS: --
         * FEATURES: Z gets the value of T
         */
        public void Drop()
        {
            X = Y; Y = Z; Z = T;
        }
        /* METHOD: DropSetX
         * PURPOSE: replaces the value of X, and rolls down
         * PARAMETERS: double newX - the new value to assign to X
         * RETURNS: --
         * FEATURES: Z gets the value of T
         * NOTES: this is used when applying binary operations consuming
         *   X and Y and putting the result in X, while rolling down the
         *   stack
         */
        public void DropSetX(double newX)
        {
            X = newX; Y = Z; Z = T;
        }
        /* METHOD: BinOp
         * PURPOSE: evaluates a binary operation
         * PARAMETERS: string op - the binary operation retrieved from the
         *   GUI buttons
         * RETURNS: --
         * FEATURES: the stack is rolled down
         */
        public void BinOp(string op)
        {
            switch (op)
            {
                case "+": DropSetX(Y + X); break;
                case "−": DropSetX(Y - X); break;
                case "×": DropSetX(Y * X); break;
                case "÷": DropSetX(Y / X); break;
                case "yˣ": DropSetX(Math.Pow(Y, X)); break;
                case "ˣ√y": DropSetX(Math.Pow(Y, 1.0 / X)); break;
            }
        }
        /* METHOD: Unop
         * PURPOSE: evaluates a unary operation
         * PARAMETERS: string op - the unary operation retrieved from the
         *   GUI buttons
         * RETURNS: --
         * FEATURES: the stack is not moved, X is replaced by the result of
         *   the operation
         */
        public void Unop(string op)
        {
            switch (op)
            {
                // Powers & Logarithms:
                case "x²": SetX(X * X); break;
                case "√x": SetX(Math.Sqrt(X)); break;
                case "log x": SetX(Math.Log10(X)); break;
                case "ln x": SetX(Math.Log(X)); break;
                case "10ˣ": SetX(Math.Pow(10, X)); break;
                case "eˣ": SetX(Math.Exp(X)); break;

                // Trigonometry:
                case "sin": SetX(Math.Sin(X)); break;
                case "cos": SetX(Math.Cos(X)); break;
                case "tan": SetX(Math.Tan(X)); break;
                case "sin⁻¹": SetX(Math.Asin(X)); break;
                case "cos⁻¹": SetX(Math.Acos(X)); break;
                case "tan⁻¹": SetX(Math.Atan(X)); break;
            }
        }
        /* METHOD: Nilop
         * PURPOSE: evaluates a "nilary operation" (insertion of a constant)
         * PARAMETERS: string op - the nilary operation (name of the constant)
         *   retrieved from the GUI buttons
         * RETURNS: --
         * FEATURES: the stack is rolled up, X is preserved in Y that is preserved in
         *   Z that is preserved in T, T is erased
         */
        public void Nilop(string op)
        {
            switch (op)
            {
                case "π": RollSetX(Math.PI); break;
                case "e": RollSetX(Math.E); break;
            }
        }
        /* METHOD: Roll
         * PURPOSE: rolls the stack up
         * PARAMETERS: --
         * RETURNS: --
         */
        public void Roll()
        {
            double tmp = T;
            T = Z; Z = Y; Y = X; X = tmp;
        }
        /* METHOD: RollSetX
         * PURPOSE: rolls the stack up and puts a new value in X
         * PARAMETERS: double newX - the new value to put into X
         * RETURNS: --
         * FEATURES: T is dropped
         */
        public void RollSetX(double newX)
        {
            T = Z; Z = Y; Y = X; X = newX;
        }
        /* METHOD: SetAddress
         * PURPOSE:Sets the index  of the user input to be saved
         * PARAMETERS: string name - variable name
         * RETURNS: An int value from 0 to 7 that represents 8 possible indexes
         * FAILS: value not inside range, return -1.
         */
        public int SetAddress(string name)
        {
            #region old
            //if (Enum.TryParse<Letters>(name, ignoreCase: true, out var whichLetter))
            //{
            //    switch (whichLetter)
            //    {
            //        case Letters.A:
            //            {
            //                letttersValues[0] = X;
            //                break;
            //            }
            //        case Letters.B:
            //            {
            //                letttersValues[1] = X;
            //                break;
            //            }
            //        case Letters.C:
            //            {
            //                letttersValues[2] = X;
            //                break;
            //            }
            //        case Letters.D:
            //            {
            //                letttersValues[3] = X;
            //                break;
            //            }
            //        case Letters.E:
            //            {
            //                letttersValues[4] = X;
            //                break;
            //            }
            //        case Letters.F:
            //            {
            //                letttersValues[5] = X;
            //                break;
            //            }
            //        case Letters.G:
            //            {
            //                letttersValues[6] = X;
            //                break;
            //            }
            //        case Letters.H:
            //            {
            //                letttersValues[7] = X;
            //                break;
            //            }
            //        default:
            //            break;
            //    }

            //}
            #endregion
            switch (name.ToUpper())
            {
                case "A":
                    return 0;
                case "B":
                    return 1;
                case "C":
                    return 2;
                case "D":
                    return 3;
                case "E":
                    return 4;
                case "F":
                    return 5;
                case "G":
                    return 6;
                case "H":
                    return 7;
            }
            return -1;
        }
        /* METHOD: SetVar
         * PURPOSE: Sets the userinput value at the specified index
         * PARAMETERS: double Xvalue - variable name, int lettersIndex - variable name
         * RETURNS: --
         */
        public void SetVar(double Xvalue, int lettersIndex)
        {
            //sets the value of specified index to X value
            if (lettersIndex >= 0 && lettersIndex <= 7)
            {
                lettersValuesArr[lettersIndex] = Xvalue;

            }

        }
        /* METHOD: GetVar
         * PURPOSE: Updates the current inputed value to the saved value at the specified index.
         * PARAMETERS: int lettersindex - variable name
         * RETURNS: --
         */
        public void GetVar(int lettersIndex)
        {
            //string strDigit;
            //Returns the value att the specific index at lettersIndex
            if (lettersIndex >= 0 && lettersIndex <= 7)
            {
                entry = ((decimal)lettersValuesArr[lettersIndex]).ToString();
            }



        }
    }
}

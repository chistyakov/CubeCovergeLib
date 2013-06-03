using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeCoverLib
{
    public class TruthTable : ITruthTable
    {
        private const byte NOT_FOUND = 0xff;
        private const byte WORD_SIZE = 8;
        public TruthTable(bool[,] table)
        {
            byte rowCount;
            byte colCount;
            byte pow;
            if (CheckTableDims(table, out rowCount, out colCount, out pow))
            {
                if (CheckTableContent(table))
                {
                    Table = table;
                    ArgCount = pow;
                }
                else throw new ArgumentOutOfRangeException("table", "wrong set of arguments. Function should be defined once for every set of arguments");
            }
            else throw new ArgumentOutOfRangeException("table", string.Format("wrong demension sizes. rows = {0} columns = {1} pow = {2}", rowCount, colCount, pow));
        }

        public TruthTable(byte argNum, bool[] funcVal)
        {
            byte rowCount = RaiseTwoToXPow(argNum);
            byte colCount = (byte)(argNum + 1);
            if (rowCount != funcVal.Length)
                throw new ArgumentOutOfRangeException(string.Format("the size of array of function's values = {0}. Expected size (based on the number of arguments) = {1}", funcVal.Length, rowCount));
            else
            {
                Table = new bool[rowCount, colCount];
                ArgCount = argNum;

                for (byte i = 0x00; i < rowCount; i++)
                {
                    ReplaceRowWithWord(ref table, i, i, 1);
                    Table[i, colCount - 1] = funcVal[i];
                }
            }
        }

        public byte ArgCount { get; private set; }

        private bool[,] table;
        public bool[,] Table
        {
            get { return table; }
            set { table = value; }
        }

        private static bool IsPowerOfTwo(byte x, out byte pow)
        {
            pow = 0;
            while (((x & 1) == 0) && x > 1) /* While x is even and > 1 */
            {
                x >>= 1;
                pow++;
            }
            return (x == 1);
            //return ((x != 0x00) && ((x & (~x + 1)) == x));
        }
        private static byte RaiseTwoToXPow(byte x)
        {
            byte res = 0x01;
            return res <<= x;
        }

        private bool CheckTableDims(bool[,] table, out byte rowCount, out byte colCount, out byte pow)
        {
            rowCount = (byte)table.GetLength(0);
            colCount = (byte)table.GetLength(1);
            if (!IsPowerOfTwo(rowCount, out pow))
            {
                return false;
            }
            else
            {
                return colCount == (pow + 1);
            }
        }

        private bool CheckTableContent(bool[,] table)
        {
            byte rowCount = (byte)table.GetLength(0);
            byte colCount = (byte)table.GetLength(1);
            for (byte i = 0x00; i < rowCount; i++)
            {
                bool[] pattern = ConvertToBoolArray(i).Take(colCount - 1).ToArray();
                //System.Console.WriteLine(string.Join(",", pattern));
                if (SearchPerRows(table, pattern) == NOT_FOUND)
                    return false;
            }
            return true;
        }

        private static byte SearchPerRows(bool[,] table, bool[] pattern)
        {
            byte rowCount = (byte)table.GetLength(0);
            byte colCount = (byte)table.GetLength(1);
            if ((colCount - 1) != pattern.Length)
            {
                throw new ArgumentOutOfRangeException(string.Format("the size of table's row should be greater then pattern by one bit colCount = {0}, patternt length = {1}", colCount, pattern.Length));
            }
            else
            {
                for (byte i = 0; i < rowCount; i++)
                {
                    bool eq = true;
                    for (byte j = 0; j < colCount - 1; j++)
                    {
                        if (!table[i, j].Equals(pattern[j]))
                        {
                            eq = false;
                            break;
                        }
                    }
                    if (eq) return i;
                }
                return NOT_FOUND;
            }
        }

        private static bool[] ConvertToBoolArray(byte word)
        {
            byte shiftWord = word;
            bool[] retArr = new bool[WORD_SIZE];
            for (byte i = 0; i < WORD_SIZE; i++)
            {
                retArr[i] = (shiftWord & 1) == 0 ? false : true;
                shiftWord >>= 1;
            }
            return retArr;
        }

        //private static bool[] ConvertToBoolArray<T>(T x)
        //    where T: struct, IComparable<T>
        //{
        //    int size = System.Runtime.InteropServices.Marshal.SizeOf(x);
        //    bool[] retArr = new bool[size];
        //    for (byte i = 0; i < size; i++)
        //    {
        //        retArr[i] = (x & 1) == 0 ? false : true;
        //        x >>= x;
        //    }
        //}

        public Coverage GetNullCoverage()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            byte rowCount = (byte)Table.GetLength(0);
            byte colCount = (byte)Table.GetLength(1);
            StringBuilder retStr = new StringBuilder();
            for (byte i = 0; i < rowCount; i++)
            {
                for (byte j = 0; j < colCount - 1; j++)
                {
                    retStr.Append(Table[i, j] ? '1' : '0');
                }
                retStr.Append("|");
                bool lastElemInLine = Table[i, colCount - 1];
                retStr.Append(lastElemInLine ? '1' : '0');
                retStr.Append("\n");
            }
            return retStr.ToString();
        }


        public void Sort()
        {
            throw new NotImplementedException();
        }

        private static void ReplaceRowWithWord(ref bool[,] table, byte rowNum, byte word,
            byte shiftWordLeft = 0)
        {
            bool[] wordByteSet = ConvertToBoolArray(word);
            byte colCount = (byte)table.GetLength(1);
            for (byte i = 0; i < colCount - shiftWordLeft; i++)
            {
                table[rowNum, colCount - 1 - i - shiftWordLeft] = wordByteSet[i];
            }
        }
    }
}

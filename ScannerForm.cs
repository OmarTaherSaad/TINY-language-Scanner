using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MetroFramework.Forms;

namespace TINY_language_Scanner
{
    internal enum DFA_State
    {
        START,
        INCOMMENT,
        INNUM,
        INID,
        INASSIGN,
        DONE,
        ERROR,
        STRLTR
    }

    public partial class ScannerForm : MetroForm
    {
        private readonly List<KeyValuePair<string, string>> Keywords;
        private readonly List<KeyValuePair<string,string>> Symbols;
        private DFA_State currentState, nextState;

        public ScannerForm()
        {
            currentState = DFA_State.START;
            nextState = DFA_State.START;
            KeyValuePair<string, string>[] _symbols=
            {
                new KeyValuePair<string, string>("+", "PLUS_TK"),
                new KeyValuePair<string, string>("-", "MINUS_TK"),
                new KeyValuePair<string, string>("*", "MULTIPLY_TK"),
                new KeyValuePair<string, string>("/", "DIVIDE_TK"),
                new KeyValuePair<string, string>("=", "EQ_TK"),
                new KeyValuePair<string, string>("<", "ST_TK"),
                new KeyValuePair<string, string>(">", "GT_TK"),
                new KeyValuePair<string, string>("(", "OPEN_BRACKET_TK"),
                new KeyValuePair<string, string>(")", "CLOSE_BRACKET_TK"),
                new KeyValuePair<string, string>(";", "SEMICOLON_TK"),
                new KeyValuePair<string, string>(",", "COMMA_TK"),
                new KeyValuePair<string, string>("<=", "STE_TK"),
                new KeyValuePair<string, string>(">=", "GTE_TK"),
                new KeyValuePair<string, string>("<>", "NOT_EQUAL_TK")
            };
            KeyValuePair<string, string>[] _keywords =
            {
                new KeyValuePair<string, string>("if", "IF_TK"),
                new KeyValuePair<string, string>("then", "THEN_TK"),
                new KeyValuePair<string, string>("else", "ELSE_TK"),
                new KeyValuePair<string, string>("end", "END_TK"),
                new KeyValuePair<string, string>("repeat", "REPEAT_TK"),
                new KeyValuePair<string, string>("until", "UNTIL_TK"),
                new KeyValuePair<string, string>("read", "READ_TK"),
                new KeyValuePair<string, string>("write", "WRITE_TK"),
                new KeyValuePair<string, string>("while", "WHILE_TK")
            };
            Keywords = new List<KeyValuePair<string, string>>(_keywords);
            Symbols = new List<KeyValuePair<string,string>>(_symbols);

            InitializeComponent();
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            scanBtn.Enabled = false;
            var code = codeTextBox.Text;
            var currentToken = "";
            outputTextBox.Text = "";
            for (var i = 0; i < code.Length; i++)
            {
                var c = code[i];
                switch (currentState)
                {
                    case DFA_State.START:
                    {
                        currentToken = "";
                        if (c == 32 || c == 9 || c == 10) //Whitespace, Tab or new line
                            //White space
                            nextState = DFA_State.START;
                        else if (c == '{')
                            nextState = DFA_State.INCOMMENT;
                        else if (char.IsDigit(c))
                            nextState = DFA_State.INNUM;
                        else if (char.IsLetter(c))
                            nextState = DFA_State.INID;
                        else if (c == ':')
                            nextState = DFA_State.INASSIGN;
                        else if (Symbols.Any(s => s.Key == c.ToString()))
                            nextState = DFA_State.DONE;
                        else if (c == '"')
                            nextState = DFA_State.STRLTR;
                        else
                            //Other
                            nextState = DFA_State.DONE;
                        break;
                    }
                    case DFA_State.INCOMMENT:
                        nextState = c == '}' ? DFA_State.START : c == '{' ? DFA_State.ERROR : DFA_State.INCOMMENT;
                        break;
                    case DFA_State.STRLTR:
                    {
                        if (c == '"' && currentToken[currentToken.Length - 1] != '\\')
                        {
                            nextState = DFA_State.DONE;
                            currentToken += c;
                            if (i + 1 < code.Length)
                                c = code[++i];
                        }
                        else
                        {
                            nextState = DFA_State.STRLTR;
                        }

                        break;
                    }
                    case DFA_State.INNUM:
                    {
                        if (char.IsDigit(c))
                            nextState = DFA_State.INNUM;
                        else if (char.IsLetter(c))
                            nextState = DFA_State.ERROR;
                        else
                            //Other
                            nextState = DFA_State.DONE;

                        break;
                    }
                    case DFA_State.INID:
                    {
                        if (char.IsLetter(c) || char.IsDigit(c))
                            nextState = DFA_State.INID;
                        else
                            nextState = DFA_State.DONE;
                        break;
                    }
                    case DFA_State.INASSIGN:
                        if (c == '=')
                            nextState = DFA_State.DONE;
                        else
                            nextState = DFA_State.ERROR;
                        break;
                    case DFA_State.DONE:
                        currentToken = "";
                        break;
                    case DFA_State.ERROR:
                        nextState = DFA_State.DONE;
                        i = code.Length;
                        break;
                }

                if (nextState == DFA_State.DONE)
                {
                    if (currentState == DFA_State.START || currentState == DFA_State.INASSIGN)
                    {
                        //It's a one character token
                        currentToken += c;
                        i++; //To cancel the one below
                    }

                    IdentifyTokenType(currentToken);
                    currentToken = "";
                    nextState = DFA_State.START;
                    i--;
                }
                else
                {
                    currentToken += c;
                }

                currentState = nextState;
                //if (currentState != nextState && nextState != DFA_State.DONE)
                //{
                //    //Still in same token (state)

                //}
                //else
                //{
                //    //Changed the state -> token ended

                //}
            }
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            scanBtn.Enabled = true;
            outputFileBtn.Enabled = true;
        }

        private void outputFileBtn_Click(object sender, EventArgs e)
        {
            outputFileBtn.Enabled = false;
            var timestamp = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
            var filePath = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location) + "\\output-" + timestamp +
                           ".txt";
            File.AppendAllText(filePath, outputTextBox.Text);
        }

        private string IdentifyTokenType(string token)
        {
            if (token.Length == 0 || token.Trim().Length == 0) return "";
            var type = "";
            if (currentState == DFA_State.ERROR)
            {
                type = "EXCEPTION! Does not belong to TINY language";
            }
            else if (currentState == DFA_State.INNUM)
            {
                type = "Integer Literal";
            }
            else if (currentState == DFA_State.INID)
            {
                type = Keywords.Any(k => k.Key == token) ? Keywords.First(k => k.Key == token).Value : "identifier";
            }
            else if (currentState == DFA_State.INASSIGN)
            {
                type = "assign";
            }
            else if (currentState == DFA_State.STRLTR)
            {
                type = "String Literal";
            }
            else if (token.Length < 3 && Symbols.Any(s => s.Key == token))
            {
                type = Symbols.First(s => s.Key == token).Value;
            }
            else
            {
                type = "EXCEPTION! Does not belong to TINY language";
            }

            var text = token + "\t--> " + type + "\n";
            outputTextBox.Text += @" " + text;
            return text;
        }
    }
}
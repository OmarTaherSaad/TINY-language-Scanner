using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MetroFramework.Forms;

namespace TINY_language_Scanner
{
    internal enum DfaState
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
        readonly List<KeyValuePair<string, string>> Keywords;
        readonly List<KeyValuePair<string, string>> Symbols;
        DfaState currentState, nextState;

        public ScannerForm()
        {
            currentState = DfaState.START;
            nextState = DfaState.START;
            KeyValuePair<string, string>[] symbols =
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
            KeyValuePair<string, string>[] keywords =
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
            Keywords = new List<KeyValuePair<string, string>>(keywords);
            Symbols = new List<KeyValuePair<string, string>>(symbols);

            InitializeComponent();
        }

        private void scanBtn_Click(object sender, EventArgs e)
        {
            //Initialize everything
            currentState = DfaState.START;
            nextState = DfaState.START;
            scanBtn.Enabled = false;
            var code = codeTextBox.Text;
            var currentToken = "";
            outputTextBox.Text = "";
            for (var i = 0; i < code.Length; i++)
            {
                var c = code[i];
                switch (currentState)
                {
                    case DfaState.START:
                        {
                            currentToken = "";
                            if (c == 32 || c == 9 || c == 10) //Whitespace, Tab or new line
                                                              //White space
                                nextState = DfaState.START;
                            else if (c == '{')
                                nextState = DfaState.INCOMMENT;
                            else if (char.IsDigit(c))
                                nextState = DfaState.INNUM;
                            else if (char.IsLetter(c))
                                nextState = DfaState.INID;
                            else if (c == ':')
                                nextState = DfaState.INASSIGN;
                            else if (Symbols.Any(s => s.Key == c.ToString()))
                                nextState = DfaState.DONE;
                            else if (c == '"')
                                nextState = DfaState.STRLTR;
                            else
                                //Other
                                nextState = DfaState.DONE;
                            break;
                        }
                    case DfaState.INCOMMENT:
                        nextState = c == '}' ? DfaState.START : c == '{' ? DfaState.ERROR : DfaState.INCOMMENT;
                        break;
                    case DfaState.STRLTR:
                        {
                            if (c == '"' && currentToken[currentToken.Length - 1] != '\\')
                            {
                                nextState = DfaState.DONE;
                                currentToken += c;
                                if (i + 1 < code.Length)
                                    c = code[++i];
                            }
                            else
                            {
                                nextState = DfaState.STRLTR;
                            }

                            break;
                        }
                    case DfaState.INNUM:
                        {
                            if (char.IsDigit(c))
                                nextState = DfaState.INNUM;
                            else if (char.IsLetter(c))
                                nextState = DfaState.ERROR;
                            else
                                //Other
                                nextState = DfaState.DONE;

                            break;
                        }
                    case DfaState.INID:
                        {
                            if (char.IsLetter(c) || char.IsDigit(c))
                                nextState = DfaState.INID;
                            else
                                nextState = DfaState.DONE;
                            break;
                        }
                    case DfaState.INASSIGN:
                        if (c == '=')
                            nextState = DfaState.DONE;
                        else
                            nextState = DfaState.ERROR;
                        break;
                    case DfaState.DONE:
                        currentToken = "";
                        break;
                    case DfaState.ERROR:
                        nextState = DfaState.DONE;
                        i = code.Length;
                        break;
                }

                if (nextState == DfaState.DONE)
                {
                    if (currentState == DfaState.START || currentState == DfaState.INASSIGN)
                    {
                        //It's a one character token
                        currentToken += c;
                        i++; //To cancel the one below
                    }

                    IdentifyTokenType(currentToken);
                    currentToken = "";
                    nextState = DfaState.START;
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

            if (nextState == DfaState.INCOMMENT)
            {
                outputTextBox.Text += " UNCLOSED COMMENT \t--> ALL CODE AFTER IT IS IGNORED";
            }
            else
            {
                IdentifyTokenType(currentToken); //for last token
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
            if (currentState == DfaState.ERROR)
            {
                type = "EXCEPTION! Does not belong to TINY language";
            }
            else if (currentState == DfaState.INNUM)
            {
                type = "Integer Literal";
            }
            else if (currentState == DfaState.INID)
            {
                type = Keywords.Any(k => k.Key == token) ? Keywords.First(k => k.Key == token).Value : "identifier";
            }
            else if (currentState == DfaState.INASSIGN)
            {
                type = "assign";
            }
            else if (currentState == DfaState.STRLTR)
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
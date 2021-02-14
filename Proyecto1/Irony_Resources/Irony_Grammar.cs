// ------------------------------------------ Librerias E Imports --------------------------------------------
using Irony.Parsing;

// ------------------------------------------------ NameSpace ------------------------------------------------
namespace Proyecto1.Irony_Resources
{
    
    // Clase Irony_Grammar
    class Irony_Grammar : Grammar
    {

        // Constructor
        public Irony_Grammar() : base(caseSensitive: false) {

            // Región Expresiones Regulares
            #region RegularExpressions

                // Comentarios Unilinea
                CommentTerminal OneLineComment = new CommentTerminal("OneLineComment", "//", "\n", "\r\n");

                // Comentarios Multilinea 1
                CommentTerminal MultiLineComment1 = new CommentTerminal("MultiLineComment1", "(*", "*)");

                // Comentarios Multilinea 2
                CommentTerminal MultiLineComment2 = new CommentTerminal("MultiLineComment2", "{", "}");

                // Identificadores 
                IdentifierTerminal SimpleIdentifier = new IdentifierTerminal("Identifier");
            
                // Cadena String 
                StringLiteral SimpleString = new StringLiteral("SimpleString", "'");

                // Integer 
                NumberLiteral SimpleInteger = new NumberLiteral("SimpleInteger");

                // Real 
                RegexBasedTerminal SimpleReal = new RegexBasedTerminal("SimpleReal", "[0-9]+'.'[0-9]+");

            #endregion

            // Región Terminales
            #region Terminals

                // Tipos De Datos 

                BnfTerm StringType = ToTerm("string");

                BnfTerm IntegerType = ToTerm("integer");

                BnfTerm RealType = ToTerm("real");
          
                BnfTerm BooleanType = ToTerm("boolean");
             
                BnfTerm VoidType = ToTerm("void");

                BnfTerm ObjetcType = ToTerm("object");
          
                BnfTerm ArrayType = ToTerm("array");

                // Palabras Reservadas 

                BnfTerm ReservedProgram = ToTerm("program");

                BnfTerm ReservedEnd = ToTerm("end");
            
                BnfTerm ReservedType = ToTerm("type");

                BnfTerm ReservedBnfTerm = ToTerm("BnfTerm");

                BnfTerm ReservedBegin = ToTerm("begin");

                BnfTerm ReservedOf = ToTerm("of");

                BnfTerm ReservedConst = ToTerm("const");

                BnfTerm ReservedTrue = ToTerm("true");

                BnfTerm ReservedFalse = ToTerm("false");

                BnfTerm ReservedIf = ToTerm("if");

                BnfTerm ReservedThen = ToTerm("then");

                BnfTerm ReservedElse = ToTerm("else");

                BnfTerm ReservedCase = ToTerm("case");

                BnfTerm ReservedWhile = ToTerm("while");

                BnfTerm ReservedDo = ToTerm("do");

                BnfTerm ReservedTo = ToTerm("to");

                BnfTerm ReservedFor = ToTerm("for");

                BnfTerm ReservedRepeat = ToTerm("repeat");

                BnfTerm ReservedUntil = ToTerm("until");

                BnfTerm ReservedBreak = ToTerm("break");

                BnfTerm ReservedContinue = ToTerm("continue");

                BnfTerm ReservedFunction = ToTerm("function");

                BnfTerm ReservedProcedure = ToTerm("procedure");

                BnfTerm ReservedExit = ToTerm("exit");

                BnfTerm ReservedWrite = ToTerm("write");

                BnfTerm ReservedWriteLine = ToTerm("writeline");

                BnfTerm ReserverdGraficar = ToTerm("graficar_ts");

                // Simbolos (Comunes)

                KeyTerm SymbolSemiColon = ToTerm(";");

                BnfTerm SymbolColon = ToTerm(":");

                BnfTerm SymbolComma = ToTerm(",");

                BnfTerm SymbolLeftParenthesis = ToTerm("(");

                BnfTerm SymbolRightParenthesis = ToTerm(")");

                BnfTerm SymbolLeftBracket = ToTerm("[");

                BnfTerm SymbolRightBracket = ToTerm("]");

                // Operadores (Aritmeticos)

                BnfTerm OperatorPlus = ToTerm("+");

                BnfTerm OperatorMinus = ToTerm("-");

                BnfTerm OperatorMult = ToTerm("*");

                BnfTerm OperatorDiv = ToTerm("/");

                BnfTerm OperatorMod = ToTerm("%");

                // Operadores (Relacionales)

                BnfTerm OperatorGreater = ToTerm(">");

                BnfTerm OperatorLess = ToTerm("<");

                BnfTerm OperatorGreaterSame = ToTerm(">=");

                BnfTerm OperatorLessSame = ToTerm("<=");

                BnfTerm OperatorEqual = ToTerm("=");

                BnfTerm OperatorDiffer = ToTerm("<>");

                // Operadores (Lógicos)

                BnfTerm OperatorAnd = ToTerm("and");

                BnfTerm OperatorOr = ToTerm("or");

                BnfTerm OperatorNot = ToTerm("not");                

            #endregion            

            // Región No Terminales 

            #region NonTerminals

                // Instrucciones Iniciales 

                NonTerminal Begin = new NonTerminal("Begin");

                NonTerminal Instruccions = new NonTerminal("Instruccions");

                NonTerminal Instruccion = new NonTerminal("Instruccion");

                // Lista De Instrucciones
                
                NonTerminal InsProgram = new NonTerminal("InsProgram");

            #endregion

            // Región De Gramatica 

            #region Grammar

                // Inicio 
                Begin.Rule              = Instruccions + Eof
                                        | Eof
                                        ;

                // Lista De Instrucciones 
                Instruccions.Rule       = Instruccions + Instruccion
                                        | Instruccion
                                        ;

                // Instruccion Individual 
                Instruccion.Rule        = InsProgram
                                        ;

                 // Producción De Errores 
                Instruccion.ErrorRule   = SyntaxError + SymbolSemiColon
                                        ;

                // Instruccion Program 
                InsProgram.Rule         = ReservedProgram + SimpleIdentifier + SymbolSemiColon
                                        ;                

            #endregion

            // Región De Preferencias 
            #region Preferences

                // Inicio Del Arbol                 
                Root = Begin;

                // Marcar Palabras Reservadas 
                MarkReservedWords("program", "if");
            
                // Precedencia
                RegisterOperators(1, Associativity.Left, OperatorNot);

                RegisterOperators(2, Associativity.Left, OperatorOr);

                RegisterOperators(3, Associativity.Left, OperatorAnd);

                RegisterOperators(4, Associativity.Left, OperatorEqual, OperatorDiffer);

                RegisterOperators(5, Associativity.Left, OperatorGreaterSame, OperatorLessSame, OperatorGreater, OperatorLess);

                RegisterOperators(6, Associativity.Left, OperatorPlus, OperatorMinus);

                RegisterOperators(7, Associativity.Left, OperatorMult, OperatorDiv);

                RegisterOperators(8, Associativity.Left, OperatorMod);

                //NonGrammarTerminals.Add(OneLineComment);

                //NonGrammarTerminals.Add(MultiLineComment1);

                //NonGrammarTerminals.Add(MultiLineComment2);

            #endregion

        }

    }

}
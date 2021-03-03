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

                BnfTerm ReservedVar = ToTerm("var");

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

                BnfTerm ReservedGraficar = ToTerm("graficar_ts");

                // Simbolos (Comunes)

                KeyTerm SymbolSemiColon = ToTerm(";");

                BnfTerm SymbolColon = ToTerm(":");

                BnfTerm SymbolComma = ToTerm(",");

                BnfTerm SymbolPoint = ToTerm(".");

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

                NonTerminal Declarations = new NonTerminal("Delcarations");

                NonTerminal Declaration = new NonTerminal("Declaration");

                // Program             
                NonTerminal InsProgram = new NonTerminal("InsProgram");

                // Declaracion De Variables             
                NonTerminal VariablesDeclaration = new NonTerminal("VariablesDeclaration");

                NonTerminal VariablesDeclarationList = new NonTerminal("VariablesDeclarationList");

                NonTerminal DeclarationList = new NonTerminal("DeclarationList");

                NonTerminal VariablesDeclarationBlock = new NonTerminal("VariablesDeclarationBlock");

                NonTerminal Types = new NonTerminal("Types");

                NonTerminal VariableAsignationDec = new NonTerminal("VariableAsginationDec");

                NonTerminal VariablesValues = new NonTerminal("VariablesValues");

                // Constantes 
                NonTerminal ConstantsDeclaration = new NonTerminal("ConstantsDeclaration");

                NonTerminal ConstantsDeclarationBlock = new NonTerminal("ConstantsDeclarationBlock");

                NonTerminal Constants = new NonTerminal("Constants");

                NonTerminal ConstantsValues = new NonTerminal("ConstantsValues");

                // Asignacion De Variables 
                NonTerminal VariablesAsignation = new NonTerminal("VariablesAsignation");

                // Bloque Main 
                NonTerminal MainBlock = new NonTerminal("MainBlock");

                // Misc 

                // Valores Boolean
                NonTerminal SimpleBoolean = new NonTerminal("SimpleBoolean");

                // Expresiones 
                NonTerminal Expression = new NonTerminal("Expression");

                // Instruccion If 
                NonTerminal InsIf = new NonTerminal("InsIf");

                // Instrucciones If 
                NonTerminal InstruccionsIfCasLo = new NonTerminal("InstruccionsIfCasLo");

                // Instruccion If 
                NonTerminal InstruccionIfCasLo = new NonTerminal("InstruccionIfCasLo");

                // Bloque If 
                NonTerminal IfBlock = new NonTerminal("IfBlock");

                // Else 
                NonTerminal InsElse = new NonTerminal("InsElse");

                // Case 
                NonTerminal InsCase = new NonTerminal("InsCase");

                // Cases 
                NonTerminal Cases = new NonTerminal("Cases");

                // Bloque Case 
                NonTerminal CaseBlock = new NonTerminal("CaseBlock");

                // Case Else 
                NonTerminal CaseElse = new NonTerminal("CaseElse");

                // While 
                NonTerminal InsWhile = new NonTerminal("InsWhile");

                // Bloque While
                NonTerminal WhileBlock = new NonTerminal("WhileBlock");

                // For
                NonTerminal InsFor = new NonTerminal("InsFor");

                // Bloque For 
                NonTerminal ForBlock = new NonTerminal("ForBlock");

                // Repeat 
                NonTerminal InsRepeat = new NonTerminal("InsRepeat");

                // Bloque Repeat
                NonTerminal RepeatBlock = new NonTerminal("RepeatBlock");

                // Sentencias De Transferencias
                NonTerminal TransferSentences = new NonTerminal("TransferSenteces");

                // Comentarios
                //NonTerminal Comments = new NonTerminal("Comentarios");    

            #endregion

            // Región De Preferencias 
            #region Preferences

                // Inicio Del Arbol                 
                Root = Begin;

                // Diccionario De Palabras Reservadas 
                string[] DictionaryReservedWords =  {

                                                            "program",
                                                            "var",
                                                            "string",
                                                            "integer",
                                                            "real",
                                                            "boolean",
                                                            "true",
                                                            "false"

                                                        };

                // Marcar Palabras Reservadas 
                MarkReservedWords(DictionaryReservedWords);

                // Precedencia
                RegisterOperators(1, Associativity.Left, OperatorNot);

                RegisterOperators(2, Associativity.Left, OperatorOr);

                RegisterOperators(3, Associativity.Left, OperatorAnd);

                RegisterOperators(4, Associativity.Left, OperatorEqual, OperatorDiffer);

                RegisterOperators(5, Associativity.Left, OperatorGreaterSame, OperatorLessSame, OperatorGreater, OperatorLess);

                RegisterOperators(6, Associativity.Left, OperatorPlus, OperatorMinus);

                RegisterOperators(7, Associativity.Left, OperatorMult, OperatorDiv);

                RegisterOperators(8, Associativity.Left, OperatorMod);

                // Excluir Comentarios De La Gramatica 
                NonGrammarTerminals.Add(OneLineComment);

                NonGrammarTerminals.Add(MultiLineComment1);

                NonGrammarTerminals.Add(MultiLineComment2);

            #endregion

            // Región De Gramatica 
            #region Grammar

                // Inicio 
                Begin.Rule                      = InsProgram + Declarations + MainBlock
                                                | InsProgram + MainBlock
                                                | Eof
                                                ;

                // Errores Inicio 
                Begin.ErrorRule                 = SyntaxError + SymbolSemiColon
                                                | SyntaxError + ReservedEnd
                                                ;

                // Main Block 
                MainBlock.Rule                  = ReservedBegin + Instruccions + ReservedEnd + SymbolPoint
                                                | ReservedBegin + ReservedEnd + SymbolPoint
                                                ;

                // Produccion De Error 
                MainBlock.ErrorRule             = SyntaxError + SymbolSemiColon
                                                | SyntaxError + ReservedEnd
                                                ;

                // Instruccion Program 
                InsProgram.Rule                 = ReservedProgram + SimpleIdentifier + SymbolSemiColon  
                                                ;

                // Lista De Declaraciones
                Declarations.Rule               = Declarations + Declaration 
                                                | Declaration
                                                ;

                // Declaracion Individual
                Declaration.Rule                = VariablesDeclaration
                                                | ConstantsDeclaration
                                                ;

                // Producción De Errores 
                Declaration.ErrorRule          = SyntaxError + SymbolSemiColon
                                               | SyntaxError + ReservedEnd
                                               ;

                // Declaracion De Variables
                VariablesDeclaration.Rule       = ReservedVar + VariablesDeclarationBlock
                                                ;
            
                // Bloque Declaraciones 
                VariablesDeclarationBlock.Rule  = VariablesDeclarationBlock + VariablesDeclarationList
                                                | VariablesDeclarationList
                                                ;

                // Lista De Declaraciones Sintaxis
                VariablesDeclarationList.Rule   = DeclarationList + SymbolColon + Types + VariableAsignationDec
                                                ;

                // Lista De Delcaraciones 
                DeclarationList.Rule            = DeclarationList + SymbolComma + SimpleIdentifier  
                                                | SimpleIdentifier
                                                ;

                // Tipos De Datos 
                Types.Rule                      = StringType
                                                | IntegerType
                                                | RealType
                                                | BooleanType
                                                ;

                // Valores Boolean 
                SimpleBoolean.Rule              = ReservedTrue
                                                | ReservedFalse
                                                ;

                // Asignacion De Variables En Declaracion
                VariableAsignationDec.Rule      = OperatorEqual + VariablesValues + SymbolSemiColon
                                                | SymbolSemiColon
                                                ;

                // Valores Para Las Variables 
                VariablesValues.Rule            = SimpleString
                                                | SimpleInteger
                                                | SimpleReal
                                                | SimpleBoolean
                                                | SimpleIdentifier
                                                ;
                // Constantes 
                ConstantsDeclaration.Rule       = ReservedConst + ConstantsDeclarationBlock
                                                ;

                // Bloque Constantes 
                ConstantsDeclarationBlock.Rule  = ConstantsDeclarationBlock + Constants
                                                | Constants
                                                ;

                // Declaracion De Una Constante 
                Constants.Rule                  = SimpleIdentifier + OperatorEqual + ConstantsValues + SymbolSemiColon
                                                ;

                // Valores De Las Constantes 
                ConstantsValues.Rule            = SimpleString
                                                | SimpleInteger
                                                | SimpleReal
                                                | SimpleBoolean
                                                ;

                // Instrucciones 
                Instruccions.Rule               = Instruccions + Instruccion
                                                | Instruccion
                                                ;

                // Instruccion 
                Instruccion.Rule                = VariablesAsignation
                                                | InsIf
                                                | InsCase
                                                | InsWhile
                                                | InsFor
                                                | InsRepeat
                                                ;

                // Producción De Error 
                Instruccion.ErrorRule           = SyntaxError + SymbolSemiColon
                                                | SyntaxError + ReservedEnd
                                                ;

                // Asignacion De Variables 
                VariablesAsignation.Rule        = SimpleIdentifier + SymbolColon + OperatorEqual + Expression + SymbolSemiColon
                                                ;

                // If Else 
                InsIf.Rule                      = ReservedIf + SymbolLeftParenthesis + Expression + SymbolRightParenthesis + ReservedThen + IfBlock + InsElse
                                                ;

                // Bloque if 
                IfBlock.Rule                    = ReservedBegin + InstruccionsIfCasLo + ReservedEnd
                                                | ReservedBegin + ReservedEnd
                                                ;

                // Produccion De Error
                IfBlock.ErrorRule               = SyntaxError + SymbolSemiColon
                                                | SyntaxError + ReservedEnd
                                                ;

                // Else 
                InsElse.Rule                    = ReservedElse + IfBlock + SymbolSemiColon
                                                | ReservedElse + InsIf
                                                | SymbolSemiColon
                                                ;

                // Instrucciones If
                InstruccionsIfCasLo.Rule         = InstruccionsIfCasLo + InstruccionIfCasLo
                                                | InstruccionIfCasLo
                                                ;

                // Instruccion If 
                InstruccionIfCasLo.Rule         = VariablesAsignation
                                                | InsIf
                                                | InsCase
                                                | InsWhile
                                                | InsFor
                                                | InsRepeat
                                                | TransferSentences
                                                ;

                // Produccion De Error 
                InstruccionIfCasLo.ErrorRule    = SyntaxError + SymbolSemiColon 
                                                | SyntaxError + ReservedEnd
                                                ;

                // Case 
                InsCase.Rule                    = ReservedCase + SymbolLeftParenthesis + Expression + SymbolRightParenthesis + ReservedOf + Cases
                                                ;

                // Casos 
                Cases.Rule                      = Expression + SymbolColon + CaseBlock + CaseElse
                                                ;

                // Bloque Case 
                CaseBlock.Rule                  = ReservedBegin + InstruccionsIfCasLo + ReservedEnd + SymbolSemiColon
                                                | ReservedBegin + ReservedEnd + SymbolSemiColon
                                                ;

                // Case Else 
                CaseElse.Rule                   = ReservedElse + ReservedBegin + InstruccionsIfCasLo + ReservedEnd + SymbolSemiColon + ReservedEnd + SymbolSemiColon
                                                | ReservedEnd + SymbolSemiColon
                                                | Cases
                                                ;

                // While Do 
                InsWhile.Rule                   = ReservedWhile + Expression + ReservedDo + WhileBlock
                                                ;

                // While Block
                WhileBlock.Rule                 = ReservedBegin + InstruccionIfCasLo + ReservedEnd + SymbolSemiColon
                                                | ReservedBegin + ReservedEnd + SymbolSemiColon
                                                ;

                // For 
                InsFor.Rule                     = ReservedFor + SimpleIdentifier + SymbolColon + OperatorEqual + Expression + ReservedTo + Expression + ReservedDo + ForBlock
                                                ;

                // Bloque For 
                ForBlock.Rule                   = ReservedBegin + InstruccionIfCasLo + ReservedEnd + SymbolSemiColon
                                                | ReservedBegin + ReservedEnd + SymbolSemiColon
                                                ;

                // Until Repeat
                InsRepeat.Rule                  = ReservedRepeat + RepeatBlock + ReservedUntil + Expression + SymbolSemiColon
                                                ;

                // Bloque Repeat 
                RepeatBlock.Rule                = ReservedBegin + InstruccionIfCasLo + ReservedEnd + SymbolSemiColon
                                                | ReservedBegin + ReservedEnd + SymbolSemiColon
                                                ;

                // Sentecia De Transeferencias
                TransferSentences.Rule          = ReservedBreak
                                                | ReservedContinue
                                                ;

                // Expresiones 
                Expression.Rule                 = SymbolLeftParenthesis + Expression + SymbolRightParenthesis 
                                                | Expression + OperatorPlus + Expression
                                                | Expression + OperatorMinus + Expression
                                                | Expression + OperatorMult + Expression
                                                | Expression + OperatorDiv + Expression
                                                | Expression + OperatorMod + Expression 
                                                | OperatorMinus + Expression
                                                | OperatorNot + Expression
                                                | Expression + OperatorLessSame + Expression
                                                | Expression + OperatorGreaterSame + Expression
                                                | Expression + OperatorLess + Expression
                                                | Expression + OperatorGreater + Expression
                                                | Expression + OperatorEqual + Expression
                                                | Expression + OperatorDiffer + Expression
                                                | Expression + OperatorAnd + Expression
                                                | Expression + OperatorOr + Expression
                                                | VariablesValues
                                                ;

                // Instruccion Comentarios 
                //Comments.Rule               = OneLineComment
                //                            | MultiLineComment1
                //                            | MultiLineComment2
                //                            ;

            #endregion   

        }

    }

}
// ------------------------------------------ Librerias E Imports -----------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using Irony.Ast;
using Irony.Parsing;

// ------------------------------------------------ NameSpace ---------------------------------------------------------
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
                              
                var StringType = ToTerm("string");    
            
                var IntegerType = ToTerm("integer");

                var RealType = ToTerm("real");
          
                var BooleanType = ToTerm("boolean");
             
                var VoidType = ToTerm("void");

                var ObjetcType = ToTerm("object");
          
                var ArrayType = ToTerm("array");

                // Palabras Reservadas 

                var ReservedProgram = ToTerm("program");

                var ReservedEnd = ToTerm("end");
            
                var ReservedType = ToTerm("type");

                var ReservedVar = ToTerm("var");

                var ReservedBegin = ToTerm("begin");

                var ReservedOf = ToTerm("of");

                var ReservedConst = ToTerm("const");

                var ReservedTrue = ToTerm("true");

                var ReservedFalse = ToTerm("false");

                var ReservedIf = ToTerm("if");

                var ReservedThen = ToTerm("then");

                var ReservedElse = ToTerm("else");

                var ReservedCase = ToTerm("case");

                var ReservedWhile = ToTerm("while");

                var ReservedDo = ToTerm("do");

                var ReservedTo = ToTerm("to");

                var ReservedFor = ToTerm("for");

                var ReservedRepeat = ToTerm("repeat");

                var ReservedUntil = ToTerm("until");

                var ReservedBreak = ToTerm("break");

                var ReservedContinue = ToTerm("continue");

                var ReservedFunction = ToTerm("function");

                var ReservedProcedure = ToTerm("procedure");

                var ReservedExit = ToTerm("exit");

                var ReservedWrite = ToTerm("write");

                var ReservedWriteLine = ToTerm("writeline");

                var ReserverdGraficar = ToTerm("graficar_ts");

                // Simbolos (Comunes)

                var SymbolSemiColon = ToTerm(";");

                var SymbolColon = ToTerm(":");

                var SymbolComma = ToTerm(",");

                var SymbolLeftParenthesis = ToTerm("(");

                var SymbolRightParenthesis = ToTerm(")");

                var SymbolLeftBracket = ToTerm("[");

                var SymbolRightBracket = ToTerm("]");

                // Operadores (Aritmeticos)

                var OperatorPlus = ToTerm("+");

                var OperatorMinus = ToTerm("-");

                var OperatorMult = ToTerm("*");

                var OperatorDiv = ToTerm("/");

                var OperatorMod = ToTerm("%");

                // Operadores (Relacionales)

                var OperatorGreater = ToTerm(">");

                var OperatorLess = ToTerm("<");

                var OperatorGreaterSame = ToTerm(">=");

                var OperatorLessSame = ToTerm("<=");

                var OperatorEqual = ToTerm("=");

                var OperatorDiffer = ToTerm("<>");

                // Operadores (Lógicos)

                var OperatorAnd = ToTerm("and");

                var OperatorOr = ToTerm("or");

                var OperatorNot = ToTerm("not");

                // Precedencia

                RegisterOperators(1, OperatorNot);

                RegisterOperators(2, OperatorOr);

                RegisterOperators(3, OperatorAnd);

                RegisterOperators(4, OperatorEqual, OperatorDiffer);

                RegisterOperators(5, OperatorGreaterSame, OperatorLessSame, OperatorGreater, OperatorLess);
                
                RegisterOperators(6, OperatorPlus, OperatorMinus);

                RegisterOperators(7, OperatorMult, OperatorDiv);

                RegisterOperators(8, OperatorMod);

                // Terminales Fuera De La Gramatica

                NonGrammarTerminals.Add(OneLineComment);

                NonGrammarTerminals.Add(MultiLineComment1);

                NonGrammarTerminals.Add(MultiLineComment2);

            #endregion

            // Región No Terminales 

            #region NonTerminals

                // Instrucciones Iniciales 

                NonTerminal Begin = new NonTerminal("Begin");

                NonTerminal Instruccions = new NonTerminal("Instruccions");

                NonTerminal Instruccion = new NonTerminal("Instruccion");

            #endregion

            // Región De Gramatica 

            #region Grammar

                // Inicio 

                Begin.Rule = Instruccions;

                // Instrucciones

                Instruccions.Rule = Instruccions + Instruccion
                                  | Instruccion
                                  ;

                // Lista De Instrucciones 
                Instruccion.Rule = SimpleIdentifier + SymbolSemiColon;

            #endregion

            // Región De Preferencias 
            #region Preferences

                // Inicio Del Arbol 
                
                this.Root = Begin;

            #endregion

        }

    }

}
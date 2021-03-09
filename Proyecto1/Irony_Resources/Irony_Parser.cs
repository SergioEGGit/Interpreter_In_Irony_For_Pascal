// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Irony.Parsing;
using Proyecto1.TranslatorAndInterpreter;
using Proyecto1.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto1.Irony_Resources
{

    // Clase Irony_Parser
    class Irony_Parser
    {

        // Analizador Sintactico 

        // Inicio 

        public void Begin(ParseTreeNode RootNode)
        {

            // Verficar Camino De La Produccion
            if(RootNode.ChildNodes.Count == 3)
            {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes[0]);

                // Lista De Delcaraciones 
                Declarations(RootNode.ChildNodes[1]);

                // Main Block 

            }
            else if (RootNode.ChildNodes.Count == 2)
            {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes[0]);

                // Lista De Delcaraciones 
                Declarations(RootNode.ChildNodes[1]);

            }
            else
            {

                // No Se Hace Nada 
                // Archivo Vacio

            }

        }

        // Instruccion Program
        public void InsProgram(ParseTreeNode ActualNode)
        {

            // Agregar Clase A La Lista 
            Variables.TranslateList.AddLast(new InsProgram(SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default")));
        
        }

        // Lista De Declaraciones 
        public void Declarations(ParseTreeNode ActualNode)
        {

            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Delcaraciones
                Declarations(ActualNode.ChildNodes[0]);

                // Declaracion
                Declaration(ActualNode.ChildNodes[1]);
            
            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Declaracion
                Declaration(ActualNode.ChildNodes[0]);

            }

        }

        // Delcaracion  
        public void Declaration(ParseTreeNode ActualNode)
        {

            // Verificar Nombre Del ActualNode 
            if(ActualNode.ChildNodes[0].ToString().Equals("VariablesDeclaration"))
            {

                // Delcaracions De Variables
                VariablesDeclaration(ActualNode.ChildNodes[0]);
  
            }

        }

        // Declaracion De Variables 
        public void VariablesDeclaration(ParseTreeNode ActualNode)
        {

            // Agregar Clase Declaracion De Variables 
            Variables.TranslateList.AddLast(new VariablesDeclaration());
            
            // Bloque Declaracion
            VariablesDeclarationBlock(ActualNode.ChildNodes[1]);

        }

        // Bloque Declaracion
        public void VariablesDeclarationBlock(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Bloque Declaracion
                VariablesDeclarationBlock(ActualNode.ChildNodes[0]);
                
                // LIsta De Declaraciones
                VariablesDeclarationList(ActualNode.ChildNodes[1]);

            }
            else if (ActualNode.ChildNodes.Count == 1)
            {

                // Lista De Declaraciones 
                VariablesDeclarationList(ActualNode.ChildNodes[0]);

            }

        }

        // Lista De Declaraciones
        public void VariablesDeclarationList(ParseTreeNode ActualNode)
        {
            
            // Obtener Identificadores 
            String Identifiers = DeclarationList(ActualNode.ChildNodes[0]);
         
            // Obtener Tipo
            String Type = SplitMethod(ActualNode.ChildNodes[2].ChildNodes[0].ToString(), "Default");

            // Expression
            AbstractExpression Expression = VariableAsignationDec(ActualNode.ChildNodes[3]);

            // Verificar Token 
            if (ActualNode.ChildNodes[0].ChildNodes.Count == 3)
            {

                // Columna Token 
                Variables.TokenColumn = ActualNode.ChildNodes[0].ChildNodes[2].Token.Location.Column;

                // Fila Token 
                Variables.TokenLine = ActualNode.ChildNodes[0].ChildNodes[2].Token.Location.Column;

            } 
            else if (ActualNode.ChildNodes[0].ChildNodes.Count == 1) 
            {

                // Columna Token 
                Variables.TokenColumn = ActualNode.ChildNodes[0].ChildNodes[0].Token.Location.Column;

                // Fila Token 
                Variables.TokenLine = ActualNode.ChildNodes[0].ChildNodes[0].Token.Location.Column;

            } 

            // Agregar A Lista De Clases 
            Variables.TranslateList.AddLast(new PrimitiveDeclaration(Identifiers, Type, Expression, "Var", Variables.TokenColumn, Variables.TokenLine));

        }

        // Lista De Identificadores 
        public String DeclarationList(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Obtener Identificadores 
                return DeclarationList(ActualNode.ChildNodes[0]) + "," + SplitMethod(ActualNode.ChildNodes[2].ToString(), "Default");
            
            }
            else if(ActualNode.ChildNodes.Count == 1)
            {
                
                // Identificador 
                return SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");
            
            }
            else
            {

                // Vacio
                return "";
            
            }
            
        }

        // Asignacion En Declaracion
        public AbstractExpression VariableAsignationDec(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 3)
            {
                
                // Retornar Expression
                return Expression(ActualNode.ChildNodes[1]);
                
            }
            else if (ActualNode.ChildNodes.Count == 1)
            {
               
                // Sin Asignacion
                return null;

            }
            else
            {

                // Error
                return null;
            
            }
        
        }

        // Expresion
        public AbstractExpression Expression(ParseTreeNode ActualNode)
        {
            
            // Verificar Camino A Seguir
            if (ActualNode.ChildNodes.Count == 1)
            {

                // Obtener Valor Primitivo 
                return VariablesValues(ActualNode.ChildNodes[0]);

            }
            else if (ActualNode.ChildNodes.Count == 2)
            {


                // Obtener Operador 
                String Operator = SplitMethod(ActualNode.ChildNodes[0].ToString(), "Default");

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn = null;

                // Switch 
                switch(Operator)
                {
                
                    case "-":

                        AuxiliaryReturn = new Arithmetic(null, Expression(ActualNode.ChildNodes[1]), "Minus", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

                    break;

                    case "not":

                        AuxiliaryReturn = new Logical(null, Expression(ActualNode.ChildNodes[1]), "Not", ActualNode.ChildNodes[0].Token.Location.Line, ActualNode.ChildNodes[0].Token.Location.Column);

                        break;


                }

                // Retornar 
                return AuxiliaryReturn;

            }
            else if (ActualNode.ChildNodes.Count == 3 && ActualNode.ChildNodes[1].ToString().Equals("Expression"))
            {

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn;

                // Agregar Clase 
                AuxiliaryReturn = new Parenthesis(Expression(ActualNode.ChildNodes[1]));

                // Retornar 
                return AuxiliaryReturn;

            }
            else
            {

                // Obtener Operador 
                String Operator = SplitMethod(ActualNode.ChildNodes[1].ToString(), "Default");

                // Objeto Auxiliar 
                AbstractExpression AuxiliaryReturn = null;

                // Switch 
                switch(Operator)
                {
                
                    case "+":
                        
                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Sum", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "-":

                        // Retornar
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Substraction", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "*":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Multiplication", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "/":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Division", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "%":

                        // Retornar 
                        AuxiliaryReturn = new Arithmetic(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Mod", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "<=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "LessSame", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case ">=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "GreaterSame", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "<":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Less", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case ">":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Greater", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "=":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Equal", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "<>":

                        // Retornar 
                        AuxiliaryReturn = new Relational(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Differ", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "and":

                        // Retornar 
                        AuxiliaryReturn = new Logical(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "And", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;

                    case "or":

                        // Retornar 
                        AuxiliaryReturn = new Logical(Expression(ActualNode.ChildNodes[0]), Expression(ActualNode.ChildNodes[2]), "Or", ActualNode.ChildNodes[1].Token.Location.Line, ActualNode.ChildNodes[1].Token.Location.Column);

                        break;
                        
                }

                // Retorno 
                return AuxiliaryReturn;

            }
            
            // Null
            return null;
 
        }

        // Valores Primitivos
        public AbstractExpression VariablesValues(ParseTreeNode ActualNode)
        {
            
            // Retornar Valor Expression
            return new PrimitiveValue(SplitMethod(ActualNode.ChildNodes[0].ToString(), "Value"));

        }

        // Misc 

        // Método Analizar 
        public void AnalyzeTranslate(String AnalyzeString)
        {

            // Instancia Clase Gramatica 
            Irony_Grammar AnalyzeGrammar = new Irony_Grammar();

            // Instancia Clase Lenguaje
            LanguageData AnalyzeData = new LanguageData(AnalyzeGrammar);

            // Errores Que Se Enuentren En Mi Gramatica 
            foreach (var item in AnalyzeData.Errors)
            {

                // Mostrar Error En La Gramatica
                MessageBox.Show(item.Message.ToString());

            }

            // Instancia Clase Analizador 
            Parser AnalyzeParser = new Parser(AnalyzeData);

            // Instancia Arbol De Analisis Sintactico
            ParseTree AnalyzeTree = AnalyzeParser.Parse(AnalyzeString);

            // Instancia ActualNode Arbol De Analisis Sintactico
            ParseTreeNode RootTreeNode = AnalyzeTree.Root;

            // Inicializar Lista De Errores 
            Variables.ErrorList = new LinkedList<ErrorTable>();

            // Manejo De Errores 
            if (AnalyzeTree.ParserMessages.Count > 0)
            {

                // Contador Auxiliar 
                Variables.AuxiliaryCounter = 1;

                // Recorrer Mensajes De Error 
                foreach (var ItemError in AnalyzeTree.ParserMessages)
                {

                    // Errores Lexicos Y Sintacticos
                    if (ItemError.Message.Contains("Invalid character"))
                    {

                        // Obtener Error 
                        String CharacterError = ItemError.Message.Split(' ')[2];

                        // SplitMethodlitear Error 
                        CharacterError = CharacterError.Split('.')[0];

                        // Agregar Error Lexico A Lista
                        Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Lexico", "El Caracter " + CharacterError + " No Es Permtdo Por El Lenguaje.", ItemError.Location.Line, ItemError.Location.Column));

                    }
                    else
                    {

                        // Agregar Error Sintactico A Lista 
                        Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Sintáctico", ItemError.Message, ItemError.Location.Line, ItemError.Location.Column));

                    }

                    // Sumar Contador
                    Variables.AuxiliaryCounter += 1;

                }

            }

            // Verificar Si La Raiz Esta Nula 
            if (RootTreeNode == null)
            {

                // Mostrar Mensage 
                MessageBox.Show("Existen Errores En El Analisis..!");

            }
            else
            {

                // Generar Arbol De Analisis Sintactico 
                GenerateReportAST(RootTreeNode);

                // Verificar Si El Archivo No Esta Vacio
                if (RootTreeNode.ChildNodes.Count > 0)
                {

                    // Limpiar Lista De Clases
                    Variables.TranslateList = new LinkedList<AbstractInstruccion>();

                    // Limpiar Variable Traduccion 
                    Variables.TranslateString = "";

                    // Ejecutar Recorrido Del Arbol 
                    Begin(RootTreeNode);

                    // Crear Primer Ambiente (Global)
                    EnviromentTable GlobalEnv = new EnviromentTable(null, "Env_Global");

                    // Recorrer Lista De Traduccion
                    foreach (var ItemTranslate in Variables.TranslateList)
                    {

                        // Llamar A Método Traducir 
                        ItemTranslate.Execute(GlobalEnv);
                        ItemTranslate.Translate(GlobalEnv);

                    }

                }

                // Mensaje De Exito 
                //MessageBox.Show("Traducción Completada Con Exito!");

            }

        }

        // Método Para Ejecutar Comando En Cmd 
        private void ExecuteCommand(String StringCommand)
        {

            // Iniciar Proceso CMD Con El Comando Indicado
            ProcessStartInfo InfoProcess = new ProcessStartInfo("cmd", "/c " + StringCommand)
            {

                // Escirbir La Salida Del Comando En Un Stream 
                RedirectStandardOutput = true,
                UseShellExecute = false,

                // Evitar Que El Proceso Inicie La Consola 
                CreateNoWindow = true

            };

            // Crear El Proceso  
            Process SimpleProcess = new Process
            {

                // Agregar Informacion E Iniciar El Proceso
                StartInfo = InfoProcess

            };

            // Inciar El Proceso 
            SimpleProcess.Start();

            // Recibiar En Un String El Resultado 
            // String Result = SimpleProcess.StandardOutput.ReadToEnd();

        }

        // Método Reporte Arbol AST 
        private void GenerateReportAST(ParseTreeNode RootTreeNode)
        {

            // Cadena Que Contiene El Codigo De Graphviz 
            String GraphicString = Irony_CommonMethods.GenerateGraphicString(RootTreeNode);

            // Cadena COn El Comando De Graphviz 
            String CommandString = "dot -Tpdf -o C:\\compiladores2\\ReporteAST.pdf C:\\compiladores2\\ReporteAST.txt";

            // Try Catch Para Evitar Errores 
            try
            {

                // Crear FStream Archivo 
                FileStream SimpleFileStream = File.Create("C:\\compiladores2\\ReporteAST.txt");

                // Crear Arreglo De Bytes Para Escribir La Cadena 
                byte[] ArchiveData = new UTF8Encoding(true).GetBytes(GraphicString);

                // Escribir Arreglo En Archivo 
                SimpleFileStream.Write(ArchiveData, 0, ArchiveData.Length);

                // Cerrar Archivo 
                SimpleFileStream.Close();

                // Ejecutar Comando 
                ExecuteCommand(CommandString);

            }
            catch (Exception)
            {

                // Mostrar Mensaje De Error 
                MessageBox.Show("Error Al Generar El Reporte!");

            }

        }

        // Método Que Realiza Un Split A ActualNode Del Arbol  Part_De_Interes (Keyword)
        private String SplitMethod(String SplitString, String Type)
        {

            // Array 
            String[] AuxiliaryArray = null;

            // Verificar Tipo
            if (Type.Equals("Value")) {

                // Array Auxiliar Para Guardar El Texto Spliteado
                AuxiliaryArray = SplitString.Split('(');

            } 
            else if(Type.Equals("Default")) 
            {

                // Array Auxiliar Para Guardar El Texto Spliteado
                AuxiliaryArray = SplitString.Split(' ');

            }

            // Verificar Si Hay Mas De Un Elemento En El Array 
            if(AuxiliaryArray.Length > 1)
            {

                // Verificar Si Es Valor
                if(Type.Equals("Value")) 
                {

                    // Recorrer Array
                    AuxiliaryArray[0] = AuxiliaryArray[0].TrimEnd();
                
                }
                
                // Retornar Primera Posicion
                return AuxiliaryArray[0];
            
            }

            // Retornar Cadena 
            return SplitString;

        }

    }

}
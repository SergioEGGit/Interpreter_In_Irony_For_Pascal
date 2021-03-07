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
            Variables.TranslateList.AddLast(new InsProgram(SplitMethod(ActualNode.ChildNodes[1].ToString())));
        
        }

        // Lista De Declaraciones 
        public void Declarations(ParseTreeNode Nodo)
        {
            //aqui miramos si hay dos o uno
            if (Nodo.ChildNodes.Count == 2)
            {//recursividad
                Declarations(Nodo.ChildNodes[0]);
                Declaration(Nodo.ChildNodes[1]);
            }
            else if (Nodo.ChildNodes.Count == 1)
            {
                Declaration(Nodo.ChildNodes[0]);
            }
            else
            {
                //imagino aqui con tu produccion de error lo miras
                //los errores los llevaria hasta deSplitMethodues pero ya miras vos
            }
        }

        public void Declaration(ParseTreeNode Nodo)
        {
            if (Nodo.ChildNodes[0].ToString() == "VariablesDeclaration")
            {
                VariablesDeclaration(Nodo.ChildNodes[0]);
            }
            else
            {

            }
        }

        public void VariablesDeclaration(ParseTreeNode Nodo)
        {
            VariablesDeclarationBlock(Nodo.ChildNodes[1]);
        }

        public void VariablesDeclarationBlock(ParseTreeNode Nodo)
        {
            if (Nodo.ChildNodes.Count == 2)
            {
                VariablesDeclarationBlock(Nodo.ChildNodes[0]);
                VariablesDeclarationList(Nodo.ChildNodes[1]);
            }
            else if (Nodo.ChildNodes.Count == 1)
            {
                VariablesDeclarationList(Nodo.ChildNodes[0]);
            }
            else
            {
            }
        }

        public void VariablesDeclarationList(ParseTreeNode Nodo)
        {
            String Ids = DeclarationList(Nodo.ChildNodes[0]);
            String Tipo = SplitMethod(Nodo.ChildNodes[2].ToString());
            AbstractExpression Expresion = VariableAsignationDec(Nodo.ChildNodes[3]);
            Variables.TranslateList.AddLast(new PrimitiveDeclaration(Ids, Tipo, Expresion, "var"));
        }

        public String DeclarationList(ParseTreeNode Nodo)
        {
            if (Nodo.ChildNodes.Count == 3)
            {
                return DeclarationList(Nodo.ChildNodes[0]) + "," + SplitMethod(Nodo.ChildNodes[0].ToString());
            }
            else if (Nodo.ChildNodes.Count == 1)
            {
                return SplitMethod(Nodo.ChildNodes[0].ToString());
            }
            else
            {
                return "";
            }
            
        }

        public AbstractExpression VariableAsignationDec(ParseTreeNode Nodo)
        {
            if (Nodo.ChildNodes.Count == 3)
            {
                return Expression(Nodo.ChildNodes[1]);
            }
            else if (Nodo.ChildNodes.Count == 1)
            {
                return null;
            }
            else
            {
                return null;
            }
        }

        public AbstractExpression Expression(ParseTreeNode Nodo)
        {
            if (Nodo.ChildNodes.Count == 1)
            {
                return VariablesValues(Nodo.ChildNodes[0]);
            }
            else if (Nodo.ChildNodes.Count == 2)
            {

            }
            else
            {
                String Operador = SplitMethod(Nodo.ChildNodes[1].ToString());
                switch (Operador)
                {
                    case "+":
                        return new SUM(Expression(Nodo.ChildNodes[0]), Expression(Nodo.ChildNodes[2]));
                        break;
                }

            }              
            return null;
        }

        public AbstractExpression VariablesValues(ParseTreeNode Nodo)
        {
            return new PrimitiveValue(SplitMethod(Nodo.ChildNodes[0].ToString()));
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

            // Instancia Nodo Arbol De Analisis Sintactico
            ParseTreeNode RootTreeNode = AnalyzeTree.Root;

            // Inicializar Lista De Errores 
            Variables.ErrorList = new LinkedList<ErrorTable>();

            // Manejo De Errores 
            if (AnalyzeTree.ParserMessages.Count > 0)
            {

                // Contador Auxiliar 
                int AuxiliaryCounter = 1;

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
                        Variables.ErrorList.AddLast(new ErrorTable(AuxiliaryCounter, "Lexico", "El Caracter " + CharacterError + " No Es Permtdo Por El Lenguaje.", ItemError.Location.Line, ItemError.Location.Column));

                    }
                    else
                    {

                        // Agregar Error Sintactico A Lista 
                        Variables.ErrorList.AddLast(new ErrorTable(AuxiliaryCounter, "Sintáctico", ItemError.Message, ItemError.Location.Line, ItemError.Location.Column));

                    }

                    // Sumar Contador
                    AuxiliaryCounter += 1;

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

        // Método Que Realiza Un Split A Nodo Del Arbol  Part_De_Interes (Keyword)
        private String SplitMethod(String SplitString)
        {

            // Array Auxiliar Para Guardar El Texto Spliteado
            String[] AuxiliaryArray = SplitString.Split(' ');

            // Verificar Si Hay Mas De Un Elemento En El Array 
            if(AuxiliaryArray.Length > 1)
            {

                // Retornar Primera Posicion
                return AuxiliaryArray[0];
            
            }

            // Retornar Cadena 
            return SplitString;

        }

    }

}
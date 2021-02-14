// ------------------------------------------ Librerias E Imports --------------------------------------------
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Irony.Parsing;
using Proyecto1.TranslatorAndInterpreter;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -----------------------------------------------
namespace Proyecto1.Irony_Resources
{
    
    // Clase Irony_Parser
    class Irony_Parser
    {

        // Analizador Sintactico 

        // Instrucciones
        public LinkedList<AbstractInstruccion> Instruccions(ParseTreeNode ActualNode) {

            // Variables 
            LinkedList<AbstractInstruccion> AuxiliaryList = new LinkedList<AbstractInstruccion>();
            
            // Verificar Gramatica

            // Si Es Igual A Dos Primera Produccion De Lo Contrario Segunda Produccion
            if(ActualNode.ToString().Equals("Instruccions"))
            {

                // Obtener Lista De Instrucciones 
                AuxiliaryList = Instruccions(ActualNode.ChildNodes.ElementAt(0));

                // Agregar Instruccion Al Final 
                AuxiliaryList.AddLast(Instruccion(ActualNode.ChildNodes.ElementAt(1)));

            }
            else 
            {
             
                // Agregar Instruccion Al Final 
                AuxiliaryList.AddLast(Instruccion(ActualNode.ChildNodes.ElementAt(0)));
            
            }

            // Retornar Valor 
            return AuxiliaryList;
        
        }

        // Instruccion 
        public AbstractInstruccion Instruccion(ParseTreeNode ActualNode) {

            // Obtener Token Actual 
            String Token1 = ActualNode.ChildNodes.ElementAt(0).ToString();

            // Verificar Instruccion 
            return Token1 switch
            {

                // Caso 1
                "InsProgram" => InsProgram(ActualNode.ChildNodes.ElementAt(0)),

                // Caso Default
                _ => null,
            
            };

        }

        // Instruccion Programa 
        public AbstractInstruccion InsProgram(ParseTreeNode ActualNode) {

            // Retornar Valor 
            return new InsProgram();            

        }

        // Misc 
        
        // Método Analizar 
        public void AnalyzeTranslate(String AnalyzeString) {

            // Instancia Clase Gramatica 
            Irony_Grammar AnalyzeGrammar = new Irony_Grammar();

            // Instancia Clase Lenguaje
            LanguageData AnalyzeData = new LanguageData(AnalyzeGrammar);

            foreach (var item in AnalyzeData.Errors) {

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
            if (AnalyzeTree.ParserMessages.Count > 0) {

                // Contador Auxiliar 
                int AuxiliaryCounter = 1;               

                // Recorrer Mensajes De Error 
                foreach(var ItemError in AnalyzeTree.ParserMessages) {
             
                    // Errores Lexicos Y Sintacticos
                    if(ItemError.Message.Contains("Invalid character"))
                    {

                        // Obtener Error 
                        String CharacterError = ItemError.Message.Split(' ')[2];

                        // Splitear Error 
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
            if(RootTreeNode == null)
            {

                // Mostrar Mensage 
                MessageBox.Show("Fallo En La Recuperación Del Análisis");

            }
            else
            {

                // Generar Arbol De Analisis Sintactico 
                GenerateReportAST(RootTreeNode);

                // Verificar Si El Archivo No Esta Vacio
                if(RootTreeNode.ChildNodes.Count > 0) {

                    // Ejecutar Recorrido Del Arbol 
                    LinkedList<AbstractInstruccion> AuxiliaryList = Instruccions(RootTreeNode.ChildNodes.ElementAt(0));

                    // Recorrido Para Traducir 
                    foreach (var ItemTranslate in AuxiliaryList)
                    {

                        // Llamar A Método Traducir 
                        // ItemTranslate.Translate();

                    }

                }

                // Mensaje De Exito 
                MessageBox.Show("Traducción Completada Con Exito!");

            }            

        }

        // Método Para Ejecutar Comando En Cmd 
        public void ExecuteCommand(String StringCommand) {

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
        public void GenerateReportAST(ParseTreeNode RootTreeNode) {

            // Variables 

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
            catch(Exception)
            {

                // Mostrar Mensaje De Error 
                MessageBox.Show("Error Al Generar El Reporte!");
            
            }
        
        }

    }

}
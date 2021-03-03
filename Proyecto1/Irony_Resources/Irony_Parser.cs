// ------------------------------------------ Librerias E Imports -----------------------------------------------
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

// ------------------------------------------------ NameSpace ---------------------------------------------------
namespace Proyecto1.Irony_Resources
{
    
    // Clase Irony_Parser
    class Irony_Parser
    {

        // Analizador Sintactico 

        // Inicio 
        public void Begin(ParseTreeNode RootNode) { 

            // Verificar Cantidad De Nodos 
            if (RootNode.ChildNodes.Count == 2)
            {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes.ElementAt(0));//guarda lo que retorna? por el momento si pero seria de reto
                //esto guardado en PROGRAM
                // Declaraciones
                Declarations(RootNode.ChildNodes.ElementAt(1));
                //esto GUARDADO en DECLARACIONES
                //por ejemplo aqui harias algo asi
                //return new InicioAbsoluto(PROGRAM,DECLARACION,Funcion no hay entonces []| null,Begin tampoco entonces [] | null)

            }
            else if(RootNode.ChildNodes.Count == 1) {

                // Instruccion Program 
                InsProgram(RootNode.ChildNodes.ElementAt(0));
                //Esto guardado en PROGRAM
                //return new InicioAbsoluto(PROGRAM,Declaracoipnes no hay emtomces null o [],Funcion no hay entonces []| null,Begin tampoco entonces [] | null)
                //si te das cuenta retorno un objeto para que cuando llemes a begin hagas esto
                // astRaiz=Begin(raiz de irony)
                // astRaiz.Traducir()
                // astRaiz.Ejecutar()
                // y no guardemos en una lista cosas  
                // creo que te entedi jaja es que yo estaba viendo el ejemplo del erik jaja 
                // y como lo hace?
            }
            else {

                // No Se Hace Nada    

            }        
            //este te tiene que retornar un objeto de tipo ASTInicioClase por ejemplo
            //que reciba paramtros 
        
        }

        // Instruccion Programa 
        public void InsProgram(ParseTreeNode ActualNode)
        {

            // Obtener Identificador -> Sintaxis: program ID ;
            String InsProgramIdentifier = ActualNode.ChildNodes.ElementAt(1).ToString().Split(' ')[0];

            // Agregar Valor A La Lista  
            Variables.TranslateList.AddLast(new InsProgram(InsProgramIdentifier));

        }

        // Declaraciones 
        public void Declarations(ParseTreeNode ActualNode) {

            // Si Es Igual A Dos Primera Produccion De Lo Contrario Segunda Produccion
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Obtener Lista De Instrucciones 
                Declarations(ActualNode.ChildNodes.ElementAt(0));

                // Agregar Instruccion Al Final 
                Declaration(ActualNode.ChildNodes.ElementAt(1));

            }
            else
            {

                // Agregar Instruccion Al Final 
                Declaration(ActualNode.ChildNodes.ElementAt(0));

            }

        }

        // Declaracion 
        public void Declaration(ParseTreeNode ActualNode)
        {

            // Obtener Token Actual 
            String Token1 = ActualNode.ChildNodes.ElementAt(0).ToString();

            // Verificar Instruccion 
            switch (Token1)
            {

                // Caso No.1 
                case "VariablesDeclaration":

                    // Llamar A Metodo Variables Declaration
                    VariablesDeclarations(ActualNode.ChildNodes.ElementAt(0));

                    break;

                // Caso No.2 
                case "ConstantsDeclaration":

                    // Llamar A Metodo Constants Declaration
                    MessageBox.Show(Token1);

                    break;

            }

        }

        // Variables Declaration
        public void VariablesDeclarations(ParseTreeNode ActualNode) {

            // Llamar A Metodo Bloque Variables 
            VariablesDeclarationsBlock(ActualNode.ChildNodes.ElementAt(1));
        
        }

        // Bloque Variables Declaration 
        public void VariablesDeclarationsBlock(ParseTreeNode ActualNode) {

            // Verificar Numero De Terminales 
            if (ActualNode.ChildNodes.Count == 2)
            {

                // Lista De Declaraciones 
                VariablesDeclarationsBlock(ActualNode.ChildNodes.ElementAt(0));

                // Declaracion
                VariablesDeclarationsList(ActualNode.ChildNodes.ElementAt(1));

            }
            else {

                // Declaracion 
                VariablesDeclarationsList(ActualNode.ChildNodes.ElementAt(0));
            
            }
        
        }

        // Lista De Declaraciones 
        public void VariablesDeclarationsList(ParseTreeNode ActualNode) {

            // Lista De Declaraciones 
            DeclarationsList(ActualNode.ChildNodes.ElementAt(0));
        
        }

        // Lista De Declaraciones 
        public void DeclarationsList(ParseTreeNode ActualNode) {

            // Verificar Cantidad De Nodos 
            if (ActualNode.ChildNodes.Count == 3)
            {

                // Declaration List 
                DeclarationsList(ActualNode.ChildNodes.ElementAt(0));

                // Obtener Identificador 
                MessageBox.Show(ActualNode.ChildNodes.ElementAt(2).ToString()); // aqui esta cosa 3, y luego cosa 4
                //var 
                //cosa : lo que sea
                // cosa2,cosa3 :Lo que sea
                //entoces el va a venir y va a encontrar cosa entra al else
                //aqui que estaria? cosa 2 y un nodo mas para cosa 3 entra al metodo recursivo
                //retornamos declaracion de cosa 2 uniendolo a declaracion de cosa3
                //es decir que podes retornar unicamente el nombre o algo por el estlo
                //return new Declaracion(Declaracion cosa2,Declaracion Cosa3,Tipo);
                //me entendiste?
                // osea si y No jaja es que te vas al final de una empezos por pasos XD 
                // yo soy imbecil XD voy inventando sobre la marcha osea yo ya lo vi y ya lo entendi XD

                //Abrite paint jaja el retorno si lo entedi l oque aun no entiendo es esto mira pues jaja 
            }
            else {

                // Obtener Identificador 
                MessageBox.Show(ActualNode.ChildNodes.ElementAt(0).ToString()); // aqui esta cosa 1, y luego la 2 
                //retorna objeto con declaracion de cosa primera vez
                //retorna objeto con declaracion de cosa3 segunda vez
                
            }            

        }

        // Instrucciones
        public void Instruccions(ParseTreeNode ActualNode) {

            // Si Es Igual A Dos Primera Produccion De Lo Contrario Segunda Produccion
            if(ActualNode.ChildNodes.ElementAt(0).ToString().Equals("Instruccions"))
            {

                // Obtener Lista De Instrucciones 
                Instruccions(ActualNode.ChildNodes.ElementAt(0));

                // Agregar Instruccion Al Final 
                Instruccion(ActualNode.ChildNodes.ElementAt(1));

            }
            else 
            {
             
                // Agregar Instruccion Al Final 
                Instruccion(ActualNode.ChildNodes.ElementAt(0));
            
            }
        
        }

        // Instruccion 
        public void Instruccion(ParseTreeNode ActualNode) {

            // Obtener Token Actual 
            String Token1 = ActualNode.ChildNodes.ElementAt(0).ToString();

            // Verificar Instruccion 
            switch(Token1)
            {

                // Caso No.1 
                case "InsProgram":

                        // Llamar A Metodo InsProgram
                        InsProgram(ActualNode.ChildNodes.ElementAt(0));

                    break;

            }   
            
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

                    // Limpiar Lista De Errores 
                    Variables.TranslateList = new LinkedList<AbstractInstruccion>();

                    // Limpiar Variable Traduccion 
                    Variables.TranslateString = "";
                    
                    // Ejecutar Recorrido Del Arbol 
                    //Begin(RootTreeNode);

                    // Recorrido Para Traducir 
                    foreach (var ItemTranslate in Variables.TranslateList)
                    {

                        // Llamar A Método Traducir 
                        //ItemTranslate.Translate();

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
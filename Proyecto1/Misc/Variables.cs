// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.Misc
{
   
    // Clase Variables 
    static class Variables
    {

        // Variables Globales 

        // Lista Que Contiene Los Errores 
        public static LinkedList<ErrorTable> ErrorList = new LinkedList<ErrorTable>();

        // Lista Que Contiene Las Clases Del Analizador(Traducción)
        public static LinkedList<TranslatorAndInterpreter.AbstractInstruccion> TranslateList = new LinkedList<TranslatorAndInterpreter.AbstractInstruccion>();

        // Lista De Ambientes
        public static LinkedList<Misc.EnviromentTable> EnviromentList = new LinkedList<EnviromentTable>();

        // String Que Contiene La Traduccion 
        public static String TranslateString = "";

        // String Que Contiene La Ejecucion
        public static String ExecuteString = "";

        // Contador De Errores 
        public static int AuxiliaryCounter;

        // Columna Locacion Del Token 
        public static int TokenColumn;

        // Linea Locacion Token
        public static int TokenLine;

    }

}
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

        // String Que Contiene La Traduccion 
        public static String TranslateString = "";

        // Contador De Errores 
        public static int AuxiliaryCounter;

        // Columna Locacion Del Token 
        public static int TokenColumn;

        // Linea Locacion Token
        public static int TokenLine;

    }

}
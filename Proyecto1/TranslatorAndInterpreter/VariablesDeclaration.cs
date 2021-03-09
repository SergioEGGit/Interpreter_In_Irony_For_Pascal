// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal 
    class VariablesDeclaration : AbstractInstruccion
    {
        
        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Retornar Null 
            return null;

        }

        // Método Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Agregar ha Traduccion
            Variables.TranslateString += "    var \n";

            // Retornar Null
            return null;

        }
    
    }

}
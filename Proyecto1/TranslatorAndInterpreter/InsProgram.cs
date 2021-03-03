// ------------------------------------------ Librerias E Imports -----------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ---------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase InsProgram
    class InsProgram : AbstractInstruccion
    {

        // Atributos
        public String IdentifierInsProgram;

        // Constructor 
        public InsProgram(String IdentifierInsProgram) {

            // Inicializar Valores
            this.IdentifierInsProgram = IdentifierInsProgram;
        
        }
        
        // Metodo Ejecutar 
        public override object Execute()
        {

            // No Implementacion
            throw new System.NotImplementedException();
        
        }

        // Metodo Traducir 
        public override object Translate()
        {

            // Traducción 
            Variables.TranslateString += "program " + this.IdentifierInsProgram + ";\n\n";

            // Retorno 
            return null;

        }

    }

}
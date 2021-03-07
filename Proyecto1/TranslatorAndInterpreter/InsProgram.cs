// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase InsProgram
    class InsProgram : AbstractInstruccion
    {

        // Atributos

        // Identificador
        public String IdentifierInsProgram;

        // Constructor 
        public InsProgram(String IdentifierInsProgram) {

            // Inicializar Valores
            this.IdentifierInsProgram = IdentifierInsProgram;
        
        }
        
        // Metodo Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Agregar Identificador A Tabla Simbolos
            Env.AddVariable(this.IdentifierInsProgram, "program", null, "program", Env.EnviromentName);

            // Retorno 
            return null;
        
        }

        // Metodo Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Traducción 
            Variables.TranslateString += "program " + this.IdentifierInsProgram + ";\n\n";

            // Retorno 
            return null;

        }

    }

}
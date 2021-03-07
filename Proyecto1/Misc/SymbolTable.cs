// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.Misc
{
    
    // Clase Simbolo
    class SymbolTable
    {

        // Atributos 

        // Identificador De Simbolo
        public String Identifier;

        // Tipo De Simbolo
        public String Type;

        // Valor Asociado Al Simbolo
        public object Value;
        
        // Tipo De Declaracion
        public String DecType;

        // Ambito 
        public String Env;

        // Constructor 
         public SymbolTable(String Identifier, String Type, object Value, String DecType, String Env) {

            // Incializar Valores
            this.Identifier = Identifier;
            this.Type = Type;
            this.Value = Value;
            this.DecType = DecType;
            this.Env = Env;
         
         }

    }

}
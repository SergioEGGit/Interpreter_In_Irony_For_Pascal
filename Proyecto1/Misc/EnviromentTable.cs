// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System.Collections.Generic;
using System;

// ------------------------------------------------ NameSpace -------------------------------------------------------

namespace Proyecto1.Misc
{
    
    // Clase Enviroment     
    class EnviromentTable
    {

        // Atributos

        // Entorno Padre 
        public EnviromentTable ParentEnviroment;

        // Lista De Variables Primitivas
        public Dictionary<String, SymbolTable> PrimitiveVariables;

        // Nombre Del Ambiente Actual
        public String EnviromentName;

        // Constructor 
        public EnviromentTable(EnviromentTable ParentEnviroment, String EnviromentName) {

            // Inicializar Valores 
            this.ParentEnviroment = ParentEnviroment;
            this.PrimitiveVariables = new Dictionary<String, SymbolTable>();
            this.EnviromentName = EnviromentName;
        
        }

        // Agregar Variable A Tabla De Simbolos
        public bool AddVariable(String Identifier, String Type, object Value, String DecType, String Env) {

            // Verificar si La Variable Existe En El Ambito
            if(this.PrimitiveVariables.ContainsKey(Identifier.ToLower())) {

                // Ya Existe 
                return false;

            }

            // Agregar Variable A Lista De Simbolos
            this.PrimitiveVariables.Add(Identifier.ToLower(), new SymbolTable(Identifier, Type, Value, DecType, Env));
            
            // Agregada Con Exito
            return true;
            
        }

    }

}
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
        public EnviromentTable(EnviromentTable ParentEnviroment, String EnviromentName) 
        {

            // Inicializar Valores 
            this.ParentEnviroment = ParentEnviroment;
            this.PrimitiveVariables = new Dictionary<String, SymbolTable>();
            this.EnviromentName = EnviromentName;

            // Agregar Entorno A Lista 
            VariablesMethods.EnviromentList.AddLast(this);
        
        }

        // Agregar Variable A Tabla De Simbolos
        public bool AddVariable(String Identifier, String Type, object Value, String DecType, String Env, int Line, int Column) {

            // Verificar si La Variable Existe En El Ambito
            if(this.PrimitiveVariables.ContainsKey(Identifier.ToLower())) {

                // Ya Existe 
                return false;

            }

            // Agregar Variable A Lista De Simbolos
            this.PrimitiveVariables.Add(Identifier.ToLower(), new SymbolTable(Identifier, Type, Value, DecType, Env, Line, Column));
            
            // Agregada Con Exito
            return true;
            
        }

        // Obtener Variable De Tabla De Simbolos
        public SymbolTable GetVariable(String VarName) 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Buscar Variable 
                if(ActualEnv.PrimitiveVariables.ContainsKey(VarName.ToLower())) 
                {

                    // Retornar Variable 
                    return ActualEnv.PrimitiveVariables[VarName.ToLower()];
                
                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }

            // Retornar Null
            return null;
        
        }

        // Setear Variable De Tabla De Simbolos
        public void SetVariable(String VarName, SymbolTable ActualVar) 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Buscar Variable 
                if(ActualEnv.PrimitiveVariables.ContainsKey(VarName.ToLower())) 
                {

                    // Agregar Variable 
                    ActualEnv.PrimitiveVariables[VarName.ToLower()] = ActualVar;
                
                }

                // Avanzar Puntero
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }
        
        }

        // Graficar Tabla De Simbolos
        public LinkedList<EnviromentTable> GraphSymbolTable() 
        {

            // Obtener Entorno Actual
            EnviromentTable ActualEnv = this;

            // Lista Auxiliar 
            LinkedList<EnviromentTable> AuxiliaryList = new LinkedList<EnviromentTable>();

            // Recorrer Entornos
            while(ActualEnv != null) 
            {

                // Agregar Entorno A Lista 
                AuxiliaryList.AddFirst(ActualEnv);

                // Recorrer Entorno
                ActualEnv = ActualEnv.ParentEnviroment;
            
            }

            // Retorno Lista 
            return AuxiliaryList;
        
        }

    }

}
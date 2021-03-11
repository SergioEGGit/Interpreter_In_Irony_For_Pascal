// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
    class MainBlock : AbstractInstruccion
    {

        // Atributos 

        // Lista De Instrucciones
        private readonly LinkedList<AbstractInstruccion> IntruccionsList = new LinkedList<AbstractInstruccion>();

        // Constructor 
        public MainBlock(LinkedList<AbstractInstruccion> InstruccionsList) 
        {

            // Inicializar Valores 
            this.IntruccionsList = InstruccionsList;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {
            // Nuevo Ambiente 
            EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

            // Verificar Si No Esta Nullo
            if (this.IntruccionsList != null) 
            {

                // Recorrer Lista De Instrucciones 
                foreach(AbstractInstruccion Instruccion in this.IntruccionsList) 
                {

                    // Ejecutar Instrucciones 
                    Instruccion.Execute(MainEnv);
                
                }
            
            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\nbegin\n";

            // Nuevo Ambiente 
            EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            if(this.IntruccionsList != null) 
            {

                // Recorrer Lista 
                foreach (AbstractInstruccion Instruccion in this.IntruccionsList)
                {

                    // Traducir 
                    Instruccion.Translate(MainEnv);

                }

            }
            else
            {

                // Agregar Traduccion
                VariablesMethods.TranslateString += "\n \n";

            }

            // Pop A Pila 
            VariablesMethods.AuxiliaryPile.Pop();

            // Agregar Traduccion
            VariablesMethods.TranslateString += "\nend.\n";
            
            // Retornar 
            return null;

        }

    }

}
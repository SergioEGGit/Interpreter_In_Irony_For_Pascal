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

            // Verificar Si No Esta Nullo
            if (this.IntruccionsList.Count > 0) 
            {

                // Nuevo Ambiente 
                EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");
                
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

        public override object Translate(EnviromentTable Env)
        {

            //Verificar Si No Hay Instrucciones 
            if(this.IntruccionsList.Count > 0) 
            {

                // Agregar Traduccion
                Variables.TranslateString += "\nbegin\n";

                // Nuevo Ambiente 
                EnviromentTable MainEnv = new EnviromentTable(Env, "Env_Main");

                // Recorrer Lista 
                foreach (AbstractInstruccion Instruccion in this.IntruccionsList) 
                {

                    // Traducir 
                    Instruccion.Translate(MainEnv);
                
                }

                // Agregar Traduccion
                Variables.TranslateString += "\nend.\n";

            }

            // Retornar 
            return null;

        }

    }

}
﻿// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal
    class ConstantsDeclaration : AbstractInstruccion
    {

        // Atributos 

        // LIsta De Variables 
        private readonly LinkedList<AbstractInstruccion> ConstList;

        // Constructor 
        public ConstantsDeclaration(LinkedList<AbstractInstruccion> ConstList)
        {

            // Inicializar Valores 
            this.ConstList = ConstList;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Si Esta Nullo
            if (this.ConstList != null)
            {

                // Ejectuar Traduccion
                foreach (AbstractInstruccion Const in this.ConstList)
                {

                    // Verifiar Si Es Nullo
                    if (Const != null)
                    {

                        // Agregar ha Traduccion
                        Const.Execute(Env);

                    }

                }

            }

            // Retornar Null 
            return null;

        }

        // Método Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Verificar Si Esta Nullo
            if (this.ConstList != null)
            {

                // Agregar ha Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "const \n";

                // Ejectuar Traduccion
                foreach (AbstractInstruccion Const in this.ConstList)
                {

                    // Verifiar Si Es Nullo
                    if (Const != null)
                    {

                        // Agregar ha Traduccion
                        Const.Translate(Env);

                    }

                }

            }
            else
            {

                // Agregar ha Traduccion
                VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "const \n";

            }

            // Retornar Null
            return null;

        }

    }

}
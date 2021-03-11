﻿// ------------------------------------------ Librerias E Imports --------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace ------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal
    class ConstantsDeclaration : AbstractInstruccion
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
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "const \n";

            // Retornar Null
            return null;

        }

    }

}
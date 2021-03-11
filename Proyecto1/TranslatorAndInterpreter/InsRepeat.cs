﻿// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal
    class InsRepeat : AbstractInstruccion
    {

        // Atributos 

        // Expression 
        public readonly AbstractExpression Expression_;

        // Lista De Intrucciones 
        public readonly LinkedList<AbstractInstruccion> InstruccionsList;

        // Token Line
        public readonly int TokenLine;

        // Token Column
        public readonly int TokenColumn;

        // Constructor 
        public InsRepeat(AbstractExpression Expression_, LinkedList<AbstractInstruccion> InstruccionsList, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores 
            this.Expression_ = Expression_;
            this.InstruccionsList = InstruccionsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override object Execute(EnviromentTable Env)
        {

            // Crear Nuevo Entorno
            EnviromentTable RepeatEnv = new EnviromentTable(Env, "Env_Repeat");

            // Verificar La Expression
            ObjectReturn RepeatExp = this.Expression_.Execute(RepeatEnv);

            // Verificar Si Hay Error Semantico 
            if (RepeatExp.Type.Equals("boolean"))
            {

                do
                {

                    // Verificar Si Hay Instrucciones 
                    if (this.InstruccionsList != null)
                    {

                        // Recorrer Lista De Instrucciones 
                        foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                        {

                            // Ejecutar Instruccion 
                            Instruccion.Execute(RepeatEnv);

                        }

                    }
                    break;
                } while (bool.Parse(RepeatExp.Value.ToString()));

            }
            else
            {

                // Agregar Error 
                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Expresion A Cumplir De Un Repeat Tiene Que Ser De Tipo Boolean", this.TokenLine, this.TokenColumn));

                // Aumentar Contador
                VariablesMethods.AuxiliaryCounter += 1;

            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override object Translate(EnviromentTable Env)
        {

            // Crear Nuevo Entorno 
            EnviromentTable RepeatEnv = new EnviromentTable(Env, "Env_Repeat");

            // Agregar Traduccion 
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "repeat\n";

            // Agregar Traduccion 
            VariablesMethods.TranslateString += VariablesMethods.Ident() + "begin\n";

            // Agregar A Pila
            VariablesMethods.AuxiliaryPile.Push("_");

            // Recorrer Instrucciones 
            if (this.InstruccionsList != null)
            {

                // Recorrer Lista De Instrucciones 
                foreach (AbstractInstruccion Instruccion in this.InstruccionsList)
                {

                    // Ejecutar Instruccion 
                    Instruccion.Translate(RepeatEnv);

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
            VariablesMethods.TranslateString += "\n" + VariablesMethods.Ident() + "end;\n";

            // Agregar A Traduccion
            VariablesMethods.TranslateString += VariablesMethods.Ident() + "until ";

            // Obtener Traduccion De Expressiones 
            this.Expression_.Translate(RepeatEnv);

            // Agregar Traduccion
            VariablesMethods.TranslateString += ";\n";

            // Retornar 
            return null;

        }

    }

}
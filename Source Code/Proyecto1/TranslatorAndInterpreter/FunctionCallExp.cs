﻿// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Collections.Generic;
using Proyecto1.Misc;
using System.Windows.Forms; 

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{

    // Clase Principal
    class FunctionCallExp : AbstractExpression
    {

        // Atributos 

        // Identificador 
        private readonly String Identifier;

        // Lista De Expressiones 
        private readonly LinkedList<AbstractExpression> ParamsList;

        // LIne 
        private readonly int TokenLine;

        // Columna 
        private readonly int TokenColumn;

        // Constructor 
        public FunctionCallExp(String Identifier, LinkedList<AbstractExpression> ParamsList, int TokenLine, int TokenColumn)
        {

            // Inicializar Valores
            this.Identifier = Identifier;
            this.ParamsList = ParamsList;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;

        }

        // Método Ejecutar 
        public override ObjectReturn Execute(EnviromentTable Env)
        {

            // Buscar Funcion 
            FunctionTable ActualFunc = Env.GetFunction(this.Identifier);

            // Crear Entorno 
            EnviromentTable FuncEnv = new EnviromentTable(Env, "Env_Func_" + this.Identifier.ToLower());

            // Verificar SI Tiene Parametros O No 
            if (this.ParamsList == null)
            {

                // Vericar Si Existe La Funcion 
                if (ActualFunc != null)
                {

                    if (ActualFunc.ParamsList == null)
                    {

                        // Verificar TIpo De Funcion 
                        if (ActualFunc.TypeFunc.Equals("Procedure"))
                        {

                            // Verificar Si ESta Nullo
                            if (ActualFunc.DeclarationsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Declaration in ActualFunc.DeclarationsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Declaration != null)
                                    {

                                        // Ejecutar 
                                        Declaration.Execute(FuncEnv);

                                    }

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.InstruccionsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Instruccion in ActualFunc.InstruccionsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Instruccion != null)
                                    {

                                        // Ejecutar 
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(FuncEnv);

                                        // Verificar SI ESta Nullo
                                        if (ObjectTransfer != null)
                                        {

                                            // Verificar Si ES Break
                                            if (ObjectTransfer.Option.Equals("break") || ObjectTransfer.Option.Equals("continue"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencias Break O Continue  Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                                // Return Object 
                                                ObjectTransfer.Type = "void";
                                                ObjectTransfer.Value = "";

                                                // Retornar 
                                                //return ObjectTransfer;

                                            }
                                            else if (ObjectTransfer.Option.Equals("return"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Exit Tiene Que Aparecer Dentro De Una Funcion", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                                // Return Object 
                                                ObjectTransfer.Type = "void";
                                                ObjectTransfer.Value = "";

                                                // Retornar 
                                                //return ObjectTransfer;

                                            }

                                        }

                                    }

                                }

                            }

                        }
                        else if (ActualFunc.TypeFunc.Equals("Function"))
                        {

                            // Verificar Si ESta Nullo
                            if (ActualFunc.DeclarationsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Declaration in ActualFunc.DeclarationsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Declaration != null)
                                    {

                                        // Ejecutar 
                                        Declaration.Execute(FuncEnv);

                                    }

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.InstruccionsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Instruccion in ActualFunc.InstruccionsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Instruccion != null)
                                    {

                                        // Ejecutar 
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(FuncEnv);

                                        // Verificar SI ESta Nullo
                                        if (ObjectTransfer != null)
                                        {

                                            // Verificar Si ES Break
                                            if (ObjectTransfer.Option.Equals("break") || ObjectTransfer.Option.Equals("continue"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencias Break O Continue  Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                            }
                                            else if (ObjectTransfer.Option.Equals("return"))
                                            {

                                                // Verificar Tipo De Funcion 
                                                if (ActualFunc.ReturnType.Equals(ObjectTransfer.Type))
                                                {

                                                    // Agregar End 
                                                    ObjectTransfer.End = "FunctionCall";

                                                    // Retornar 
                                                    return ObjectTransfer;

                                                }
                                                else
                                                {

                                                    // Agregar Error 
                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Retorno No Coincide Con El Tipo De La Funcion", this.TokenLine, this.TokenColumn));

                                                    // Aumentar Contador
                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                    // Return Object 
                                                    ObjectTransfer.Type = "error";
                                                    ObjectTransfer.Value = "";

                                                    // Retornar 
                                                    //return ObjectTransfer;

                                                }

                                            }

                                        }

                                    }

                                }

                            }

                        }

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion Necesita " + ActualFunc.ParamsList.Count.ToString() + " parametros.", this.TokenLine, this.TokenColumn));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion Indiciada No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }
            else
            {

                // Vericar Si Existe La Funcion 
                if (ActualFunc != null)
                {

                    if (ActualFunc.ParamsList != null)
                    {

                        // Verificar TIpo De Funcion 
                        if (ActualFunc.TypeFunc.Equals("Procedure"))
                        {

                            // Verificar Parametros 
                            if (ActualFunc.ParamsList != null)
                            {

                                // Cantidad De Parametros 
                                int ParamsActual = this.ParamsList.Count;

                                // Parametros Funciones 
                                int ParamsFunc = 0;

                                // Obtener Parametros 
                                foreach (ObjectReturn Param in ActualFunc.ParamsList)
                                {

                                    // Split No1 
                                    String[] Split1 = Param.Value.ToString().Split(',');

                                    // Recorer Split 
                                    foreach (String Var in Split1)
                                    {

                                        // Sumar Parametros 
                                        ParamsFunc += 1;

                                    }

                                }

                                // Verificar Si Tiene EL Mismo Tamaño 
                                if (ParamsActual == ParamsFunc)
                                {

                                    // Contador 
                                    int AuxiliaryCounter = 0;

                                    // Obtener Parametros 
                                    foreach (ObjectReturn Param in ActualFunc.ParamsList)
                                    {

                                        // Split No1 
                                        String[] Split1 = Param.Value.ToString().Split(',');

                                        // Recorer Split 
                                        foreach (String Var in Split1)
                                        {

                                            int AuxiliaryCounter2 = 0;

                                            // Verificar Por Referencias 
                                            String[] Split2 = Var.Split(' ');

                                            // Obtener TIpo 
                                            foreach (AbstractExpression TypeAux in this.ParamsList)
                                            {

                                                // Obtener Tipos
                                                ObjectReturn TypeObject = TypeAux.Execute(FuncEnv);

                                                // Verifivar Si ESTa Null
                                                if (TypeAux != null)
                                                {

                                                    // Verificar 
                                                    if (AuxiliaryCounter == AuxiliaryCounter2)
                                                    {

                                                        // Verificar Si Hay Dos 
                                                        if (Split2.Length == 2)
                                                        {

                                                            // Verificar Tipo
                                                            if (Param.Type.ToString().Equals(TypeObject.Type))
                                                            {

                                                                // Agregar Varialbe 
                                                                bool Flag = FuncEnv.AddVariable(Split2[1], Param.Type.ToString(), TypeObject, "Param_Ref", FuncEnv.EnviromentName, this.TokenLine, this.TokenColumn);

                                                                // Verificar Si Existe 
                                                                if (!Flag)
                                                                {

                                                                    // Agregar Error 
                                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                                                    // Aumentar Contador
                                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                                }

                                                            }
                                                            else
                                                            {

                                                                // Agregar Error 
                                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Parametro Y El Tipo De Expression Indicado No Coinciden", this.TokenLine, this.TokenColumn));

                                                                // Aumentar Contador
                                                                VariablesMethods.AuxiliaryCounter += 1;

                                                                // TErminar 
                                                                break;

                                                            }

                                                        }
                                                        else
                                                        {

                                                            // Verificar Tipo
                                                            if (Param.Type.ToString().Equals(TypeObject.Type))
                                                            {

                                                                // Agregar Varialbe 
                                                                bool Flag = FuncEnv.AddVariable(Split2[0], Param.Type.ToString(), TypeObject, "Param", FuncEnv.EnviromentName, this.TokenLine, this.TokenColumn);

                                                                // Verificar Si Existe 
                                                                if (!Flag)
                                                                {

                                                                    // Agregar Error 
                                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                                                    // Aumentar Contador
                                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                                    // Terminar 
                                                                    break;

                                                                }

                                                            }
                                                            else
                                                            {

                                                                // Agregar Error 
                                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Parametro Y El Tipo De Expression Indicado No Coinciden", this.TokenLine, this.TokenColumn));

                                                                // Aumentar Contador
                                                                VariablesMethods.AuxiliaryCounter += 1;

                                                                // TErminar 
                                                                break;

                                                            }

                                                        }

                                                    }
                                                }

                                                // Sumar 
                                                AuxiliaryCounter2 += 1;

                                            }

                                            // SUmar 
                                            AuxiliaryCounter += 1;

                                        }

                                    }

                                }
                                else
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Cantidad De Parametros Indicada No Coincide Con La Cantidad Esperada", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.DeclarationsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Declaration in ActualFunc.DeclarationsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Declaration != null)
                                    {

                                        // Ejecutar 
                                        Declaration.Execute(FuncEnv);

                                    }

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.InstruccionsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Instruccion in ActualFunc.InstruccionsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Instruccion != null)
                                    {

                                        // Ejecutar 
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(FuncEnv);

                                        // Verificar SI ESta Nullo
                                        if (ObjectTransfer != null)
                                        {

                                            // Verificar Si ES Break
                                            if (ObjectTransfer.Option.Equals("break") || ObjectTransfer.Option.Equals("continue"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencias Break O Continue  Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                                // Return Object 
                                                ObjectTransfer.Type = "void";
                                                ObjectTransfer.Value = "";

                                                // Retornar 
                                                //return ObjectTransfer;

                                            }
                                            else if (ObjectTransfer.Option.Equals("return"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencia Exit Tiene Que Aparecer Dentro De Una Funcion", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                                // Return Object 
                                                ObjectTransfer.Type = "void";
                                                ObjectTransfer.Value = "";

                                                // Retornar 
                                                //return ObjectTransfer;

                                            }

                                        }

                                    }

                                }

                            }

                        }
                        else if (ActualFunc.TypeFunc.Equals("Function"))
                        {

                            // Verificar Parametros 
                            if (ActualFunc.ParamsList != null)
                            {

                                // Cantidad De Parametros 
                                int ParamsActual = this.ParamsList.Count;

                                // Parametros Funciones 
                                int ParamsFunc = 0;

                                // Obtener Parametros 
                                foreach (ObjectReturn Param in ActualFunc.ParamsList)
                                {

                                    // Split No1 
                                    String[] Split1 = Param.Value.ToString().Split(',');

                                    // Recorer Split 
                                    foreach (String Var in Split1)
                                    {

                                        // Sumar Parametros 
                                        ParamsFunc += 1;

                                    }

                                }

                                // Verificar Si Tiene EL Mismo Tamaño 
                                if (ParamsActual == ParamsFunc)
                                {

                                    // Contador 
                                    int AuxiliaryCounter = 0;

                                    // Obtener Parametros 
                                    foreach (ObjectReturn Param in ActualFunc.ParamsList)
                                    {

                                        // Split No1 
                                        String[] Split1 = Param.Value.ToString().Split(',');

                                        // Recorer Split 
                                        foreach (String Var in Split1)
                                        {

                                            int AuxiliaryCounter2 = 0;

                                            // Verificar Por Referencias 
                                            String[] Split2 = Var.Split(' ');

                                            // Obtener TIpo 
                                            foreach (AbstractExpression TypeAux in this.ParamsList)
                                            {

                                                // Obtener Tipos
                                                ObjectReturn TypeObject = TypeAux.Execute(FuncEnv);

                                                // Verifivar Si ESTa Null
                                                if (TypeAux != null)
                                                {

                                                    // Verificar 
                                                    if (AuxiliaryCounter == AuxiliaryCounter2)
                                                    {

                                                        // Verificar Si Hay Dos 
                                                        if (Split2.Length == 2)
                                                        {

                                                            // Verificar Tipo
                                                            if (Param.Type.ToString().Equals(TypeObject.Type))
                                                            {

                                                                // Agregar Varialbe 
                                                                bool Flag = FuncEnv.AddVariable(Split2[1], Param.Type.ToString(), TypeObject, "Param_Ref", FuncEnv.EnviromentName, this.TokenLine, this.TokenColumn);

                                                                // Verificar Si Existe 
                                                                if (!Flag)
                                                                {

                                                                    // Agregar Error 
                                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                                                    // Aumentar Contador
                                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                                }

                                                            }
                                                            else
                                                            {

                                                                // Agregar Error 
                                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Parametro Y El Tipo De Expression Indicado No Coinciden", this.TokenLine, this.TokenColumn));

                                                                // Aumentar Contador
                                                                VariablesMethods.AuxiliaryCounter += 1;

                                                                // TErminar 
                                                                break;

                                                            }

                                                        }
                                                        else
                                                        {

                                                            // Verificar Tipo
                                                            if (Param.Type.ToString().Equals(TypeObject.Type))
                                                            {

                                                                // Agregar Varialbe 
                                                                bool Flag = FuncEnv.AddVariable(Split2[0], Param.Type.ToString(), TypeObject, "Param", FuncEnv.EnviromentName, this.TokenLine, this.TokenColumn);

                                                                // Verificar Si Existe 
                                                                if (!Flag)
                                                                {

                                                                    // Agregar Error 
                                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Parametro Indicado Ya Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                                                                    // Aumentar Contador
                                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                                    // Terminar 
                                                                    break;

                                                                }

                                                            }
                                                            else
                                                            {

                                                                // Agregar Error 
                                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Parametro Y El Tipo De Expression Indicado No Coinciden", this.TokenLine, this.TokenColumn));

                                                                // Aumentar Contador
                                                                VariablesMethods.AuxiliaryCounter += 1;

                                                                // TErminar 
                                                                break;

                                                            }

                                                        }

                                                    }
                                                }

                                                // Sumar 
                                                AuxiliaryCounter2 += 1;

                                            }

                                            // SUmar 
                                            AuxiliaryCounter += 1;

                                        }

                                    }

                                }
                                else
                                {

                                    // Agregar Error 
                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Cantidad De Parametros Indicada No Coincide Con La Cantidad Esperada", this.TokenLine, this.TokenColumn));

                                    // Aumentar Contador
                                    VariablesMethods.AuxiliaryCounter += 1;

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.DeclarationsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Declaration in ActualFunc.DeclarationsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Declaration != null)
                                    {

                                        // Ejecutar 
                                        Declaration.Execute(FuncEnv);

                                    }

                                }

                            }

                            // Verificar Si ESta Nullo
                            if (ActualFunc.InstruccionsList != null)
                            {

                                // Verificar Si ES Nullo
                                foreach (AbstractInstruccion Instruccion in ActualFunc.InstruccionsList)
                                {

                                    // Verificar Si ESta Nullo
                                    if (Instruccion != null)
                                    {

                                        // Ejecutar 
                                        ObjectReturn ObjectTransfer = (ObjectReturn)Instruccion.Execute(FuncEnv);

                                        // Verificar SI ESta Nullo
                                        if (ObjectTransfer != null)
                                        {

                                            // Verificar Si ES Break
                                            if (ObjectTransfer.Option.Equals("break") || ObjectTransfer.Option.Equals("continue"))
                                            {

                                                // Agregar Error 
                                                VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Sentencias Break O Continue  Tiene Que Aparecer Dentro De Un Ciclo", this.TokenLine, this.TokenColumn));

                                                // Aumentar Contador
                                                VariablesMethods.AuxiliaryCounter += 1;

                                            }
                                            else if (ObjectTransfer.Option.Equals("return"))
                                            {

                                                // Verificar Tipo De Funcion 
                                                if (ActualFunc.ReturnType.Equals(ObjectTransfer.Type))
                                                {

                                                    // Agregar End 
                                                    ObjectTransfer.End = "FunctionCall";

                                                    // Retornar 
                                                    return ObjectTransfer;

                                                }
                                                else
                                                {

                                                    // Agregar Error 
                                                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "El Tipo De Retorno No Coincide Con El Tipo De La Funcion", this.TokenLine, this.TokenColumn));

                                                    // Aumentar Contador
                                                    VariablesMethods.AuxiliaryCounter += 1;

                                                    // Return Object 
                                                    ObjectTransfer.Type = "error";
                                                    ObjectTransfer.Value = "";

                                                    // Retornar 
                                                    //return ObjectTransfer;

                                                }

                                            }

                                        }

                                    }

                                }

                            }

                        }

                    }
                    else
                    {

                        // Agregar Error 
                        VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion No Necesita Parametros", this.TokenLine, this.TokenColumn));

                        // Aumentar Contador
                        VariablesMethods.AuxiliaryCounter += 1;

                    }

                }
                else
                {

                    // Agregar Error 
                    VariablesMethods.ErrorList.AddLast(new ErrorTable(VariablesMethods.AuxiliaryCounter, "Semántico", "La Funcion Indiciada No Existe En El Contexto Actual", this.TokenLine, this.TokenColumn));

                    // Aumentar Contador
                    VariablesMethods.AuxiliaryCounter += 1;

                }

            }

            // Retornar 
            return null;

        }

        // Método Traducir
        public override ObjectReturn Translate(EnviromentTable Env)
        {

            // Agregar A Traducion
            VariablesMethods.TranslateString += this.Identifier + "(";

            // Veriricar Si ES Nullo
            if (this.ParamsList != null)
            {

                // Contador Aux 
                int AuxiliaryCounter = 1;

                // Recorrer 
                foreach (AbstractExpression Param in this.ParamsList)
                {

                    // Veriicar Si ESta Nullo
                    if (Param != null)
                    {

                        // Vericiar Numero 
                        if (AuxiliaryCounter == this.ParamsList.Count)
                        {

                            // Traducir 
                            Param.Translate(Env);

                        }
                        else
                        {

                            // Traducir 
                            Param.Translate(Env);

                            // Agregar Coma 
                            VariablesMethods.TranslateString += ", ";

                        }

                    }

                    // Agregar 
                    AuxiliaryCounter += 1;

                }

            }

            // Agregar A Traducion
            VariablesMethods.TranslateString += ")";

            // Retornar 
            return null;

        }

    }

}
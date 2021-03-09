// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using Proyecto1.Misc;

// ------------------------------------------------ Namespace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
    class PrimitiveDeclaration : AbstractInstruccion
    {
        
        // Atributos
        
        // Identifiers 
        private String Identifiers;

        // Tipo 
        private String Type;

        // Tipo De Declaracion 
        private String DecType;

        // Valor
        private AbstractExpression Value;

        // Token Linea
        private int TokenLine;

        // Token Columna
        private int TokenColumn;
        
        // Constructor
        public PrimitiveDeclaration(String Identifiers, String Type, AbstractExpression Value, String DecType, int TokenLine, int TokenColumn) {

            // Inicializar Valores 
            this.Identifiers = Identifiers;
            this.Type = Type;
            this.Value = Value;
            this.DecType = DecType;
            this.TokenLine = TokenLine;
            this.TokenColumn = TokenColumn;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Si Las Constantes Tiene Un Valor 
            if (this.DecType == "Const" && this.Value == null)
            {

                // Agregar Errores Constantes 
                Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Semántico", "Una Constante Siempre Debe De Ser Inicializada Con Un Valor", this.TokenLine, this.TokenColumn));

            }
            else if (this.DecType == "Const" && this.Value != null)
            {

                // Variable Auxiliar 
                bool AuxiliaryReturn;

                // Objeto 
                ObjectReturn Value = this.Value.Execute(Env);

                // Agregar Variable 
                AuxiliaryReturn = Env.AddVariable(Identifiers, Value.Type.ToString(), Value, this.DecType, Env.EnviromentName);

                // Si Existe Error 
                AddError(AuxiliaryReturn);

            }
            else if(this.DecType == "Var") {

                // Arreglo De Identificadores 
                String[] Identifiers = this.Identifiers.Split(',');

                // Verificar Si Se Asigna A Un Solo ID
                if (this.Value != null && Identifiers.Length > 1)
                {

                    // Agregar Error Variables 
                    Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Semántico", "Unicamente Se Puede Realizar Asignacion A Un Unico Identificador", this.TokenLine, this.TokenColumn));

                }
                else 
                {

                    // Recorrer Todos Los Identificadores
                    foreach (String Identifier in Identifiers)
                    {

                        if (this.Value == null && this.Identifiers.Length > 1)
                        {

                            // Variable Auxiliar 
                            bool AuxiliaryReturn;

                            if (this.Type == "integer")
                            {

                                // Verificar Si La Variable Existe O NO 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, 0, this.DecType, Env.EnviromentName);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn);

                            }
                            else if (this.Type == "string")
                            {

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, "", this.DecType, Env.EnviromentName);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn);

                            }
                            else if (this.Type == "boolean")
                            {

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, false, this.DecType, Env.EnviromentName);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn);

                            }
                            else if (this.Type == "real")
                            {

                                // Verificar Si La Variable Existe O No 
                                AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, 0.0, this.DecType, Env.EnviromentName);

                                // Si Existe Error 
                                AddError(AuxiliaryReturn);

                            }
                            else
                            {

                                //Tipos distintos

                            }

                        }
                        else
                        {

                            // Objeto 
                            ObjectReturn Value = this.Value.Execute(Env);

                            // Verififcar Que no Sea Nulo 
                            if (Value != null)
                            {

                                // Verificar Que Sean Del Mismo Tipo
                                if(Value.Type.ToString().Equals("integer") && this.Type.ToString().Equals("real")) {

                                    // Variable Auxiliar 
                                    bool AuxiliaryReturn;

                                    // Convertir Value
                                    Value.Value = Decimal.Parse(Value.Value.ToString());

                                    // Convertir Typo 
                                    Value.Type = "real";

                                    // Verificar Si La Variable Existe O No 
                                    AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName);

                                    // Si Existe Error 
                                    AddError(AuxiliaryReturn);

                                }
                                else if (Value.Type.ToString().Equals(this.Type.ToString()))
                                {

                                    // Variable Auxiliar 
                                    bool AuxiliaryReturn;

                                    // Verificar Si La Variable Existe O No 
                                    AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName);

                                    // Si Existe Error 
                                    AddError(AuxiliaryReturn);

                                }
                                else
                                {

                                    // Agregar Error 
                                    Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Semántico", "El Valor Asignado (" + Value.Type.ToString() + ") No Coinicide con El Tipo De Variable (" + this.Type.ToString() + ")", this.TokenLine, this.TokenColumn));

                                }

                            }

                        }
                    }

                }                

            }
            
            // Retornar Null
            return null;
        }

        // Método Traducir 
        public override object Translate(EnviromentTable Env)
        {

            // Obtener Identificadores
            String[] IdentifierList = this.Identifiers.Split(',');

            // Agregar Identacion
            Variables.TranslateString += "        ";

            // Contador Auxiliar
            int AuxiliaryCounter = 1;

            // Recorrer Ids
            foreach(String Identifier in IdentifierList) 
            {

                // Verificar Si Es El Ultimo
                if(AuxiliaryCounter == IdentifierList.Length)
                {

                    // Agregar Sin Coma 
                    Variables.TranslateString += Identifier;

                }
                else 
                {

                    // Agregar Traduccion 
                    Variables.TranslateString += Identifier + ", ";

                }

                AuxiliaryCounter += 1;

            }

            // Agregar Tipo y Resto Traducción
            Variables.TranslateString += " : " + this.Type.ToString();

            // Verificar Si Hay Un Valor
            if(this.Value != null) 
            {

                // Agregar Traduccion
                Variables.TranslateString += " = ";

                // Ejecutar Metodo Traducir 
                this.Value.Translate(Env);

            }

            // Agregar Final 
            Variables.TranslateString += "; \n";

            // Retonar 
            return null;

        }

        // Indicar Error
        private void AddError(bool IsError) {
            
            // Verificar Si Hay Error
            if (IsError == false) {

                // Agregar Error
                Variables.ErrorList.AddLast(new ErrorTable(Variables.AuxiliaryCounter, "Semático", "La Variable O Constante Indicada Ya Existe En El Ambito", this.TokenLine, this.TokenColumn));
            
            }

        }

    }

}
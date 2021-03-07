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
        
        // Constructor
        public PrimitiveDeclaration(String Identifiers, String Type, AbstractExpression Value, String DecType) {

            // Inicializar Valores 
            this.Identifiers = Identifiers;
            this.Type = Type;
            this.Value = Value;
            this.DecType = DecType;
        
        }

        // Método Ejecutar
        public override object Execute(EnviromentTable Env)
        {

            // Verificar Si Las Constantes Tiene Un Valor 
            if (this.DecType == "Const" && this.Value == null)
            {

                throw new Exception("Error Las Constantes Siempre Tiene Que Tener Un Valor ");

            }
            else if (this.DecType == "Const" && this.Value != null)
            {

                // Variable Auxiliar 
                bool AuxiliaryReturn;

                // Objeto 
                Retorno Value = this.Value.Execute(Env);

                // Agregar Variable 
                AuxiliaryReturn = Env.AddVariable(Identifiers, Value.Tipo.ToString(), Value, this.DecType, Env.EnviromentName);

                // Si Existe Error 
                AddError(AuxiliaryReturn);

            }
            else if(this.DecType == "Var") {

                // Arreglo De Identificadores 
                String[] Identifiers = this.Identifiers.Split(',');

                // Recorrer Todos Los Identificadores
                foreach(String Identifier in Identifiers)
                {

                    // Verificar Si Se Asigna A Un Solo ID
                    if (this.Value != null && Identifier.Length > 1)
                    {

                        throw new Exception("Error Unicamente Se Puede Asginar Un Valor A Una Unica Variable");
                    
                    }
                    else if (this.Value == null && this.Identifiers.Length > 1)
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

                            //Error tipos distintos
                        
                        }

                    }
                    else
                    {
                        
                        // Objeto 
                        Retorno Value = this.Value.Execute(Env);

                        // Verificar Que Sean Del Mismo Tipo
                        if (Value.Tipo.ToString().Equals(this.Type.ToString()))
                        {

                            // Variable Auxiliar 
                            bool AuxiliaryReturn;
                            
                            // Verificar Si La Variable Existe O No 
                            AuxiliaryReturn = Env.AddVariable(Identifier, this.Type, Value, this.DecType, Env.EnviromentName);

                            // Si Existe Error 
                            AddError(AuxiliaryReturn);

                        }

                    }
                }

            }
            
            // Retornar Null
            return null;
        }

        // Método Traducir 
        public override object Translate(EnviromentTable ambiente)
        {
            throw new NotImplementedException();
        }

        // Indicar Error
        private void AddError(bool IsError) {
            
            // Verificar Si Hay Error
            if (IsError == false) {

                throw new Exception("La Variable O Constante Indicada Ya Existe En El Ambito");
            
            }

        }

    }
}
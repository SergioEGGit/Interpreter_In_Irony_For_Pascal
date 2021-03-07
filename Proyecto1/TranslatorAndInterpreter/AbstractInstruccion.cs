// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1.TranslatorAndInterpreter
{
   
    // Clase Abastracta     
    abstract class AbstractInstruccion
    {

        // Creación De Metodos Abstractos 
        
        // Método Traducir 
        public abstract object Translate(EnviromentTable Env);

        // Método Ejecutar 
        public abstract object Execute(EnviromentTable Env);

    }
    
}
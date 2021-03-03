// ------------------------------------------ Librerias E Imports --------------------------------------------


// ------------------------------------------------ NameSpace ------------------------------------------------
namespace Proyecto1.Misc
{
    
    // Clase Enviroment     
    class EnviromentTable
    {

        // Entorno Padre 
        public EnviromentTable ParentEnviroment;

        // Constructor 
        public EnviromentTable(EnviromentTable ParentEnviroment) {

            // Inicializar Valores 
            this.ParentEnviroment = ParentEnviroment;
        
        }

    }

}
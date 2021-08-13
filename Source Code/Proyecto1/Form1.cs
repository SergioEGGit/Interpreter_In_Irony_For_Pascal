// ------------------------------------------ Librerias E Imports ---------------------------------------------------
using System;
using System.Windows.Forms;
using Proyecto1.Misc;

// ------------------------------------------------ NameSpace -------------------------------------------------------
namespace Proyecto1
{
    
    // Clase Principal Form
    public partial class Form1 : Form
    {
        // Instancia Clase Irony_Analyze
        readonly Irony_Resources.Irony_Parser ParserTranslate = new Irony_Resources.Irony_Parser();

        // Constructor Inicial 
        public Form1()
        {
            
            // Inicializar Componentes 
            InitializeComponent();

        }

        // Acción Click Botón Analizar 
        private void ButtonTranslate_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Analizar Texto 
            ParserTranslate.AnalyzeTranslate(EntranceString);

            // Limpiar Consola
            TextEntrance.Text = "";

            // Agregar Traduccion
            TextEntrance.Text = VariablesMethods.TranslateString;

        }

        // Acción Click Botón Ejecutar
        private void ButtonExecute_Click(object sender, EventArgs e)
        {

            // Obtener Texto De Consola De Entrada 
            String EntranceString = TextEntrance.Text;

            // Analizar Texto 
            ParserTranslate.AnalyzeExecute(EntranceString);

            // Limpiar Consola 
            TextConsole.Text = "";

            // Agregar Ejecucion
            TextConsole.Text = VariablesMethods.ExecuteString;

        }

        // Acción Click Botón Reportes 
        private void ButtonReports_Click(object sender, EventArgs e)
        {

            // Llamar A Metodo Report
            ParserTranslate.GenerateReports();

        }
    
    }

}
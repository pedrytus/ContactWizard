using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Speech.Synthesis;
using System.Xml;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace Contact_Wizard
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Contacto> ListaContactos;
        private String archivoAbierto = "";

        public MainWindow()
        {
            InitializeComponent();
            ListaContactos = new List<Contacto>();
        }

        private void LimpiarCampos()
        {
            txtNombreContacto.Text = "";
            txtEmpresaContacto.Text = "";
            txtTlfPrincipalContacto.Text = "";
            txtTlfSecundarioContacto.Text = "";
            txtEmailContacto.Text = "";
            DatePickerContacto.Text = "";
            txtAnotacionContacto.Text = "";
        }

        private void EscribirXML(String nombreArchivo, List<Contacto> contactos)
        {
            XmlWriter writer = null;

            try
            {
                // Opciones para el writer
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Encoding = Encoding.UTF8;
                settings.Indent = true;
                settings.IndentChars = ("\t");

                // Creando el writer y escribiendo correctamente
                writer = XmlWriter.Create(nombreArchivo, settings);
                writer.WriteStartElement("contactos");

                foreach (Contacto c in contactos)
                {
                    writer.WriteStartElement("contacto");
                    writer.WriteElementString("nombre", c.Nombre);
                    writer.WriteElementString("empresa", c.Empresa);
                    writer.WriteStartElement("telefonos");
                    writer.WriteElementString("telefono", c.TelefonoPrincipal);
                    writer.WriteElementString("telefono", c.TelefonoSecundario);
                    writer.WriteEndElement();
                    writer.WriteElementString("email", c.Email);
                    writer.WriteElementString("aniversario", c.Aniversario);
                    writer.WriteElementString("anotacion", c.Anotacion);
                    writer.WriteEndElement();
                }

                writer.WriteEndElement();
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        private void EscribirCSV(String nombreArchivo, List<Contacto> contactos)
        {
            StringBuilder contenidoCSV = new StringBuilder();
            contenidoCSV.AppendLine("Nombre,Empresa,TlfPrincipal,TlfSecundario,Email,Aniversario,Anotacion");

            foreach(Contacto c in contactos)
            {
                contenidoCSV.AppendLine(c.Nombre + "," + c.Empresa + "," + c.TelefonoPrincipal + "," 
                    + c.TelefonoSecundario + "," + c.Email + "," + c.Aniversario + "," + c.Anotacion);
            }

            File.AppendAllText(nombreArchivo, contenidoCSV.ToString());
        }

        private void LeerXML(String pathArchivo)
        {
            FileStream fileStream = new FileStream(@pathArchivo, FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
            XmlReader lector = XmlReader.Create(fileStream);

            String[] paramsContacto = new String[7] { "", "", "", "", "", "", "" };
            int counter = 0;

            while (lector.Read())
            {
                switch (lector.NodeType)
                {
                    case XmlNodeType.XmlDeclaration:
                        // No es necesario hacer nada aqui.
                        break;
                    case XmlNodeType.ProcessingInstruction:
                        // No es necesario hacer nada aqui.
                        break;
                    case XmlNodeType.Comment:
                        // No es necesario hacer nada aqui.
                        break;
                    case XmlNodeType.Element:
                        // No es necesario hacer nada aqui.
                        break;
                    case XmlNodeType.Text:
                        paramsContacto[counter] = lector.Value;
                        counter++;
                        break;
                    case XmlNodeType.EndElement:
                        // Cuando llega al final del elemento contacto, lo crea.
                        if (lector.Name.Equals("contacto"))
                        {
                            Contacto nuevoContacto = new Contacto(paramsContacto[0], paramsContacto[1], paramsContacto[2],
                            paramsContacto[3], paramsContacto[4], paramsContacto[5], paramsContacto[6]);
                            ListaContactos.Add(nuevoContacto);
                            counter = 0;
                        }
                        break;
                }
            }

            foreach(Contacto c in ListaContactos)
            {
                listBoxContactos.Items.Add(c);
            }
        }

        private void btnAddContacto_Click(object sender, RoutedEventArgs e)
        {
            String nombre = txtNombreContacto.Text;
            String empresa = txtEmpresaContacto.Text;
            String tlfprincipal = txtTlfPrincipalContacto.Text;
            String tlfsecundario = txtTlfSecundarioContacto.Text;
            String email = txtEmailContacto.Text;
            String aniversario = DatePickerContacto.Text;
            String anotacion = txtAnotacionContacto.Text;

            Contacto contacto = new Contacto(nombre, empresa, tlfprincipal, tlfsecundario, email, aniversario, anotacion);
            ListaContactos.Add(contacto);
            listBoxContactos.Items.Add(contacto);

            LimpiarCampos();
        }

        private void btnDeleteContacto_Click(object sender, RoutedEventArgs e)
        {
            Contacto contactoSeleccionado = (Contacto) listBoxContactos.SelectedItem;
            ListaContactos.Remove(contactoSeleccionado);
            listBoxContactos.Items.Remove(contactoSeleccionado);

            LimpiarCampos();
        }

        private void listBoxContactos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int indexSeleccionado = listBoxContactos.SelectedIndex;

            if (indexSeleccionado != -1)
            {
                Contacto contactoSeleccionado = ListaContactos[indexSeleccionado];

                txtNombreContacto.Text = contactoSeleccionado.Nombre;
                txtEmpresaContacto.Text = contactoSeleccionado.Empresa;
                txtTlfPrincipalContacto.Text = contactoSeleccionado.TelefonoPrincipal;
                txtTlfSecundarioContacto.Text = contactoSeleccionado.TelefonoSecundario;
                txtEmailContacto.Text = contactoSeleccionado.Email;
                DatePickerContacto.Text = contactoSeleccionado.Aniversario;
                txtAnotacionContacto.Text = contactoSeleccionado.Anotacion;
            }
        }

        private void btnLeerVozContacto_Click(object sender, RoutedEventArgs e)
        {
            SpeechSynthesizer speech = new SpeechSynthesizer();
            speech.Rate = -2;

            int indexSeleccionado = listBoxContactos.SelectedIndex;

            if (indexSeleccionado == -1)
            {
                speech.SpeakAsync("No se ha seleccionado ningún contacto de la lista.");
            }
            else
            {
                speech.SpeakAsync("Datos del contacto seleccionado:");
                speech.SpeakAsync("Nombre: " + txtNombreContacto.Text);
                speech.SpeakAsync("Empresa: " + txtEmpresaContacto.Text);
                speech.SpeakAsync("Teléfono principal: " + txtTlfPrincipalContacto.Text);
                speech.SpeakAsync("Teléfono secundario: " + txtTlfSecundarioContacto.Text);
                speech.SpeakAsync("Email: " + txtEmailContacto.Text);
                speech.SpeakAsync("Cumpleaños: " + DatePickerContacto.Text);
                speech.SpeakAsync("Anotaciones: " + txtAnotacionContacto.Text);
            }
        }

        private void rbXML_Checked(object sender, RoutedEventArgs e)
        {
            txtNombreArchivo.Text = "default.xml";
        }

        private void rbCSV_Checked(object sender, RoutedEventArgs e)
        {
            txtNombreArchivo.Text = "default.csv";
        }

        private void btnGenerarArchivo_Click(object sender, RoutedEventArgs e)
        {
            if (rbXML.IsChecked == true)
            {
                EscribirXML(txtNombreArchivo.Text, ListaContactos);
                MessageBox.Show("El archivo " + txtNombreArchivo.Text + " ha sido generado correctamente.\n" +
                    "Gracias por usar Contact-Wizard.\n\n" +
                    "(C) 2017 - Pedro Blanco Suárez, UO251935@uniovi.es", 
                    "Resultado de la operación", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            if (rbCSV.IsChecked == true)
            {
                EscribirCSV(txtNombreArchivo.Text, ListaContactos);
                MessageBox.Show("El archivo " + txtNombreArchivo.Text + " ha sido generado correctamente.\n" +
                    "Gracias por usar Contact-Wizard.\n\n" +
                    "(C) 2017 - Pedro Blanco Suárez, UO251935@uniovi.es",
                    "Resultado de la operación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnAbrir_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "Archivos XML validos (.xml)|*.xml|Todos los archivos (*.*)|*.*";
            fileChooser.FilterIndex = 1;

            fileChooser.ShowDialog();

            archivoAbierto = fileChooser.FileName;
            txtNombreArchivo.Text = archivoAbierto;

            listBoxContactos.Items.Clear();
            ListaContactos.Clear();

            LeerXML(archivoAbierto);
        }
    }
}

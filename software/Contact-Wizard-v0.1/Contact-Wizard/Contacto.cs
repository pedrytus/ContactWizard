using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Wizard
{
    class Contacto
    {
        public String Nombre { get; set; }
        public String Empresa { get; set; }
        public String TelefonoPrincipal { get; set; }
        public String TelefonoSecundario { get; set; }
        public String Email { get; set; }
        public String Aniversario { get; set; }
        public String Anotacion { get; set; }

        public Contacto(String nombre, String empresa, String telefonoprincipal, 
            String telefonosecundario, String email, String aniversario, String anotacion)
        {
            this.Nombre = nombre;
            this.Empresa = empresa;
            this.TelefonoPrincipal = telefonoprincipal;
            this.TelefonoSecundario = telefonosecundario;
            this.Email = email;
            this.Aniversario = aniversario;
            this.Anotacion = anotacion;
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}

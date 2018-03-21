# Contact Wizard
Utilidad de escritorio todo-en-uno para leer y generar estructuras de datos en XML relacionadas con agendas de contacto, tanto personales como profesionales.

## Motivación
Desarrollado y diseñado explícitamente para la asignatura Software y Estándares Web por Pedro Blanco, estudiante de Ingeniería Informática del Software en la Universidad de Oviedo.
_Noviembre 2016. Versión experimental v0.1._

## Introducción a la herramienta
Como su propio nombre indica, Contact-Wizard intenta ser una herramienta 'magica' todo-en-uno para leer y generar estructuras de datos en XML relacionadas con agendas de contacto, tanto personales como profesionales.

A su vez, y debido a la gran extensión del formato .CSV entre las hojas de contactos, esta herramienta permite generar archivos de dicho tipo a partir de la lectura de .XML o de la propia generación de las estructuras de datos de contactos desde la aplicación.

Por último, y como un importante 'plus' en accesibilidad, Contact-Wizard provee al usuario de una herramienta de lectura mediante voz (Text to Speech) de los datos de cada uno de los contactos que forman parte de la aplicación en dicho momento.

## Detalles técnicos
- El programa ha sido desarrollado en su totalidad bajo el lenguaje de programación orientado a objetos C# (Microsoft .NET).
- Con la finalidad de hacer más fácil y amigable la interacción con el usuario, el programa cuenta con una interfaz gráfica basada en Windows Presentation Foundation (WPF).
- La solución ha sido programada mediante el IDE 'Visual Studio Community 2017' en su versión estable 15.4.3.
- La solución ha sido compilada en el mismo IDE, con el compilador correspondiente a C# en su edición 2017.
- Se han utilizado las librerias referenciadas propias de C#, .NET Framework y Microsoft. No se ha incluido ninguna librería externa al proyecto. Entre las más reseñables se encuentran: System.Speech (Síntesis de voz), System.XML (Tratamiento de los archivos XML tanto lectura como escritura), System.IO (Todas las labores para abrir/grabar archivos y CSV).
- Como archivos de prueba se han utilizado los generados propiamente por el programa, ya que se trata de una herramienta que sirve para realizar tanto las tareas de generación como de lectura de datos en XML.

## Visión de conjunto e instrucciones
- La aplicación consta de una interfaz en una única vista de pantalla desde la cual se puede acceder a todas las funciones que provee la misma.
- El botón 'Generar' creará (si no existe) o sobreescribirá (si ya existe) un archivo de la extensión seleccionada (XML o CSV) dependiendo de qué radio-botón se encuentre activado. Dicho archivo se guardará en la carpeta raíz del programa con el nombre elegido.
- El botón 'Abrir XML' generará un nuevo diálogo que permitirá escoger el archivo XML deseado para abrir en la aplicación. Si la estructura es la utilizada por el programa, se desplegarán todos los datos correctamente. Por ello, se recomienda únicamente abrir aquellos .XML generados por la aplicación.
- El formulario puede rellenarse de forma total o parcial. Cuando esté completado a gusto del usuario, si se pulsa el botón 'Añadir contacto' se incluirá en la lista ese nuevo contacto. Para eliminar un contacto, simplemente debe seleccionarse en la lista y posteriormente pulsar el botón 'Eliminar contacto'.
- Mediante la selección de los contactos en la lista, se desplegará su información de nuevo en los cuadros de texto del formulario, lo que permitirá también una edición mas rápida de los mismos.
- Para utilizar la herramienta de accesibilidad por voz, simplemente será necesario seleccionar un contacto de la lista y posteriormente pulsar el botón 'Leer con voz'.

## Distribución y licencia
La aplicación y su código se encuentran registrados bajo [licencia MIT](https://github.com/pedrytus/ContactWizard/blob/master/LICENSE).

**Para cualquier reutilización del código así como posibles usos comerciales, se ruega contactar previamente conmigo.**

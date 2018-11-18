Antes que nada: Pongan el proyecto en c:/ o el disco que quieran, pero si lo ponen dentro de muchas carpetas hay problemas con la cantidad de caracteres de la ruta.

Para poder llamar a un web service de forma local necesitamos hacer lo siguiente:

1. En Visual Studio ir a "Tools->Extensions and Updates dialog" y buscar "Conveyor". Luego Instalarla
2. Cerrar el Visual Studio para finalizar la instalacion y luego reiniciarlo.
3. Debemos añadir una nueva regla de entrada en el firewall de Windows:
	a. Abrimos el buscador de Windows y escribimos WF.msc
	b. Seleccionamos "Inbound Rules" (o "Reglas de Entrega") en el panel izquierdo
	c. Seleccionamos "New Rules" (o "Nueva Regla") en el panel derecho
	d. Elegimos "Port" (o "Puerto") y hacemos click en Siguiente.
	e. Seleccionamos TCP y en "Specific local ports" (o "Puertos Locales Especificos") ingresamos el
		puerto de acceso a nuestra aplicacion (en nuestro caso 57162). Luego click en Siguiente.
	f. Continuamos la configuracion y por ultimo agregar un nombre a la nueva entrada creada.

4. Ejecutar el proyeto (BackendTp), luego ir a Tools -> Conveyor -> y permitir el acceso remoto
5. Ir a la aplicacion mobile. En la clase App.xaml.cs reemplazarr UrlApi con la ruta que nos da el 
	Conveyor. (Ej: http://192.168.0.9:45455)

6. Conectar el dispositivo (recordar que el proyecto tiene que estar corriendo)
7. Click derecho sobre la solucion -> Propiedades -> Elegimos Multiple StartUp Projects y seleccionamos
Start en la aplicacion movil (App.Android) y lo mismo en el proyecto base (BackendTp)
8. Correr las aplicaciones (Start)
 
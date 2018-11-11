Antes que nada: Pongan el proyecto en c:/ o el disco que quieran, pero si lo ponen dentro de muchas carpetas va a romper las b*** con la cantidad de caracteres

Para poder pegarle a un web service de forma local necesitamos hacer lo siguiente:

Seguir los pasos del siguiente link (No descargar desde allí, sino desde las extenciones de visual studio, ahi mismo dice donde)

https://marketplace.visualstudio.com/items?itemName=vs-publisher-1448185.ConveyorbyKeyoti#overview

Posteriormente van a ejecutar BackendTp y les va a dar una Local Url y una Remote Url (Si no lo ven, una vez que ejecutaron el proyecto van a tools - conveyor y ahi les va a aparecer la ventanita que les digo)

Tienen que reemplazar en:
MainPage.xaml.cs la linea 38
Lista.xaml.cs la linea 33

Posteriormente enchufan su dispositivo  (acuerdense que que BackendTp tiene que estar corriendo) y le dan click derecho en App.Android -> Debug -> Start new instance
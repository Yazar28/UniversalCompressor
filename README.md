📘 UniversalCompressor

UniversalCompressor es una aplicación de escritorio desarrollada en C# (.NET 8, Windows Forms) que permite comprimir y descomprimir archivos de texto utilizando tres algoritmos clásicos: Huffman, LZ77 y LZ78.

El programa cumple con los requerimientos de la tarea extraclase del curso Estructuras de Datos II, incluyendo compresión de uno o varios archivos .txt, generación de un solo archivo .myzip, y la recuperación completa de los archivos originales.

🎯 Objetivos del programa

Comprimir uno o más archivos .txt en un único archivo .myzip.

Descomprimir un .myzip y recuperar todos los archivos .txt originales.

Implementar los algoritmos:

Huffman

LZ77

LZ78

Mostrar estadísticas al usuario:

Tiempo de ejecución

Memoria utilizada

Razón de compresión

🖥️ Requisitos para ejecutar el programa

Siga estos pasos para ejecutar el proyecto en la computadora del profesor:

1. Instalar Visual Studio 2022 (o superior)

Con la carga de trabajo:

Desarrollo de escritorio .NET

2. Instalar .NET 8 SDK

https://dotnet.microsoft.com/en-us/download

3. Clonar el repositorio
git clone <URL_DEL_REPOSITORIO>

4. Abrir la solución en Visual Studio
Archivo → Abrir → Proyecto o solución → UniversalCompressor.sln

5. Seleccionar el proyecto de inicio
Clic derecho en UniversalCompressor → Establecer como proyecto de inicio

6. Ejecutar

Presione F5
o

Depurar → Iniciar depuración


La ventana de la aplicación se abrirá con la interfaz gráfica lista para usar.

🧱 Interfaz del programa
📌 Entrada de archivos

TextBox para seleccionar archivo mediante el botón “Buscar…”

Soporte para arrastrar y soltar .txt o .myzip

ListBox para agregar múltiples .txt simultáneamente

📌 Archivo de salida

TextBox para seleccionar archivo o carpeta de salida

Botón “Guardar como…”

Extensión ajustada automáticamente:

Entrada .txt → salida .myzip

Entrada .myzip → salida .txt

📌 Selección de algoritmo

Huffman

LZ77

LZ78

📌 Acciones principales

Comprimir

Descomprimir

📌 Resultados

Estadísticas detalladas

Mensajes de estado en barra inferior

📦 Formato del archivo .myzip (Propio del proyecto)

Este proyecto usa un formato propio, no compatible con ZIP estándar.

Estructura del archivo:
[int32 cantidadDeArchivos]

Por cada archivo:
    [int32 longitudNombre]
    [bytes nombre en UTF-8]
    [int32 longitudComprimido]
    [bytes comprimidos dependiendo del algoritmo]


Cada archivo se comprime individualmente, pero todos se almacenan dentro del mismo .myzip.

🧠 Algoritmos implementados
1. Huffman
Compresión

Calcula frecuencias de bytes (0–255)

Construye el árbol de Huffman con prioridad por frecuencia

Genera códigos binarios por símbolo

Contenido del archivo comprimido:

256 frecuencias (int32)

Bits codificados del texto original

Descompresión

Reconstruye el árbol desde las frecuencias

Interpreta los bits para regenerar cada símbolo

Detecta archivos corruptos o incompatibles

2. LZ77 (Implementación simple)
Compresión

Utiliza ventana deslizante

Encuentra coincidencias previas

Codifica como tuplas:

(offset, length, nextSymbol)


Serialización:

[int32 cantidadDeTuplas]

Por cada tupla:
    [int32 offset]
    [int32 length]
    [byte nextSymbol]

Descompresión

Copia fragmentos previos desde la salida

Añade el siguiente símbolo

Validaciones de seguridad y formato

3. LZ78 (Implementación simple)
Compresión

Mantiene diccionario incremental

Genera pares:

(dicIndex, nextSymbol)


Serialización:

[int32 cantidadDePares]
[int32 dicIndex][byte symbol]

Descompresión

Reconstruye cadenas basadas en el diccionario

Maneja inconsistencias y errores de formato

📊 Estadísticas mostradas

Después de cada operación el programa muestra:

Operación: Compresión / Descompresión

Algoritmo utilizado

Archivos de entrada

Archivo/carpeta de salida

Tamaño original total (bytes)

Tamaño comprimido (bytes)

Razón de compresión

Porcentaje de reducción

Tiempo de ejecución (ms)

Memoria utilizada (bytes)

En caso de error:

ERROR: <mensaje explicativo>

🧩 Casos de uso
1️⃣ Comprimir un archivo .txt

Seleccione un .txt

Elija un .myzip de salida

Seleccione algoritmo

Pulse Comprimir

2️⃣ Comprimir varios .txt

Agregue varios archivos desde “Agregar archivos…”

Seleccione archivo .myzip de salida

Algoritmo

Comprimir

3️⃣ Descomprimir un .myzip

Seleccione .myzip

Seleccione carpeta de salida

Seleccione el algoritmo correcto

Descomprimir

⚠️ Validaciones importantes

La aplicación evita:

Comprimir .myzip → .txt

Descomprimir .txt → .myzip

Usar algoritmo incorrecto al descomprimir

Archivos corruptos

Extensiones inválidas

Entradas duplicadas

En caso de equivocación, se muestra un mensaje claro en la barra inferior.

✔️ Estado del proyecto

El sistema implementa completamente:

Huffman

LZ77

LZ78

Compresión multinivel

Descompresión múltiple

Interfaz Windows Forms

Estadísticas completas

Validaciones

Formato .myzip propio

Todo está listo y funcionando según lo requerido por el profesor.
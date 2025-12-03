# UniversalCompressor

Aplicación de escritorio en **.NET 8 (C# WinForms)** que permite **comprimir y descomprimir archivos de texto** utilizando tres algoritmos clásicos de compresión:

- **Huffman**
- **LZ77**
- **LZ78**

La aplicación fue desarrollada como parte de la tarea extraclase sobre compresión para el curso de Estructuras de Datos II.

---

## 🎯 Objetivos del proyecto

- Implementar una aplicación de escritorio en C# capaz de:
  - **Comprimir** uno o varios archivos de texto (`.txt`) en un solo archivo comprimido (`.myzip`).
  - **Descomprimir** un archivo `.myzip` y recuperar todos los archivos originales.
- Implementar y utilizar los algoritmos de compresión:
  - **Huffman**
  - **LZ77**
  - **LZ78**
- Mostrar estadísticas de la operación:
  - **Tiempo de ejecución**
  - **Memoria consumida**
  - **Tasa de compresión**

---

## 🧱 Tecnologías utilizadas

- **Lenguaje:** C#
- **Plataforma:** .NET 8
- **Tipo de proyecto:** Windows Forms App
- **IDE:** Visual Studio 2022 (o superior)

---

## 🖥️ Interfaz de usuario (GUI)

La interfaz se organiza en varias secciones:

- **Archivo de entrada**
  - TextBox + botón **"Buscar..."** para seleccionar un archivo de entrada.
  - Zona de **"Arrastrar y soltar"** para seleccionar rápidamente un archivo (`.txt` o `.myzip`).
  - **Lista de archivos de entrada** (`ListBox`) donde se pueden agregar varios `.txt` para comprimirlos juntos.

- **Archivo de salida**
  - TextBox + botón **"Guardar como..."** para elegir el archivo de salida o la ruta.
  - La aplicación ajusta automáticamente la extensión esperada:
    - Si la entrada es `.txt` → sugiere `.myzip`.
    - Si la entrada es `.myzip` → sugiere `.txt` (descompresión a carpeta).

- **Configuración**
  - `ComboBox` para seleccionar el algoritmo:
    - `Huffman`
    - `LZ77`
    - `LZ78`

- **Acciones**
  - Botón **"Comprimir"**
  - Botón **"Descomprimir"**

- **Resultados**
  - Cuadro de texto donde se muestran las estadísticas de la operación.
  - Barra de estado inferior con mensajes de progreso y errores.

---

## 📦 Formato del archivo `.myzip`

El archivo `.myzip` es un contenedor binario simple definido para este proyecto. No es un ZIP estándar, sino un formato propio.

La estructura general es:

```text
[int32 cantidadDeArchivos]

Por cada archivo:
    [int32 longitudNombre]
    [bytes nombre en UTF-8]
    [int32 longitudComprimido]
    [bytes de los datos comprimidos]

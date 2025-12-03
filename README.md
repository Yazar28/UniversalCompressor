# UniversalCompressor

UniversalCompressor es una aplicación de escritorio desarrollada en **C# (.NET 8, Windows Forms)** que permite **comprimir y descomprimir archivos de texto** utilizando tres algoritmos clásicos: **Huffman, LZ77 y LZ78**.

El programa cumple con los requerimientos de la tarea extraclase del curso **Estructuras de Datos II**, incluyendo compresión de uno o varios archivos `.txt`, generación de un archivo `.myzip` único, y la restauración completa de los archivos originales.

---

# 🎯 Objetivos del programa

- Comprimir uno o más archivos `.txt` en un único archivo `.myzip`.
- Descomprimir un `.myzip` y recuperar todos los archivos `.txt` originales.
- Implementar y utilizar los algoritmos:
  - **Huffman**
  - **LZ77**
  - **LZ78**
- Mostrar estadísticas al usuario:
  - Tiempo de ejecución
  - Memoria utilizada
  - Razón de compresión

---

# 🖥️ Requisitos para ejecutar el programa

## 1. Instalar Visual Studio 2022 (o superior)
Con la carga de trabajo **Desarrollo de escritorio .NET**.

## 2. Instalar .NET 8 SDK
https://dotnet.microsoft.com/en-us/download

## 3. Clonar el repositorio
```bash
git clone <URL_DEL_REPOSITORIO>
```

## 4. Abrir la solución en Visual Studio
```
Archivo → Abrir → Proyecto o solución → UniversalCompressor.sln
```

## 5. Establecer proyecto de inicio
```
Clic derecho en UniversalCompressor → Establecer como proyecto de inicio
```

## 6. Ejecutar
Presione **F5**, o  
```
Depurar → Iniciar depuración
```

---

# 🧱 Interfaz del programa

## Entrada de archivos
- TextBox para seleccionar archivos mediante "Buscar…"
- Soporte para arrastrar y soltar `.txt` o `.myzip`
- ListBox para agregar múltiples archivos `.txt`

## Archivo de salida
- TextBox para ruta de salida
- Botón “Guardar como…”
- Extensiones automáticas:
  - `.txt` → `.myzip`
  - `.myzip` → carpeta con `.txt`

## Selección de algoritmo
- Huffman  
- LZ77  
- LZ78  

## Acciones
- Comprimir
- Descomprimir

## Resultados
- Estadísticas detalladas
- Mensajes de estado en barra inferior

---

# 📦 Formato del archivo `.myzip` (propio del proyecto)

Este proyecto utiliza un formato personalizado, no compatible con ZIP estándar.

### Estructura del archivo:

```
[int32 cantidadDeArchivos]

Por cada archivo:
    [int32 longitudNombre]
    [bytes nombre en UTF-8]
    [int32 longitudComprimido]
    [bytes comprimidos (según algoritmo)]
```

Cada archivo se comprime individualmente y luego se empaqueta dentro del `.myzip`.

---

# 🧠 Algoritmos implementados

## 1. Huffman

### Compresión
- Calcula frecuencias de bytes (0–255).
- Construye un árbol de Huffman.
- Genera códigos binarios por símbolo.

Contenido del archivo comprimido:
- 256 int32 (frecuencias)
- Bits del archivo codificado

### Descompresión
- Reconstruye el árbol a partir de las frecuencias.
- Interpreta los bits para reconstruir cada símbolo.
- Detecta archivos corruptos.

---

## 2. LZ77 (implementación simple)

### Compresión
- Utiliza una ventana deslizante.
- Encuentra coincidencias anteriores.
- Codifica como tuplas:

```
(offset, length, nextSymbol)
```

Serialización:
```
[int32 cantidadDeTuplas]
[offset][length][nextSymbol]
```

### Descompresión
- Copia fragmentos previos desde la salida.
- Añade el siguiente símbolo.
- Maneja errores de formato.

---

## 3. LZ78 (implementación simple)

### Compresión
- Mantiene un diccionario incremental.
- Genera pares:

```
(indexDictionary, nextSymbol)
```

Serializa:
```
[int32 cantidadDePares]
[index][symbol]
```

### Descompresión
- Reconstruye cadenas usando el diccionario.
- Maneja inconsistencias o archivos corruptos.

---

# 📊 Estadísticas mostradas

Después de cada operación se muestra:

- Operación (compresión/descompresión)
- Algoritmo utilizado
- Archivos procesados
- Ruta de salida
- Tamaño original total (bytes)
- Tamaño comprimido (bytes)
- Razón comprimido/original
- Porcentaje de reducción
- Tiempo de ejecución (ms)
- Memoria utilizada (bytes)

En caso de error:

```
ERROR: <detalle del error>
```

---

# 🧩 Casos de uso

## 1. Comprimir un archivo `.txt`
1. Seleccione un archivo `.txt`.
2. Elija archivo `.myzip` de salida.
3. Seleccione algoritmo.
4. Presione **Comprimir**.

## 2. Comprimir varios `.txt`
1. Agregue varios archivos con “Agregar archivos…”.
2. Seleccione un `.myzip` de salida.
3. Seleccione algoritmo.
4. Presione **Comprimir**.

## 3. Descomprimir un `.myzip`
1. Seleccione un archivo `.myzip`.
2. Elija una carpeta de salida.
3. Seleccione el mismo algoritmo con el que se comprimió.
4. Presione **Descomprimir**.

---

# ⚠️ Validaciones importantes

El programa evita:

- Comprimir `.myzip` → `.txt`
- Descomprimir `.txt` → `.myzip`
- Usar algoritmo incorrecto al descomprimir
- Archivos corruptos
- Extensiones incorrectas
- Entradas duplicadas

Se muestra un mensaje claro en caso de error.

---

# ✔ Estado del proyecto

Implementado completamente:

- Huffman  
- LZ77  
- LZ78  
- Compresión múltiple  
- Descompresión múltiple  
- Interfaz gráfica  
- Estadísticas completas  
- Validaciones robustas  
- Formato `.myzip` propio  
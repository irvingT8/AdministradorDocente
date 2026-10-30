# Actividad 2: Interfaz Gráfica con Conexión a MySQL
**Materia:** Ciberinfraestructura  
**Profesor:** Ma. Inés Calderón Zetter  
**Alumno:** Irving Eduardo Torrico Olivares
**Proyecto:** Sistema de Gestión de Personal Académico y Asignaturas (`ControlDocenteDB`)

---

## 1. Descripción del Proyecto
Aplicación de escritorio desarrollada en C# conectada a una base de datos relacional MySQL (`ControlDocenteDB`). El sistema permite la gestión dinámica de carga académica universitaria asociando docentes, asignaturas y aulas.

---

## 2. Arquitectura de la Base de Datos

La base de datos se compone de 3 tablas relacionales con restricciones de clave foránea (`FOREIGN KEY` y `ON DELETE CASCADE`):

1. **`CatPersonalDocente`**: Registro de profesores y sus departamentos.
2. **`CatAsignaturas`**: Catálogo de materias y sus créditos.
3. **`TblAsignacionClases`**: Tabla relacional que vincula docentes, asignaturas y aulas asignadas.

```sql
CREATE DATABASE ControlDocenteDB
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE ControlDocenteDB;

CREATE TABLE CatPersonalDocente (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre VARCHAR(100) NOT NULL,
    departamento VARCHAR(80) NOT NULL
) ENGINE=InnoDB;

CREATE TABLE CatAsignaturas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nombre_materia VARCHAR(100) NOT NULL,
    creditos INT NOT NULL
) ENGINE=InnoDB;

CREATE TABLE TblAsignacionClases (
    id INT AUTO_INCREMENT PRIMARY KEY,
    id_docente INT NOT NULL,
    id_asignatura INT NOT NULL,
    aula VARCHAR(20) NOT NULL,
    FOREIGN KEY (id_docente) REFERENCES CatPersonalDocente(id) ON DELETE CASCADE,
    FOREIGN KEY (id_asignatura) REFERENCES CatAsignaturas(id) ON DELETE CASCADE
) ENGINE=InnoDB;
```

## 3. Evidencias de Funcionamiento (Screenshots)

### Captura 1: Prueba de Conexión Exitosa
*(Muestra el mensaje de confirmación al presionar "Probar Conexión")*

![Prueba de Conexión Exitosa](docs/captura1_conexion.png)

---

### Captura 2: Captura y Registro de Datos Dinámicos
*(Muestra el formulario con los campos de Docente, Departamento, Materia, Créditos y Aula completados)*

![Registro de Datos](docs/captura2_registro.png)

---

### Captura 3: Consulta Relacional (INNER JOIN) en DataGridView
*(Muestra los datos cruzados de las 3 tablas reflejados en el DataGridView tras presionar "Mostrar Carga Académica")*

![Consulta DataGridView](docs/captura3_datagrid.png)

---

### Captura 4: Verificación de Registros en MySQL Workbench
*(Muestra la ejecución de las consultas SELECT directamente en MySQL Workbench)*

![Verificación MySQL Workbench](docs/captura4_workbench.png)

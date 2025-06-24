// ============ CONFIGURACIÓN DE DEPURACIÓN ============
const DEBUG_MODE = true;

function debugLog(message, data = null) {
    if (DEBUG_MODE) {
        console.log(`[DEBUG] ${message}`, data || '');
    }
}

function debugError(message, error) {
    if (DEBUG_MODE) {
        console.error(`[ERROR] ${message}`, error);
    }
}

// ============ FUNCIONES BÁSICAS ============
function ocultarTodo() {
    try {
        debugLog("Ocultando todas las secciones");
        document.getElementById('estudiantes-table').style.display = 'none';
        document.getElementById('notas-table').style.display = 'none';
        document.getElementById('promedios-table').style.display = 'none';
    } catch (error) {
        debugError("Error en ocultarTodo", error);
    }
}

async function mostrarEstudiantes() {
    debugLog("Mostrando sección de estudiantes");
    try {
        ocultarTodo();
        document.getElementById('estudiantes-table').style.display = 'block';
        await cargarEstudiantes();
    } catch (error) {
        debugError("Error al mostrar estudiantes", error);
        alert('Error al cargar la sección de estudiantes');
    }
}

async function mostrarNotas() {
    debugLog("Mostrando sección de notas");
    try {
        ocultarTodo();
        document.getElementById('notas-table').style.display = 'block';
        await cargarNotas();
    } catch (error) {
        debugError("Error al mostrar notas", error);
        alert('Error al cargar la sección de notas');
    }
}

async function mostrarPromedios() {
    debugLog("Mostrando sección de promedios");
    try {
        ocultarTodo();
        document.getElementById('promedios-table').style.display = 'block';
        await cargarPromedios();
    } catch (error) {
        debugError("Error al mostrar promedios", error);
        alert('Error al cargar la sección de promedios');
    }
}

// ============ CRUD ESTUDIANTES (MEJORADO) ============
function mostrarFormularioEstudiante() {
    console.log("Mostrando formulario de estudiante");
    try {
        const form = document.getElementById('form-estudiante');
        if (!form) {
            console.error("❌ No se encontró el formulario");
            return;
        }

        // Ocultar otros elementos primero
        document.querySelectorAll('.form-container').forEach(el => {
            el.style.display = 'none';
        });

        // Mostrar este formulario
        form.style.display = 'block';
        form.scrollIntoView({ behavior: 'smooth' });

        // Resetear campos
        document.getElementById('form-estudiante-titulo').textContent = 'Nuevo Estudiante';
        document.getElementById('form-nuevo-estudiante').reset();
        document.getElementById('estudiante-cedula-original').value = '';
        document.getElementById('mensaje-estado-estudiante').textContent = '';

    } catch (error) {
        console.error("Error al mostrar formulario:", error);
    }
}

function ocultarFormularioEstudiante() {
    debugLog("Ocultando formulario de estudiante");
    try {
        document.getElementById('form-estudiante').style.display = 'none';
    } catch (error) {
        debugError("Error al ocultar formulario estudiante", error);
    }
}

async function cargarEstudiantes() {
    console.log("🔍 Iniciando carga de estudiantes...");
    const tbody = document.getElementById('tbody-estudiantes');

    try {
        if (!tbody) {
            console.error("❌ Error: No se encontró tbody-estudiantes");
            return;
        }

        tbody.innerHTML = '<tr><td colspan="6" class="cargando">Cargando estudiantes...</td></tr>';

        const response = await fetch('https://localhost:7030/api/Estudiantes');
        if (!response.ok) throw new Error(`Error HTTP: ${response.status}`);

        const estudiantes = await response.json();
        console.log("📊 Estudiantes recibidos:", estudiantes);

        tbody.innerHTML = '';

        if (estudiantes.length === 0) {
            tbody.innerHTML = '<tr><td colspan="6" class="sin-datos">No hay estudiantes registrados</td></tr>';
            return;
        }

        estudiantes.forEach(est => {
            const tr = document.createElement('tr');

            // Versión ALTERNATIVA que asegura que los botones se creen
            const celdas = [
                est.cedula || 'N/A',
                est.nombre || 'N/A',
                est.apellido || 'N/A',
                est.correo || 'N/A',
                est.genero || 'N/A'
            ];

            celdas.forEach(texto => {
                const td = document.createElement('td');
                td.textContent = texto;
                tr.appendChild(td);
            });

            // Celda de acciones MANUALMENTE
            const tdAcciones = document.createElement('td');
            tdAcciones.className = 'acciones';

            const btnEditar = document.createElement('button');
            btnEditar.className = 'btn-editar';
            btnEditar.innerHTML = '✏️ Editar';
            btnEditar.onclick = () => editarEstudiante(est.cedula);

            const btnEliminar = document.createElement('button');
            btnEliminar.className = 'btn-eliminar';
            btnEliminar.innerHTML = '🗑️ Eliminar';
            btnEliminar.onclick = () => eliminarEstudiante(est.cedula);

            tdAcciones.appendChild(btnEditar);
            tdAcciones.appendChild(btnEliminar);
            tr.appendChild(tdAcciones);

            tbody.appendChild(tr);
        });

    } catch (error) {
        console.error("❌ Error cargando estudiantes:", error);
        if (tbody) {
            tbody.innerHTML = `<tr><td colspan="6" class="error">Error al cargar: ${error.message}</td></tr>`;
        }
    }
}

async function editarEstudiante(cedula) {
    debugLog(`Editando estudiante con cédula: ${cedula}`);
    try {
        const response = await fetch(`https://localhost:7030/api/Estudiantes/${cedula}`);

        if (!response.ok) {
            throw new Error(`Error HTTP: ${response.status}`);
        }

        const estudiante = await response.json();
        debugLog("Datos del estudiante recibidos", estudiante);

        // Validación de datos
        if (!estudiante.cedula) {
            throw new Error("Datos del estudiante incompletos");
        }

        // Rellenar formulario
        document.getElementById('estudiante-cedula-original').value = estudiante.cedula;
        document.getElementById('estudiante-cedula').value = estudiante.cedula;
        document.getElementById('estudiante-nombre').value = estudiante.nombre || '';
        document.getElementById('estudiante-apellido').value = estudiante.apellido || '';
        document.getElementById('estudiante-correo').value = estudiante.correo || '';
        document.getElementById('estudiante-genero').value = estudiante.genero || '';

        // Actualizar UI
        document.getElementById('form-estudiante-titulo').textContent = 'Editar Estudiante';
        document.getElementById('form-estudiante').style.display = 'block';

    } catch (error) {
        debugError("Error editando estudiante", error);
        alert(`Error al cargar datos del estudiante: ${error.message}`);
    }
}

async function eliminarEstudiante(cedula) {
    debugLog(`Intentando eliminar estudiante con cédula: ${cedula}`);
    try {
        if (!confirm(`¿Está seguro que desea eliminar al estudiante con cédula ${cedula}?`)) {
            debugLog("Eliminación cancelada por el usuario");
            return;
        }

        const response = await fetch(`https://localhost:7030/api/Estudiantes/${cedula}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            throw new Error(`Error HTTP: ${response.status}`);
        }

        debugLog("Estudiante eliminado correctamente");
        alert('Estudiante eliminado correctamente');
        await cargarEstudiantes();

    } catch (error) {
        debugError("Error eliminando estudiante", error);
        alert(`Error al eliminar estudiante: ${error.message}`);
    }
}

// ============ MANEJO DEL FORMULARIO DE ESTUDIANTES ============
document.getElementById('form-nuevo-estudiante').addEventListener('submit', async function (e) {
    e.preventDefault();
    debugLog("Enviando formulario de estudiante");

    try {
        const cedulaOriginal = document.getElementById('estudiante-cedula-original').value;
        const cedula = document.getElementById('estudiante-cedula').value.trim();
        const nombre = document.getElementById('estudiante-nombre').value.trim();
        const apellido = document.getElementById('estudiante-apellido').value.trim();
        const correo = document.getElementById('estudiante-correo').value.trim();
        const genero = document.getElementById('estudiante-genero').value;

        // Validación básica
        if (!cedula || !nombre || !apellido) {
            throw new Error("Cédula, nombre y apellido son obligatorios");
        }

        if (!/^\d{6,10}$/.test(cedula)) {
            throw new Error("La cédula debe tener entre 6 y 10 dígitos");
        }

        const estudiante = {
            cedula,
            nombre,
            apellido,
            correo: correo || null,
            genero: genero || null
        };

        debugLog("Datos del estudiante a guardar", estudiante);

        let url = 'https://localhost:7030/api/Estudiantes';
        let method = 'POST';

        if (cedulaOriginal) {
            url += `/${cedulaOriginal}`;
            method = 'PUT';
        }

        const response = await fetch(url, {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(estudiante)
        });

        if (!response.ok) {
            const errorData = await response.json().catch(() => ({}));
            throw new Error(errorData.title || 'Error al guardar el estudiante');
        }

        const successMessage = cedulaOriginal
            ? '✅ Estudiante actualizado correctamente'
            : '✅ Estudiante creado correctamente';

        document.getElementById('mensaje-estado-estudiante').textContent = successMessage;
        debugLog(successMessage);

        setTimeout(async () => {
            ocultarFormularioEstudiante();
            await cargarEstudiantes();
        }, 1500);

    } catch (error) {
        debugError("Error en el formulario de estudiante", error);
        document.getElementById('mensaje-estado-estudiante').textContent = `❌ Error: ${error.message}`;
        document.getElementById('mensaje-estado-estudiante').className = 'error-message';
    }
});
<script>
    document.addEventListener('DOMContentLoaded', function() {
  // Mostrar/ocultar formulario
  const mostrarBtn = document.getElementById('mostrar-formulario');
    const cancelarBtn = document.getElementById('cancelar-formulario');
    const formulario = document.getElementById('formulario-estudiante');

    mostrarBtn.addEventListener('click', function() {
        formulario.style.display = 'block';
  });

    cancelarBtn.addEventListener('click', function() {
        formulario.style.display = 'none';
  });

    // Manejar envío del formulario
    const form = document.getElementById('nuevo-estudiante-form');
    form.addEventListener('submit', function(e) {
        e.preventDefault();

    // Aquí iría la lógica para guardar el nuevo estudiante
    alert('Estudiante guardado (implementar lógica de guardado real)');

    // Limpiar y ocultar el formulario
    form.reset();
    formulario.style.display = 'none';
  });

  // Agregar eventos a los botones de editar/eliminar
  document.querySelectorAll('.btn-editar').forEach(btn => {
        btn.addEventListener('click', function () {
            const fila = this.closest('tr');
            // Lógica para editar (poblar formulario con datos existentes)
            alert('Implementar lógica de edición');
        });
  });
  
  document.querySelectorAll('.btn-eliminar').forEach(btn => {
        btn.addEventListener('click', function () {
            if (confirm('¿Estás seguro de eliminar este estudiante?')) {
                // Lógica para eliminar
                alert('Implementar lógica de eliminación');
            }
        });
  });
});
</script>

// ============ FUNCIONALIDADES ORIGINALES (OPTIMIZADAS) ============
// [Las funciones originales de notas y promedios se mantienen igual pero con el mismo sistema de depuración]
// ... (código existente de cargarNotas, cargarPromedios, etc.)

// ============ INICIALIZACIÓN ============
window.onload = () => {
    debugLog("Inicializando aplicación");
    try {
        ocultarTodo();
        cargarCursos();
        cargarListaEstudiantes();
        cargarEstudiantes();
    } catch (error) {
        debugError("Error en la inicialización", error);
        alert('Error al iniciar la aplicación');
    }
};
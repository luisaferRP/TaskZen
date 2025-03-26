document.addEventListener("DOMContentLoaded", function () {
    document.querySelectorAll(".delete-task").forEach(button => {
        button.addEventListener("click", async function () {
            const taskId = this.getAttribute("data-id");

            // Muestra la alerta de confirmación
            Swal.fire({
                title: "¿Estás seguro?",
                text: "Esta acción no se puede deshacer.",
                icon: "warning",
                showCancelButton: true,
                confirmButtonColor: "#d33",
                cancelButtonColor: "#3085d6",
                confirmButtonText: "Sí, eliminar",
                cancelButtonText: "Cancelar"
            }).then(async (result) => {
                if (result.isConfirmed) {
                    const response = await fetch(`/Tasks/Eliminar/${taskId}`, {
                        method: "DELETE"
                    });

                    if (response.ok) {
                        Swal.fire({
                            position: "top-end",
                            title: "Eliminado",
                            text: "La tarea ha sido eliminada con éxito.",
                            icon: "success",
                            timer: 2000,
                            showConfirmButton: false
                        }).then(() => {
                            location.reload();
                        });
                    } else {
                        Swal.fire({
                            title: "Error",
                            text: "No se pudo eliminar la tarea.",
                            icon: "error"
                        });
                    }
                }
            });
        });
    });
});



//permite soltar los elementos
function allowDrop(ev) {
    ev.preventDefault(); 
}

//permite arrastrar tareas
function drag(ev, taskId) {
    ev.dataTransfer.setData("taskId", taskId);
}

function drop(ev, newStatus) {
    ev.preventDefault();

    let taskId = ev.dataTransfer.getData("taskId");
    let taskElement = document.getElementById(`task-${taskId}`);
    let targetColumn = ev.target.closest(".task-column").querySelector(".task-list");

    if (!taskElement || !targetColumn) {
        console.error("❌ No se encontró el elemento de la tarea o la columna de destino.");
        return;
    }

    targetColumn.appendChild(taskElement);

    //animación para hacer la transición más fluida
    taskElement.style.opacity = "0.5";
    setTimeout(() => taskElement.style.opacity = "1", 300);

    console.log("Enviando:", JSON.stringify({
        id: taskId,
        status: newStatus
    }));

    let tokenElement = document.querySelector('input[name="__RequestVerificationToken"]');
    let token = tokenElement ? tokenElement.value : null; // Evita el error si no se encuentra el token


    fetch(`/Tasks/ActualizarEstadoTarea`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            ...(token && { "X-CSRF-TOKEN": token }) // Agrega el token solo si existe
        },
        body: JSON.stringify({
            id: taskId,
            status: newStatus
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.success) {
                location.reload(); 
            } else {
                alert("⚠️ Error al actualizar la tarea: " + data.message);
            }
        })
        .catch(error => console.error("❌ Error en fetch:", error));
}

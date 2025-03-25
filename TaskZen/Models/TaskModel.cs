namespace TaskZen.Models
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DueDate { get; set; }
        public StatusLevel Status { get; set; }
        public PriorityLevel Priority { get; set; }
        public LabelLevel Label { get; set; }
        public int UserId { get; set; }
    }
}

// Enum para definir la prioridad de forma clara
public enum PriorityLevel
{
    Baja,
    Media,
    Alta
}

public enum StatusLevel
{
    Hacer,
    EnProgreso,
    Terminado
}

public enum LabelLevel
{
    DormirMejor,
    Estudio,
    Ejercicio,
    Relajacion,
    Compras
}


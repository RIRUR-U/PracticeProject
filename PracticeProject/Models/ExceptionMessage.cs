using System.Text.Json;
namespace PracticeProject.Models
{
    public class MessageHelper
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public long? AutoId { get; set; }
    }
    public class MessageHelperCreate
    {
        public string Message { get; set; } = "Created Successfully";
        public int StatusCode { get; set; } = 200;
        public long? AutoId { get; set; }
    }
    public class MessageHelperUpdate
    {
        public string Message { get; set; } = "Update Successfully";
        public int StatusCode { get; set; } = 200;
        public long? AutoId { get; set; }
    }
    public class MessageHelperDelete
    {
        public string Message { get; set; } = "Delete Successfully";
        public int StatusCode { get; set; } = 200;
        public long? AutoId { get; set; }
    }
    public class MessageHelperCustom
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public long? AutoId { get; set; }
    }
    public class ExceptionMessage
    {
        public string? Message { get; set; }
        public int? StatusCode { get; set; }
    }
    public class CustomMessageHelper
    {
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public long AutoId { get; set; }
        public string AutoName { get; set; }
    }
    public class ApiError
    {
        public string? Message { get; set; }
        public int StatusCode { get; set; }
        public string? StackTrace { get; set; }
        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}

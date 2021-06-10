using System;
using System.ComponentModel.DataAnnotations;

namespace asp.net.core.mvc.demo.Models
{
    public class MLModel
    {
        [Key]
        [Required]
        public string modelName { get; set; }

        [Required]
        public string modelType { get; set; }

        [Required]
        public string modelVersion { get; set; }

        [Required]
        public string modelStatus { get; set; }

        public MLModel() { }

        public MLModel(string modelName, string modelType, string modelVersion, string modelStatus)
        {
            this.modelName = modelName;
            this.modelType = modelType;
            this.modelVersion = modelVersion;
            this.modelStatus = modelStatus;
        }
    }
}

﻿using System;

namespace OppJar.Domain
{
    public class BaseEntity : IBaseEntity, IAudit
    {
        public string Id { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}

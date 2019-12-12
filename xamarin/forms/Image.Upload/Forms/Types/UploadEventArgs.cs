﻿using System;

namespace Forms.Types
{
    public class UploadEventArgs
    {
        public DateTimeOffset? UploadedTime { get; set; }

        public UploadState State { get; set; }

        public int Id { get; set; }
    }
}
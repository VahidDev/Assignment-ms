﻿using DomainModels.Dtos.Base;

namespace DomainModels.Dtos
{
    public class LocationDto : BaseDto
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}

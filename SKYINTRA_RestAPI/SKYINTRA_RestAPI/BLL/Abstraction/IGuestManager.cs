﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKYINTRA_RestAPI.BLL.Abstraction
{
    public interface IGuestManager
    {
        Guest AddGuest(Guest guest);
    }
}

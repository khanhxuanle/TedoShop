﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface IFeedBackService
    {
        Feedback Create(Feedback feedback);
        void Save();
    }
}

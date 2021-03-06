﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace Services.Common.Base
{
    public class VmsControllerBase : ControllerBase
    { 

        protected ActionResult<T> Single<T>(T dto)
        {
            if (dto == null)
            {
                return NoContent();
            }

            return Ok(dto);
        }



        protected ActionResult<IEnumerable<T>> Collection<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return NoContent();
            }

            return Ok(items);
        }

        protected ActionResult<IAsyncEnumerable<T>> CollectionAsync<T>(IEnumerable<T> items)
        {
            if (items == null)
            {
                return NoContent();
            }

            return Ok(items);
        }
    }
}

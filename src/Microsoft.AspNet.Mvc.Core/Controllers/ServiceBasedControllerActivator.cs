// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.AspNet.Mvc.Controllers
{
    /// <summary>
    /// A <see cref="IControllerActivator"/> that retrieves controllers as services from the request's
    /// <see cref="IServiceProvider"/>.
    /// </summary>
    public class ServiceBasedControllerActivator : IControllerActivator
    {
        /// <inheritdoc />
        public object Create(ControllerContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException(nameof(actionContext));
            }

            var controllerType = actionContext.ActionDescriptor.ControllerTypeInfo?.AsType();

            return actionContext.HttpContext.RequestServices.GetRequiredService(controllerType);
        }

        /// <inheritdoc />
        public virtual void Release(object controller)
        {
            var disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}

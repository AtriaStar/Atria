﻿using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Backend.AspPlugins;

public class RouteConvention : IApplicationModelConvention {
    private readonly AttributeRouteModel _centralPrefix;

    public RouteConvention(IRouteTemplateProvider routeTemplateProvider) {
        _centralPrefix = new(routeTemplateProvider);
    }

    public void Apply(ApplicationModel application) {
        foreach (var selectorModel in application.Controllers.SelectMany(x => x.Selectors)) {
            if (selectorModel.AttributeRouteModel == null) {
                selectorModel.AttributeRouteModel = _centralPrefix;
            } else {
                selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_centralPrefix,
                    selectorModel.AttributeRouteModel);
            }
        }
    }
}

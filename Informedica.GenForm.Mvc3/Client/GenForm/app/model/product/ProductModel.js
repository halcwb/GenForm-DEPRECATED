/**
 * Created by .
 * User: hal
 * Date: 28-4-11
 * Time: 13:45
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.ProductModel', {
    extend: 'Ext.data.Model',

/*    constructor: function (config) {
        config = config || this.createConfig();

        this.applyConfig(config)
    },*/

    fields: [
            { name: 'ProductName', type: 'string' },
            { name: 'Generic', type: 'string' },
            { name: 'Brand', type: 'string' },
            { name: 'Shape', type: 'string' },
            { name: 'Package', type: 'string' },
            { name: 'Quantity', type: 'float' },
            { name: 'Divisor', type: 'float' },
            { name: 'Unit', type: 'string' },
            { name: 'ProductCode', type: 'string' },
            { name: 'TradeCode', type: 'string' }
     ],

    createConfig: function () {
        return {/*
            fields: this.createFields(),
            hasMany: this.createHasMany()*/
        };
    },

    createFields: function () {
        return [
            { name: 'ProductName', type: 'string' },
            { name: 'Generic', type: 'string' },
            { name: 'Brand', type: 'string' },
            { name: 'Shape', type: 'string' },
            { name: 'Package', type: 'string' },
            { name: 'Quantity', type: 'float' },
            { name: 'Divisor', type: 'float' },
            { name: 'Unit', type: 'string' },
            { name: 'ProductCode', type: 'string' },
            { name: 'TradeCode', type: 'string' }
        ];
    },

    hasMany: [
            { model: 'GenForm.model.product.GenericNameModel', name: 'generics'},
            { model: 'GenForm.model.product.BrandNameModel', name: 'brands' },
            { model: 'GenForm.model.product.ShapeNameModel', name: 'shapes' },
            { model: 'GenForm.model.product.PackageNameModel', name: 'packages' },
            { model: 'GenForm.model.product.ProductSubstanceModel', name: 'substances' },
            { model: 'GenForm.model.product.ProductRouteModel', name: 'routes' }
    ],

    createHasMany: function () {
        return [
            { model: 'GenForm.model.product.GenericNameModel', name: 'generics'},
            { model: 'GenForm.model.product.BrandNameModel', name: 'brands' },
            { model: 'GenForm.model.product.ShapeNameModel', name: 'shapes' },
            { model: 'GenForm.model.product.PackageNameModel', name: 'packages' },
            { model: 'GenForm.model.product.ProductSubstanceModel', name: 'substances' },
            { model: 'GenForm.model.product.ProductRouteModel', name: 'routes' }
        ];
    }
});
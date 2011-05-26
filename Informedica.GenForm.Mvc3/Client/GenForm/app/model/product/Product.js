/**
 * Created by .
 * User: hal
 * Date: 28-4-11
 * Time: 13:45
 * To change this template use File | Settings | File Templates.
 */
Ext.define('GenForm.model.product.Product', {
    extend: 'Ext.data.Model',

    idProperty: 'ProductId',

    fields: [
            { name: 'ProductId', type: 'integer', mapping: 'ProductId' },
            { name: 'ProductName', type: 'string', mapping: 'ProductName' },
            { name: 'GenericName', type: 'string', mapping: 'GenericName' },
            { name: 'BrandName', type: 'string', mapping: 'BrandName' },
            { name: 'ShapeName', type: 'string', mapping: 'ShapeName' },
            { name: 'PackageName', type: 'string', mapping: 'PackageName' },
            { name: 'Quantity', type: 'float', mapping: 'Quantity'},
            { name: 'Divisor', type: 'float' },
            { name: 'UnitName', type: 'string', mapping: 'UnitName' },
            { name: 'ProductCode', type: 'string' },
            { name: 'TradeCode', type: 'string' }
     ],

    hasMany: [
            { model: 'GenForm.model.product.GenericName', name: 'generics'},
            { model: 'GenForm.model.product.BrandName', name: 'brands' },
            { model: 'GenForm.model.product.ShapeName', name: 'shapes' },
            { model: 'GenForm.model.product.PackageName', name: 'packages' },
            { model: 'GenForm.model.product.ProductSubstance', name: 'substances' },
            { model: 'GenForm.model.product.ProductRoute', name: 'routes' }
    ],

    proxy: {
        type: 'direct',
        directFn: Product.GetProduct,
        paramsAsHash: true,
        autoLoad: false,
        api: {
            load: Product.GetProduct,
            submit: Product.SaveProduct
        },
        reader: {
            type: 'json',
            root: 'data',
            idProperty: 'ProductId'
        }
    }
});
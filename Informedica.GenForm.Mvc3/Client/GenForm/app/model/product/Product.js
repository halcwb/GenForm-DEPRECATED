Ext.define('GenForm.model.product.Product', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
            { name: 'Id',          type: 'string' },
            { name: 'Name',        type: 'string' },
            { name: 'GenericName', type: 'string' },
            { name: 'BrandName',   type: 'string' },
            { name: 'ShapeName',   type: 'string' },
            { name: 'PackageName', type: 'string' },
            { name: 'Quantity',    type: 'float' },
            { name: 'Divisor',     type: 'float' },
            { name: 'UnitName',    type: 'string' },
            { name: 'ProductCode', type: 'string' },
            { name: 'TradeCode',   type: 'string' }
     ],

    hasMany: [
            { model: 'GenForm.model.product.ProductSubstance', name: 'substances' },
            { model: 'GenForm.model.product.ProductRoute',     name: 'routes' }
    ]
});
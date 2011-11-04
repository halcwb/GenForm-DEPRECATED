Ext.define('GenForm.model.product.Shape', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string' },
        {name: 'Name', type: 'string' }
    ],

    hasMany: [
        { model: 'GenForm.model.product.UnitGroup', name: 'unitGroups' },
        { model: 'GenForm.model.product.Package', name: 'packages' },
        { model: 'GenForm.model.product.Route', name: 'routes' }
    ]

});
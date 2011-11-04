Ext.define('GenForm.model.product.UnitGroup', {
    extend: 'Ext.data.Model',

    idProperty: 'Id',

    fields: [
        {name: 'Id', type: 'string' },
        {name: 'Name', type: 'string' },
        {name: 'AllowConversion', type: 'boolean' }
    ],

    hasMany: [
        { model: 'GenForm.model.product.Unit', name: 'units'}
    ]

});